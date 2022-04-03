using Dapper;
using Domain.Model.Dao;
using Domain.Model.Request;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repository
{
    public class PersonRepository : IPersonRepository
    {

        private readonly ILogger _logger;
        private readonly string _connectionString;

        public PersonRepository
            (
                ILogger logger,
                IConfiguration configuration
            )
        {
                _logger = logger;
                _connectionString = configuration.GetConnectionString("DatabaseConnectionString");
        }

        public Response PostPhysicalPerson(PhysicalPersonRequest physicalPersonRequest, bool vipPlan)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                string sql = $@"
                INSERT INTO PHYSICAL_PERSON
                    (
                       FULL_NAME
                       , BIRTH_DATE
                       , CPF
                       , STREET_ADDRESS
                       , SUBURB
                       , ZIP_CODE
                       , CITY
                       , STATE
                       , ADDITIONAL_INFORMATION
                       , MONTHLY_INCOME
                       , CREATED_AT
                     )
                     VALUES
                     (
                        '{physicalPersonRequest.FullName}'
                        , (SELECT CONVERT(DATETIME, CONVERT(DATETIMEOFFSET,'{physicalPersonRequest.BirthDate}')))
                        , '{physicalPersonRequest.Cpf}'
                        , '{physicalPersonRequest.Address.Street_Address}'
                        , '{physicalPersonRequest.Address.Suburb}'
                        , '{physicalPersonRequest.Address.ZipCode}'
                        , '{physicalPersonRequest.Address.City}'
                        , '{physicalPersonRequest.Address.State}'
                        , '{physicalPersonRequest.Address.AdditionalInformation}'
                        , {physicalPersonRequest.MonthlyIncome}
                        , (SELECT CURRENT_TIMESTAMP)  
                      )";

                //RETURNING ID_PERSON

                var result = connection.Execute(sql);
                
                //int id = param.Get<int>("ID_PERSON");
            
                if(result != 0) { 
                    return new Response()
                    {
                        Registered = true,
                        VipPlan = vipPlan
                    };
                }
                else
                {
                    return new Response()
                    {
                        Registered = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[PersonRepository] Exception in PostPhysicalPerson!");

            }

            return null;
        }


        public ResponseAddressPerson GetAddressById(int idPerson)
        {
            try
            {
               using var connection = new SqlConnection(_connectionString);

               string sql = $@"SELECT
                                        ID_PERSON,
		                                STREET_ADDRESS,
		                                SUBURB,
		                                ZIP_CODE,
		                                CITY,
		                                STATE,
		                                ADDITIONAL_INFORMATION
                               FROM PHYSICAL_PERSON WHERE ID_PERSON = '{idPerson}';";

                var result = connection.Query<AddressPersonReturn>(sql);

                if (result != null)
                {
                    return new ResponseAddressPerson()
                    {
                         AddressPerson = result,
                         IsReturned = true
                       };
                    }
                    else
                    {
                        return new ResponseAddressPerson()
                        {
                            IsReturned = false
                        };
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"[PersonRepository] Exception in GetAddressById!");
                }

                return null;
            }


        public Response PutAddressById(AddressPersonReturn addressPersonReturn)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                string sql = $@"                
                                UPDATE PHYSICAL_PERSON
						                                SET STREET_ADDRESS = '{addressPersonReturn.Street_Address}',
						                                SUBURB = '{addressPersonReturn.Suburb}',
						                                ZIP_CODE = '{addressPersonReturn.Zip_Code}',
						                                CITY = '{addressPersonReturn.City}',
						                                STATE = '{addressPersonReturn.State}',
						                                ADDITIONAL_INFORMATION = '{addressPersonReturn.Additional_Information}'
		                                WHERE ID_PERSON= '{addressPersonReturn.Id_Person}';";

                var result = connection.Execute(sql);

                if (result != 0)
                {
                    return new Response()
                    {
                        Registered = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        Registered = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[PersonRepository] Exception in PutAddressById!");
            }

            return new Response()
            {
                Registered = false
            };
            
        }
    }
}