namespace AdapterImec.Application.Infrastructure
{
    internal interface ISerializionManager
    {
        T Deserialize<T>(string json) where T : class;
        string Serialize(object obj);
    }
}