using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

namespace Server
{
    class Program
    {
        private static Thread listeningThread;
        private static List<Client> clients;
        private static IPEndPoint ipPoint;
        private static Socket listenSocket;
        static int port = 55555;
        private static MessageParser parser;

        static void Main(string[] args)
        {
            Init();
            Start();
        }

        private static void Init()
        {
            ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clients = new List<Client>();
            parser = new MessageParser();
        }

        private static void Start()
        {
            listeningThread = new Thread(() => ListeningThread());
            listeningThread.Start();
            Console.ReadKey();
        }

        private static void ListeningThread()
        {
            listenSocket.Bind(ipPoint);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Сервер запущен. Ожидание подключений...");
            Console.BackgroundColor = ConsoleColor.Black;

            while (true)
            {
                try
                {
                    listenSocket.Listen(10);

                    Socket handler = listenSocket.Accept();

                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Клиент {handler.RemoteEndPoint} подключен");
                    Console.BackgroundColor = ConsoleColor.Black;

                    Client newClient = new Client();
                    newClient.ip_port = handler.RemoteEndPoint.ToString();

                    Thread newThread = new Thread(() => SocketThread(handler, newClient));
                    newThread.Start();

                    newClient.socketThread = newThread;

                    //ТРЕБУЕТСЯ ПОЛНАЯ СИСТЕМА ТИПИЗАЦИИ КЛИЕНТОВ
                    //newClient.type = eClientType.Reg_module;

                    clients.Add(newClient);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void SocketThread(Socket handler, Client client)
        {
            while (true)
            {
                try
                {
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];

                    do
                    {
                        bytes = handler.Receive(data);

                        if (bytes <= 0)
                        {
                            throw new SocketException();
                        }

                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    string recievedMessage = builder.ToString();
                    parser.ParseMessage(recievedMessage, client);
                }
                catch
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Клиент {client.ip_port} отключен");
                    Console.BackgroundColor = ConsoleColor.Black;
                    handler.Close();
                    clients.Remove(client);
                    break;
                }
            }

            //Старая система получения сообщений. Пока пусть тут будет

            //while (client.connected)
            //{
            //    StringBuilder builder = new StringBuilder();
            //    int bytes = 0;
            //    byte[] data = new byte[256];

            //    do
            //    {
            //        bytes = handler.Receive(data);
            //        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            //    }
            //    while (handler.Available > 0);

            //    string recievedMessage = builder.ToString();
            //    Console.WriteLine(recievedMessage);
            //}
        }
    }
}