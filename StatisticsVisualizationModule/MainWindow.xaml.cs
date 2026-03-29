using LiveCharts;
using Newtonsoft.Json;
using StatisticsVisualizationModule.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace StatisticsVisualizationModule
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ChartData chartData { get; set; }
        public Func<double, string> YAxisFormatter { get; set; }
        private Data currentData = null;

        public MainWindow()
        {
            InitializeComponent();
            chartData = new ChartData();
            DataContext = this;
            YAxisFormatter = value => value.ToString("N0");

        }

        public string GetFileName()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".json";
            string filename = null;


            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                filename = dlg.FileName;
                textFile.Text = filename;

            }

            return filename;
        }

        private async Task<bool> LoadData(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        string json = await reader.ReadToEndAsync();
                        currentData = JsonConvert.DeserializeObject<Data>(json);
                        textBox.Text = json;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                return false;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string filePath = GetFileName();
            if (!string.IsNullOrEmpty(filePath))
            {
                await LoadData(filePath);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (currentData == null)
            {
                MessageBox.Show("Json файл не загружен");
                return;
            }

            var allStamps = currentData.data.SelectMany(track => track.stamps).ToList();

            var allTracks = allStamps
                .Where(s => s.tag != null)
                .GroupBy(s => s.tag.name)
                .Select(g => new
                {
                    tagName = g.Key,
                    count = g.Count()
                })
                .ToList();


            chartData.Labels.Clear();
            chartData.Series.Clear();

            var values = new ChartValues<int>();
            textBoxStat.Clear();

            foreach (var track in allTracks)
            {
                chartData.Labels.Add(track.tagName);
                values.Add(track.count);
                textBoxStat.Text += $"{track.tagName} : {track.count}{Environment.NewLine}";
            }

            chartData.Series.Add(new LiveCharts.Wpf.ColumnSeries
            {
                Title = "Количество",
                Values = values,
                DataLabels = true,
                Fill = Brushes.Purple,
                Foreground = Brushes.Yellow
            });


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (currentData == null)
            {
                MessageBox.Show("Json файл не загружен");
                return;
            }

            var allStamps = currentData.data.SelectMany(track => track.stamps).ToList();
            var allGroups = allStamps
                          .Where(g => g.Labels != null)
                          .GroupBy(g => g.tag.Group.Name)
                          .Select(g => new
                          {
                              groupName = g.Key,
                              groupCount = g.Count()
                          })
                          .ToList();
            chartData.Labels.Clear();
            chartData.Series.Clear();

            var values = new ChartValues<int>();
            textBoxStat.Clear();

            foreach (var group in allGroups)
            {
                chartData.Labels.Add(group.groupName);
                values.Add(group.groupCount);
                textBoxStat.Text += $"{group.groupName} : {group.groupCount}{Environment.NewLine}";
            }

            chartData.Series.Add(new LiveCharts.Wpf.ColumnSeries
            {
                Title = "Количество",
                Values = values,
                DataLabels = true,
                Fill = Brushes.Purple,
                Foreground = Brushes.Yellow
            });


        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (currentData == null)
            {
                MessageBox.Show("Json файл не загружен");
                return;
            }
            var allStamps = currentData.data.SelectMany(track => track.stamps).ToList();

            var allEvents = allStamps
                .Where(ev => ev.TimeEvents != null)
                .GroupBy(ev => ev.TimeEvents[0].name)
                .ToDictionary(
                    period => period.Key,
                    period => period
                        .GroupBy(g => g.tag.Group.Name)
                        .ToDictionary(
                            groupName => groupName.Key,
                            groupCount => groupCount.Count()
                        )
                );

            chartData.Labels.Clear();
            chartData.Series.Clear();

            var allGroups = allEvents
                .SelectMany(a => a.Value.Keys)
                .Distinct()
                .ToList();

            foreach (var group in allGroups)
            {
                var values = new ChartValues<int>();

                foreach (var period in allEvents)
                {
                    int count = period.Value.ContainsKey(group) ? period.Value[group] : 0;
                    values.Add(count);

                    if (group == allGroups.First())
                    {
                        chartData.Labels.Add(period.Key);
                    }
                }
                chartData.Series.Add(new LiveCharts.Wpf.ColumnSeries
                {
                    Title = group,
                    Values = values,
                    DataLabels = true,
                    Fill = Brushes.Purple,
                    ColumnPadding = 2,
                    MaxColumnWidth = 40,
                    Foreground = Brushes.Yellow
                });

            }
            textBoxStat.Clear();
            foreach (var events in allEvents)
            {
                textBoxStat.Text += $"{Environment.NewLine}{events.Key}{Environment.NewLine}";

                foreach (var group in events.Value)
                {
                    textBoxStat.Text += $"  {group.Key.ToString()} : {group.Value}{Environment.NewLine}";
                }

            }

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (currentData == null)
            {
                MessageBox.Show("Json файл не загружен");
                return;
            }

            var allStamps = currentData.data.SelectMany(track => track.stamps).ToList();

            var allPlayers = allStamps
                .Where(p => p.Labels != null && p.Labels.Any())
                .Select(p => new
                {
                    playerNum = p.Labels.FirstOrDefault(l => l.Group.Name == "Состав").name,
                    eventGroup = p.tag.Group.Name,
                    eventName = p.tag.name
                })
                .Where(p => p.playerNum != null)
                .GroupBy(p => p.playerNum)
                .ToDictionary(
                    playerGroup => playerGroup.Key,
                    playerGroup => playerGroup
                    .GroupBy(p => p.eventGroup)
                    .ToDictionary(
                        groupEvents => groupEvents.Key,
                        groupEvents => groupEvents.Count()
                     )
                 );

            chartData.Labels.Clear();
            chartData.Series.Clear();

            var allGroups = allPlayers
                .SelectMany(a => a.Value.Keys)
                .Distinct()
                .ToList();

            foreach (var group in allGroups)
            {
                var values = new ChartValues<int>();

                foreach (var period in allPlayers)
                {
                    int count = period.Value.ContainsKey(group) ? period.Value[group] : 0;
                    values.Add(count);

                    if (group == allGroups.First())
                    {
                        chartData.Labels.Add(period.Key);
                    }
                }
                chartData.Series.Add(new LiveCharts.Wpf.ColumnSeries
                {
                    Title = group,
                    Values = values,
                    DataLabels = true,

                    Fill = Brushes.Purple,
                    ColumnPadding = 2,
                    MaxColumnWidth = 20,
                    Foreground = Brushes.Yellow
                });

            }

            textBoxStat.Clear();
            foreach (var player in allPlayers)
            {
                textBoxStat.Text += $"Игрок: {player.Key}{Environment.NewLine}";
                textBoxStat.Text += $"  Активность: {player.Value.Values.Sum()}{Environment.NewLine}";
                textBoxStat.Text += $"  Детально:{Environment.NewLine}";

                foreach (var group in player.Value)
                {
                    textBoxStat.Text += $"   - {group.Key}: {group.Value}{Environment.NewLine}";
                }

            }

        }
    }
}
