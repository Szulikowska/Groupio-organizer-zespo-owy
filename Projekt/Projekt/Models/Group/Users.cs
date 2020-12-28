using Newtonsoft.Json;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;


namespace Projekt.Models
{
    public class Users
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("surname")]
        public string Surname { get; set; } 
        [JsonProperty("birth_date")]
        public string Birth_date { get; set; } 
        [JsonProperty("sex")]
        public string Sex { get; set; }
        [JsonProperty("city")]
        public string City { get; set; } 
        [JsonProperty("login")]
        public string Login { get; set; } 
        [JsonProperty("password")]
        public string Password { get; set; } 
        [JsonProperty("admin")]
        public string Admin { get; set; } 
        [JsonProperty("email")]
        public string Email { get; set; } 
        [JsonProperty("groups")]
        [OneToMany]
        public List<Group> Groups { get; set; } = new List<Group>();
        [JsonIgnore]
        [SQLite.PrimaryKey]
        public int Id { get; set; }
    }

    
}
