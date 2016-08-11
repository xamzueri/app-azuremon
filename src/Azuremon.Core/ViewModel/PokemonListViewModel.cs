using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Azuremon.Core.Extensions;
using Azuremon.DataObjects;
using Azuremon.Utils.Helpers;
using FormsToolkit;
using MvvmHelpers;
using Xamarin.Forms;

namespace Azuremon.Core.ViewModel
{
    public class PokemonListViewModel
        : ViewModelBase
    {
        public PokemonListViewModel(INavigation navigation) : base(navigation)
        {
        }

        public ObservableRangeCollection<Pokemon> Pokemons { get; } = new ObservableRangeCollection<Pokemon>();
        public ObservableRangeCollection<Pokemon> PokemonsFiltered { get; } = new ObservableRangeCollection<Pokemon>();

        #region Properties
        
        string filter = string.Empty;
        public string Filter
        {
            get { return filter; }
            set
            {
                if (SetProperty(ref filter, value))
                    ExecuteFilterPokemonsAsync();

            }
        }
        #endregion

        #region Filtering and Sorting

        bool noPokemonsFound;
        public bool NoPokemonsFound
        {
            get { return noPokemonsFound; }
            set { SetProperty(ref noPokemonsFound, value); }
        }


        #endregion


        #region Commands

        ICommand forceRefreshCommand;
        public ICommand ForceRefreshCommand =>
        forceRefreshCommand ?? (forceRefreshCommand = new Command(async () => await ExecuteForceRefreshCommandAsync()));

        async Task ExecuteForceRefreshCommandAsync()
        {
            await ExecuteLoadPokemonsAsync(true);
        }

        ICommand filterPokemonsCommand;
        public ICommand FilterPokemonsCommand =>
            filterPokemonsCommand ?? (filterPokemonsCommand = new Command(async () => await ExecuteFilterPokemonsAsync()));

        async Task ExecuteFilterPokemonsAsync()
        {
            IsBusy = true;
            NoPokemonsFound = false;

            // Abort the current command if the query has changed and is not empty
            if (!string.IsNullOrEmpty(Filter))
            {
                var query = Filter;
                await Task.Delay(250);
                if (query != Filter)
                    return;
            }

            PokemonsFiltered.ReplaceRange(Pokemons.Search(Filter));

            if (PokemonsFiltered.Count == 0)
            {
                NoPokemonsFound = true;
            }
           
            IsBusy = false;
        }



        ICommand loadPokemonsCommand;
        public ICommand LoadPokemonsCommand =>
            loadPokemonsCommand ?? (loadPokemonsCommand = new Command<bool>(async (f) => await ExecuteLoadPokemonsAsync()));


        async Task<bool> ExecuteLoadPokemonsAsync(bool force = false)
        {
            if (IsBusy)
                return false;

            try
            {
                IsBusy = true;
                NoPokemonsFound = false;
                Filter = string.Empty;

#if DEBUG
                await Task.Delay(1000);
#endif

                Pokemons.ReplaceRange(await StoreManager.PokemonStore.GetItemsAsync(force));
                PokemonsFiltered.ReplaceRange(Pokemons);
                NoPokemonsFound = PokemonsFiltered.Count == 0;
                
            }
            catch (Exception ex)
            {
                MessagingService.Current.SendMessage(MessageKeys.Error, ex);
            }
            finally
            {
                IsBusy = false;
            }

            return true;
        }

        ICommand favoriteCommand;
        public ICommand FavoriteCommand =>
            favoriteCommand ?? (favoriteCommand = new Command<Pokemon>(async (s) => await ExecuteFavoriteCommandAsync(s)));

        async Task ExecuteFavoriteCommandAsync(Pokemon pokemon)
        {
            await FavoriteService.ToggleFavorite(pokemon);
        }


        #endregion
    }
}
