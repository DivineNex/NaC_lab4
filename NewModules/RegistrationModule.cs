using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NewModules
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
        private MainForm mainForm;

        public bool Connected
        {
            get { return connected; }
        }

        private bool active;
        public bool Active
        {
            get { return active; }
        }

        public RegistrationModule(GenerationModule generationModule, MainForm mainForm)
        {
            gm = generationModule;
            this.mainForm = mainForm;
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
                    mainForm.AddLog("RM: Подключение...");
                    ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // подключаемся к удаленному хосту
                    socket.Connect(ipPoint);
                    mainForm.AddLog("RM: Соединение с сервером установлено");
                    connected = true;
                    SendInitMessage();
                    SendInitParamsMessage();
                    manualDisconnection = false;
                }
                catch (Exception ex)
                {
                    mainForm.AddLog($"RM: {ex.Message}");
                }
            }
            else
            {
                mainForm.AddLog("RM: Вы уже подключены к серверу");
            }
        }

        public void DisconnectFromServer()
        {
            if (connected)
            {
                // закрываем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                mainForm.AddLog("RM: Соединение с сервером разорвано");
                connected = false;
                manualDisconnection = true;
            }
            else
            {
                mainForm.AddLog("RM: Нет подключения с сервером, чтобы его разорвать");
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
                    mainForm.AddLog("RM: Отправка данных не удалась, соединение с сервером потеряно");
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
                    allGeneratingParams += $"//{param.Name}##{param.Interval}##{param.MinValue}##{param.MaxValue}";
                }
            }

            string message = "&init_params" + allGeneratingParams;
            byte[] data = Encoding.Unicode.GetBytes(message);
            socket.Send(data);
        }
    }
}
