using Application.Service.Interfaces;
using Domain.Model.Dao;
using Domain.Model.Request;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Application.Service
{
    public class LocalizationService : ILocalizationService
    {
        public LocalizationService()
        {
           
        }

        public async Task<ResponseLocalization> GetClientApiState(string url, string getParam)
        {
            try
            {
                HttpClient client = new HttpClient { BaseAddress = new Uri(url) };
                var response = await client.GetAsync(getParam);
                var content = response.Content.ReadAsStringAsync();
                Console.WriteLine(content.ToString());
                Console.WriteLine(content.Result.ToString());
                Console.WriteLine(content.Result.ToString());
                Console.WriteLine(content.Result.ToString());
                var states = JsonConvert.DeserializeObject<List<State>>(content.Result);
                //Console.WriteLine(states);

                if (response != null)
                {
                    return new ResponseLocalization()
                    {
                        States = states,
                        Status = 200,
                        IsReturned = true                        
                    };
                }
                else
                {
                    return new ResponseLocalization()
                    {
                        IsReturned = false
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[LocalizationService] Exception in GetClientApi!");
            }
            return null;

        }

        public async Task<ResponseLocalizationFilter> GetClientApiCity(string url, string request,string getParam)
        {
            try
            {

                var param = getParam.Replace("{request}", request); // para não no front end fazer no front end
                HttpClient client = new HttpClient { BaseAddress = new Uri(url) };
                var response = await client.GetAsync(param);
                var content = response.Content.ReadAsStringAsync();
                Console.WriteLine(content.ToString());
                Console.WriteLine(content.Result.ToString());
                Console.WriteLine(content.Result.ToString());
                Console.WriteLine(content.Result.ToString());

                
                var citys = JsonConvert.DeserializeObject<List<AddressByState>>(content.Result);
                
                /*var filtered = new List<AddressByState>();
                if (citys.Count > 0)
                    foreach (var city in citys)
                    {

                        if (Convert.ToString(city.municipio.microrregiao.mesorregiao.UF.id) == request)
                        {
                            filtered.Add(city);
                        } 
                    }
                    Console.WriteLine(filtered.ToString());
                    Console.WriteLine(filtered.ToString());                        
                */
                    if (response != null && citys.Count > 0)
                    {
                        return new ResponseLocalizationFilter()
                        {
                            AddressByState = citys,
                            Status = 200,
                            IsReturned = true
                        };
                    }
                    else
                    {
                        return new ResponseLocalizationFilter()
                        {
                            IsReturned = false
                        };
                    }
                }
            catch (Exception ex)
            {
                Console.WriteLine("[LocalizationService] Exception in GetClientApi!");
            }
            return null;

        }
    }
}
