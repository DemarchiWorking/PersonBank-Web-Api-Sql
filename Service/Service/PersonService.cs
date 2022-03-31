using Application.Service.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Application.Service
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(
            IPersonRepository personRepository
            )
        {
            _personRepository = personRepository;
        }
       

        public Response PostPhysicalPerson(PhysicalPersonRequest physicalPersonRequest)
        {
            try
            {
                bool vipPlan = false;
                if (physicalPersonRequest.MonthlyIncome >= 6000)
                {
                    vipPlan = true;
                }

                var personResponse = _personRepository.PostPhysicalPerson(physicalPersonRequest, vipPlan);
                return personResponse;

            }
            catch (Exception e)
            {
                Console.WriteLine("[PersonController] Exception in PostPhysicalPerson!");
            }
            return null;
        }


    }
}
