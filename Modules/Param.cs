using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Diagnostics;

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
        public GenerationModule gm;
        public RegistrationModule rm;

        Stopwatch sw = new Stopwatch();
        TimeSpan ts = new TimeSpan();

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

            //SendToBuffer();

            ////Улучшится при новой системе логирования
            //Console.WriteLine("Сгенерирован: " + name + ":" + value);
            ////Console.WriteLine("Всего в буфере: " + bufferForSend.Count);

            ////УДАЛИТЬ ПОТОМ
            //sw.Stop();
            //ts = sw.Elapsed;
            //string elapsedTime = String.Format("{0:00}с {1:00}мс", ts.Seconds, ts.Milliseconds);
            //Console.WriteLine("С посл. генерации. прошло: " + elapsedTime);
            //Console.WriteLine("----------------------------------------------------------");
            //sw.Restart();


            //ТЕСТ
            Console.WriteLine($"{name}:{value}");

            ParamForSend paramForSend = new ParamForSend();
            paramForSend.name = name;
            paramForSend.value = value;

            SendToServer(paramForSend);
        }

        private void SendToBuffer()
        {
            ParamForSend paramForSend = new ParamForSend();
            paramForSend.name = name;
            paramForSend.value = value;

            bufferForSend.Add(paramForSend);
        }

        //ТЕСТОВЫЙ МЕТОД
        private void SendToServer(ParamForSend param)
        {
            rm.SendParam(param);
        }
    }
}
