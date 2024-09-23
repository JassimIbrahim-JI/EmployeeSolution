

using System.Text.Json.Serialization;

namespace BaseLibrary.Class.Entities
{
    public class OverTimeType : BaseEntity
    {
        [JsonIgnore]
        public List<OverTime>? overTimes {  get; set; }
    }
}
