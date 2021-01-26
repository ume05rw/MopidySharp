namespace Mopidy
{
    /// <summary>
    /// Settings
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Protocol
        /// </summary>
        public enum Protocol
        {
            /// <summary>
            /// http
            /// </summary>
            Http,

            /// <summary>
            /// https
            /// </summary>
            Https
        }

        private const string ProtocolHttp = "http";
        private const string ProtocolHttps = "https";
        private const string DirRpc = "/mopidy/rpc";
        private static readonly Settings _instance = new Settings();

        /// <summary>
        /// The protocol where Mopidy is running. http or https.
        /// </summary>
        public static Protocol ServerProtocol
        {
            get
            {
                return Settings._instance._serverProtocol;
            }
            set
            {
                Settings._instance._serverProtocol = value;
                Settings._instance.SetUrls();
            }
        }


        /// <summary>
        /// The address where Mopidy is running. IP-Address or HostName.
        /// </summary>
        public static string ServerAddress
        {
            get
            {
                return Settings._instance._serverAddress;
            }
            set
            {
                Settings._instance._serverAddress = value;
                Settings._instance.SetUrls();
            }
        }

        /// <summary>
        /// Port of the mopidy connection. Default is 6680
        /// </summary>
        public static int Port
        {
            get
            {
                return Settings._instance._port;
            }
            set
            {
                Settings._instance._port = value;
                Settings._instance.SetUrls();
            }
        }

        /// <summary>
        /// Base URL
        /// </summary>
        public static string BaseUrl => Settings._instance._baseUrl;

        /// <summary>
        /// JSON-Rpc Destination URL
        /// </summary>
        public static string RpcUrl => Settings._instance._rpcUrl;




        private Protocol _serverProtocol;
        private string _serverAddress;
        private int _port;
        private string _baseUrl;
        private string _rpcUrl;

        private Settings()
        {
            this._serverProtocol = Protocol.Http;
            this._serverAddress = "localhost";
            this._port = 6680;
            this.SetUrls();
        }

        private void SetUrls()
        {
            var protocolString = (this._serverProtocol == Protocol.Http)
                ? Settings.ProtocolHttp
                : Settings.ProtocolHttps;
            this._baseUrl = $"{protocolString}://{this._serverAddress}:{this._port}";
            this._rpcUrl = $"{this._baseUrl}{Settings.DirRpc}";
        }
    }
}
