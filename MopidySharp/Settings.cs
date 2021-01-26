namespace Mopidy
{
    /// <summary>
    /// Settings
    /// </summary>
    public static class Settings
    {
        public enum Protocol
        {
            Http,
            Https
        }

        private static Protocol _serverProtocol = Protocol.Http;
        private static string _serverAddress = "localhost";
        private static int _port = 6680;
        private static string _rpcUrl = "http://localhost:6680/mopidy/rpc";
        //private static string _imageUrl = "http://localhost:6680/mopidy/images";

        public static Protocol ServerProtocol
        {
            get
            {
                return Settings._serverProtocol;
            }
            set
            {
                Settings._serverProtocol = value;
                Settings.SetUrls();
            }
        }


        /// <summary>
        /// The address where Mopidy is running. IP-Address or HostName.
        /// </summary>
        public static string ServerAddress
        {
            get
            {
                return Settings._serverAddress;
            }
            set
            {
                Settings._serverAddress = value;
                Settings.SetUrls();
            }
        }

        /// <summary>
        /// Port of the mopidy connection. Default is 6680
        /// </summary>
        public static int Port
        {
            get
            {
                return Settings._port;
            }
            set
            {
                Settings._port = value;
                Settings.SetUrls();
            }
        }

        /// <summary>
        /// JSON-Rpc Destination URL
        /// </summary>
        public static string RpcUrl => Settings._rpcUrl;

        /// <summary>
        /// Image Base URL
        /// </summary>
        //public static string ImageUrl => Settings._imageUrl;

        private static void SetUrls()
        {
            var protocolString = (Settings.ServerProtocol == Protocol.Http)
                ? "http"
                : "https";
            var baseUrl = $"{protocolString}://{Settings._serverAddress}:{Settings._port}/mopidy";
            Settings._rpcUrl = $"{baseUrl}/rpc";
            //Settings._imageUrl = $"{baseUrl}/images";
        }
    }
}
