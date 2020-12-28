using Newtonsoft.Json;
using System.Collections.Generic;

namespace Projekt.Models
{
    public class Banned
    {
        [JsonProperty("login")]
        public string Login { get; set; }
    }
}
