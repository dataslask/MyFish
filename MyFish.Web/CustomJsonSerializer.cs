using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MyFish.Web
{
    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}