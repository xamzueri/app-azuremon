using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Azuremon.Android.Helpers;
using Azuremon.DataStore.Abstractions;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(AzureAuthentication))]
namespace Azuremon.Android.Helpers
{
    public class AzureAuthentication : IAuthenticate
    {
        public async Task<MobileServiceUser> Authenticate(MobileServiceClient client, string provider)
        {
            return await client.LoginAsync((Activity)Forms.Context, provider);
        }
    }
}