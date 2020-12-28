using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Projekt.Models
{
    public class Event
    {
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonProperty("date_from")]
        public DateTime Date_from { get; set; }
        [JsonProperty("date_to")]
        public DateTime Date_to { get; set; }
        [JsonProperty("Create_date")]
        public DateTime Create_date { get; set; }
    }
}
