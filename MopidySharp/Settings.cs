namespace Mopidy
{
    /// <summary>
    /// Settings
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Connection Method
        /// </summary>
        public enum Connection
        {
            /// <summary>
            /// Http-Post
            /// </summary>
            HttpPost,

            /// <summary>
            /// WebSocket
            /// </summary>
            WebSocket
        }

        /// <summary>
        /// Protocol
        /// </summary>
        public enum UrlScheme
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

        private const string UrlSchemeHttp = "http";
        private const string UrlSchemeHttps = "https";
        private const string DirHttpPost = "/mopidy/rpc";
        private const string DirWebSocket = "/mopidy/ws";
        private static readonly Settings _instance = new Settings();

        /// <summary>
        /// Connection Method, Http-Post or WebSocket
        /// </summary>
        public static Connection ConnectionMethod
        {
            get
            {
                return Settings._instance._connection;
            }
            set
            {
                Settings._instance._connection = value;
                Settings._instance.SetUrls();
            }
        }

        /// <summary>
        /// The URL Scheme where Mopidy is running. http or https.
        /// </summary>
        public static UrlScheme ServerUrlScheme
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
        /// Http-Post on JSON-Rpc Destination URL
        /// </summary>
        public static string HttpPostUrl => Settings._instance._httpPostUrl;

        /// <summary>
        /// WebSocket Destination URL
        /// </summary>
        public static string WebSocketUrl => Settings._instance._webSocketUrl;


        private Connection _connection;
        private UrlScheme _serverProtocol;
        private string _serverAddress;
        private int _port;
        private string _baseUrl;
        private string _httpPostUrl;
        private string _webSocketUrl;

        private Settings()
        {
            this._connection = Connection.HttpPost;
            this._serverProtocol = UrlScheme.Http;
            this._serverAddress = "localhost";
            this._port = 6680;
            this.SetUrls();
        }

        private void SetUrls()
        {
            var protocolString = (this._serverProtocol == UrlScheme.Http)
                ? Settings.UrlSchemeHttp
                : Settings.UrlSchemeHttps;
            this._baseUrl = $"{protocolString}://{this._serverAddress}:{this._port}";
            this._httpPostUrl = $"{this._baseUrl}{Settings.DirHttpPost}";
            this._webSocketUrl = $"{this._baseUrl}{Settings.DirWebSocket}";
        }
    }
}
