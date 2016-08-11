using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.DataObjects;

namespace Azuremon.Core.Extensions
{
    public static class PokemonExtensions
    {
        public static IEnumerable<Pokemon> Search(this IEnumerable<Pokemon> pokemon, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return pokemon;

            var searchSplit = searchText.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            //search title, then category, then speaker name
            return pokemon.Where(t =>
                                  searchSplit.Any(search =>
                                t.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0));
        }
    }
}
