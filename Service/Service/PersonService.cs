using Application.Service.Interfaces;
using Domain.Model.Dao;
using Domain.Model.Request;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Application.Service
{
    public class PersonService : IPersonService
    {
        private readonly ILogger _logger;
        private readonly IPersonRepository _personRepository;

        public PersonService(
            ILogger logger,
            IPersonRepository personRepository
            )
        {
            _logger = logger;
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
            catch (Exception ex)
            {
                _logger.Error(ex, $"[PersonService] Exception in PostPhysicalPerson!");
            }
            return null;
        }

        public ResponseAddressPerson GetAddressById(int idPerson)
        {
            try
            {
                var personResponse = _personRepository.GetAddressById(idPerson);
                return personResponse;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[PersonService] Exception in GetAddressById!");
            }
            return null;
        }

        public Response PutAddressById(AddressPersonReturn addressPersonReturn)
        {
            try
            {

                var personResponse = _personRepository.PutAddressById(addressPersonReturn);
                return personResponse;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[PersonService] Exception in PutAddressById!");
            }
            return null;
        }

        
    }
}
