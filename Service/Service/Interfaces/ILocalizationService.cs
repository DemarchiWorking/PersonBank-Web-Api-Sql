using Domain.Model.Request;
using Domain.Model.Response;
using System.Threading.Tasks;

namespace Application.Service.Interfaces
{
    public interface ILocalizationService
    {
        Task<ResponseLocalization> GetClientApiState(string url, string getParam);

        Task<ResponseLocalizationFilter> GetClientApiCity(string url, string request,string getParam);
    }
}
