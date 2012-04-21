using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Apphbify
{
    public class DataStore
    {
        private static List<Site> _Sites;

        public DataStore()
        {
            string json;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Apphbify.Sites.json"))
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }
            _Sites = JsonConvert.DeserializeObject<List<Site>>(json);
        }

        public IList<Site> GetAllSites()
        {
            return _Sites;
        }
    }

    public class Site
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("project_url")]
        public string ProjectUrl { get; set; }

        [JsonProperty("download_url")]
        public string DownloadUrl { get; set; }
    }
}