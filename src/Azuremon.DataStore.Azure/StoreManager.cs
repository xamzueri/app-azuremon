using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.DataObjects;
using Azuremon.DataStore.Abstractions;
using Azuremon.DataStore.Azure.Objects;
using Azuremon.DataStore.Azure.Stores;
using Azuremon.Utils;
using Azuremon.Utils.Helpers;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Azuremon.DataStore.Azure
{
    public class StoreManager : IStoreManager
    {
        #region [ Lazies ]
        private readonly Lazy<ICategoryStore> _lazyCategory = new Lazy<ICategoryStore>(() => new CategoryStore());
        private readonly Lazy<IPokemonStore> _lazyPokemon = new Lazy<IPokemonStore>(() => new PokemonStore());
        private readonly Lazy<IFavoriteStore> _lazyFavorite = new Lazy<IFavoriteStore>(() => new FavoriteStore());
        #endregion 

        public MobileServiceClient MobileService { get; set; }

        public bool IsInitialized { get; private set; }

        private readonly object _locker = new object();

        public async Task InitializeAsync()
        {
            
            MobileServiceSQLiteStore store;
            lock (_locker)
            { 
                if (IsInitialized)
                    return;

                IsInitialized = true;
                var dbId = Settings.DatabaseId;
                var path = $"syncstore{dbId}.db";
                MobileService = new MobileServiceClient("https://azuremon.azurewebsites.net/");
                store = new MobileServiceSQLiteStore(path);
                
                store.DefineTable<Category>();
                store.DefineTable<Favorite>();
                store.DefineTable<Pokemon>();
                store.DefineTable<StoreSettings>();
            }

            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler()).ConfigureAwait(false);

            await LoadCachedTokenAsync().ConfigureAwait(false);
        }

        public IPokemonStore PokemonStore => _lazyPokemon.Value;
        public IFavoriteStore FavoriteStore => _lazyFavorite.Value;
        public ICategoryStore CategoryStore => _lazyCategory.Value;

        public async Task<bool> SyncAllAsync(bool syncUserSpecific)
        {
            if (!IsInitialized)
                await InitializeAsync();

            var taskList = new List<Task<bool>>();
            taskList.Add(CategoryStore.SyncAsync());
            taskList.Add(PokemonStore.SyncAsync());

            if (syncUserSpecific)
            {
                taskList.Add(FavoriteStore.SyncAsync());
            }

            var successes = await Task.WhenAll(taskList).ConfigureAwait(false);
            return successes.Any(x => !x);//if any were a failure.

        }

        public Task DropEverythingAsync()
        {
            Settings.UpdateDatabaseId();
            CategoryStore.DropTable();
            PokemonStore.DropTable();
            FavoriteStore.DropTable();
            IsInitialized = false;
            return Task.FromResult(true);
        }

        public async Task<MobileServiceUser> LoginAsync(string provider)
        {
            var auth = DependencyService.Get<IAuthenticate>();

            if (!IsInitialized)
            {
                await InitializeAsync();
            }

            MobileServiceUser user = await auth.Authenticate(MobileService, provider.ToLowerInvariant());

            await CacheToken(user);

            return user;

        }

        public async Task LogoutAsync()
        {
            if (!IsInitialized)
            {
                await InitializeAsync();
            }

            await MobileService.LogoutAsync();

            var settings = await ReadSettingsAsync();

            if (settings != null)
            {
                settings.AuthToken = string.Empty;
                settings.UserId = string.Empty;

                await SaveSettingsAsync(settings);
            }
        }

        public int Version => 1;

        async Task SaveSettingsAsync(StoreSettings settings) =>
            await MobileService.SyncContext.Store.UpsertAsync(nameof(StoreSettings), new[] { JObject.FromObject(settings) }, true);

        async Task<StoreSettings> ReadSettingsAsync() =>
            (await MobileService.SyncContext.Store.LookupAsync(nameof(StoreSettings), StoreSettings.StoreSettingsId))?.ToObject<StoreSettings>();


        async Task CacheToken(MobileServiceUser user)
        {
            var settings = new StoreSettings
            {
                UserId = user.UserId,
                AuthToken = user.MobileServiceAuthenticationToken
            };

            await SaveSettingsAsync(settings);

        }

        async Task LoadCachedTokenAsync()
        {
            StoreSettings settings = await ReadSettingsAsync();

            if (settings != null)
            {
                try
                {
                    if (!string.IsNullOrEmpty(settings.AuthToken))
                    {
                        MobileService.CurrentUser = new MobileServiceUser(settings.UserId);
                        MobileService.CurrentUser.MobileServiceAuthenticationToken = settings.AuthToken;
                        MobileService.CurrentUser = await MobileService.RefreshUserAsync().ConfigureAwait(false);
                    }
                }
                catch (Exception)
                {
                    settings.AuthToken = string.Empty;
                    settings.UserId = string.Empty;

                    await SaveSettingsAsync(settings);
                }
            }
        }
    }
}
