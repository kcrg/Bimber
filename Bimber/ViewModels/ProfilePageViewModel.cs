using Prism.Commands;
using Prism.Mvvm;
using System;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Essentials;
using Prism.Navigation;

namespace Bimber.ViewModels
{
    public class ProfilePageViewModel : BindableBase
    {
        private readonly INavigationService navigationService;

        public DelegateCommand SettingsCommand { get; }
        public DelegateCommand PhoneTappedCommand { get; }
        public DelegateCommand EmailTappedCommand { get; }
        public DelegateCommand OpenGithubCommand { get; }

        public ProfilePageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            SettingsCommand = new DelegateCommand(() => MainThread.BeginInvokeOnMainThread(async () => await this.navigationService.NavigateAsync("SettingsPage", useModalNavigation: true).ConfigureAwait(false)));

            PhoneTappedCommand = new DelegateCommand(() =>
            {
                try
                {
                    PhoneDialer.Open("+48733428869");
                }
                catch (ArgumentNullException ex)
                {
                    ShowToastMessage("The phone number is incorrect.", ex.HResult);
                }
                catch (FeatureNotSupportedException ex)
                {
                    ShowToastMessage("Device does not support phone calls or does not have phone app.", ex.HResult);
                }
                catch (Exception ex)
                {
                    ShowToastMessage("An error occurred while opening the phone application.", ex.HResult);
                }
            });

            EmailTappedCommand = new DelegateCommand(async () =>
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
                    ShowToastMessage("Device does not support E-mail or does not have E-mail app.", ex.HResult);
                }
                catch (Exception ex)
                {
                    ShowToastMessage("An error occurred while opening the email application.", ex.HResult);
                }
            });

            OpenGithubCommand = new DelegateCommand(async () => await Browser.OpenAsync("https://github.com/kcrg", BrowserLaunchMode.SystemPreferred).ConfigureAwait(false));
        }

        private void ShowToastMessage(string message, int exceptionCode)
        {
            //Replace to XCT version
            //UserDialogs.Instance.Toast(message + "\nError code: " + exceptionCode);
        }
    }
}
