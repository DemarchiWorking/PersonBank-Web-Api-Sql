using Application.Service.Interfaces;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using System;

namespace Application.Service
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;

        public PlanService(
            IPlanRepository planRepository
            )
        {
            _planRepository = planRepository;
        }


        public ResponsePlan GetAllPlans()
        {
            try
            {
                var planResponse = _planRepository.GetAllPlans();
                return planResponse;

            }
            catch (Exception e)
            {
                Console.WriteLine("[PlanService] Exception in GetAllPlans!");
            }
            return null;
        }

        public ResponseConfirmVip ConfirmVipPlan(int idUsuario)
        {
            try
            {
                var planResponse = _planRepository.ConfirmVipPlan(idUsuario);
                return planResponse;

            }
            catch (Exception e)
            {
                Console.WriteLine("[PlanService] Exception in ConfirmVipPlan!");
            }
            return null;
        }
    }
}

        