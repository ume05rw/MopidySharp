using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace MopidySharpTest.Bases
{
    public abstract class TestBase
    {
        private const string SettingsName = "settings.json";
        private const string SettingsTemplateName = "settings.template.json";
        private static Settings.Settings _settings = null;


        public TestBase()
        {
            if (TestBase._settings == null)
            {
                var dir = Directory.GetCurrentDirectory();
                var path = Path.Combine(dir, TestBase.SettingsName);

                if (!File.Exists(path))
                {
                    var tmplPath = Path.Combine(dir, TestBase.SettingsTemplateName);
                    if (!File.Exists(tmplPath))
                        throw new InvalidOperationException("File[settings.template.json] Not Found.");

                    try
                    {
                        File.Copy(tmplPath, path);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("File[settings.template.json] Copy Failed.", ex);
                    }
                }

                var json = File.ReadAllText(path, Encoding.UTF8);
                TestBase._settings = JsonConvert
                    .DeserializeObject<MopidySharpTest.Settings.Settings>(json);
            }

            Mopidy.Settings.ConnectionType = Mopidy.Settings.Connection.WebSocket;
            //Mopidy.Settings.ConnectionType = Mopidy.Settings.Connection.HttpPost;
            Mopidy.Settings.ServerAddress = TestBase._settings.ServerAddress;
            Mopidy.Settings.ServerPort = TestBase._settings.Port;
        }
    }
}
