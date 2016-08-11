using System;
using System.Collections.Generic;
using System.Text;
#if MOBILE
using MvvmHelpers;
#endif
namespace Azuremon.DataObjects
{
    public interface IBaseDataObject
    {
        string Id {get;set;}
    }
#if BACKEND
    public class BaseDataObject : Microsoft.Azure.Mobile.Server.EntityData
    {
        public BaseDataObject ()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string RemoteId { get; set; }

        public string Locale { get; set; }
    }
#else
    public class BaseDataObject : ObservableObject, IBaseDataObject
    {
        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string RemoteId { get; set; }

        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }
        
    }
#endif
}
