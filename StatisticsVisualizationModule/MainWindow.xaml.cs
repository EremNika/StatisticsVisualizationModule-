using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StatisticsVisualizationModule.Models;
using Newtonsoft.Json;

namespace StatisticsVisualizationModule
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       private async void Button_Click(object sender, RoutedEventArgs e)
{
    try
    {
        string filePath = "Хоккей МИЭТ МИСИС 8.03.2026.json";
        
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(fs))
            {
                string json = await reader.ReadToEndAsync();
                Data data = JsonConvert.DeserializeObject<Data>(json);
                
               textBox.Text=json;
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Ошибка: {ex.Message}");
    }
}
    }
}
