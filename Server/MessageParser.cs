using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class MessageParser
    {
        public void ParseMessage(string message, Client client)
        {
            string[] parsedMessage = message.Split(new string[] { "//" }, StringSplitOptions.None);

            switch (parsedMessage[0])
            {
                case "init":
                    switch (parsedMessage[1])
                    {
                        case "reg":
                            client.type = eClientType.Reg_module;
                            break;
                        case "desktop":
                            client.type = eClientType.Desktop_client;
                            break;
                        case "web":
                            client.type = eClientType.Web_client;
                            break;
                    }
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Клиент {client.ip_port} является типом {client.type}");
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case "param":
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + " " + client.ip_port + " " + client.type + " " + parsedMessage[1]);
                    break;
            }
        }
    }
}
