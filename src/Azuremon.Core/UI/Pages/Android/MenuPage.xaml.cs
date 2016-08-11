using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Azuremon.Core.UI.Pages.Android
{
    public partial class MenuPage : ContentPage
    {
        private readonly RootPageAndroid _root;
        public MenuPage(RootPageAndroid root)
        {
            _root = root;
            InitializeComponent();

            NavView.NavigationItemSelected += (sender, e) =>
            {
                _root.IsPresented = false;
                Device.StartTimer(TimeSpan.FromMilliseconds(300), () =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await _root.NavigateAsync(e.Index);
                    });
                    return false;
                });
            };
        }
    }
}
