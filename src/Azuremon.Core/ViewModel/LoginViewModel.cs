using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Azuremon.DataObjects;
using Azuremon.DataStore.Abstractions;
using Azuremon.Utils.Helpers;
using FormsToolkit;
using Xamarin.Forms;

namespace Azuremon.Core.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(INavigation navigation) : base(navigation)
        {

        }

        string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        ICommand loginCommand;
        public ICommand LoginCommand =>
            loginCommand ?? (loginCommand = new Command<string>(async param => await ExecuteLoginAsync(param)));

        async Task ExecuteLoginAsync(string provider)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                Message = "Signing in...";
#if DEBUG
                await Task.Delay(1000);
#endif
                var storeManager = DependencyService.Get<IStoreManager>();

                var result = await storeManager.LoginAsync(provider);

                if (result != null)
                {
                    Message = "Updating Pokedex...";

                    var azureManager = storeManager as DataStore.Azure.StoreManager;
                    if (azureManager != null && false)
                    {
                        var user = await azureManager.MobileService
                            .InvokeApiAsync<User>("User", HttpMethod.Get, null);

                        Settings.UserDisplayName = user.DisplayName;

                        MessagingService.Current.SendMessage(MessageKeys.LoggedIn);
                    }

                    Settings.UserDisplayName = result.UserId;

                    try
                    {
                        await StoreManager.SyncAllAsync(true);
                        Settings.LastSync = DateTime.UtcNow;
                        Settings.HasSyncedData = true;
                    }
                    catch (Exception ex)
                    {
                        //if sync doesn't work don't worry it is alright we can recover later
                    }
                    await Finish();
                    Settings.FirstRun = false;
                }
            }
            catch (Exception ex)
            {
                MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message, new MessagingServiceAlert
                {
                    Title = "Unable to Sing in",
                    Message = "Uhh ohh something went wrong. But don't worry, the code monkeys have been dispatched.",
                    Cancel = "OK"
                });
            }
            finally
            {
                Message = string.Empty;
                IsBusy = false;
            }

        }


        ICommand cancelCommand;
        public ICommand CancelCommand =>
            cancelCommand ?? (cancelCommand = new Command(async () => await ExecuteCancelAsync()));

        async Task ExecuteCancelAsync()
        {
            if (Settings.FirstRun)
            {
                try
                {
                    Message = "Updating Pokedex";
                    IsBusy = true;
                    await StoreManager.SyncAllAsync(false);
                    Settings.LastSync = DateTime.UtcNow;
                    Settings.HasSyncedData = true;
                }
                catch (Exception ex)
                {
                    //if sync doesn't work don't worry it is alright we can recover later

                }
                finally
                {
                    Message = string.Empty;
                    IsBusy = false;
                }
            }
            await Finish();
            Settings.FirstRun = false;
        }

        async Task Finish()
        {
            await Navigation.PopModalAsync();
        }
    }
}
