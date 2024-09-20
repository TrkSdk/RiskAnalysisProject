using System.ComponentModel.DataAnnotations;

namespace RiskAnalysis.Models.DTO
{
    public class ClientResponseDTO
    {
        [Required]
        public string PartnerName { get; set; }  //FK


        [Required(AllowEmptyStrings = false)]
        public string ContractName { get; set; }  //Kontratın adı


        [Required]
        public double ContractAmount { get; set; }  //Kontrat tutarı


        [Required]
        public bool IsAccepted { get; set; }  //Kontrat onaylandı mı


        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi

    }
}
