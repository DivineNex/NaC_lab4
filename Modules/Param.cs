using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace Modules
{
    internal class Param
    {
        public readonly string name;
        Timer timer;
        Random random;
        private double minValue;
        private double maxValue;
        List<ParamForSend> bufferForSend;
        bool isInteger;

        private double value;
        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }

        private bool active = false;
        public bool Active
        {
            get { return active; }
            set { active = value; timer.Enabled = value; }
        }

        public Param(string name, int interval, double minValue, double maxValue, bool isInteger, List<ParamForSend> buffer)
        {
            this.name = name;
            timer = new Timer(interval);
            random = new Random();
            timer.Elapsed += Timer_Elapsed;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.bufferForSend = buffer;
            this.isInteger = isInteger;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Generate();
        }

        private void Generate()
        {
            if (isInteger)
                value = random.Next(Convert.ToInt32(minValue), Convert.ToInt32(maxValue));
            else
                value = Math.Round(random.NextDouble() * (maxValue - minValue) + minValue, 3);

            SendToBuffer();

            Console.WriteLine($"{name}:{value}");
        }

        private void SendToBuffer()
        {
            ParamForSend paramForSend = new ParamForSend();
            paramForSend.name = name;
            paramForSend.value = value;

            bufferForSend.Add(paramForSend);
        }
    }
}
