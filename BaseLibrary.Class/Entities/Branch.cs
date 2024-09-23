

using System.Text.Json.Serialization;

namespace BaseLibrary.Class.Entities
{
    public class Branch : BaseEntity
    {
        // One Department with many Branch
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        
        [JsonIgnore]
        public List<Employee>? Employees { get; set; }
    }
}
