using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Client
{
    internal class Param
    {
        public string name;
        private double value;

        public double Value
        {
            get { return value; }
        }

        public void UpdateValue (double newValue)
        {
            value = newValue;
        }
    }
}
