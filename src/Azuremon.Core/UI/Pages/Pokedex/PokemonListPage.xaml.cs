using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.Core.UI.Helpers;
using Azuremon.Core.ViewModel;
using Azuremon.DataObjects;
using Azuremon.Utils.Helpers;
using Xamarin.Forms;

namespace Azuremon.Core.UI.Pages.Pokedex
{
    public partial class PokemonListPage : ContentPage
    {
        private PokemonListViewModel _vm;
        public PokemonListPage()
        {
            InitializeComponent();

            BindingContext = _vm = new PokemonListViewModel(Navigation);

            ListViewPokemon.ItemSelected += async (sender, e) =>
            {
                var pokemon = ListViewPokemon.SelectedItem as Pokemon;
                if (pokemon == null)
                    return;

                var pokemonDetails = new PokemonDetailsPage(pokemon);

                await NavigationService.PushAsync(Navigation, pokemonDetails);
                ListViewPokemon.SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm.LoadPokemonsCommand.Execute(true);
        }
    }
}
