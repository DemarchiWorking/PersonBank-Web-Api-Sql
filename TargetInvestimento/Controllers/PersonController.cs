using Application.Service.Interfaces;
using Domain.Model.Dao;
using Domain.Model.Request;
using Domain.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;

namespace TargetInvestimento.Controllers
{
    [Route("person")]
    [ApiController]    
    public class PersonController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IPersonService _personService;


        public PersonController(
            ILogger logger,
            IPersonService personService
        
            )
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpPost("create-person")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> PostPhysicalPerson(PhysicalPersonRequest physicalPersonRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(physicalPersonRequest.FullName))
                {
                    return BadRequest(new Response()
                    {
                        Title = "Informe o nome completo do usuário!"
                    });
                }
                if (string.IsNullOrEmpty(physicalPersonRequest.Cpf))
                {
                    return BadRequest(new Response()
                    {
                        Title = "Informe o CPF do usuário!"
                    });
                }
                CPFCNPJ.IMain checkCpfCnpj = new CPFCNPJ.Main();
                if (!checkCpfCnpj.IsValidCPFCNPJ(physicalPersonRequest.Cpf))
                {
                    return BadRequest(new Response()
                    {
                        Title = "Informe um CPF válido!"
                    });
                }

                var response = _personService.PostPhysicalPerson(physicalPersonRequest);

                if (response?.Registered == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Usuário cadastrado com sucesso!",
                        Status = 200,
                        Registered = response.Registered,
                        VipPlan = response.VipPlan
                    });
                }
                if (response?.Registered == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel cadastrar o usuário!",
                        Status = 400,
                        Registered = response.Registered,
                        VipPlan = response.VipPlan
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
                _logger.Error(ex, $"[PersonController] Exception in PostPhysicalPerson!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Registered = false
              });

        }

        /// <summary>
        /// Endpoint responsável por retornar endereços pelo id.
        /// </summary>
        /// <returns>Retorna endereço.</returns>
        [HttpGet("search-address-by-id/{idPerson}")]
        [ProducesResponseType(typeof(ResponsePerson), 200)]
        [ProducesResponseType(typeof(ResponsePerson), 404)]
        [ProducesResponseType(typeof(ResponsePerson), 500)]
        public ActionResult<AddressPersonReturn> GetAddressById([FromRoute]int idPerson)
        {
            try
            {
                var response = _personService.GetAddressById(idPerson);

                if (response?.IsReturned == true)
                {
                    return Ok(new ResponseAddressPerson()
                    {
                        AddressPerson = response.AddressPerson,
                        Status = 200,
                        IsReturned = true,
                        Title = "Endereço localizado com sucesso!"
                    });
                }
                if (response?.IsReturned == false)
                {
                    return BadRequest(new ResponseAddressPerson()
                    {
                        IsReturned = false,
                        Status = 400,
                        Title = "Não foi possivel localizar esse endereço!"
                    });
                }

                if (response?.IsReturned == false)
                {
                    return Ok(new ResponseAddressPerson()
                    {
                        IsReturned = false,
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[PersonController] Exception in GetAddressById!");
            }

            return StatusCode(500, new Response()
            {
                Status = 500,
                Title = "Erro interno no servidor!"
            });
        }


        [HttpPut("change-address-by-id")]
        [ProducesResponseType(typeof(AddressPersonReturn), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AddressPersonReturn), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AddressPersonReturn), StatusCodes.Status500InternalServerError)]
        public ActionResult<Response> PutAddressById(AddressPersonReturn addressPersonReturn)
        {
            try
            {
                var response = _personService.PutAddressById(addressPersonReturn);

                if (response?.Registered == true)
                {
                    return Ok(new Response()
                    {
                        Title = "Endereço alterado com sucesso!",
                        Status = 200
                    });
                }
                if (response?.Registered == false)
                {
                    return BadRequest(new Response()
                    {
                        Title = "Não foi possivel alterar endereço!",
                        Status = 400
                    });
                }
                else
                {
                    return BadRequest(new Response()
                    {
                        Status = 400
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[PersonController] Exception in PutAddressById!");
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
              new Response()
              {
                  Status = 500,
                  Title = "Erro interno no servidor!"
              });

        }
    }
}
