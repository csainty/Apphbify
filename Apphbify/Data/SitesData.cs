using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Apphbify.Data
{
    public class DataStore
    {
        private static List<App> _Apps = LoadData<List<App>>("Apphbify.Apps.json").OrderBy(d => d.Name.ToLowerInvariant()).ToList();
        private static List<Addon> _Addons = LoadData<List<Addon>>("Apphbify.Addons.json").ToList();
        private static List<Region> _Regions = LoadData<List<Region>>("Apphbify.Regions.json").ToList();

        private static T LoadData<T>(string name)
        {
            string json;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<T>(json);
        }

        public IList<App> GetAllApps()
        {
            return _Apps;
        }

        public App GetAppByKey(string key)
        {
            return _Apps.Where(d => d.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }

        public Addon GetAddonByKey(string key)
        {
            return _Addons.Where(d => d.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }

        public IList<Region> GetAllRegions()
        {
            return _Regions;
        }

        public bool DoesRegionExist(string regionId)
        {
            return _Regions.Any(d => d.Value.Equals(regionId, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    public class App
    {
        public App()
        {
            Variables = new Dictionary<string, string>();
        }

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

        [JsonProperty("variables")]
        public Dictionary<string, string> Variables { get; set; }
    }

    public class Addon
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("plan")]
        public string Plan { get; set; }
    }

    public class Region
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}