using Bimber.Models;
using Bimber.Services;
using Prism.Mvvm;
using Prism.Navigation;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Bimber.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CarouselPageViewModel : BindableBase
    {
        private readonly IRestService restService;
        private readonly INavigationService navigationService;

        private bool _isLoading = true;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private uint _threshold;
        public uint Threshold
        {
            get => _threshold;
            set => SetProperty(ref _threshold, value);
        }

        public ObservableCollection<PersonModel> PeopleList { get; set; }
        //public DelegateCommand<PersonModel> ItemTappedCommand { get; }
        public CarouselPageViewModel(IRestService restService, INavigationService navigationService)
        {
            this.restService = restService;
            this.navigationService = navigationService;

            PeopleList = new ObservableCollection<PersonModel>();

            //ItemTappedCommand = new DelegateCommand<PersonModel>((person) => MainThread.BeginInvokeOnMainThread(async () => await NavigateToPersonPage(person).ConfigureAwait(false)));

            Task.Run(() => this.LoadDataAsync()).Wait();
        }

        //private async Task NavigateToPersonPage(PersonModel person)
        //{
        //    var navigationParams = new NavigationParameters
        //    {
        //        { "person", new PersonModel() }
        //    };
        //    await this.navigationService.NavigateAsync("PersonPage", navigationParams, useModalNavigation: false).ConfigureAwait(false);
        //}

        public async Task LoadDataAsync()
        {
            if (PeopleList.Count > 0)
            {
                return;
            }

            var response = await restService.GetPeopleAsync().ConfigureAwait(false);

            if (response is null || response.Data is null)
            {
                return;
            }

            foreach (var todo in response.Data)
            {
                PeopleList.Add(todo);
            }
            IsLoading = false;
        }
    }
}
