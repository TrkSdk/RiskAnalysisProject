using RiskAnalysis.Models;
using RiskAnalysis.Services.Interfaces;

public class PartnerRiskService : IPartnerRiskService
{
    private readonly AppDbContext _context;

    public PartnerRiskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CalculatePartnerRiskFactorAsync(int partnerId)
    {
        // DB'den Partner'in risk skorunu bul
        var partner = await _context.Partners.FindAsync(partnerId);

        if (partner == null)
        {
            throw new ArgumentException($" {partnerId} ID nolu Partner bulunamadı.");
        }

        int riskFactor = partner.RiskFactor;

        return riskFactor;
    }
}
