namespace RiskAnalysis.Services.Interfaces
{
    public interface IBusinessRiskService
    {
        Task<int> CalculateBusinessRiskFactorAsync(int businessId);

    }
}
