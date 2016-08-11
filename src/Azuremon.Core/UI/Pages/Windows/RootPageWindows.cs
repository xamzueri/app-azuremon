using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using MenuItem = Azuremon.Core.Model.MenuItem;

namespace Azuremon.Core.UI.Pages.Windows
{
    public class RootPageWindows
        : MasterDetailPage
    { 
        Dictionary<AppPage, Page> pages;
        MenuPageUWP menu;
        public static bool IsDesktop { get; set; }
        public RootPageWindows()
        {
            //MasterBehavior = MasterBehavior.Popover;
            pages = new Dictionary<AppPage, Page>();

            var items = new ObservableCollection<MenuItem>
            {
                new MenuItem { Name = "Pokédex", Icon = "menu_pokedex.png", Page = AppPage.Pokedex },
                new MenuItem { Name = "Settings", Icon = "menu_settings.png", Page = AppPage.Settings }
            };

            menu = new MenuPageUWP();
            menu.MenuList.ItemsSource = items;


            menu.MenuList.ItemSelected += (sender, args) =>
            {
                if (menu.MenuList.SelectedItem == null)
                    return;

                Device.BeginInvokeOnMainThread(() =>
                {
                    NavigateAsync(((MenuItem)menu.MenuList.SelectedItem).Page);
                    if (!IsDesktop)
                        IsPresented = false;
                });
            };

            Master = menu;
            NavigateAsync((int)AppPage.Pokedex);
            Title = "Azurémon GO";
        }



        public void NavigateAsync(AppPage menuId)
        {
            Page newPage = null;
            if (!pages.ContainsKey(menuId))
            {
                //only cache specific pages
                switch (menuId)
                {
                    case AppPage.Pokedex:
                        pages.Add(menuId, new FancyNavigationPage(new PokemonListPage()));
                        break;
                    case AppPage.Settings://sessions
                        pages.Add(menuId, new FancyNavigationPage(new SettingsPage()));
                        break;
                }
            }

            if (newPage == null)
                newPage = pages[menuId];

            if (newPage == null)
                return;

            Detail = newPage;
            //await Navigation.PushAsync(newPage);
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

