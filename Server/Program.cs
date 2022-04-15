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
        private static DataBaseManager dbManager;
        static int port = 55555;
        private static MessageParser parser;
        private static string allGeneratingParams;

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
            dbManager = new DataBaseManager();
            dbManager.Connect();

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
                    newClient.socket = handler;

                    Thread newThread = new Thread(() => SocketThread(handler, ref newClient));
                    newThread.Start();

                    newClient.socketThread = newThread;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void SocketThread(Socket handler, ref Client client)
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
                    eMessageType messageType;

                    //Для разделения мультимессенджинга
                    string[] splittedRecievedMessage = recievedMessage.Split('&');
                    for (int i = 0; i < splittedRecievedMessage.Length; i++)
                    {
                        if (splittedRecievedMessage[i] != "")
                        {
                            parser.ParseMessage(splittedRecievedMessage[i], ref client, out messageType);
                            HandleMessage(splittedRecievedMessage[i], splittedRecievedMessage, messageType, client);                                
                        }
                    }
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
        }

        private static void SendMessageToClient(string message, Client client)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            client.socket.Send(data);
        }

        private static void HandleMessage(string messagePart, string[] allMessage, eMessageType messageType, Client client)
        {
            switch (messageType)
            {
                case eMessageType.not_assigned:
                    break;
                case eMessageType.initMessage:
                    clients.Add(client);

                    if (client.Type != eClientType.Reg_module)
                    {
                        if (allGeneratingParams != null)
                            SendMessageToClient(allGeneratingParams, client);
                    }

                    break;
                case eMessageType.initParamsMessage:
                    for (int j = 0; j < allMessage.Length; j++)
                    {
                        if (allMessage[j] != "" && allMessage[j] != "init//reg" && allMessage[j] != "init//desktop" && allMessage[j] != "init//web")
                        {
                            allGeneratingParams += allMessage[j];
                        }
                    }
                    foreach (var clnt in clients)
                    {
                        if (clnt.Type != eClientType.Reg_module)
                        {
                            SendMessageToClient(allGeneratingParams, clnt);
                        }
                    }

                    break;
                case eMessageType.paramsMessage:
                    for (int j = 0; j < clients.Count; j++)
                    {
                        if (clients[j].Type != eClientType.Reg_module)
                        {
                            SendMessageToClient("&" + messagePart, clients[j]);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}