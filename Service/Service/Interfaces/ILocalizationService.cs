using Domain.Model.Request;
using Domain.Model.Response;
using System.Threading.Tasks;

namespace Application.Service.Interfaces
{
    public interface ILocalizationService
    {
        Task<ResponseLocalization> GetClientApi(string url, string getParam);
    }
}
