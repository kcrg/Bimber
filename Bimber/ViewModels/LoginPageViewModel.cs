using Acr.UserDialogs;
using Bimber.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Bimber.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private readonly IRestService restService;
        private readonly INavigationService navigationService;

        public string? Email { get; set; } = "eve.holt@reqres.in";
        public string? Password { get; set; } = "cityslicka";

        public DelegateCommand LoginCommand { get; }
        public LoginPageViewModel(IRestService restService, INavigationService navigationService)
        {
            this.restService = restService;
            this.navigationService = navigationService;

            LoginCommand = new DelegateCommand(() => MainThread.BeginInvokeOnMainThread(async () => await LoginAsync().ConfigureAwait(false)));
        }

        private async Task LoginAsync()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ShowToastMessage("Please complete all fields.");

                return;
            }

            var response = await this.restService.LoginAsync(Email, Password).ConfigureAwait(false);

            if (response is null)
            {
                return;
            }
            else if (response.Error is not null)
            {
                ShowToastMessage($"Something went wrong. Error: '{response.Error}'");

                return;
            }

            ShowToastMessage($"You have logged in successfully. Token: {response.Token}");

            Preferences.Set("token", response.Token);

            MainThread.BeginInvokeOnMainThread(async () => await this.navigationService.NavigateAsync("app:///MainPage").ConfigureAwait(false));
        }

        private void ShowToastMessage(string message)
        {
            UserDialogs.Instance.Toast(message);
        }
    }
}
