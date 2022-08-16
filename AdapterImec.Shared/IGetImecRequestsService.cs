using System.Text.Json;

namespace AdapterImec.Shared
{
    public interface IGetImecRequestsService
    {
        Task<JsonDocument> GetPendingRequestsAsync(ImecSettings imecSettings, string dataSourceId);
    }
}