using AdapterImec.Shared;
using System.Text.Json;

namespace AdapterImec.Services
{
    public class GetImecRequestsService : IGetImecRequestsService
    {
        public async Task<JsonDocument> GetPendingRequestsAsync(DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            //TMP
            JsonDocument doc = JsonDocument.Parse("{}");
            await Task.Run(() =>
            {
                var json = @"
                [
                    {
                        ""id"": ""059f1520 - 6400 - 4965 - bee4 - 074e01ed5008"",
                        ""specification"": {
                                ""$schema"": ""https://schema.openplanet.cloud/request.json"",
                            ""$id"": ""5450c513-548a-4007-ab49-af90e75fdd63"",
                            ""date"": ""2022-08-10T12:09:23.1531606Z"",
                            ""organisation"": ""Stichting imec Nederland"",
                            ""purpose"": ""Test1 - To retrieve any data we can get our hands on freely."",
                            ""endDate"": ""2033-06-08T12:58:16.957Z"",
                            ""measures"": [
                                {
                                    ""name"": ""https://vocabulary.openplanet.cloud/health.json#hr"",
                                    ""key"": false
                                }
                            ],
                            ""criteria"": [],
                            ""aggregation"": false,
                            ""aggregateMinimumRecords"": 0
                        },
                        ""state"": ""Pending""
                    },
                    {
                        ""id"": ""cafebabe - 6400 - 4965 - bee4 - 074e01ed5008"",
                        ""specification"": {
                                ""$schema"": ""https://schema.openplanet.cloud/request.json"",
                            ""$id"": ""5450c513-548a-4007-ab49-af90e75fdd63"",
                            ""date"": ""2022-08-10T12:09:23.1531606Z"",
                            ""organisation"": ""Stichting imec Nederland"",
                            ""purpose"": ""Test2 - To retrieve any data we can get our hands on freely."",
                            ""endDate"": ""2033-06-08T12:58:16.957Z"",
                            ""measures"": [
                                {
                                    ""name"": ""https://vocabulary.openplanet.cloud/health.json#hr"",
                                    ""key"": false
                                }
                            ],
                            ""criteria"": [],
                            ""aggregation"": false,
                            ""aggregateMinimumRecords"": 0
                        },
                        ""state"": ""Pending""
                    }                ]";

                doc = JsonDocument.Parse(json);
            });

            return doc;
            //tmp
        }
    }
}