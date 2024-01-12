using Newtonsoft.Json;

namespace Zabbix.JsonRpc.Core.Models
{
    [JsonObject]
    internal class Request
    {
        [JsonProperty("jsonrpc", Required = Required.Always)]
        public string JsonRpcSpecification { get; set; }
        [JsonProperty("method", Required = Required.Always)]
        public string Method { get; set; }
        [JsonProperty("params")]
        public dynamic @Parameters { get; set; }
        [JsonProperty("id")]
        public int RequestId { get; set; }
        [JsonProperty("auth")]
        public string? AuthenticationToken { get; set; }

        public Request(string jsonRpcSpecification, string method, dynamic parameters, int requestId, string? authenticationToken)
        {
            JsonRpcSpecification = jsonRpcSpecification;
            Method = method;
            Parameters = parameters;
            RequestId = requestId;
            AuthenticationToken = authenticationToken;
        }
    }
}
