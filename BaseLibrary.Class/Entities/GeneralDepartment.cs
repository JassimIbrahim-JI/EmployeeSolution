
using System.Text.Json.Serialization;

namespace BaseLibrary.Class.Entities
{
    public class GeneralDepartment : BaseEntity
    {
        //Relationship : one General to many Department
        [JsonIgnore]
        public List<Department>? Departments { get; set; }
    }
}
