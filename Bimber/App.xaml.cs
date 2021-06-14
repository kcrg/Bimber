using Bimber.Services;
using Bimber.Services.Implementations;
using Bimber.ViewModels;
using Bimber.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("SegoeFluentIcons.ttf", Alias = "FontIcons")]
[assembly: ExportFont("Lato-Bold.ttf", Alias = "LatoBold")]
[assembly: ExportFont("Lato-Semibold.ttf", Alias = "LatoSemiBold")]
[assembly: ExportFont("BoldFont.ttf", Alias = "Bold")]
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Bimber
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);

            await NavigationService.NavigateAsync(nameof(MainPage)).ConfigureAwait(false);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.Register(typeof(IRestService), typeof(RestService));

            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<PersonPage, PersonPageViewModel>();
        }
    }
}
