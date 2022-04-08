using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    internal class GenerationModule
    {
        List<Param> allParams;
        private bool active = false;

        public bool Active
        {
            get { return active; }
        }

        public int minInterval = Int32.MaxValue;
        private List<ParamForSend> registrationModuleBuffer;

        public GenerationModule(List<ParamForSend> rmBuffer)
        {
            allParams = new List<Param>();
            registrationModuleBuffer = rmBuffer;
        }

        public void Start()
        {
            active = true;
            foreach (var param in allParams)
            {
                param.Active = true;
            }
        }

        public void Stop()
        {
            active = false;
            foreach (var param in allParams)
            {
                param.Active = false;
            }
        }

        public void LoadAndParseParams()
        {
            StreamReader sr = new StreamReader(@"..\\..\\params.txt");
            string line;
            string[] paramConfigs;

            sr.ReadLine();
            sr.ReadLine(); //скип первые две линии

            line = sr.ReadLine();

            while (line != null)
            {
                paramConfigs = line.Split(' ');
                if (paramConfigs[0] == "//")
                {
                    line = sr.ReadLine();
                    continue;
                }
                else
                {
                    if (Convert.ToInt32(paramConfigs[2]) < minInterval)
                        minInterval = Convert.ToInt32(paramConfigs[1]);

                    Param newParam = new Param(paramConfigs[0],                    //name
                        Convert.ToInt32(paramConfigs[1]),   //interval
                        Convert.ToDouble(paramConfigs[2]),  //minValue
                        Convert.ToDouble(paramConfigs[3]),  //maxValue
                        Convert.ToBoolean(paramConfigs[4]), //isInteger
                        registrationModuleBuffer);          //буфер модуля регистрации
                    allParams.Add(newParam);
                    line = sr.ReadLine();
                }

            }

            //Задать минимальный интервал там же в цикле!
        }
    }
}
