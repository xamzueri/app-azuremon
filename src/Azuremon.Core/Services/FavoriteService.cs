using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.DataObjects;
using Azuremon.DataStore.Abstractions;
using Azuremon.Utils;
using Azuremon.Utils.Helpers;
using FormsToolkit;
using Xamarin.Forms;

namespace Azuremon.Core.Services
{
    public class FavoriteService
    {
        Pokemon _pokemonQueued;
        public FavoriteService()
        {
            MessagingService.Current.Subscribe(MessageKeys.LoggedIn, async (s) =>
            {
                if (_pokemonQueued == null)
                    return;

                await ToggleFavorite(_pokemonQueued);
            });
        }
        public async Task<bool> ToggleFavorite(Pokemon pokemon)
        {
            if (!Settings.Current.IsLoggedIn)
            {
                _pokemonQueued = pokemon;
                

                var storeManager = DependencyService.Get<IStoreManager>();

                if (storeManager is Azuremon.DataStore.Mock.StoreManager)
                {
                    MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.Message,
                        new MessagingServiceAlert
                        {
                            Title = "¯\\_(ツ)_/¯",
                            Message = "Computer say no.",
                            Cancel = "Ok"
                        });
                }
                else
                {
                    MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
                }

                return false;
            }

            _pokemonQueued = null;

            var store = DependencyService.Get<IFavoriteStore>();
            pokemon.IsFavorite = !pokemon.IsFavorite;//switch first so UI updates :)
            if (!pokemon.IsFavorite)
            {
                var items = await store.GetItemsAsync();
                foreach (var item in items.Where(s => s.PokemonId == pokemon.Id))
                {
                    await store.RemoveAsync(item);
                }
            }
            else
            {
                await store.InsertAsync(new Favorite { PokemonId = pokemon.Id });
            }

            return true;
        }
    }
}
