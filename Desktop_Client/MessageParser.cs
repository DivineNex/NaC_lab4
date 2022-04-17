using System;

namespace Desktop_Client
{
    internal class MessageParser
    {
        private MainForm mainForm;

        public MessageParser(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        public void ParseMessage(string message)
        {
            string[] parsedMessage = message.Split(new string[] { "//" }, StringSplitOptions.None);

            switch (parsedMessage[0])
            {
                case "param":
                    mainForm.AddLog(DateTime.Now.ToString("HH:mm:ss") + " " + parsedMessage[1]);
                    string[] parsedParamMessage = parsedMessage[1].Split(new string[] { " = " }, StringSplitOptions.None);

                    mainForm.SetParamValue(parsedParamMessage[0], float.Parse(parsedParamMessage[1]));
                    break;
                case "init_params":
                    mainForm.allParams.Clear();
                    for (int i = 1; i < parsedMessage.Length; i++)
                    {
                        //"//{param.name}##{param.Interval}##{param.MinValue}##{param.MaxValue}";
                        string[] splittedParamData = parsedMessage[i].Split(new string[] { "##" }, StringSplitOptions.None);
                        Param newParam = new Param(splittedParamData[0], 
                                                   Convert.ToDouble(splittedParamData[1]),
                                                   Convert.ToDouble(splittedParamData[2]),
                                                   Convert.ToDouble(splittedParamData[3]));
                        newParam.UpdateValue(0);
                        mainForm.allParams.Add(newParam);
                    }
                    break;
            }
        }
    }
}
