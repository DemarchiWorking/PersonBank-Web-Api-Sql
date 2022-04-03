using Application.Service.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace TargetInvestimento.Controllers
{
    [Route("kpi")]
    [ApiController]    
    public class KpiController : ControllerBase
    {
        private readonly IKpiService _kpiService;
        private readonly ILogger _logger;


        public KpiController(
            ILogger logger,
            IKpiService kpiService
            )
        {
            _logger = logger;
            _kpiService = kpiService;
        }

        /// <summary>
        /// Endpoint responsável por retornar planos.
        /// </summary>
        /// <returns>Retorna lista de planos.</returns>
        [HttpGet("membership-index")]
        [ProducesResponseType(typeof(ResponsePlansPerson), 200)]
        [ProducesResponseType(typeof(ResponsePlansPerson), 404)]
        [ProducesResponseType(typeof(ResponsePlansPerson), 500)]
        public ActionResult<ResponsePlansPerson> GetInformationPlanVip()
        {
            try
            {
            var response = _kpiService.GetInformationPlanVip();

                if (response?.IsReturned == true)
                {
                    return Ok(new ResponsePlansPerson()
                    {
                        PersonsPlan = response.PersonsPlan,
                        NumberPersons = response.NumberPersons,
                        VipPlansNumber = response.VipPlansNumber,                        
                        Status = 200,
                        IsReturned = true,
                        Title = "Lista de pessoas localizada com sucesso!"
                    });
                }
                if (response?.IsReturned == false)
                {
                    return BadRequest(new ResponsePlansPerson()
                    {
                        IsReturned = false,
                        Status = 400,
                        Title = "Não foi possivel localizar a lista de pessoas!"
                    });
                }

                if (response?.IsReturned == false)
                {
                    return Ok(new ResponsePlansPerson()
                    {
                        IsReturned = false,  
                        Status = 400
                    });
          
        }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[KpiController] Exception in GetInformationPlanVip!");
            }

            return StatusCode(500, new Response()
            {
                Status = 500,
                Title = "Erro interno no servidor!"
            }); 
        }

    }
}
