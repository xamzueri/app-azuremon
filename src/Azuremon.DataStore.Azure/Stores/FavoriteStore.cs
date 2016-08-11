using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.DataObjects;
using Azuremon.DataStore.Abstractions;

namespace Azuremon.DataStore.Azure.Stores
{
    public class FavoriteStore : BaseStore<Favorite>, IFavoriteStore
    {
        public async Task<bool> IsFavorite(string pokemonId)
        {
            await InitializeStore().ConfigureAwait(false);
            var items = await Table.Where(s => s.PokemonId == pokemonId).ToListAsync().ConfigureAwait(false);
            return items.Count > 0;
        }

        public Task DropFavorites()
        {
            return Task.FromResult(true);
        }

        public override async Task<bool> InsertAsync(Favorite item)
        {
            await InitializeStore().ConfigureAwait(false);
            if (StoreManager.Version == 1)
            {
                item.UniversalId = Guid.Empty.ToString();
            }
            return await base.InsertAsync(item);
        }

        public override string Identifier => nameof(Favorite);
    }
}
