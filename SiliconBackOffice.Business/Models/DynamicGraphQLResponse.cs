using System.Text.Json.Serialization;
using System.Text.Json;

namespace SiliconBackOffice.Business.Models;

public class DynamicGraphQLResponse
{
    [JsonPropertyName("data")]
    public JsonElement Data { get; set; }
}
