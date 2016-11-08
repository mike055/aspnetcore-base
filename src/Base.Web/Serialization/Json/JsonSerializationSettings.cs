using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Base.Web.Serialization.Json
{
    public class JsonSerializationSettings
    {
        public static JsonSerializerSettings Default()
        {
            var settings = new JsonSerializerSettings 
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

            return settings;
        }
    }
}
