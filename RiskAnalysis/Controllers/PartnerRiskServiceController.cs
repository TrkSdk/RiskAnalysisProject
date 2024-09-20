using Microsoft.AspNetCore.Mvc;
using RiskAnalysis.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class PartnerRiskServiceController : ControllerBase
{
    private readonly IPartnerRiskService _partnerRiskService;

    public PartnerRiskServiceController(IPartnerRiskService partnerRiskService)
    {
        _partnerRiskService = partnerRiskService;
    }

    [HttpGet("{partnerId}/riskfactor")]
    public async Task<IActionResult> GetRiskFactor(int partnerId)
    {
        try
        {
            var riskFactor = await _partnerRiskService.CalculatePartnerRiskFactorAsync(partnerId);
            return Ok(new { PartnerId = partnerId, RiskFactor = riskFactor });
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
















/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RiskAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerRiskServiceController : ControllerBase
    {
    }
}
*/