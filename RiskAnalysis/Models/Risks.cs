using System.ComponentModel.DataAnnotations;


namespace RiskAnalysis.Models
{
    public class Risks
    {

        public int RiskId { get; set; }  // Primary key

        [Required]
        public int RiskScore { get; set; }  // Hesaplanacak olan Risk Skoru
       
        [Required]
        public int RiskEstimationSuccess { get; set; }  // Risk başarı oranı

        public virtual List<Contracts> ContractsList { get; set; }
    }
}
