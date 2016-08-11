using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.Mobile.Server.Authentication;

namespace Azuremon.Backend.Extensions
{
    public static class IdentityExtensions
    {
        public static async Task<string> UniversalUserId(this IPrincipal principal, HttpRequestMessage message)
        {
            ClaimsPrincipal claimsUser = (ClaimsPrincipal)principal;

            var credentials = await ProviderCredentials(principal, message);

            var provider = Provider(claimsUser);

            if (string.IsNullOrEmpty(provider))
                return Guid.Empty.ToString();

            var sid = credentials.UserId;

            return $"{provider}|{sid}";
        }

        private static string Provider(ClaimsPrincipal claimsUser)
        {
            return claimsUser.HasClaim(x => x.Type == IdentityProvider) ?
                claimsUser.Claims.Single(x => x.Type == IdentityProvider).Value :
                claimsUser.Identity.AuthenticationType;
        }

        private const string IdentityProvider = "http://schemas.microsoft.com/identity/claims/identityprovider";

        public static async Task<ProviderCredentials> ProviderCredentials(this IPrincipal principal, HttpRequestMessage message)
        {
            ClaimsPrincipal claimsUser = (ClaimsPrincipal)principal;

            var provider = Provider(claimsUser);

            ProviderCredentials credentials = null;

            switch (provider.ToLowerInvariant())
            {
                case "facebook":
                    credentials = await claimsUser.GetAppServiceIdentityAsync<FacebookCredentials>(message);
                    break;
                case "google":
                    credentials = await claimsUser.GetAppServiceIdentityAsync<GoogleCredentials>(message);
                    break;
                case "twitter":
                    credentials = await claimsUser.GetAppServiceIdentityAsync<TwitterCredentials>(message);
                    break;
                case "microsoftaccount":
                    credentials = await claimsUser.GetAppServiceIdentityAsync<MicrosoftAccountCredentials>(message);
                    break;
            }

            return credentials;
        }
    }
}