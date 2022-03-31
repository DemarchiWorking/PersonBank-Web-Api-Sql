using Dapper;
using Domain.Model.Dao;
using Domain.Model.Request;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repository
{
    public class PlanRepository : IPlanRepository
    {
        private readonly string _connectionString;

        public PlanRepository
            (
                IConfiguration configuration
            )
        {
                _connectionString = configuration.GetConnectionString("DatabaseConnectionString");
        }

        public ResponsePlan GetAllPlans()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                string sql = $@"SELECT * FROM PLANS;";

                //var result = connection.Execute(sql);
                var result = connection.Query<dynamic>(@"SELECT * FROM PLANS");
                List<Plan> planListResponse = new List<Plan>();

                Console.WriteLine(result);
                //Console.WriteLine(result[0].toString());

                if(result != null) {

                    foreach (var item in result)
                    {
                        planListResponse.Add(new Plan
                        {
                            NAME = item.NAME,
                            DESCRIPTION = item.DESCRIPTION,
                            PRECO = item.PRECO
                        });

                    }
                    return new ResponsePlan()
                    {
                        Title = "Lista de planos",
                        Plans = planListResponse,
                        Status = 200,
                        IsReturned = true
                    };
                }
                else
                {
                    return new ResponsePlan()
                    {
                        IsReturned = false
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[PlanRepository] Exception in GetAllPlans!");
            }

            return null;
        }

        public ResponseConfirmVip ConfirmVipPlan(int idUsuario)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                string sql = $@"
                INSERT INTO USERPLAN
                    (
                       ID_PERSON
                       , ID_PLAN
                     )
                     VALUES
                     (
                        '{idUsuario}'
                        , 1
                      )";

                var result = connection.Execute(sql);


                Console.WriteLine(result);
                Console.WriteLine(result);

                if (result != 0)
                {
                    return new ResponseConfirmVip()
                    {
                        IsReturned = true
                    };
                }
                else
                {
                    return new ResponseConfirmVip()
                    {
                        IsReturned = false
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[PlanRepository] Exception in ConfirmVipPlan!");
            }

            return null;
        }
    }
}
