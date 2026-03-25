using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsVisualizationModule.Models
{
    public class Stamps
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("timeStart")]
        public string timeStart { get; set; }

        [JsonProperty("timeFinish")]
        public string timeFinish { get; set; }

        [JsonProperty("timeEvents")]
        public List<TimeEvents> TimeEvents { get; set; } = new List<TimeEvents>();

        [JsonProperty("tag")]
        public TagInfo tag {  get; set; }

        [JsonProperty("labels")]
        public List<Labels> Labels {  get; set; }=new List<Labels>();

        [JsonProperty("position")]
        public List<double> position { get; set; }
    }
}
