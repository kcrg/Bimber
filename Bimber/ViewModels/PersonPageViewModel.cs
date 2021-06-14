using Bimber.Models;
using Bimber.Services;
using Prism.Mvvm;
using Prism.Navigation;

namespace Bimber.ViewModels
{
    public class PersonPageViewModel : BindableBase, INavigatedAware
    {
        private readonly IRestService restService;

        public PersonModel? Person { get; set; }

        public PersonPageViewModel(IRestService restService)
        {
            this.restService = restService;

            Person = new PersonModel();

            //Task.Run(() => this.LoadDataAsync()).Wait();
        }

        //public async Task LoadDataAsync(int id)
        //{
        //    if (Person is not null)
        //    {
        //        return;
        //    }

        //    var response = await restService.GetPersonByIdAsync().ConfigureAwait(false);

        //    if (response is null || response.Data is null)
        //    {
        //        return;
        //    }

        //    IsLoading = false;
        //}

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Person = parameters.GetValue<PersonModel>("person");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            // nothing to do
        }
    }
}
