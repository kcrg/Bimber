using Bimber.Models;
using System.Threading.Tasks;

namespace Bimber.Services
{
    public interface IRestService
    {
        Task<ResponseModel> GetPeopleAsync(int page = 1);
        Task<PersonModel> GetPersonByIdAsync(int id);
    }
}
