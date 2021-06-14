using Bimber.Models;
using System.Threading.Tasks;

namespace Bimber.Services
{
    public interface IRestService
    {
        Task<LoginResponseModel> LoginAsync(string email, string password);
        Task<LoginResponseModel> RegisterAsync(string email, string password);

        Task<ResponseModel> GetPeopleAsync(int page = 1);
        Task<PersonModel> GetPersonByIdAsync(int id);
    }
}
