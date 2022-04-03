using Application.Service.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Infrastructure.Repository.Interfaces;
using Serilog;
using System;

namespace Application.Service
{
    public class InvestmentService : IInvestmentService
    {
        private readonly ILogger _logger;
        private readonly IInvestmentRepository _investmentRepository;

        public InvestmentService(
            ILogger logger,
            IInvestmentRepository investmentRepository
            )
        {
            _logger = logger;
            _investmentRepository = investmentRepository;
        }

        public ResponsePerson SearchPersonByRegistrationDate(FilterDateRequest filterDateRequest)
        {
            try
            {
                var investmentResponse = _investmentRepository.SearchPersonByRegistrationDate(filterDateRequest);
                return investmentResponse;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[InvestmentService] Exception in SearchPersonByRegistrationDate!");
            }
            return null;
        }

        public ResponsePerson SearchForIncome(IncomeFilterRequest incomeFilterRequest)
        {
            try
            {
                var planResponse = _investmentRepository.SearchForIncome(incomeFilterRequest);
                return planResponse;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"[InvestmentService] Exception in SearchForIncome!");
            }
            return null;
        }
    }
}

        