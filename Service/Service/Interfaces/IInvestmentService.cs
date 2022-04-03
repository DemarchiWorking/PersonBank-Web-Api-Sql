using Domain.Model.Request;
using Domain.Model.Response;
using System.Threading.Tasks;

namespace Application.Service.Interfaces
{
    public interface IInvestmentService
    {
        ResponsePerson SearchPersonByRegistrationDate(FilterDateRequest filterDateRequest);

        ResponsePerson SearchForIncome(IncomeFilterRequest incomeFilterRequest);

    }
}
