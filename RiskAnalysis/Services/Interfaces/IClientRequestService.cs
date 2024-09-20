using RiskAnalysis.Models;
using RiskAnalysis.Models.DTO;

namespace RiskAnalysis.Services.Interfaces
{
    public interface IClientRequestService
    {
        Task<Contracts> ProcessClientRequestDTOAsync(ClientRequestDTO clientRequestDTO);
        int CalculateRisk(int PartnerId, int BusinessId, double Amount);
    }
}