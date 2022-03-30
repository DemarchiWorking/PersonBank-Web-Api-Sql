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
        public async Task<ResponseLocalization> GetClientApi(string url, string getParam)
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


    }
}
