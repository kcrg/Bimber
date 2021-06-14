using Bimber.Models;
using RestSharp;
using System.Threading.Tasks;

namespace Bimber.Services.Implementations
{
    public class RestService : IRestService
    {
        private readonly RestClient restClient = new("https://reqres.in/api/");

        public async Task<ResponseModel> GetPeopleAsync(int page = 1)
        {
            var request = new RestRequest("users", Method.GET, DataFormat.Json);
            request.AddParameter("page", page, ParameterType.QueryString);
            return await restClient.GetAsync<ResponseModel>(request).ConfigureAwait(false);
        }

        public async Task<PersonModel> GetPersonByIdAsync(int id)
        {
            var request = new RestRequest($"users/{id}", Method.GET, DataFormat.Json);
            return await restClient.GetAsync<PersonModel>(request).ConfigureAwait(false);
        }

        public async Task<LoginResponseModel> LoginAsync(string email, string password)
        {
            var request = new RestRequest("login", Method.POST, DataFormat.Json);
            request.AddParameter("email", email, ParameterType.GetOrPost);
            request.AddParameter("password", password, ParameterType.GetOrPost);

            return await restClient.PostAsync<LoginResponseModel>(request).ConfigureAwait(false);
        }

        public async Task<LoginResponseModel> RegisterAsync(string email, string password)
        {
            var request = new RestRequest("register", Method.POST, DataFormat.Json);
            request.AddParameter("email", email, ParameterType.GetOrPost);
            request.AddParameter("password", password, ParameterType.GetOrPost);
            return await restClient.PostAsync<LoginResponseModel>(request).ConfigureAwait(false);
        }
    }
}
