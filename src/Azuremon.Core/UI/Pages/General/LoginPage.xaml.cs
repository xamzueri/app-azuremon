using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azuremon.Core.ViewModel;
using Azuremon.Utils;
using Xamarin.Forms;

namespace Azuremon.Core.UI.Pages.General
{
    public partial class LoginPage : ContentPage
    {
        readonly LoginViewModel _vm;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = _vm = new LoginViewModel(Navigation);

            if (!Settings.Current.FirstRun)
            {
                Title = "My Account";
                var cancel = new ToolbarItem
                {
                    Text = "Cancel",
                    Command = new Command(async () =>
                    {
                        if (_vm.IsBusy)
                            return;
                        await Navigation.PopModalAsync();
                    })
                };
                ToolbarItems.Add(cancel);

                if (Device.OS != TargetPlatform.iOS)
                    cancel.Icon = "toolbar_close.png";
            }


        }

        protected override bool OnBackButtonPressed()
        {
            if (Settings.Current.FirstRun)
                return true;

            return base.OnBackButtonPressed();
        }
    }
}
