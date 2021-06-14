using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using Xamarin.Essentials;

namespace Bimber.ViewModels
{
    public class SettingsPageViewModel : BindableBase
    {
        private readonly INavigationService navigationService;

        public DelegateCommand DoneCommand { get; }
        public DelegateCommand PhoneCommand { get; }
        public DelegateCommand GithubCommand { get; }
        public DelegateCommand EmailCommand { get; }
        public DelegateCommand LogoutCommand { get; }

        public SettingsPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            DoneCommand = new DelegateCommand(() => MainThread.BeginInvokeOnMainThread(async () => await this.navigationService.GoBackAsync(useModalNavigation: true).ConfigureAwait(false)));

            PhoneCommand = new DelegateCommand(() =>
            {
                try
                {
                    PhoneDialer.Open("+48733428869");
                }
                catch (ArgumentNullException ex)
                {
                    ShowToastMessage($"The phone number is incorrect. {ex.Message}");
                }
                catch (FeatureNotSupportedException ex)
                {
                    ShowToastMessage($"Device does not support phone calls or does not have phone app. {ex.Message}");
                }
                catch (Exception ex)
                {
                    ShowToastMessage($"An error occurred while opening the phone application. {ex.Message}");
                }
            });

            GithubCommand = new DelegateCommand(async () => await Browser.OpenAsync("https://github.com/kcrg", BrowserLaunchMode.SystemPreferred).ConfigureAwait(false));

            EmailCommand = new DelegateCommand(async () =>
            {
                try
                {
                    var message = new EmailMessage
                    {
                        To = { "kacper@tryniecki.com" },
                    };
                    await Email.ComposeAsync(message).ConfigureAwait(false);
                }
                catch (FeatureNotSupportedException ex)
                {
                    ShowToastMessage($"Device does not support E-mail or does not have E-mail app. {ex.Message}");
                }
                catch (Exception ex)
                {
                    ShowToastMessage($"An error occurred while opening the email application. {ex.Message}");
                }
            });

            LogoutCommand = new DelegateCommand(Logout);
        }

        private void Logout()
        {
            Preferences.Remove("token");

            MainThread.BeginInvokeOnMainThread(async () => await this.navigationService.NavigateAsync("app:///LoginPage").ConfigureAwait(false));
        }

        private void ShowToastMessage(string message)
        {
            UserDialogs.Instance.Toast(message);
        }
    }
}
