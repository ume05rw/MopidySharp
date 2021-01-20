using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MopidySharpTest.Settings
{
    public class Settings
    {
        [JsonProperty("ServerAddress")]
        public string ServerAddress { get; set; }

        [JsonProperty("Port")]
        public int Port { get; set; }
    }
}
