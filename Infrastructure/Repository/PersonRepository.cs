using Dapper;
using Domain.Model.Request;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly string _connectionString;
        //private readonly IDbConnection _dbConnection;

        public PersonRepository
            (
               // IDbConnection dbConnection,
                IConfiguration configuration
            )
        {
               // _dbConnection = dbConnection;
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
                     )
                     VALUES
                     (
                        '{physicalPersonRequest.FullName}'
                        , ''
                        , '{physicalPersonRequest.Cpf}'
                        , '{physicalPersonRequest.Address.Street_Address}'
                        , '{physicalPersonRequest.Address.Suburb}'
                        , '{physicalPersonRequest.Address.ZipCode}'
                        , '{physicalPersonRequest.Address.City}'
                        , '{physicalPersonRequest.Address.State}'
                        , '{physicalPersonRequest.Address.AdditionalInformation}'
                        , {physicalPersonRequest.MonthlyIncome}
                      )";
                //RETURNING ID_PERSON

                var result = connection.Execute(sql);
                
                //int id = param.Get<int>("ID_PERSON");

                Console.WriteLine(result);
                Console.WriteLine(result);

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

                /*
                if (dbQuery > 0)
                    return new Response()
                    {
                        Registered = true,
                        VipPlan = vipPlan
                    };
                else
                    return new Response()
                    {
                        Registered = false
                    };*/
            }
            catch (Exception e)
            {
                Console.WriteLine("[PersonRepository] Exception in PostPhysicalPerson!");
            }

            return null;
        }
    }
}