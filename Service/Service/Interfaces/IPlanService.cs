using Domain.Model.Request;
using Domain.Model.Response;
using System.Threading.Tasks;


namespace Application.Service.Interfaces
{
public interface IPlanService
{
ResponsePlan GetAllPlans();

ResponseConfirmVip ConfirmVipPlan(int idUsuario);


}
}
 