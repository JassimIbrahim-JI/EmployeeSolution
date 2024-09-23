
using System.Text.Json.Serialization;

namespace BaseLibrary.Class.Entities
{
    public class Department : BaseEntity
    { 
        // Many Department to one General Department
      public GeneralDepartment? GeneralDepartment {  get; set; }
      public int GeneralDepartmentId {  get; set; }
        [JsonIgnore]
        public List<Branch>? Branches { get; set; }

    }
}
