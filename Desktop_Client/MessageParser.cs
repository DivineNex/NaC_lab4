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

                    break;
            }
        }
    }
}
