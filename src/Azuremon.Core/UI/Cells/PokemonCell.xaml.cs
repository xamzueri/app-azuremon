using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Azuremon.Core.UI.Helpers;
using Azuremon.Core.UI.Pages.Pokedex;
using Azuremon.DataObjects;
using Azuremon.Utils.Helpers;
using Xamarin.Forms;

namespace Azuremon.Core.UI.Cells
{
    public class PokemonCell : ViewCell
    {

        readonly INavigation _navigation;
        public PokemonCell(INavigation navigation = null)
        {
            Height = 120;
            View = new PokemonCellView();
            _navigation = navigation;

        }

        protected override async void OnTapped()
        {
            base.OnTapped();
            if (_navigation == null)
                return;
            var pokemon = BindingContext as Pokemon;
            if (pokemon == null)
                return;

            await NavigationService.PushAsync(_navigation, new PokemonDetailsPage(pokemon));
        }
    }


    public partial class PokemonCellView : ContentView
    {
        public PokemonCellView()
        {
            InitializeComponent();
        }


        public static readonly BindableProperty FavoriteCommandProperty =
            BindableProperty.Create(nameof(FavoriteCommand), typeof(ICommand), typeof(PokemonCellView), default(ICommand));

        public ICommand FavoriteCommand
        {
            get { return GetValue(FavoriteCommandProperty) as Command; }
            set { SetValue(FavoriteCommandProperty, value); }
        }
    }
}
