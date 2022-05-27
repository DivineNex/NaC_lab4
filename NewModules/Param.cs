using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;
using System.Threading;

namespace NewModules
{
    internal class Param
    {
        private string lastValueDateTime;
        public GenerationModule gm;
        public RegistrationModule rm;
        System.Timers.Timer timer;
        private Random random;
        private bool isInteger;

        private bool active = false;
        public bool Active
        {
            get { return active; }
            set { active = value; timer.Enabled = value; }
        }

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

        public Param(string name, int interval, double minValue, double maxValue, bool isInteger)
        {
            this.interval = interval;
            this.name = name;
            timer = new System.Timers.Timer(interval);
            timer.Elapsed += Timer_Elapsed;
            this.minValue = (float)minValue;
            this.maxValue = (float)maxValue;
            this.isInteger = isInteger;
            random = new Random();
            Thread.Sleep(10);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Generate();
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

        private void Generate()
        {
            if (isInteger)
            {
                value = random.Next(Convert.ToInt32(minValue), Convert.ToInt32(maxValue));
            }
            else
            {
                value = Math.Round(random.NextDouble() * (maxValue - minValue) + minValue, 3);
            }

            SendToServer($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff")} = {name} = {value}");
        }

        private void SendToServer(string param)
        {
            rm.SendParam(param);
        }
    }
}
