using Acr.UserDialogs;
using Bimber.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Bimber.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ProfilePageViewModel : BindableBase
    {
        private readonly INavigationService navigationService;

        public string? ButtonText { get; set; }
        public Color? ButtonTextColor { get; set; }
        public Color? PromoColor { get; set; }

        public ObservableCollection<BimberPromoModel> PromoList { get; set; }
        public DelegateCommand<BimberPromoModel> ItemChangedCommand { get; }
        public DelegateCommand SettingsCommand { get; }
        public DelegateCommand PhoneTappedCommand { get; }
        public DelegateCommand EmailTappedCommand { get; }
        public DelegateCommand OpenGithubCommand { get; }

        public ProfilePageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            PromoList = new ObservableCollection<BimberPromoModel>();
            ItemChangedCommand = new DelegateCommand<BimberPromoModel>((bimberPromoModel) => UpdateVisuals(bimberPromoModel));

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

            LoadPromoList();
        }

        private void UpdateVisuals(BimberPromoModel bimberPromoModel)
        {
            ButtonTextColor = bimberPromoModel.ButtonTextColor;
            ButtonText = bimberPromoModel.ButtonText;
            PromoColor = bimberPromoModel.PromoColor;
        }

        private void LoadPromoList()
        {
            var bimberPlatinum = new BimberPromoModel()
            {
                Title = "Bimber Platinum",
                Description = "Odkryj zupełnie nowy potencjał Bimbera",
                ButtonText = "DOWIEDZ SIĘ WIĘCEJ",
                Glyph = "",
                PromoColor = Color.Black,
                ButtonTextColor = Color.Black
            };
            var bimberLikes = new BimberPromoModel()
            {
                Title = "Polubiły Cię 4 osoby",
                Description = "Zobacz ich w wersji Bimber Gold™",
                ButtonText = "ZOBACZ, KTO CIĘ POLUBIŁ",
                Glyph = "",
                PromoColor = Color.FromHex("#e3a955"),
                ButtonTextColor = Color.FromHex("#e3a955")
            };
            var bimberFast = new BimberPromoModel()
            {
                Title = "Twórz pary jeszcze szybciej",
                Description = "Wylansuj swój profil raz w miesiącu!",
                ButtonText = "MÓJ BIMBER PLUS®",
                Glyph = "",
                PromoColor = Color.FromHex("#d157ff"),
                ButtonTextColor = Color.FromHex("#ea5165")
            };
            var bimberSuper = new BimberPromoModel()
            {
                Title = "Wyróżnij się superlajkami",
                Description = "Masz trzykrotnie więcej szans na znalezienie pary!",
                ButtonText = "MÓJ BIMBER PLUS®",
                Glyph = "",
                PromoColor = Color.FromHex("#04cef4"),
                ButtonTextColor = Color.FromHex("#ea5165")
            };
            var bimberWorldwide = new BimberPromoModel()
            {
                Title = "Szukaj par na całym świecie",
                Description = "Wraz z wersją Bimber Plus® otrzymasz Paszport do dowolnego miejsca na świecie!",
                ButtonText = "MÓJ BIMBER PLUS®",
                Glyph = "",
                PromoColor = Color.FromHex("#033e82"),
                ButtonTextColor = Color.FromHex("#ea5165")
            };
            var bimberLock = new BimberPromoModel()
            {
                Title = "Wiek i odległość pod Twoją kontrolą",
                Description = "W wersji Bimber Plus® to ty decydujesz, co zobaczą inni użytkownicy.",
                ButtonText = "MÓJ BIMBER PLUS®",
                Glyph = "",
                PromoColor = Color.FromHex("#fc8f7a"),
                ButtonTextColor = Color.FromHex("#ea5165")
            };
            var bimberRedo = new BimberPromoModel()
            {
                Title = "Ponowny wybór bez ograniczeń",
                Description = "W wersji Bimber Plus® otrzymasz nielimitowaną Drugą szansę!",
                ButtonText = "MÓJ BIMBER PLUS®",
                Glyph = "",
                PromoColor = Color.FromHex("#fcd403"),
                ButtonTextColor = Color.FromHex("#ea5165")
            };
            var bimberUnlimited = new BimberPromoModel()
            {
                Title = "Zwiększ swoje szanse",
                Description = "W wersji Bimber Plus® otrzymasz nielimitowaną liczbę Polubień!",
                ButtonText = "MÓJ BIMBER PLUS®",
                Glyph = "",
                PromoColor = Color.FromHex("#2ee5af"),
                ButtonTextColor = Color.FromHex("#ea5165")
            };

            PromoList.Add(bimberPlatinum);
            PromoList.Add(bimberLikes);
            PromoList.Add(bimberFast);
            PromoList.Add(bimberSuper);
            PromoList.Add(bimberWorldwide);
            PromoList.Add(bimberLock);
            PromoList.Add(bimberRedo);
            PromoList.Add(bimberUnlimited);
        }

        private void ShowToastMessage(string message, int exceptionCode)
        {
            UserDialogs.Instance.Toast(message + "\nError code: " + exceptionCode);
        }
    }
}
