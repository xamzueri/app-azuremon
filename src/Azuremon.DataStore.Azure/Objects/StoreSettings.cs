using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azuremon.DataStore.Azure.Objects
{
    public class StoreSettings
    {
        public const string StoreSettingsId = "store_settings";

        public StoreSettings()
        {
            Id = StoreSettingsId;
        }

        public string Id { get; set; }

        public string UserId { get; set; }

        public string AuthToken { get; set; }
    }
}
