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
   public class PlanRepository : IPlanRepository
   {
       private readonly ILogger _logger;
       private readonly string _connectionString;

       public PlanRepository
           (
               ILogger logger,
               IConfiguration configuration
           )
       {
                _logger = logger;
                _connectionString = configuration.GetConnectionString("DatabaseConnectionString");
       }

       public ResponsePlan GetAllPlans()
       {
           try
           {
               using var connection = new SqlConnection(_connectionString);

               string sql = $@"SELECT 
                                        NAME
                                        DESCRIPTION
                                        PRECO
                                        FROM PLANS;";

               var result = connection.Query<dynamic>(@"SELECT * FROM PLANS");
               List<Plan> planListResponse = new List<Plan>();              

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
                       Plans = planListResponse,
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
           catch (Exception ex)
           {
                _logger.Error(ex, $"[PlanRepository] Exception in GetAllPlans!");
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
                      , UPDATED_AT
                    )
                    VALUES
                    (
                       '{idUsuario}'
                       , 1
                       , (SELECT CURRENT_TIMESTAMP) 
                     )";

               var result = connection.Execute(sql);

               if (result != 0)
               {
                   return new ResponseConfirmVip()
                   {
                       Title = "O usuário foi cadastrado na lista de Planos vip!",
                       IsReturned = true
                   };
               }
               else
               {
                   return new ResponseConfirmVip()
                   {
                       Title = "O já está cadastradp na lista de Planos Vip!",
                       IsReturned = false
                   };
               }
           }
           catch (Exception ex)
           {
                _logger.Error(ex, $"[PlanRepository] Exception in ConfirmVipPlan!");
           }

            return new ResponseConfirmVip()
            {
                IsReturned = false
            };
        }

        public bool AlreadyExists(int idUsuario)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                string sql = $@"  
                                 SELECT 
                                            ID_USERPLAN,
                                            ID_PERSON,
                                            ID_PLAN,
                                            UPDATED_AT
                                 FROM USERPLAN WHERE ID_PERSON = '{idUsuario}'";


                var result = connection.Query<List<string>>(sql);
                List<string> planListResponse = new List<string>();               

                if(result.AsList().Count == 0)
                {
                    return true;
                }if (result.AsList().Count > 0)
                {
                    return false;
                }else
                {
                return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[PlanRepository] Exception in AlreadyExists!");
            }
            return false;
        }

    }

}

 