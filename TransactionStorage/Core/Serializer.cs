using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TransactionStorage.Interface;

namespace TransactionStorage.Core
{
    public class Serializer : ISerializer
    {
        private readonly JsonSerializerSettings _options = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public string Serialize(object o)
        {
            return JsonConvert.SerializeObject(o, _options);
        }
    }
}
