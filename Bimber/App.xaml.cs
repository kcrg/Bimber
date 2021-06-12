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

           // MainPage = new Views.MainPage();

            await NavigationService.NavigateAsync("MainPage").ConfigureAwait(false); //?{KnownNavigationParameters.SelectedTab}=ToDoListPage&{KnownNavigationParameters.CreateTab}=AboutPage

            //Routing.RegisterRoute("about", typeof(AboutPage));
            //Routing.RegisterRoute("todolist/todocreate", typeof(ToDoCreatePage));
            //Routing.RegisterRoute("todolist", typeof(ToDoListPage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            //containerRegistry.Register(typeof(IRestService), typeof(RestService));

            //containerRegistry.RegisterForNavigation<AppShell>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            //containerRegistry.RegisterForNavigation<ToDoCreatePage, ToDoCreatePageViewModel>();
            //containerRegistry.RegisterForNavigation<AboutPage, AboutPageViewModel>();
        }
    }
}
