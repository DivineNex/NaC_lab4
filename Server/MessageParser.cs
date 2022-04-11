using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class MessageParser
    {
        public void ParseMessage(string message, ref Client client, out bool isInitMessage)
        {
            string[] parsedMessage = message.Split(new string[] { "//" }, StringSplitOptions.None);
            isInitMessage = false;

            switch (parsedMessage[0])
            {
                case "init":
                    switch (parsedMessage[1])
                    {
                        case "reg":
                            client.Type = eClientType.Reg_module;
                            break;
                        case "desktop":
                            client.Type = eClientType.Desktop_client;
                            break;
                        case "web":
                            client.Type = eClientType.Web_client;
                            break;
                    }
                    isInitMessage = true;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Клиент {client.ip_port} является типом {client.Type}");
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case "param":
                    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " " + client.ip_port + " " + client.Type + " " + parsedMessage[1]);
                    isInitMessage = false;
                    break;
            }
        }
    }
}
