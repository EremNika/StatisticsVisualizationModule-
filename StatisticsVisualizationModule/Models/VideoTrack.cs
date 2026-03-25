using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsVisualizationModule.Models
{
    public class VideoTrack
    {
        [JsonProperty("id")]
        public string id {get;set;}

        [JsonProperty("name")]
        public string name {get;set;}

        [JsonProperty("stamps")]
        public List<Stamps> stamps { get; set; }=new List<Stamps>();
    }
}
