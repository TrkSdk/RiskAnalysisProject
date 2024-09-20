using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RiskAnalysis.Models.DTO
{
    public class ClientRequestDTO  //Müşteriden servisle gelen finansman talebi..alınıp risk değeri hesaplandıktan sonra kabul veya red edilecek
    {
        [Required]
        public int PartnerId { get; set; }  //FK
        public virtual Partners Partner { get; set; }  // Navigation property for 1-N relationship


        [Required]
        public int BusinessId { get; set; }  //FK
        public virtual Businesses Business { get; set; }  // 1-1 ilişki için navigation property


        [Required(AllowEmptyStrings = false)]
        public string ContractName { get; set; }  //Kontratın adı

        
        [Required]
        public double ContractAmount { get; set; }  //Kontrat tutarı

        
        [Required]
        public DateTime StartDate { get; set; } //Kontratın başlangıç tarihi


        [Required]
        public DateTime EndDate { get; set; } //Kontratın bitiş tarihi


        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi


    }
}
