using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Class.Entities
{
    public class Doctor : OtherBaseEntity
    {
   
        [Range(typeof(DateTime), "2024-01-01", "2025-04-04", ErrorMessage = "Date must be between January 1, 2024 and January 1, 2025")]
        public DateTime? Date { get; set; }
        [Required]
        public string MedicalDiagonse {  get; set; } = string.Empty;
        [Required]
        public string MedicalRecommendation {  get; set; } = string.Empty;  
    }
}
