using Newtonsoft.Json;

namespace Apphbify.Models
{
    public class ServiceHookModel
    {
        [JsonProperty("application")]
        public ServiceHookApplicationModel Application { get; set; }

        [JsonProperty("build")]
        public ServiceHookBuildModel Build { get; set; }
    }

    public class ServiceHookApplicationModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class ServiceHookBuildModel
    {
        [JsonProperty("commit")]
        public ServiceHookCommitModel Commit { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class ServiceHookCommitModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}