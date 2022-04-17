using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Modules
{
    internal class RegistrationModule
    {
        private int port = 55555; // порт сервера
        private string address = "127.0.0.1"; // адрес сервера
        private Socket socket;
        private IPEndPoint ipPoint;
        public string allGeneratingParams = "";
        private bool connected = false;
        public GenerationModule gm;
        private bool manualDisconnection = false;

        public bool Connected
        {
            get { return connected; }
        }

        private bool active;
        public bool Active
        {
            get { return active; }
        }

        public RegistrationModule(GenerationModule generationModule)
        {
            gm = generationModule;
        }

        public void Start()
        {
            active = true;
        }

        public void Stop()
        {
            active = false;
        }

        public void ConnectToServer()
        {
            if (!connected)
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Подключение...");
                    Console.BackgroundColor = ConsoleColor.Black;
                    ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // подключаемся к удаленному хосту
                    socket.Connect(ipPoint);
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Соединение с сервером установлено");
                    Console.BackgroundColor = ConsoleColor.Black;
                    connected = true;
                    SendInitMessage();
                    SendInitParamsMessage();
                    manualDisconnection = false;
                }
                catch (Exception ex)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(ex.Message);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Вы уже подключены к серверу");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public void DisconnectFromServer()
        {
            if (connected)
            {
                // закрываем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Соединение с сервером разорвано");
                Console.BackgroundColor = ConsoleColor.Black;
                connected = false;
                manualDisconnection = true;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Нет подключения к серверу, чтобы его разорвать");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public void SendParam(string param)
        {
            string paramMessage = "&param//" + param;
            byte[] data = Encoding.Unicode.GetBytes(paramMessage);
            try
            {
                socket.Send(data);
            }
            catch
            {
                if (!manualDisconnection)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Отправка данных не удалась, соединение с сервером потеряно");
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                socket.Close();
                connected = false;
                gm.Stop();
                Stop();
            }
        }

        private void SendInitMessage()
        {
            string message = "&init//reg";
            byte[] data = Encoding.Unicode.GetBytes(message);
            socket.Send(data);
        }

        private void SendInitParamsMessage()
        {
            if (allGeneratingParams == "")
            {
                foreach (var param in gm.allParams)
                {
                    allGeneratingParams += $"//{param.name}##{param.Interval}##{param.MinValue}##{param.MaxValue}";
                }
            }

            string message = "&init_params" + allGeneratingParams;
            byte[] data = Encoding.Unicode.GetBytes(message);
            socket.Send(data);
        }
    }
}
