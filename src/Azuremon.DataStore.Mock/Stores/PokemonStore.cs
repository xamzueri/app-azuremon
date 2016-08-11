using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.DataObjects;
using Azuremon.DataStore.Abstractions;

namespace Azuremon.DataStore.Mock.Stores
{
    public class PokemonStore : BaseStore<Pokemon>, IPokemonStore
    {
        public readonly HashSet<Pokemon> _pokemonDb = new HashSet<Pokemon>();

        public override async Task InitializeStore()
        {
            if (_pokemonDb.Count > 0) return;

            await StoreManager.CategoryStore.InitializeStore().ConfigureAwait(false);

            var categories = (await StoreManager.CategoryStore.GetItemsAsync(false).ConfigureAwait(false)).ToList();

            _pokemonDb.Add(new Pokemon
            {
                Id = Guid.NewGuid().ToString(),
                Number = 1,
                Name = "Bulbasaur",
                Categories = categories.Where(t => t.Name == "Grass" || t.Name == "Poison").ToList(),
                Stamina = 90,
                Attack = 126,
                Defense = 126,
                CaptureRate = "16",
                FleeRate = "10",
                Candy = "25",
                QuickMoves = "Tackle,Vine Whip",
                SpecialMoves = "Power Whip,Seed Bomb,Sludge Bomb"
            });
            _pokemonDb.Add(new Pokemon
            {
                Id = Guid.NewGuid().ToString(),
                Number = 4,
                Name = "Charmander",
                Categories = categories.Where(t => t.Name == "Fire").ToList(),
                Stamina = 78,
                Attack = 128,
                Defense = 108,
                CaptureRate = "16",
                FleeRate = "10",
                Candy = "25",
                QuickMoves = "Ember,Scratch",
                SpecialMoves = "Flame Burst,Flame Charge,Flamethrower"
            });
            _pokemonDb.Add(new Pokemon
            {
                Id = Guid.NewGuid().ToString(),
                Number = 7,
                Name = "Squirtle",
                Categories = categories.Where(t => t.Name == "Water").ToList(),
                Stamina = 88,
                Attack = 112,
                Defense = 142,
                CaptureRate = "16",
                FleeRate = "10",
                Candy = "25",
                QuickMoves = "Bubble,Tackle",
                SpecialMoves = "Aqua Jet,Aqua Tail,Water Pulse"
            });
            _pokemonDb.Add(new Pokemon
            {
                Id = Guid.NewGuid().ToString(),
                Number = 25,
                Name = "Pikachu",
                Categories = categories.Where(t => t.Name == "Electric").ToList(),
                Stamina = 70,
                Attack = 124,
                Defense = 108,
                CaptureRate = "16",
                FleeRate = "10",
                Candy = "50",
                QuickMoves = "Quick Attack,Thunder Shock",
                SpecialMoves = "Discharge,Thunder,Thunder Punch",
                IsFavorite = true
            });

            return;
        }

        public override Task<Pokemon> GetItemAsync(string id)
        {

            return Task.FromResult(_pokemonDb.SingleOrDefault(t => t.Id == id));
        }

        public override async Task<IEnumerable<Pokemon>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeStore();
            return _pokemonDb.OrderBy(t => t.Number).AsEnumerable();
        }
    }
}
