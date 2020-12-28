using Newtonsoft.Json;
using System.Collections.Generic;


namespace Projekt.Models
{
    public class Groups
    {
        [JsonIgnore]
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Col { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("school")]
        public string School { get; set; }
        [JsonProperty("year")]
        public string Year { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("users")]
        public List<Users> Users { get; set; } = new List<Users>();
        [JsonProperty("events")]
        public List<Event> Events { get; set; } = new List<Event>();
        [JsonProperty("posts")]
        public List<Post> Posts { get; set; } = new List<Post>();
        [JsonProperty("baned")]
        public List<Banned> Baned { get; set; } = new List<Banned>();
        [JsonProperty("requests")]
        public List<Users> Requests { get; set; } = new List<Users>();
    }
}
