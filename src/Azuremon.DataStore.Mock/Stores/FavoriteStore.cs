using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.DataObjects;
using Azuremon.DataStore.Abstractions;

namespace Azuremon.DataStore.Mock.Stores
{
    public class FavoriteStore : BaseStore<Favorite>, IFavoriteStore
    {
        public Task<bool> IsFavorite(string pokemonId)
        {
            return Task.FromResult(false);
        }

        public Task DropFavorites()
        {
            return Task.FromResult(false);
        }
    }
}
