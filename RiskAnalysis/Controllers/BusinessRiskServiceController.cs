using Microsoft.AspNetCore.Mvc;
using RiskAnalysis.Services.Interfaces;

namespace RiskAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessRiskServiceController : ControllerBase
    {
        private readonly IBusinessRiskService _businessRiskService;

        public BusinessRiskServiceController(IBusinessRiskService businessRiskService)
        {
            _businessRiskService = businessRiskService;
        }

        [HttpGet("{businessId}/riskfactor")]
        public async Task<IActionResult> GetRiskFactor(int businessId)
        {
            try
            {
                var riskFactor = await _businessRiskService.CalculateBusinessRiskFactorAsync(businessId);
                return Ok(new { BusinessId = businessId, RiskFactor = riskFactor });
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
