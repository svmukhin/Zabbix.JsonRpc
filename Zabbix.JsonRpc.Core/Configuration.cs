namespace Zabbix.JsonRpc.Core
{
    public class Configuration
    {
        public string UserName => _userName;
        public string Password => _password;
        public string ApiUri => _apiUri;
        public bool UseBasicAuthentication => _useBasicAuthentication;

        private string _userName;
        private string _password;
        private string _apiUri;
        private bool _useBasicAuthentication;

        public Configuration(string userName, string password, string apiUri)
            : this(userName, password, apiUri, false) { }

        public Configuration(string userName, string password, string apiUri, bool useBasicAuthentication)
        {
            _userName = userName;
            _password = password;
            _apiUri = apiUri;
            _useBasicAuthentication = useBasicAuthentication;
        }
    }
}
