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
    public class KpiRepository : IKpiRepository
    {
        private readonly ILogger _logger;
        private readonly string _connectionString;

        public KpiRepository
            (
                ILogger logger,
                IConfiguration configuration
            )
        {
                _logger = logger;
                _connectionString = configuration.GetConnectionString("DatabaseConnectionString");
        }

        public ResponsePlansPerson GetInformationPlanVip()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                string sql = $@"SELECT
                                        PP.ID_PERSON,
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
		                                CREATED_AT,
										UP.ID_PLAN,
                                        UP.UPDATED_AT,
										PL.NAME
                                FROM PHYSICAL_PERSON PP
                                LEFT JOIN USERPLAN UP
                                ON PP.ID_PERSON = UP.ID_PERSON
                                LEFT JOIN PLANS PL
                                ON UP.ID_PLAN = PL.ID_PLAN;";

                var result = connection.Query<dynamic>(sql);
                List<PlanPhysicalPersonReturn> personListResponse = new List<PlanPhysicalPersonReturn>();
                int vipPlansNumber = 0;
                int NumberPersons = 0;

                if (result != null) {

                    foreach (var item in result)
                    {
                        personListResponse.Add(new PlanPhysicalPersonReturn
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
                            CreatedAt = item.CREATED_AT,
                            LastPlanChange = item.UPDATED_AT,
                            IdPlan = item.ID_PLAN,
                            Plan = item.NAME
                        });

                    }
                    foreach (PlanPhysicalPersonReturn item in (personListResponse))
                    {
                        NumberPersons++;
                        if (item.IdPlan == 1)
                        {
                            vipPlansNumber++;
                        }

                    }

                    return new ResponsePlansPerson()
                    {
                        PersonsPlan = personListResponse,
                        VipPlansNumber = vipPlansNumber,
                        NumberPersons = NumberPersons,
                        IsReturned = true
                    };
                }
                else
                {
                    return new ResponsePlansPerson()
                    {
                        IsReturned = false
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[KpiRepository] Exception in GetInformationPlanVip!");

            }

            return null;
        }

    }
}
