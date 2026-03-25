using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsVisualizationModule.Models
{
    public class TagInfo
    {
        [JsonProperty("id")]
        public string Id {  get; set; }

        [JsonProperty("primaryID")]
        public string primaryId {  get; set; }

        [JsonProperty("name")]
        public string name {  get; set; }

        [JsonProperty("collection")]
        public string collection {  get; set; }

        [JsonProperty("color")]
        public string color {  get; set; }

        [JsonProperty("hotkey")]
        public string hotkey {  get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("defaultTimeBefore")]
        public int defaultTimeBefore { get; set; }

        [JsonProperty("defaultTimeAfter")]
        public int defaultTimeAfter { get; set; }

        [JsonProperty("labelHotkeys")]
        public Dictionary<string, string> LabelHotkeys { get; set; }=new Dictionary<string, string>();

        [JsonProperty("group")]
        public Group Group { get; set; }
    }
}
