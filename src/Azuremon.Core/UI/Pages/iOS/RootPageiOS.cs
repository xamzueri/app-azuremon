using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.Core.UI.Controls;
using Azuremon.Core.UI.Pages.General;
using Azuremon.Core.UI.Pages.Pokedex;
using Azuremon.Utils;
using Azuremon.Utils.Helpers;
using FormsToolkit;
using Xamarin.Forms;

namespace Azuremon.Core.UI.Pages.iOS
{
    public class RootPageiOS
        : TabbedPage
    {

        public RootPageiOS()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            Children.Add(new FancyNavigationPage(new PokemonListPage()));
            Children.Add(new FancyNavigationPage(new SettingsPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Settings.Current.FirstRun)
            {
                MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
            }
        }
    }
}
