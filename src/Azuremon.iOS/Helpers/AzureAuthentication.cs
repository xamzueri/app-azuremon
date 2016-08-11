using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Azuremon.DataStore.Abstractions;
using Azuremon.iOS.Helpers;
using Microsoft.WindowsAzure.MobileServices;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureAuthentication))]
namespace Azuremon.iOS.Helpers
{
    public class AzureAuthentication
        : IAuthenticate
    {
        public static UIViewController CurrentLoginViewController { get; set; }

        public async Task<MobileServiceUser> Authenticate(MobileServiceClient client, string provider)
        {
            return await client.LoginAsync(CurrentLoginViewController, provider);
        }
    }
}
