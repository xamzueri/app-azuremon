using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Azuremon.Android.Renderers;
using Azuremon.Utils;
using Azuremon.Utils.Helpers;
using FormsToolkit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(Azuremon.Core.UI.Controls.NavigationView), typeof(NavigationViewRenderer))]
namespace Azuremon.Android.Renderers
{
    public class NavigationViewRenderer : ViewRenderer<Azuremon.Core.UI.Controls.NavigationView, NavigationView>
    {
        private NavigationView _navView;

        private TextView _profileName;
        protected override void OnElementChanged(ElementChangedEventArgs<Azuremon.Core.UI.Controls.NavigationView> e)
        {

            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;


            var view = Inflate(Forms.Context, Resource.Layout.nav_view, null);
            _navView = view.JavaCast<NavigationView>();


            _navView.NavigationItemSelected += NavView_NavigationItemSelected;

            Settings.Current.PropertyChanged += SettingsPropertyChanged;
            SetNativeControl(_navView);

            var header = _navView.GetHeaderView(0);
            _profileName = header.FindViewById<TextView>(Resource.Id.profile_name);

            _profileName.Click += (sender, e2) => NavigateToLogin();

            UpdateName();

            _navView.SetCheckedItem(Resource.Id.nav_pokedex);
        }

        void NavigateToLogin()
        {
            if (Settings.Current.IsLoggedIn)
                return;

            MessagingService.Current.SendMessage(MessageKeys.NavigateLogin);
        }

        void SettingsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Settings.Current.UserDisplayName))
            {
                UpdateName();
            }
        }

        void UpdateName()
        {
            _profileName.Text = Settings.Current.UserDisplayName;
        }

        public override void OnViewRemoved(View child)
        {
            base.OnViewRemoved(child);
            _navView.NavigationItemSelected -= NavView_NavigationItemSelected;
            Settings.Current.PropertyChanged -= SettingsPropertyChanged;
        }

        IMenuItem previousItem;

        void NavView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {


            if (previousItem != null)
                previousItem.SetChecked(false);

            _navView.SetCheckedItem(e.MenuItem.ItemId);

            previousItem = e.MenuItem;

            int id = 0;
            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.nav_pokedex:
                    id = (int)AppPage.Pokedex;
                    break;
                case Resource.Id.nav_settings:
                    id = (int)AppPage.Settings;
                    break;
            }
            this.Element.OnNavigationItemSelected(new Azuremon.Core.UI.Controls.NavigationItemSelectedEventArgs
            {
                Index = id
            });
        }


    }
}