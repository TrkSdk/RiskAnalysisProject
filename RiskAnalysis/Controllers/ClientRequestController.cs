using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskAnalysis.Models.DTO;
using RiskAnalysis.Services;

namespace RiskAnalysis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientRequestController : ControllerBase
    {
        private readonly ClientRequestService _riskService;

        // Inject the RiskService through constructor dependency injection
        public ClientRequestController(ClientRequestService riskService)
        {
            _riskService = riskService;
        }

        // Define an endpoint that handles client requests
        [HttpPost("evaluate")]
        public IActionResult EvaluateRisk([FromBody] ClientRequestDTO clientRequestDTO)
        {
            // Validate the input (optional)
            if (clientRequestDTO == null)
            {
                return BadRequest("Invalid request data.");
            }

            // Call the service to evaluate the risk
            var result = _riskService.CalculateRisk(clientRequestDTO.PartnerId, clientRequestDTO.BusinessId, clientRequestDTO.ContractAmount);

            // Return the result as "Yes" or "No"
            return Ok(new { Result = result });
        }
    }
}


