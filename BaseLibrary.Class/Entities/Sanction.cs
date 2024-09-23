
using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Class.Entities
{
    public class Sanction :OtherBaseEntity
    {
      
        [Range(typeof(DateTime), "2024-01-01", "2025-01-01", ErrorMessage = "Date must be between January 1, 2024 and January 1, 2025")]
        public DateTime? Date { get; set; } 
        [Required]
        public string Punishment { get; set; } = string.Empty;
     
        [Range(typeof(DateTime), "DateTime.Now", "2025-03-03", ErrorMessage = "Date must be between January 1, 2024 and January 1, 2025")]
        public DateTime? PunishmentDate { get; set; } 

        public int SanctionTypeId { get; set; }
        public SanctionType? SanctionType { get; set; }
        
    }
}
