using Domain.Model.Request;
using Domain.Model.Response;

namespace Application.Service.Interfaces
{
    public interface IPersonService
    {
        Response PostPhysicalPerson(PhysicalPersonRequest physicalPersonRequest);


    }
}
