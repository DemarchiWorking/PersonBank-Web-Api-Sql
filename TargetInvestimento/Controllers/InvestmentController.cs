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
    [Route("investment")]
    [ApiController]    
    public class InvestmentController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IInvestmentService _investmentService;

        public InvestmentController(
            ILogger logger,
            IInvestmentService investmentService
            )
        {
            _logger = logger;
            _investmentService = investmentService;
        }

        /// <summary>
        /// Endpoint responsável por retornar planos.
        /// </summary>
        /// <returns>Retorna lista de planos.</returns>
        [HttpGet("filter-by-date-register")]
        [ProducesResponseType(typeof(ResponsePerson), 200)]
        [ProducesResponseType(typeof(ResponsePerson), 404)]
        [ProducesResponseType(typeof(ResponsePerson), 500)]
        public ActionResult<ResponsePerson> SearchPersonByRegistrationDate([FromBody] FilterDateRequest filterDateRequest)
        {
            try
            {
                var response = _investmentService.SearchPersonByRegistrationDate(filterDateRequest);

                if (response?.IsReturned == true)
                {
                    return Ok(new ResponsePerson()
                    {
                        Persons = response.Persons,
                        Status = 200,
                        IsReturned = true,
                        Title = "Lista de pessoas localizada com sucesso!"
                    });
                }
                if (response?.IsReturned == false)
                {
                    return BadRequest(new ResponsePerson()
                    {
                        IsReturned = false,
                        Status = 400,
                        Title = "Não foi possivel localizar a lista de pessoas!"
                    });
                }

                if (response?.IsReturned == false)
                {
                    return Ok(new ResponsePerson()
                    {
                        IsReturned = false,  
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[InvestmentController] Exception in SearchPersonByRegistrationDate!");
            }

            return StatusCode(500, new Response()
            {
                Status = 500,
                Title = "Erro interno no servidor!"
            });
        }


        [HttpGet("filter-by-income")]
        [ProducesResponseType(typeof(ResponsePerson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponsePerson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponsePerson), StatusCodes.Status500InternalServerError)]
        public ActionResult<ResponsePerson> SearchForIncome([FromBody] IncomeFilterRequest incomeFilterRequest)
        {
            try {

                var response = _investmentService.SearchForIncome(incomeFilterRequest);

                if (response?.IsReturned == true)
                {
                    return Ok(new ResponsePerson()
                    {
                        Persons = response.Persons,
                        IsReturned = true,
                        Status = 200,
                        Title = "Lista de pessoas localizada com sucesso!"
                    });
                }
                if (response?.IsReturned == false)
                {
                    return BadRequest(new ResponsePerson()
                    {
                        Status = 400,
                        IsReturned = false,
                        Title = "Não foi possivel localizar a lista de pessoas!"

                    });
                }
                else
                {
                    return BadRequest(new ResponsePerson()
                    {
                        IsReturned = false,
                        Status = 400,
                        Title = "Não foi possivel localizar a lista de pessoas."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[InvestmentController] Exception in SearchForIncome!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new ResponseConfirmVip()
              {
                  IsReturned = false,
                  Status = 500,
                  Title = "Internal Error"
              });

        }

    }
}
