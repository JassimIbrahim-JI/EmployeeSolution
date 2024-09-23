

using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.Class.Entities
{
    public class OverTime :OtherBaseEntity
    {

        [Range(typeof(DateTime), "2024-01-01", "2025-01-01", ErrorMessage = "Start date must be between January 1, 2024 and January 1, 2025")]
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate
        {
            get;
            set;
        }

        private int _numOfDays;
        public int numOfDays
        {
            get => _numOfDays;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(numOfDays), "Number of days cannot be negative.");
                _numOfDays = value;
            }
        }
        public OverTimeType? OverTimeType { get; set; }
        [Required]
        public int OverTimeTypeId { get; set; }
    }
}
