using SciChart.Charting.Model.DataSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using taskKeyphasors;

namespace phaseTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private XyDataSeries<double, double> firstDataSeries = new XyDataSeries<double, double>();
        private XyDataSeries<double, double> secondDataSeries = new XyDataSeries<double, double>();
        private XyDataSeries<double, double> keyPhasorDataSeries = new XyDataSeries<double, double>();

        DataReader dataReader = new DataReader();
        const string pathToDataRunOut = @"C:\Users\kjgug\OneDrive\Рабочий стол\ТИК\графики\задача фаза\KE201М012_Виброперемещение_выборка_Без_преобразования_25_03_24_16 (2) (1).csv";
        const string pathToDataStart = @"C:\Users\kjgug\OneDrive\Рабочий стол\ТИК\графики\задача фаза\KE201М012_Виброперемещение_выборка_Без_преобразования_25_03_24_16 (3).csv";

        FilterManager filterManager = new FilterManager();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoad(object sender, RoutedEventArgs e)
        {
            KeyPhasorSeries.DataSeries = keyPhasorDataSeries;

            double[] keyPhasorData = TakeValuesFromSource(pathToDataStart, 1);
            keyPhasorData = filterManager.IntervalsFilter(keyPhasorData, 1000);
            keyPhasorData = filterManager.IntervalDrop(keyPhasorData, 1000);
            UpdateDataSeries(keyPhasorDataSeries, keyPhasorData);

        }


        private double[] TakeValuesFromSource(string pathToData, int colomnIndex)
        {
            double[] data = new double[0];

            try
            {
                data = dataReader.ReadColumn(pathToData, 1).ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return data;
        }

        private void UpdateDataSeries(XyDataSeries<double, double> series, IList<double> newData)
        {
            using (series.SuspendUpdates())
            {
                series.Clear();
                for (int i = 0; i < newData.Count; i++)
                {
                    series.Append(i, newData[i]);
                }
            }

        }

        
    }
}
