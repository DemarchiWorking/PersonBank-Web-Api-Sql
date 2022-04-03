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
using System.Globalization;

namespace Infrastructure.Repository
{
    public class InvestmentRepository : IInvestmentRepository
    {

        private readonly ILogger _logger;
        private readonly string _connectionString;

        public InvestmentRepository
            (
                ILogger logger,
                IConfiguration configuration

            )
        {
                _logger = logger;
                _connectionString = configuration.GetConnectionString("DatabaseConnectionString");
        }

        public ResponsePerson SearchPersonByRegistrationDate(FilterDateRequest filterDateRequest)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var initialDate = filterDateRequest.InitialDate.ToString("MM-dd-yyyy HH:mm:ss");
                var finallDate = filterDateRequest.FinalDate.ToString("MM-dd-yyyy HH:mm:ss");

                string sql = $@"SELECT
                                        ID_PERSON,
		                                FULL_NAME,
		                                BIRTH_DATE,
		                                CPF,
		                                STREET_ADDRESS,
		                                SUBURB,
		                                ZIP_CODE,
		                                CITY,
		                                STATE,
		                                ADDITIONAL_INFORMATION,
		                                MONTHLY_INCOME,
		                                CREATED_AT
                                FROM PHYSICAL_PERSON WHERE CREATED_AT BETWEEN (SELECT CONVERT(DATETIME2, '{initialDate}'))
                                AND  (SELECT CONVERT(DATETIME2, '{finallDate}'))";

                var result = connection.Query<dynamic>(sql);
                List<PhysicalPersonReturn> personListResponse = new List<PhysicalPersonReturn>();

                

                if(result != null) {

                    foreach (var item in result)
                    {
                        personListResponse.Add(new PhysicalPersonReturn
                        {
                            IdPerson = item.ID_PERSON,
                            FullName = item.FULL_NAME,
                            BirthDate = item.BIRTH_DATE,
                            Cpf = item.CPF,
                            StreetAddress = item.STREET_ADDRESS,
                            Suburb = item.SUBURB,
                            ZipCode = item.ZIP_CODE,
                            City = item.CITY,
                            State = item.STATE,
                            AdditionalInformation = item.ADDITIONAL_INFORMATION,
                            MonthlyIncome = (item.MONTHLY_INCOME) == "" ? null: Convert.ToDouble(item.MONTHLY_INCOME),
                            CreatedAt = item.CREATED_AT
                        });

                    }
                    return new ResponsePerson()
                    {
                        Persons = personListResponse,
                        IsReturned = true
                    };
                }
                else
                {
                    return new ResponsePerson()
                    {
                        IsReturned = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[InvestmentRepository] Exception in SearchPersonByRegistrationDate!");
            }

            return null;
        }

        public ResponsePerson SearchForIncome(IncomeFilterRequest incomeFilterRequest)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                string sql = $@"SELECT
                                        ID_PERSON,
		                                FULL_NAME,
		                                BIRTH_DATE,
		                                CPF,
		                                STREET_ADDRESS,
		                                SUBURB,
		                                ZIP_CODE,
		                                CITY,
		                                STATE,
		                                ADDITIONAL_INFORMATION,
		                                MONTHLY_INCOME,
		                                CREATED_AT
                               FROM PHYSICAL_PERSON WHERE MONTHLY_INCOME >= '{incomeFilterRequest.monthlyIncomeFilter}';";


                var result = connection.Query<dynamic>(sql);
                List<PhysicalPersonReturn> personListResponse = new List<PhysicalPersonReturn>();

                if (result != null)
                {

                    foreach (var item in result)
                    {
                        personListResponse.Add(new PhysicalPersonReturn
                        {
                            IdPerson = item.ID_PERSON,
                            FullName = item.FULL_NAME,
                            BirthDate = item.BIRTH_DATE,
                            Cpf = item.CPF,
                            StreetAddress = item.STREET_ADDRESS,
                            Suburb = item.SUBURB,
                            ZipCode = item.ZIP_CODE,
                            City = item.CITY,
                            State = item.STATE,
                            AdditionalInformation = item.ADDITIONAL_INFORMATION,
                            MonthlyIncome = Convert.ToDouble(item.MONTHLY_INCOME),
                            CreatedAt = item.CREATED_AT
                        });
                    }

                    return new ResponsePerson()
                    {
                        Persons = personListResponse,
                        IsReturned = true
                    };
                }                
                else
                {
                    return new ResponsePerson()
                    {
                        IsReturned = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[InvestmentRepository] Exception in SearchForIncome!");
            }

            return null;
        }
    }
}
