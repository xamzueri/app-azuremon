using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Azuremon.Core.UI;
using FormsToolkit.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Azuremon.Android
{
    [Activity(Label = "Azurémon GO", Icon = "@mipmap/ic_launcher", LaunchMode = LaunchMode.SingleTask, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {

            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;


            base.OnCreate(savedInstanceState);

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            Forms.Init(this, savedInstanceState);

            Toolkit.Init();

            LoadApplication(new App());
        }
    }
}

