using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Client
{
    public class Param
    {
        private string lastValueDateTime;

        public string LastValueDateTime
        {
            get { return lastValueDateTime; }
        }

        private string name;

        public string Name
        {
            get { return name; }
        }

        private double value;

        public double Value
        {
            get { return value; }
        }

        private int interval;

        public int Interval
        {
            get { return interval; }
        }

        private float minValue;

        public float MinValue
        {
            get { return minValue; }
        }

        private float maxValue;

        public float MaxValue
        {
            get { return maxValue; }
        }

        public List<ChartSerie> assignedSeries;

        public Param(string name, int interval, float minValue, float maxValue)
        {
            this.name = name;
            this.interval = interval;
            this.minValue = minValue;
            this.maxValue = maxValue;
            assignedSeries = new List<ChartSerie>();
        }

        public void UpdateValue (string time, double newValue)
        {
            lastValueDateTime = time;
            value = newValue;

            foreach (ChartSerie serie in assignedSeries)
            {
                DateTime date = DateTime.ParseExact(time, "yyyy-MM-dd HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture);
                string convertedTime = date.ToString("HH:mm:ss");
                if (!serie.Chart.timeStamps.Contains(convertedTime))
                    serie.Chart.timeStamps.Add(convertedTime);
            }
        }
    }
}
