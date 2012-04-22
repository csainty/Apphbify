using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Apphbify
{
    public class DataStore
    {
        private static List<App> _Apps;

        public DataStore()
        {
            string json;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Apphbify.Apps.json"))
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }
            _Apps = JsonConvert.DeserializeObject<List<App>>(json);
        }

        public IList<App> GetAllApps()
        {
            return _Apps;
        }

        public App GetAppByKey(string key)
        {
            return _Apps.Where(d => d.Key.Equals(key, System.StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }
    }

    public class App
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("project_url")]
        public string ProjectUrl { get; set; }

        [JsonProperty("download_url")]
        public string DownloadUrl { get; set; }

        [JsonProperty("addons")]
        public string[] Addons { get; set; }

        [JsonProperty("enableFileSystem")]
        public bool EnableFileSystem { get; set; }
    }
}