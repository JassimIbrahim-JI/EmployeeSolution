
using System.Text.Json.Serialization;

namespace BaseLibrary.Class.Entities
{
    public class SanctionType : BaseEntity
    {
        [JsonIgnore]
        public List<Sanction>? Sanctions { get; set; }
    }
}