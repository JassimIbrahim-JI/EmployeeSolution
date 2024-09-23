
using System.Text.Json.Serialization;

namespace BaseLibrary.Class.Entities
{
    public class VactionType : BaseEntity
    {
        [JsonIgnore]
        public List<Vaction>? Vactions { get; set; }
    }
}
