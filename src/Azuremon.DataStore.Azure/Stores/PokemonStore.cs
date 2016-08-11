using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.DataObjects;
using Azuremon.DataStore.Abstractions;
using Xamarin.Forms;

namespace Azuremon.DataStore.Azure.Stores
{
    public class PokemonStore : BaseStore<Pokemon>, IPokemonStore
    {

        
        public override string Identifier => nameof(Pokemon);

        public override async Task<IEnumerable<Pokemon>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeStore().ConfigureAwait(false);
            if (forceRefresh)
                await PullLatestAsync().ConfigureAwait(false);
            
            var items = await Table.OrderBy(s => s.Number).ToListAsync().ConfigureAwait(false);
            var favStore = DependencyService.Get<IFavoriteStore>();
            await favStore.GetItemsAsync(true).ConfigureAwait(false);//pull latest

            foreach (var item in items)
            {
                var isFav = await favStore.IsFavorite(item.Id).ConfigureAwait(false);
                item.IsFavorite = isFav;
            }

            return items;
        }
    }
}
