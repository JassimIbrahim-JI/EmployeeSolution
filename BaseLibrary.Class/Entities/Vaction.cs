using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Class.Entities
{
    public class Vaction :OtherBaseEntity
    {
      
        [Range(typeof(DateTime), "2024-01-01", "2025-01-01", ErrorMessage = "Date must be between January 1, 2024 and January 1, 2025")]
        public DateTime? StartDate { get; set; } 
        [Required] public int NumberOfDays { get; set; }
        public DateTime EndDate
        {
            get
            {
                // Use null-coalescing operator to handle null StartDate
                return StartDate?.AddDays(NumberOfDays) ?? DateTime.MinValue; // or handle it as needed
            }
        }

        public VactionType? VactionType { get; set; }
        [Required]
        public int VactionTypeId { get; set; }
    }
}
