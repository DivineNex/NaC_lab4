using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Desktop_Client
{
    public class ConnectionManager
    {
        MainForm mainForm;
        public string serverIP;
        public int serverPort;
        public Socket serverSocket;
        private IPEndPoint ipPoint;
        public bool connected = false;
        private Thread serverSocketThread;
        private MessageParser messageParser;
        public bool manualDisconnection = false;
        private bool formClosingDisconnection = false;

        public ConnectionManager(MainForm mainForm)
        {
            this.mainForm = mainForm;
            messageParser = new MessageParser(mainForm);
        }

        public void ConnectToServer()
        {
            if (!connected)
            {
                try
                {
                    mainForm.AddLog("Подключение...");
                    ipPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
                    serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // подключаемся к удаленному хосту
                    serverSocket.Connect(ipPoint);
                    mainForm.AddLog($"Соединение с сервером {serverSocket.RemoteEndPoint} установлено");
                    serverSocketThread = new Thread(() => SocketThread());
                    serverSocketThread.Start();
                    SendInitMessage();

                    connected = true;
                    mainForm.SetConnectionStatus(true);
                    manualDisconnection = false;
                }
                catch (Exception ex)
                {
                    mainForm.AddLog(ex.Message);
                }
            }
            else
            {
                mainForm.AddLog("Подключение уже установлено");
            }
        }

        public void DisconnectFromServer(bool formClosing)
        {
            formClosingDisconnection = formClosing;

            if (formClosing)
            {
                serverSocket.Shutdown(SocketShutdown.Both);
                serverSocket.Close();
                connected = false;
            }
            else
            {
                mainForm.AddLog($"Соединение с сервером {serverSocket.RemoteEndPoint} разорвано");
                serverSocket.Shutdown(SocketShutdown.Both);
                serverSocket.Close();

                connected = false;
                manualDisconnection = true;
                mainForm.SetConnectionStatus(false);
            }
        }

        private void SendInitMessage()
        {
            string message = "&init//desktop";
            byte[] data = Encoding.Unicode.GetBytes(message);
            serverSocket.Send(data);
        }

        private void SocketThread()
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
                        bytes = serverSocket.Receive(data);

                        if (bytes <= 0)
                        {
                            throw new SocketException();
                        }

                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (serverSocket.Available > 0);

                    string recievedMessage = builder.ToString();

                    //Для разделения мультимессенджинга
                    string[] splittedRecievedMessage = recievedMessage.Split('&');
                    for (int i = 0; i < splittedRecievedMessage.Length; i++)
                    {
                        if (splittedRecievedMessage[i] != "")
                        {
                            messageParser.ParseMessage(splittedRecievedMessage[i]);
                        }
                    }
                }
                catch
                {
                    //Если отключение не по собственному желанию, то вывести лог о том, что сервер упал
                    if (!manualDisconnection && !formClosingDisconnection)
                        mainForm.AddLog($"Соединение с сервером {serverSocket.RemoteEndPoint} потеряно");
                    if (!formClosingDisconnection)
                        mainForm.SetConnectionStatus(false);
                    connected = false;
                    serverSocket.Close();
                    break;
                }
            }
        }
    }
}
