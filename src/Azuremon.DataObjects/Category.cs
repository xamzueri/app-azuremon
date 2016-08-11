using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Azuremon.DataObjects
{
    public class Category : BaseDataObject
    {
        /// <summary>
        /// Gets or sets the name that is displayed during filtering
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the color in Hex form, for instance #FFFFFF
        /// </summary>
        /// <value>The color.</value>
        public string Color { get; set; }
#if MOBILE
        bool filtered;
        [JsonIgnore]
        public bool IsFiltered
        {
            get { return filtered; }
            set { SetProperty(ref filtered, value); }
        }

        bool enabled;
        [JsonIgnore]
        public bool IsEnabled
        {
            get { return enabled; }
            set { SetProperty(ref enabled, value); }
        }
#else
        public virtual ICollection<Pokemon> Pokemons { get; set; }
#endif
    }
}
