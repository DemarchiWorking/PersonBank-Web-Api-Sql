using Application.Service.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Serilog;
using System;

namespace Application.Service
{
    public class KpiService : IKpiService
    {
        private readonly ILogger _logger;
        private readonly IKpiRepository _kpiRepository;

        public KpiService(
            ILogger logger,
            IKpiRepository kpiRepository
            )
        {
            _logger = logger;
            _kpiRepository = kpiRepository;
        }

        public ResponsePlansPerson GetInformationPlanVip()
        {
            try
            {
                var kpiResponse = _kpiRepository.GetInformationPlanVip();
                return kpiResponse;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[KpiService] Exception in GetInformationPlanVip!");
            }
            return null;
        }
    }
}

        