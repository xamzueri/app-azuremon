using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.Core.ViewModel;
using Azuremon.DataObjects;
using Xamarin.Forms;

namespace Azuremon.Core.UI.Pages.Pokedex
{
    public partial class PokemonDetailsPage : ContentPage
    {
        private PokemonDetailsViewModel _vm;

        public PokemonDetailsPage(Pokemon pokemon)
        {
            InitializeComponent();

            BindingContext = _vm = new PokemonDetailsViewModel(Navigation, pokemon);
        }
    }
}
