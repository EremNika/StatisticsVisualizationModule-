using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsVisualizationModule.Models
{
    public class TimeEvents
    {

        [JsonProperty("id")]
        public string Id {  get; set; }

        [JsonProperty("name")]
        public string name {  get; set; }
    }
}
