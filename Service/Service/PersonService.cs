using Application.Service.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using System;

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
                bool pipPlan = false;
                if (physicalPersonRequest.MonthlyIncome >= 6000)
                {
                    pipPlan = true;
                }

                var personResponse = _personRepository.PostPhysicalPerson(physicalPersonRequest, pipPlan);
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
