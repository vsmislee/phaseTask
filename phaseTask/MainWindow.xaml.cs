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
        SignalManager signalManager = new SignalManager();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoad(object sender, RoutedEventArgs e)
        {
            KeyPhasorSeries.DataSeries = keyPhasorDataSeries;
            FirstSeries.DataSeries = firstDataSeries;
            SecondSeries.DataSeries = secondDataSeries;

            keyPhasorDataSeries.SeriesName = "KeyPhasorSignal";
            firstDataSeries.SeriesName = "FirstSignal";
            secondDataSeries.SeriesName = "SecondSignal";

            double[] keyPhasorData = FilterKeyPhasorSignal(TakeValuesFromSource(pathToDataStart, 1));
            double[] firstData = TakeValuesFromSource(pathToDataStart, 3);
            double[] secondData = TakeValuesFromSource(pathToDataStart, 5);
            UpdateDataSeries(keyPhasorDataSeries, keyPhasorData);
            UpdateDataSeries(firstDataSeries, firstData);
            UpdateDataSeries(secondDataSeries, secondData);


        }


        private double[] TakeValuesFromSource(string pathToData, int colomnIndex)
        {
            double[] data = new double[0];

            try
            {
                data = dataReader.ReadColumn(pathToData, colomnIndex).ToArray();
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


        private double[] FilterKeyPhasorSignal(double[] signal)
        {
            int intervalLenght = 1000;
            double procent = 0.75d;

            filterManager.TrimSignal(signal, -20);
            signal = filterManager.IntervalsFilter(signal, intervalLenght, procent);

            return signal;
        }

    }
}
