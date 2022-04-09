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
        //тестом сделал public, но не должен быть
        public List<Param> allParams;

        private bool active = false;
        public bool Logging = true;
        public bool Active
        {
            get { return active; }
        }

        public GenerationModule()
        {
            allParams = new List<Param>();
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
        // Метод для подгрузки настроек параметров из текстового документа
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
                    Param newParam = new Param(paramConfigs[0],                    //name
                        Convert.ToInt32(paramConfigs[1]),   //interval
                        Convert.ToDouble(paramConfigs[2]),  //minValue
                        Convert.ToDouble(paramConfigs[3]),  //maxValue
                        Convert.ToBoolean(paramConfigs[4]));          //буфер модуля регистрации
                    newParam.gm = this;
                    allParams.Add(newParam);
                    line = sr.ReadLine();
                }

            }
        }
    }
}
