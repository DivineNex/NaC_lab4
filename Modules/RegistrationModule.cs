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
        private bool connected = false;

        private bool active;
        public bool Active
        {
            get { return active; }
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
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(ex.Message);
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
            //string sendString = $"{param.name}|{param.value}";
            byte[] data = Encoding.Unicode.GetBytes(param);
            socket.Send(data);
        }
    }
}
