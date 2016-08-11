using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Azuremon.DataStore.Abstractions
{
    public interface IStoreManager
    {
        bool IsInitialized { get; }
        Task InitializeAsync();
        IPokemonStore PokemonStore { get; }
        IFavoriteStore FavoriteStore { get; }
        ICategoryStore CategoryStore { get; }

        Task<bool> SyncAllAsync(bool syncUserSpecific);
        Task DropEverythingAsync();

        Task<MobileServiceUser> LoginAsync(string provider);

        Task LogoutAsync();

        int Version { get; }
    }
}