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

namespace Azuremon.Core.UI.Pages.Android
{
    public class RootPageAndroid
        : MasterDetailPage
    {
        readonly Dictionary<int, FancyNavigationPage> _pages;

        public RootPageAndroid()
        {
            _pages = new Dictionary<int, FancyNavigationPage>();
            Master = new MenuPage(this);

            _pages.Add(0, new FancyNavigationPage(new PokemonListPage()));

            Detail = _pages[0];

        }
        
        public async Task NavigateAsync(int menuId)
        {
            FancyNavigationPage newPage = null;
            if (!_pages.ContainsKey(menuId))
            {
                //only cache specific pages
                switch (menuId)
                {
                    case (int)AppPage.Pokedex: //Feed
                        _pages.Add(menuId, new FancyNavigationPage(new PokemonListPage()));
                        break;
                    case (int)AppPage.Settings: //Settings
                        newPage = new FancyNavigationPage(new SettingsPage());
                        break;
                }
            }

            if (newPage == null)
                newPage = _pages[menuId];

            if (newPage == null)
                return;

            //if we are on the same tab and pressed it again.
            if (Detail == newPage)
            {
                await newPage.Navigation.PopToRootAsync();
            }

            Detail = newPage;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            if (Settings.Current.FirstRun)
            {
                MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
            }
        }

    }
}
