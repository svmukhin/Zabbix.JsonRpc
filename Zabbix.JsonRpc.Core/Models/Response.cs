using Newtonsoft.Json;
using System.Dynamic;

namespace Zabbix.JsonRpc.Core.Models
{
    [JsonObject]
    public class Response
    {
        [JsonProperty("jsonrpc", Required = Required.Always)]
        public string? JsonRpcSpecification { get; set; }
        [JsonProperty("result")]
        public dynamic Result = new ExpandoObject();
        [JsonProperty("id", Required = Required.AllowNull)]
        public int ResponseId { get; set; }
        [JsonProperty("error")]
        public Error? Error { get; set; }
    }
}
