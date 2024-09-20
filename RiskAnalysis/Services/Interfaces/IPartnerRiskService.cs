namespace RiskAnalysis.Services.Interfaces
{
    public interface IPartnerRiskService
    {
        Task<int> CalculatePartnerRiskFactorAsync(int partnerId);
    }
}
