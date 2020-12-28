using Newtonsoft.Json;
using System.Collections.Generic;

namespace Projekt.Models
{
    public class Post
    {
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonProperty("date")]
        public uint Date { get; set; }
    }

    public class Post2
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("posts")]
        public List<Post> Posts { get; set; } = new List<Post>();
    }

    public class Post3
    {
        public string Name { get; set; }
        public Post Posts { get; set; }


        [JsonIgnore]
        public int TextMode { get; set; }
    }
}
