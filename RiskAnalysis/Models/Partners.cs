using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiskAnalysis.Models
{
    public class Partners // Çoğul isim
    {
        public int PartnerId { get; set; }  // Primary key

        [ForeignKey("SectorId")]
        public int SectorId { get; set; }  // Foreign key. Sectors tablosuna bağlı
        public virtual Sectors Sector { get; set; }  // Navigation property for 1-1 relationship (Tekil "Sector")

        
        [Required(AllowEmptyStrings = false)]
        public string PartnerName { get; set; }


        [Required(AllowEmptyStrings = false)]
        public string ContactPerson { get; set; }


        [ForeignKey("CityId")]
        public int CityId { get; set; } //FK Cities tablosuna bağlıyor
        public virtual Cities City { get; set; }  // Navigation property, burada "City" olarak değiştirdik


        [Required(AllowEmptyStrings = false)]
        public string ContactEMail { get; set; }

     
        [Range(0, 100, ErrorMessage = "0 ile 100 arasında bir değer giriniz.")]
        public int RiskFactor { get; set; }  // Varsa firmanın geçmiş işlemlerden gelen belirlenmiş risk oranı
        
       
        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi

        public virtual List<Contracts> ContractsList { get; set; }  // 1-N ilişki, Contracts için "ContractsList" kullanarak çakışmayı önledik

    }

}
