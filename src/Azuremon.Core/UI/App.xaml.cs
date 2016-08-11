using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.Core.UI.Controls;
using Azuremon.Core.UI.Helpers;
using Azuremon.Core.UI.Pages.Android;
using Azuremon.Core.UI.Pages.General;
using Azuremon.Core.UI.Pages.iOS;
using Azuremon.Core.UI.Pages.Windows;
using Azuremon.Core.ViewModel;
using Azuremon.Utils;
using Azuremon.Utils.Helpers;
using FormsToolkit;
using Xamarin.Forms;

namespace Azuremon.Core.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ViewModelBase.Init();
            // The root page of your application
            switch (Device.OS)
            {
                case TargetPlatform.Android:
                    MainPage = new RootPageAndroid();
                    break;
                case TargetPlatform.iOS:
                    MainPage = new FancyNavigationPage(new RootPageiOS());
                    break;
                case TargetPlatform.Windows:
                case TargetPlatform.WinPhone:
                    MainPage = new RootPageWindows();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        protected override void OnStart()
        {
            OnResume();
        }

        private bool _registered = false;
        protected override void OnResume()
        {
            if (_registered)
                return;

            MessagingService.Current.Subscribe<MessagingServiceAlert>(MessageKeys.Message, async (m, info) =>
            {
                var task = Application.Current?.MainPage?.DisplayAlert(info.Title, info.Message, info.Cancel);

                if (task == null)
                    return;

                await task;
                info?.OnCompleted?.Invoke();
            });

            //TODO: Can be removed (Release 2)
            return;
            MessagingService.Current.Subscribe(MessageKeys.NavigateLogin, async m =>
            {
                if (Device.OS == TargetPlatform.Android)
                {
                    ((RootPageAndroid)MainPage).IsPresented = false;
                }

                Page page = null;
                if (Settings.Current.FirstRun && Device.OS == TargetPlatform.Android)
                    page = new LoginPage();
                else
                    page = new FancyNavigationPage(new LoginPage());


                var nav = Application.Current?.MainPage?.Navigation;
                if (nav == null)
                    return;

                await NavigationService.PushModalAsync(nav, page);

            });
        }

        protected override void OnSleep()
        {
            base.OnSleep();

            if (!_registered)
                return;

            _registered = false;
            MessagingService.Current.Unsubscribe(MessageKeys.NavigateLogin);
            MessagingService.Current.Unsubscribe<MessagingServiceAlert>(MessageKeys.Message);
        }
    }
}
