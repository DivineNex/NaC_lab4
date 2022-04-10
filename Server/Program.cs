﻿using System;
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
        private static List<Thread> clientThreads;
        private static IPEndPoint ipPoint;
        private static Socket listenSocket;
        static int port = 55555;

        static void Main(string[] args)
        {
            Init();
            Start();
        }

        private static void Init()
        {
            ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientThreads = new List<Thread>();
        }

        private static void Start()
        {
            listeningThread = new Thread(() => ListeningThread());
            listeningThread.Start();
            Console.ReadKey();
        }

        private static void SocketThread(Socket handler)
        {
            while (true)
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
                Console.WriteLine(recievedMessage);
            }
        }

        private static void ListeningThread()
        {
            listenSocket.Bind(ipPoint);
            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            while (true)
            {
                try
                {
                    listenSocket.Listen(10);

                    Socket handler = listenSocket.Accept();

                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Клиент {handler.RemoteEndPoint} подключен");
                    Console.BackgroundColor = ConsoleColor.Black;

                    Thread newThread = new Thread(() => SocketThread(handler));
                    newThread.Start();
                    clientThreads.Add(newThread);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}