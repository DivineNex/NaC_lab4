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
                    mainForm.AddLog(parsedMessage[1]);
                    string[] parsedParamMessage = parsedMessage[1].Split(new string[] { " = " }, StringSplitOptions.None);

                    mainForm.SetParamValue(parsedParamMessage[0], parsedParamMessage[1], float.Parse(parsedParamMessage[2]));
                    break;
                case "init_params":
                    mainForm.allParams.Clear();
                    for (int i = 1; i < parsedMessage.Length; i++)
                    {
                        //"//{param.name}##{param.Interval}##{param.MinValue}##{param.MaxValue}"
                        string[] splittedParamData = parsedMessage[i].Split(new string[] { "##" }, StringSplitOptions.None);
                        Param newParam = new Param(splittedParamData[0], 
                                                   Convert.ToInt32(splittedParamData[1]),
                                                   float.Parse(splittedParamData[2]),
                                                   float.Parse(splittedParamData[3]));
                        newParam.UpdateValue(null, 0);
                        mainForm.allParams.Add(newParam);
                    }
                    break;
            }
        }
    }
}
