using Newtonsoft.Json;
using System.Collections.Generic;


namespace Projekt.Models
{
    public class Group
    {
        [JsonIgnore]
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Col { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("last_time_post")]
        public string Last_time_post { get; set; }
        [JsonProperty("last_time_message")]
        public string Last_time_message { get; set; }
        [JsonProperty("last_time_event")]
        public string Last_time_events { get; set; }
    }
}
