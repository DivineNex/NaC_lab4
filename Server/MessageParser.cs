using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class MessageParser
    {
        public void ParseMessage(string message, ref Client client, out eMessageType messageType)
        {
            string[] parsedMessage = message.Split(new string[] { "//" }, StringSplitOptions.None);

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
                    messageType = eMessageType.initMessage;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Клиент {client.ip_port} является типом {client.Type}");
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case "param":
                    Console.WriteLine(client.ip_port + " " + client.Type + " " + parsedMessage[1]);
                    messageType = eMessageType.paramsMessage;
                    break;
                case "init_params":
                    messageType = eMessageType.initParamsMessage;
                    break;
                default:
                    messageType = eMessageType.not_assigned;
                    break;
            }
        }
    }
}
