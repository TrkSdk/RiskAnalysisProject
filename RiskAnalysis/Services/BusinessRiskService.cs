using RiskAnalysis.Models;
using RiskAnalysis.Services.Interfaces;

public class BusinessRiskService : IBusinessRiskService
{
    private readonly AppDbContext _context;

    public BusinessRiskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CalculateBusinessRiskFactorAsync(int businessId)
    {
        // DB'den Business'in risk skorunu bul
        var business = await _context.Partners.FindAsync(businessId);

        if (business == null)
        {
            throw new ArgumentException($" {businessId} ID nolu Business bulunamadı.");
        }

        int riskFactor = business.RiskFactor;

        return riskFactor;
    }
}
