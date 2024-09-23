

using System.Text.Json.Serialization;

namespace BaseLibrary.Class.Entities
{
    public class Town : BaseEntity
    {
        //Relationship : one to many
        [JsonIgnore]
        public List<Employee>? Employees { get; set; }

        public City? City { get; set; }
        public int CityId { get; set; }
    }
}
