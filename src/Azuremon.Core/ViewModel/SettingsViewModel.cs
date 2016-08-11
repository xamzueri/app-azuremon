using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Azuremon.DataStore.Abstractions;
using Azuremon.Utils.Helpers;
using FormsToolkit;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Azuremon.Core.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel()
        {
            //This will be triggered wen 
            Settings.PropertyChanged += async (sender, e) =>
            {
                if (e.PropertyName == "UserDisplayName")
                {
                    Settings.NeedsSync = true;
                    OnPropertyChanged("LoginText");
                    //if logged in you should go ahead and sync data.
                    if (Settings.IsLoggedIn)
                    {
                        await ExecuteSyncCommandAsync();
                    }
                }
            };
        }

        public string LoginText => Settings.IsLoggedIn ? "Sign out" : "Sign In";

        public string LastSyncDisplay
        {
            get
            {
                if (!Settings.HasSyncedData)
                    return "Never";

                return Settings.LastSync.ToString("s");
            }
        }

        ICommand loginCommand;
        public ICommand LoginCommand =>
            loginCommand ?? (loginCommand = new Command(ExecuteLoginCommand));

        void ExecuteLoginCommand()
        {

           if (IsBusy)
                return;

            if (Settings.IsLoggedIn)
            {
                Logout();
            }
            else
            {
                MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
            }

        }

        async Task Logout()
        {

            try
            {
                IStoreManager storeManager = DependencyService.Get<IStoreManager>();
                if (storeManager != null)
                {
                    await storeManager.LogoutAsync();
                }

                Settings.UserDisplayName = string.Empty;

                //drop favorites and feedback because we logged out.
                await StoreManager.FavoriteStore.DropFavorites();
                await StoreManager.DropEverythingAsync();
                await ExecuteSyncCommandAsync();
            }
            catch (Exception ex)
            {
                ex.Data["method"] = "ExecuteLoginCommandAsync";
                //TODO validate here.
            }
        }

        string syncText = "Sync Now";
        public string SyncText
        {
            get { return syncText; }
            set { SetProperty(ref syncText, value); }
        }

        ICommand syncCommand;
        public ICommand SyncCommand =>
            syncCommand ?? (syncCommand = new Command(async () => await ExecuteSyncCommandAsync()));

        async Task ExecuteSyncCommandAsync()
        {

            if (IsBusy)
                return;


            SyncText = "Syncing...";
            IsBusy = true;

            try
            {


#if DEBUG
                await Task.Delay(1000);
#endif

                Settings.HasSyncedData = true;
                Settings.LastSync = DateTime.UtcNow;
                OnPropertyChanged("LastSyncDisplay");

                await StoreManager.SyncAllAsync(Settings.IsLoggedIn);
                if (!Settings.IsLoggedIn)
                {
                    MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message, new MessagingServiceAlert
                    {
                        Title = "Data Synced",
                        Message = "You now have the latest data. Sign in to synchronise your favorites.",
                        Cancel = "Ok"
                    });
                }

            }
            catch (Exception ex)
            {
                ex.Data["method"] = "ExecuteSyncCommandAsync";

                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message, new MessagingServiceAlert
                {
                    Title = "Unable to sync",
                    Message = "Uh oh, something went wrong with the sync, please try again. \n\n Debug:" + ex.Message,
                        Cancel = "Ok"
                });
            }
            finally
            {
                SyncText = "Sync Now";
                IsBusy = false;
            }
        }
    }
}