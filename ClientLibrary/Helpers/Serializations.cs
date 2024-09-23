
using System.Text.Json;

namespace ClientLibrary.Helpers
{
    public static class Serializations
    {
        public static string SerializeObj<T>(T model)=>JsonSerializer.Serialize(model);
        public static T DeserializeJsonString<T>(string jsonString) =>
            JsonSerializer.Deserialize<T>(jsonString);

        public static IList<T> DeserializeJsonStringList<T>(string jsonString) =>
            JsonSerializer.Deserialize<IList<T>>(jsonString);
    }
}
