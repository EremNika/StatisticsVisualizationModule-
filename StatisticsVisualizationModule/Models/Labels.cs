using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsVisualizationModule.Models
{
    public class Labels
    {

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string name {  get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("group")]
        public Group Group{ get; set; }
    }
}
