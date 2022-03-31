using Application.Service.Interfaces;
using Domain.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TargetInvestimento.Controllers
{
    [Route("plan")]
    [ApiController]    
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;


        public PlanController(
            IPlanService planService
            )
        {
            _planService = planService;
        }

        /// <summary>
        /// Endpoint responsável por retornar planos.
        /// </summary>
        /// <returns>Retorna lista de planos.</returns>
        [HttpGet("plans")]
        [ProducesResponseType(typeof(ResponsePlan), 200)]
        [ProducesResponseType(typeof(ResponsePlan), 404)]
        [ProducesResponseType(typeof(ResponsePlan), 500)]
        public ActionResult<ResponsePlan> GetAllPlans()
        {
            try
            {
                var response = _planService.GetAllPlans();

                if (response?.IsReturned == true)
                {
                    return Ok(new ResponsePlan()
                    {
                        Plans = response.Plans,
                        Status = 200,
                        IsReturned = true,
                        Title = "Lista de planos localizada com sucesso!"
                    });
                }
                if (response?.IsReturned == false)
                {
                    return BadRequest(new ResponsePlan()
                    {
                        IsReturned = false,
                        Status = 400,
                        Title = "Não foi possivel localizar a lista de planos!"
                    });
                }

                if (response?.IsReturned == false)
                {
                    return Ok(new ResponseLocalization()
                    {
                        IsReturned = false                       
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[PlanController] Exception in GetAllPlans!");
            }

            return StatusCode(500, new Response()
            {
                Status = 500,
                Title = "Erro interno no servidor!"
            });
        }


        [HttpPost("confirm-plan/vip")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<ResponseConfirmVip> ConfirmVipPlan(int idUsuario)
        {
            try { 
                var response = _planService.ConfirmVipPlan(idUsuario);

                if (response?.IsReturned == true)
                {
                    return Ok(new ResponseConfirmVip()
                    {
                        IsReturned = true,
                        Status = 200,
                        Title = "O usuário foi cadastrado na lista de Planos vip!"
                    });
                }
                if (response?.IsReturned == false)
                {
                    return BadRequest(new ResponseConfirmVip()
                    {
                        Status = 400,
                        IsReturned = false,
                        Title = "Não foi possivel cadastrar o usuario na lista de planos vip."
                        
                    });
                }

                else
                {
                    return BadRequest(new Response()
                    {
                        Registered = false
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PersonController] Exception in PostPhysicalPerson!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Registered = false
              });

        }

    }
}
