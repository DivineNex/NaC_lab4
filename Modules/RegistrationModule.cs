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
        private string address = "172.20.10.2"; // адрес сервера
        private Socket socket;
        private IPEndPoint ipPoint;
        private bool connected = false;

        //ТЕСТ, УДАЛИТЬ ПОТОМ
        Stopwatch sw = new Stopwatch();
        TimeSpan ts = new TimeSpan();

        public List<ParamForSend> buffer;
        private int sendInterval;
        private Timer timer;

        private bool active;
        public bool Active
        {
            get { return active; }
        }

        public RegistrationModule()
        {
            buffer = new List<ParamForSend>();
            timer = new Timer();
            timer.Enabled = false;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //SendDataToServer(buffer);
            
            buffer.Clear();
        }

        public void Start()
        {
            active = true;
            timer.Enabled = true;
        }

        public void Stop()
        {
            active = false;
            timer.Enabled = false;
        }

        public void SetSendInterval(int interval)
        {
            sendInterval = interval;
            timer.Interval = sendInterval;
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

        private void SendDataToServer(List<ParamForSend> buffer)
        {
            string stringData = "";

            for (int i = 0; i < buffer.Count; i++)
            {
                //Параметры разделяются слэшем, а имя и значение параметра разделяется символом |
                if (i != buffer.Count - 1)
                    stringData += $"{buffer[i].name}|{buffer[i].value}/";
                else
                    stringData += $"{buffer[i].name}|{buffer[i].value}";
            }
            byte[] data = Encoding.Unicode.GetBytes(stringData);
            socket.Send(data);
        }

        public void SendParam(ParamForSend param)
        {
            string sendString = $"{param.name}|{param.value}";
            byte[] data = Encoding.Unicode.GetBytes(sendString);
            socket.Send(data);
        }
    }
}
