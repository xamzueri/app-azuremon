using System;
using System.Collections.Generic;
using System.Text;

namespace Azuremon.DataObjects
{
    public class Favorite : BaseDataObject
    {
        public string UniversalId { get; set; }
        public string PokemonId { get; set; }
    }
}
