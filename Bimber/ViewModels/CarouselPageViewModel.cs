using Acr.UserDialogs;
using Bimber.Extensions;
using Bimber.Models;
using Bimber.Services;
using MLToolkit.Forms.SwipeCardView.Core;
using Prism.Commands;
using Prism.Mvvm;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bimber.ViewModels
{
    public class CarouselPageViewModel : BindableBase
    {
        private readonly IRestService restService;

        private int pagination = 1;

        //private bool _isLoading = true;
        //public bool IsLoading
        //{
        //    get => _isLoading;
        //    set => SetProperty(ref _isLoading, value);
        //}
        public int Threshold { get; set; }

        public bool LoadAgainButtonVisibility { get; set; }

        public SafeObservableCollection<PersonModel> PeopleList { get; set; }
        public DelegateCommand<object> SwipedCommand { get; }
        public DelegateCommand LoadAgainCommand { get; }

        public CarouselPageViewModel(IRestService restService)
        {
            this.restService = restService;

            PeopleList = new SafeObservableCollection<PersonModel>();

            SwipedCommand = new DelegateCommand<object>(async (currentItem) => await LoadMoreDataAsync(currentItem).ConfigureAwait(false));

            LoadAgainCommand = new DelegateCommand(async () => await LoadDataAsync().ConfigureAwait(false));

            Task.Run(() => this.LoadDataAsync()).Wait();
        }

        private async Task LoadDataAsync()
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

            Device.BeginInvokeOnMainThread(() => PeopleList.AddRange(response.Data));

            LoadAgainButtonVisibility = false;
            pagination = 1;
            //IsLoading = false;
        }

        private async Task LoadMoreDataAsync(object? eventArgsObject = null)
        {
            if (!(eventArgsObject is SwipedCardEventArgs eventArgs))
            {
                return;
            }
            if (!(eventArgs.Item is PersonModel person))
            {
                return;
            }

            if (PeopleList.Count > 0 && 6 * pagination > person.Id)
            {
                return;
            }
            pagination++;

            var response = await restService.GetPeopleAsync(pagination).ConfigureAwait(false);

            if (response.TotalPages < pagination || response is null || response.Data is null || response.Data.Count == 0)
            {
                ShowToastMessage("There are no more people in your neighborhood.");

                PeopleList.Clear();

                LoadAgainButtonVisibility = true;

                return;
            }

            Device.BeginInvokeOnMainThread(() => PeopleList.Reset(response.Data));

            //IsLoading = false;
        }

        private void ShowToastMessage(string message)
        {
            UserDialogs.Instance.Toast(message);
        }
    }
}
