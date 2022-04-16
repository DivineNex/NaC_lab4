using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Client
{
    public class Param
    {
        public string name;
        private double value;
        public List<ChartSerie> assignedSeries;

        public double Value
        {
            get { return value; }
        }

        public Param()
        {
            assignedSeries = new List<ChartSerie>();
        }

        public void UpdateValue (double newValue)
        {
            value = newValue;
        }
    }
}
