using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RiskAnalysis.Models
{
    public class Sectors
    {
        public int SectorId { get; set; }  // Primary key

        
        [Required(AllowEmptyStrings = false)]
        public string SectorName { get; set; }

        
        [Required(AllowEmptyStrings = false)]
        public string SectorDescription { get; set; }

                
        public DateTime CreatedDate { get; set; }  // Kaydın oluşturulma tarihi

        public virtual Businesses Business { get; set; }  // 1-1 ilişki için navigation property
    }

}
