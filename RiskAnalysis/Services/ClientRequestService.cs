using RiskAnalysis.Models;
using RiskAnalysis.Models.DTO;
using RiskAnalysis.Services.Interfaces;

namespace RiskAnalysis.Services
{
    public class ClientRequestService : IClientRequestService
    {
        private readonly AppDbContext _context;

        public ClientRequestService(AppDbContext context)
        {
            _context = context;
        }
        //Gelen talebe bakıyor ve riskini hesaplıyor, belli bşr yüzdenin üzerindeyse kabul ediyor, altındays reddediyor. her durumda pertnere cevap dönüyor
        public async Task<Contracts> ProcessClientRequestDTOAsync(ClientRequestDTO clntRequestDTO)
        {
            var partner = await _context.Partners.FindAsync(clntRequestDTO.PartnerId);
            if (partner == null)
            {
                throw new Exception("Partner not found");
            }

            // Risk calculation
            int riskScore = CalculateRisk(clntRequestDTO.PartnerId, clntRequestDTO.BusinessId, clntRequestDTO.ContractAmount);

            var risk = new Risks
            {
                RiskScore = riskScore
            };

            _context.Risks.Add(risk);
            await _context.SaveChangesAsync();
            int newRiskId = risk.RiskId;

            var contract = new Contracts
            {
                Amount = clntRequestDTO.ContractAmount,
                BusinessId = clntRequestDTO.BusinessId,
                PartnerId = clntRequestDTO.PartnerId,
                ContractName = clntRequestDTO.ContractName,
                StartDate = clntRequestDTO.StartDate,
                EndDate = clntRequestDTO.EndDate,
                RiskId = newRiskId //yeni oluşturulan risk kaydının ID'si
            };

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            return contract;
        }

        // Simple risk calculation based on keywords
        public int CalculateRisk(int PartnerId, int BusinessId, double Amount)
        {
            int PartnerRisk, BusinessRisk, RiskFactor = 0;

            var partnerRiskService = new PartnerRiskService();
            var businessRiskService = new BusinessRiskService();

            /** Miktara göre artan risk basamakları **/
            double VeryLowRiskLimit = 1000000;
            double LowRiskLimit = 5000000;
            double MediumRiskLimit = 10000000;
            double HighRiskLimit = 25000000;
            double VeryHighRiskLimit = 50000000;

            /** Partnerin risk değerini bul **/
            PartnerRisk = partnerRiskService.CalculatePartnerRiskFactorAsync(PartnerId);

            /** Business'in risk değerini bul **/
            BusinessRisk = businessRiskService.CalculateBusinessRiskFactorAsync(BusinessId);

            RiskFactor = (PartnerRisk + BusinessRisk) / 2;

            if (Amount <= VeryLowRiskLimit) RiskFactor = Convert.ToInt32(RiskFactor * 1.1);
            else if (Amount <= LowRiskLimit) RiskFactor = Convert.ToInt32(RiskFactor * 1.15);
            else if (Amount <= MediumRiskLimit) RiskFactor = Convert.ToInt32(RiskFactor * 1.20);
            else if (Amount <= HighRiskLimit) RiskFactor = Convert.ToInt32(RiskFactor * 1.25);
            else if (Amount <= VeryHighRiskLimit) RiskFactor = Convert.ToInt32(RiskFactor * 1.30);

            return RiskFactor;
        }
    }
}


