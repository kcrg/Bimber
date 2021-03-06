using Bimber.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PropertyChanged;
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
        public DelegateCommand GithubCommand { get; }

        public ProfilePageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            PromoList = new ObservableCollection<BimberPromoModel>();
            ItemChangedCommand = new DelegateCommand<BimberPromoModel>((bimberPromoModel) => UpdateVisuals(bimberPromoModel));

            SettingsCommand = new DelegateCommand(() => MainThread.BeginInvokeOnMainThread(async () => await this.navigationService.NavigateAsync("SettingsPage", useModalNavigation: true).ConfigureAwait(false)));

            GithubCommand = new DelegateCommand(async () => await Browser.OpenAsync("https://github.com/kcrg", BrowserLaunchMode.SystemPreferred).ConfigureAwait(false));

            LoadPromoList();
        }

        private void UpdateVisuals(BimberPromoModel bimberPromoModel)
        {
            PromoColor = bimberPromoModel.PromoColor;

            if (ButtonText == bimberPromoModel.ButtonText || ButtonTextColor == bimberPromoModel.ButtonTextColor)
            {
                return;
            }

            ButtonText = bimberPromoModel.ButtonText;
            ButtonTextColor = bimberPromoModel.ButtonTextColor;
        }

        private void LoadPromoList()
        {
            BimberPromoModel bimberPlatinum = new()
            {
                Title = "Bimber Platinum",
                Description = "Odkryj zupełnie nowy potencjał Bimbera",
                ButtonText = "DOWIEDZ SIĘ WIĘCEJ",
                Glyph = "",
                PromoColor = Color.Black,
                ButtonTextColor = Color.Black
            };
            BimberPromoModel bimberLikes = new()
            {
                Title = "Polubiły Cię 4 osoby",
                Description = "Zobacz ich w wersji Bimber Gold™",
                ButtonText = "ZOBACZ, KTO CIĘ POLUBIŁ",
                Glyph = "",
                PromoColor = Color.FromHex("#e3a955"),
                ButtonTextColor = Color.FromHex("#e3a955")
            };
            BimberPromoModel bimberFast = new()
            {
                Title = "Twórz pary jeszcze szybciej",
                Description = "Wylansuj swój profil raz w miesiącu!",
                ButtonText = "MÓJ BIMBER PLUS®",
                Glyph = "",
                PromoColor = Color.FromHex("#d157ff"),
                ButtonTextColor = Color.FromHex("#ea5165")
            };
            BimberPromoModel bimberSuper = new()
            {
                Title = "Wyróżnij się superlajkami",
                Description = "Masz trzykrotnie więcej szans na znalezienie pary!",
                ButtonText = "MÓJ BIMBER PLUS®",
                Glyph = "",
                PromoColor = Color.FromHex("#04cef4"),
                ButtonTextColor = Color.FromHex("#ea5165")
            };
            BimberPromoModel bimberWorldwide = new()
            {
                Title = "Szukaj par na całym świecie",
                Description = "Wraz z wersją Bimber Plus® otrzymasz Paszport do dowolnego miejsca na świecie!",
                ButtonText = "MÓJ BIMBER PLUS®",
                Glyph = "",
                PromoColor = Color.FromHex("#033e82"),
                ButtonTextColor = Color.FromHex("#ea5165")
            };
            BimberPromoModel bimberLock = new()
            {
                Title = "Wiek i odległość pod Twoją kontrolą",
                Description = "W wersji Bimber Plus® to ty decydujesz, co zobaczą inni użytkownicy.",
                ButtonText = "MÓJ BIMBER PLUS®",
                Glyph = "",
                PromoColor = Color.FromHex("#fc8f7a"),
                ButtonTextColor = Color.FromHex("#ea5165")
            };
            BimberPromoModel bimberRedo = new()
            {
                Title = "Ponowny wybór bez ograniczeń",
                Description = "W wersji Bimber Plus® otrzymasz nielimitowaną Drugą szansę!",
                ButtonText = "MÓJ BIMBER PLUS®",
                Glyph = "",
                PromoColor = Color.FromHex("#fcd403"),
                ButtonTextColor = Color.FromHex("#ea5165")
            };
            BimberPromoModel bimberUnlimited = new()
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
    }
}
