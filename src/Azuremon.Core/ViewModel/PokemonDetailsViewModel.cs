using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Azuremon.DataObjects;
using Xamarin.Forms;

namespace Azuremon.Core.ViewModel
{
    public class PokemonDetailsViewModel
        : ViewModelBase
    {
        private Pokemon _pokemon;

        public Pokemon Pokemon
        {
            get { return _pokemon; }
            set { SetProperty(ref _pokemon, value); }
        }

        public PokemonDetailsViewModel(INavigation navigation, Pokemon pokemon)
            : base(navigation)
        {
            Pokemon = pokemon;
        }

        private ICommand _favoriteCommand;
        public ICommand FavoriteCommand =>
        _favoriteCommand ?? (_favoriteCommand = new Command(async () => await ExecuteFavoriteCommandAsync()));

        async Task ExecuteFavoriteCommandAsync()
        {
            await FavoriteService.ToggleFavorite(Pokemon);

        }
    }
}
