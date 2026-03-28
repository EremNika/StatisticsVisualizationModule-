using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsVisualizationModule.Models
{
    public class ChartData
    {
        public ObservableCollection<string> Labels {  get; set; }
        public SeriesCollection Series { get; set; }

        public ChartData() { 
            Labels=new ObservableCollection<string>();
            Series=new SeriesCollection();
        }
    }
}
