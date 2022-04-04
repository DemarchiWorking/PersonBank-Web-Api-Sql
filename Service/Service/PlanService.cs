using Application.Service.Interfaces;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Serilog;
using System;

namespace Application.Service
{

  public class PlanService : IPlanService
  {
        private readonly ILogger _logger;
        private readonly IPlanRepository _planRepository;

      public PlanService(
          ILogger logger,
          IPlanRepository planRepository
          )
      {
            _logger = logger;
            _planRepository = planRepository;
      }


      public ResponsePlan GetAllPlans()
      {
          try
          {
              var planResponse = _planRepository.GetAllPlans();
              return planResponse;

          }
          catch (Exception ex)
          {
                _logger.Error(ex, $"[PlanService] Exception in GetAllPlans!");
          }
          return null;
      }

      public ResponseConfirmVip ConfirmVipPlan(int idUsuario)
      {
          try
          {

              var alreadyExists = _planRepository.AlreadyExists(idUsuario); // verifica se já esta cadastrado em um plano
              if(alreadyExists == true)
                {
                    return new ResponseConfirmVip()
                    {
                        Title = "O usuário já está cadastrado no Plano Vip",
                        IsReturned = false
                    };
                }
               
              var planResponse = _planRepository.ConfirmVipPlan(idUsuario);
              return planResponse;

          }
          catch (Exception ex)
          {
                _logger.Error(ex, $"[PlanService] Exception in ConfirmVipPlan!");
          }
          return null;
      }       
  }
}

