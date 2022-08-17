using System.Text.Json;

namespace AdapterImec.Shared;

public interface IImecService
{
    Task<JsonDocument> GetPendingRequestsAsync(string dataSourceId);
}