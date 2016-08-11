using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.PushNotifications;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Azuremon.Core.Services;
using Azuremon.Core.ViewModel;
using Azuremon.Utils.Helpers;

namespace Azuremon.UWP
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            // If we are on mobile (Hence having the status bar API), set the status bar color to purple.
            //if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            //{
            //    var statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();

            //    // Set this to any Windows Color or ARGB value.
            //    //F44336

            //    statusBar.BackgroundColor = Color.FromArgb(244, 67, 54, 255);
            //    statusBar.BackgroundOpacity = 1;
            //}

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                // you'll need to add `using System.Reflection;`
                List<Assembly> assembliesToInclude = new List<Assembly>();

                //Now, add in all the assemblies your app uses
                assembliesToInclude.Add(typeof(FormsToolkit.HasDataConverter).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Microsoft.WindowsAzure.MobileServices.MobileServiceClient).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Microsoft.WindowsAzure.MobileServices.Sync.MobileServiceSyncHandler).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(System.Net.Http.HttpClient).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Newtonsoft.Json.JsonConvert).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Plugin.Connectivity.ConnectivityImplementation).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Plugin.Connectivity.Abstractions.BaseConnectivity).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Plugin.Settings.SettingsImplementation).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Plugin.Settings.Abstractions.ISettings).GetTypeInfo().Assembly);

                assembliesToInclude.Add(typeof(Azuremon.DataStore.Azure.StoreManager).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Azuremon.DataStore.Mock.StoreManager).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(Azuremon.DataStore.Mock.StoreManager).GetTypeInfo().Assembly);
                assembliesToInclude.Add(typeof(AppPage).GetTypeInfo().Assembly);
                

                //Also do this for all your other 3rd party libraries

                Xamarin.Forms.Forms.Init(e, assembliesToInclude);
                ViewModelBase.Init();

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
