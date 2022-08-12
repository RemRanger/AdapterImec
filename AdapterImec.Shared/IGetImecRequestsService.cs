using System.Text.Json;

namespace AdapterImec.Shared
{
    public interface IGetImecRequestsService
    {
        Task<JsonDocument> GetPendingRequestsAsync(DateTime dateTimeStart, DateTime dateTimeEnd);
    }
}