using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsVisualizationModule.Models
{
    public class Data
    {
        [JsonProperty("data")]
        public List<VideoTrack> data {  get; set; }=new List<VideoTrack>();
        
    }
}
