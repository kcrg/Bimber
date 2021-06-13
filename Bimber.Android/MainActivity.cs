using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using AndroidX.Core.View;
using Prism;
using Acr.UserDialogs;
using Prism.Ioc;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bimber.Droid
{
    [Activity(MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetTheme(Resource.Style.MainTheme);

            Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            Android.Glide.Forms.Init(this, debug: true);
            FormsMaterial.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            LoadApplication(new App(new AndroidInitializer()));

            SetStatusBarColor();
        }

        protected override void OnResume()
        {
            base.OnResume();

            SetStatusBarColor();
        }

        public void SetStatusBarColor()
        {
            var currentTheme = Xamarin.Forms.Application.Current.RequestedTheme;
            var color = currentTheme == OSAppTheme.Light ? Android.Graphics.Color.White : Android.Graphics.Color.DarkGray;
            var window = Platform.CurrentActivity.Window;

            if (window is null)
            {
                return;
            }

            window.SetStatusBarColor(color);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                window.SetNavigationBarColor(color);
            }

            using (var controller = WindowCompat.GetInsetsController(window, window.DecorView.RootView))
            {
                controller.AppearanceLightStatusBars = currentTheme == OSAppTheme.Light;
                controller.AppearanceLightNavigationBars = currentTheme == OSAppTheme.Light;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}