using System.Threading.Tasks;
using Azuremon.DataObjects;

namespace Azuremon.DataStore.Abstractions
{
    public interface IFavoriteStore : IBaseStore<Favorite>
    {
        Task<bool> IsFavorite(string pokemonId);
        Task DropFavorites();
    }
}