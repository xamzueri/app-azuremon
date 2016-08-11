using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.DataStore.Abstractions;
using Azuremon.DataStore.Mock.Stores;
using Microsoft.WindowsAzure.MobileServices;

namespace Azuremon.DataStore.Mock
{
    public class StoreManager : IStoreManager
    {
        public bool IsInitialized => true;
        public Task InitializeAsync()
        {
            return Task.FromResult(true);
        }

        private readonly Lazy<IPokemonStore> _lazyPokemon = new Lazy<IPokemonStore>(() => new PokemonStore());
        public IPokemonStore PokemonStore => _lazyPokemon.Value;

        private readonly Lazy<IFavoriteStore> _lazyFavorite = new Lazy<IFavoriteStore>(() => new FavoriteStore());
        public IFavoriteStore FavoriteStore => _lazyFavorite.Value;
        private readonly Lazy<ICategoryStore> _lazyCategory = new Lazy<ICategoryStore>(() => new CategoryStore());
        public ICategoryStore CategoryStore => _lazyCategory.Value;
        public Task<bool> SyncAllAsync(bool syncUserSpecific)
        {
            return Task.FromResult(true);
        }

        public Task DropEverythingAsync()
        {
            return Task.FromResult(true);
        }

        public Task<MobileServiceUser> LoginAsync(string provider)
        {
            return Task.FromResult<MobileServiceUser>(null);
        }

        public Task LogoutAsync()
        {
            return Task.FromResult(false);
        }

        public int Version => 1;
    }
}
