﻿using Application.Service.Interfaces;
using Domain.Model.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TargetInvestimento.Controllers
{
    [Route("localization")]
    [ApiController]    
    public class LocalizationController : ControllerBase
    {
        private readonly ILocalizationService _localizationService;


        public LocalizationController(
            ILocalizationService localizationService
            )
        {
            _localizationService = localizationService;
        }

        /// <summary>
        /// Endpoint responsável por retornar lista de Cidades.
        /// </summary>
        /// <returns>Retorna lista de Cidades.</returns>
        [HttpGet("states")]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(Response), 404)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> GetAllStateBrazil()
        {
            try
            {
                var response = await _localizationService.GetClientApiState("https://servicodados.ibge.gov.br/api/v1/", "localidades/estados/");

                if (response?.IsReturned == true)
                {
                    return Ok(new ResponseLocalization()
                    {
                        States = response.States,
                        Status = 200,
                        IsReturned = true,
                        Title = "Lista de Estados localizada com sucesso!"
                    });
                }
                if (response?.IsReturned == false)
                {
                    return BadRequest(new ResponseLocalization()
                    {
                        IsReturned = false,
                        Status = 400,
                        Title = "Não foi possivel localizar a lista de estados!"
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
                Console.WriteLine("[LocalizationController] Exception in GetAllStateBrazil!");
            }

            return StatusCode(500, new Response()
            {
                Status = 500,
                Title = "Erro interno no servidor!"
            });
        }


        /// <summary>
        /// Endpoint responsável por retornar lista de Cidades.
        /// </summary>
        /// <returns>Retorna lista de Cidades.</returns>
        [HttpGet("city")]
        [ProducesResponseType(typeof(Response), 200)]
        [ProducesResponseType(typeof(Response), 404)]
        [ProducesResponseType(typeof(Response), 500)]
        public async Task<IActionResult> GetAllCity(string request)
        {
            try
            {
                //var param = "localidades/estados/{request}/distritos/";
                //param = param.Replace("{request}", request.ToString());
               

                var response = await _localizationService.GetClientApiCity("https://servicodados.ibge.gov.br/api/v1/", request,"localidades/estados/{request}/distritos/");

                if (response?.IsReturned == true)
                {
                    return Ok(new ResponseLocalizationFilter()
                    {
                        AddressByState = response.AddressByState,
                        Status = 200,
                        IsReturned = true,
                        Title = "Lista de Endereços por estado localizada com sucesso!"
                    });
                }
                if (response?.IsReturned == false)
                {
                    return BadRequest(new ResponseLocalization()
                    {
                        IsReturned = false,
                        Status = 400,
                        Title = "Não foi possivel localizar a lista de endereços!"
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
                Console.WriteLine("[LocalizationController] Exception in GetAllCity!");
            }

            return StatusCode(500, new Response()
            {
                Status = 500,
                Title = "Erro interno no servidor!"
            });
        }
    }
}
