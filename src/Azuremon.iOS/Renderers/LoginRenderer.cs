using System;
using System.Collections.Generic;
using System.Text;
using Azuremon.Core.UI.Pages.General;
using Azuremon.iOS.Helpers;
using Azuremon.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace Azuremon.iOS.Renderers
{

    public class LoginPageRenderer : PageRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            AzureAuthentication.CurrentLoginViewController = this;
        }
        
    }
}
