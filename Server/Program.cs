using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        private static IPEndPoint ipPoint;
        private static Socket listenSocket;
        static int port = 55555;
        static Socket handler;
        static void Main(string[] args)
        {
            Init();
            Start();
        }

        private static void Init()
        {
            ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private static void Start()
        {
            try
            {
                listenSocket.Bind(ipPoint);

                listenSocket.Listen(10);

                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                handler = listenSocket.Accept();

                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Клиент {handler.RemoteEndPoint} подключен");
                Console.BackgroundColor = ConsoleColor.Black;

                while (true)
                {
                    GetData();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void GetData()
        {
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            byte[] data = new byte[256];

            do
            {
                bytes = handler.Receive(data);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (handler.Available > 0);

            string recievedMessage = builder.ToString();
            string[] receivedParams = recievedMessage.Split('/');
            for (int i = 0; i < receivedParams.Length; i++)
            {
                Console.Write($"{receivedParams[i]}\t");
            }
            Console.WriteLine();
        }
    }
}