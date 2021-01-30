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
        /// Encryption
        /// </summary>
        public enum Encryption
        {
            /// <summary>
            /// use http:, ws:
            /// </summary>
            None,

            /// <summary>
            /// use https:, wss:
            /// </summary>
            Ssl
        }

        private const string UrlSchemeHttp = "http";
        private const string UrlSchemeHttps = "https";
        private const string UrlSchemeWebSocket = "ws";
        private const string UrlSchemeWebSocketSsl = "wss";
        private const string DirHttpPost = "/mopidy/rpc";
        private const string DirWebSocket = "/mopidy/ws";
        private static readonly Settings _instance = new Settings();

        /// <summary>
        /// Connection Type, Http-Post or WebSocket. Default is Http-Post.
        /// </summary>
        public static Connection ConnectionType
        {
            get
            {
                return Settings._instance._connectionType;
            }
            set
            {
                Settings._instance._connectionType = value;
                Settings._instance.SetUrls();
            }
        }

        /// <summary>
        /// SSL or None. Default is None.
        /// </summary>
        public static Encryption EncryptionType
        {
            get
            {
                return Settings._instance._encryptionType;
            }
            set
            {
                Settings._instance._encryptionType = value;
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
        public static int ServerPort
        {
            get
            {
                return Settings._instance._serverPort;
            }
            set
            {
                Settings._instance._serverPort = value;
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


        private Connection _connectionType;
        private Encryption _encryptionType;
        private string _serverAddress;
        private int _serverPort;
        private string _baseUrl;
        private string _httpPostUrl;
        private string _webSocketUrl;

        private Settings()
        {
            this._connectionType = Connection.HttpPost;
            this._encryptionType = Encryption.None;
            this._serverAddress = "localhost";
            this._serverPort = 6680;
            this.SetUrls();
        }

        private void SetUrls()
        {
            var hostAndPort = $"://{this._serverAddress}:{this._serverPort}";
            var postToProtocol = (this._encryptionType == Encryption.None)
                ? Settings.UrlSchemeHttp
                : Settings.UrlSchemeHttps;
            var wsProtocol = (this._encryptionType == Encryption.None)
                ? Settings.UrlSchemeWebSocket
                : Settings.UrlSchemeWebSocketSsl;

            this._baseUrl = $"{postToProtocol}{hostAndPort}";
            this._httpPostUrl = $"{this._baseUrl}{Settings.DirHttpPost}";
            this._webSocketUrl = $"{wsProtocol}{hostAndPort}{Settings.DirWebSocket}";
        }
    }
}
