using Application.Service.Interfaces;
using Domain.Model.Dao;
using Domain.Model.Request;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Application.Service
{
    public class LocalizationService : ILocalizationService
    {

        private readonly ILogger _logger;
        public LocalizationService(
            ILogger logger
            )
        {
            _logger = logger;
        }

        public async Task<ResponseLocalizationStates> GetClientApiState(string url, string getParam)
        {
            try
            {
                HttpClient client = new HttpClient { BaseAddress = new Uri(url) };
                var response = await client.GetAsync(getParam);
                var content = response.Content.ReadAsStringAsync();            
                var states = JsonConvert.DeserializeObject<List<State>>(content.Result);
                

                if (response != null)
                {
                    return new ResponseLocalizationStates()
                    {
                        States = states,
                        Status = 200,
                        IsReturned = true                        
                    };
                }
                else
                {
                    return new ResponseLocalizationStates()
                    {
                        IsReturned = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[LocalizationService] Exception in GetClientApiState!");
            }
            return null;

        }

        public async Task<ResponseLocalizationAddressByState> GetClientApiCity(string url, string request,string getParam)
        {
            try
            {

                var param = getParam.Replace("{request}", request); // para não no front end fazer no front end
                HttpClient client = new HttpClient { BaseAddress = new Uri(url) };
                var response = await client.GetAsync(param);
                var content = response.Content.ReadAsStringAsync();
                
                var citys = JsonConvert.DeserializeObject<List<AddressByState>>(content.Result);
                
                /*
                 * Primeira lista, apenas as cidades,
                 * Segunda lista informações completas;
                 * */

                 var filtered = new List<County>();
                if (citys.Count > 0)
                    foreach (var city in citys)
                    {                                          
                            var county = new County();
                                county.id = city.municipio.id;
                                county.nome = city.municipio.nome;
                            filtered.Add(county);
                         
                    }

                var filteredNoRepeat = filtered.GroupBy(county => county.id)    // remover cidades repetidas da lista 
                                        .Select(g => g.First())
                                        .ToList();



                if (response != null && citys.Count > 0)
                    {
                        return new ResponseLocalizationAddressByState()
                        {                            
                            CountyByState = filteredNoRepeat,
                            AddressByState = citys,
                            Status = 200,
                            IsReturned = true
                        };
                    }
                    else
                    {
                        return new ResponseLocalizationAddressByState()
                        {
                            IsReturned = false
                        };
                    }
                }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[LocalizationService] Exception in GetClientApiCity!");
            }
            return null;

        }
    }
}
