using Newtonsoft.Json;
using System.Text;
using Zabbix.JsonRpc.Core.Models;

namespace Zabbix.JsonRpc.Core
{
    public class Client
    {
        private string _user;
        private string _password;
        private string _apiURL;
        private string? _authenticationToken;
        private string? _basicAuthorization;
        private int _requestId;

        public Client(string user, string password, string apiURL)
            : this(user, password, apiURL, false) { }

        public Client(Configuration configuration)
            : this(configuration.UserName,
                  configuration.Password,
                  configuration.ApiUri,
                  configuration.UseBasicAuthentication)
        { }

        public Client(string user, string password, string apiURL, bool useBasicAuthentication)
        {
            if (String.IsNullOrEmpty(user))
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (Uri.IsWellFormedUriString(apiURL, UriKind.Absolute) == false)
            {
                throw new UriFormatException(apiURL);
            }

            _user = user;
            _password = password;
            _apiURL = apiURL;
            _requestId = 1;

            if (useBasicAuthentication)
            {
                _basicAuthorization = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(_user + ":" + _password));
            }
        }

        public async Task<bool> LoginAsync()
        {
            var response = await GetResponseAsync("user.login", new { user = _user, password = _password });
            if (response == null || response.Error != null)
            {
                return false;
            }

            _authenticationToken = response.Result;
            return true;
        }

        public async Task<bool> LogoutAsync()
        {
            var response = await GetResponseAsync("user.logout", new string[] { });
            if (response == null || response.Error != null)
            {
                return false;
            }

            return response.Result;
        }

        public async Task<Response> GetResponseAsync(string method, object parameters)
        {
            Request request = new Request("2.0", method, parameters, _requestId, _authenticationToken);
            string jsonResponse = await GetJsonResponseAsync(request);
            Response response = JsonConvert.DeserializeObject<Response>(jsonResponse);

            return response;
        }

        private async Task<string> GetJsonResponseAsync(Request request)
        {
            string jsonRequest = JsonConvert.SerializeObject(request);
            string jsonResponse = await SendRequestAsync(jsonRequest);
            return jsonResponse;
        }

        private async Task<string> SendRequestAsync(string jsonRequest)
        {
            string jsonResponse;

            using (var httpClient = new HttpClient())
            {
                if (_basicAuthorization != null)
                {
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + _basicAuthorization);
                }
                var clientResponse = await httpClient.PostAsync(_apiURL, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
                jsonResponse = await clientResponse.Content.ReadAsStringAsync();
            }

            return jsonResponse;
        }
    }
}
