using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.Core.Services;
using Azuremon.Utils;
using Azuremon.DataStore.Abstractions;
using MvvmHelpers;
using Xamarin.Forms;

namespace Azuremon.Core.ViewModel
{
    public class ViewModelBase : BaseViewModel
    {

        protected INavigation Navigation { get; }

        public ViewModelBase(INavigation navigation = null)
        {
            Navigation = navigation;
        }

        public static void Init(bool mock = true)
        {
            if (mock)
            {
                DependencyService.Register<IFavoriteStore, DataStore.Mock.Stores.FavoriteStore>();
                DependencyService.Register<IPokemonStore, DataStore.Mock.Stores.PokemonStore>();
                DependencyService.Register<ICategoryStore, DataStore.Mock.Stores.CategoryStore>();
                DependencyService.Register<IStoreManager, DataStore.Mock.StoreManager>();
            }
            else
            {
                DependencyService.Register<IFavoriteStore, DataStore.Azure.Stores.FavoriteStore>();
                DependencyService.Register<IPokemonStore, DataStore.Azure.Stores.PokemonStore>();
                DependencyService.Register<ICategoryStore, DataStore.Azure.Stores.CategoryStore>();
                DependencyService.Register<IStoreManager, DataStore.Azure.StoreManager>();
            }

            DependencyService.Register<FavoriteService>();
        }

        protected IStoreManager StoreManager { get; } = DependencyService.Get<IStoreManager>();

        protected FavoriteService FavoriteService { get; } = DependencyService.Get<FavoriteService>();

        public Settings Settings
        {
            get { return Settings.Current; }
        }
    }
}
