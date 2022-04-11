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

                    mainForm.SetParamValue(parsedParamMessage[0], Convert.ToDouble(parsedParamMessage[1]));
                    break;
                case "init_params":
                    mainForm.allGettingParamsNames.Clear();
                    for (int i = 1; i < parsedMessage.Length; i++)
                    {
                        mainForm.allGettingParamsNames.Add(parsedMessage[i]);
                    }
                    mainForm.InitializeParamsArray();
                    break;
            }
        }
    }
}
