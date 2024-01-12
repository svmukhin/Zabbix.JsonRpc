using Newtonsoft.Json;

namespace Zabbix.JsonRpc.Core.Models
{
    [JsonObject]
    public class Error
    {
        [JsonProperty("code", Required = Required.Always)]
        public int? Code { get; set; }
        [JsonProperty("message", Required = Required.Always)]
        public string? Message { get; set; }
        [JsonProperty("data")]
        public string? Details { get; set; }

        public string FormatErrorMessage()
        {
            return $"Code: {Code}; Message: {Message}; Details: {Details}";
        }
    }
}
