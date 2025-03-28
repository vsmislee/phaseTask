using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phaseTask
{
    class SignalManager
    {
        public double[] Periods(double[] signal)
        {
            List<double> periods = new List<double>();

            int periodCount = 0;

            double previousValue = signal[0];
            double max = signal.Max();
            double min = signal.Min();

            for (int i = 0; i < signal.Length; i++)
            {
                if (previousValue == max && signal[i] == min)
                {
                    periods.Add(periodCount);
                    periodCount = 0;
                    previousValue = signal[i];
                }
                else
                {
                    periodCount++;
                    previousValue = signal[i];
                }
            }

            return periods.ToArray();
        }


        private double[] AbsolutePhaseCalculation(double[] signal, double[] keyPhasorSignal)
        {
            double[] phases = new double[0];
            double[] periods = Periods(keyPhasorSignal);


            return phases;
        }
    }
}
