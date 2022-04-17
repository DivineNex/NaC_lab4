using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Client
{
    public class Param
    {
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

        private double interval;

        public double Interval
        {
            get { return minValue; }
        }

        private double minValue;

        public double MinValue
        {
            get { return minValue; }
        }

        private double maxValue;

        public double MaxValue
        {
            get { return maxValue; }
        }

        public List<ChartSerie> assignedSeries;

        public Param(string name, double interval, double minValue, double maxValue)
        {
            this.name = name;
            this.interval = interval;
            this.minValue = minValue;
            this.maxValue = maxValue;
            assignedSeries = new List<ChartSerie>();
        }

        public void UpdateValue (double newValue)
        {
            value = newValue;
        }
    }
}
