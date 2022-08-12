using Newtonsoft.Json;

namespace AdapterImec.Application.Infrastructure
{
    internal class SerializionManager : ISerializionManager
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string json) where T : class
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
