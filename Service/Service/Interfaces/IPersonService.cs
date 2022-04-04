using Domain.Model.Dao;
using Domain.Model.Request;
using Domain.Model.Response;

namespace Application.Service.Interfaces
{
    public interface IPersonService
    {
        Response PostPhysicalPerson(PhysicalPersonRequest physicalPersonRequest);
        ResponseAddressPerson GetAddressById(int idPerson);
        ResponsePutAddress PutAddressById(AddressPersonReturn addressPersonReturn);

    }
}
