using Application.Service.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TargetInvestimento.Controllers
{
    [Route("person")]
    [ApiController]    
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        //private readonly ILogger _logger;


        public PersonController(
            IPersonService personService //,
          //  ILogger logger
            )
        {
            _personService = personService;
            //_logger = logger;
        }

        [HttpPost("create")]
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
                        Registered = response.Registered,
                        VipPlan = response.VipPlan
                    });
                }
                if (response?.Registered == false)
                {
                    return BadRequest(new Response()
                    {
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
