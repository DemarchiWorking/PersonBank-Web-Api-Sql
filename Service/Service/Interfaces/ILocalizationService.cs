using Domain.Model.Request;
using Domain.Model.Response;
using System.Threading.Tasks;

namespace Application.Service.Interfaces
{
    public interface ILocalizationService
    {
        Task<ResponseLocalizationStates> GetClientApiState(string url, string getParam);

        Task<ResponseLocalizationAddressByState> GetClientApiCity(string url, string request,string getParam);
    }
}
