using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Client
{
    public partial class MainForm : Form
    {
        public ConnectionManager connectionManager;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            connectionManager = new ConnectionManager(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!connectionManager.connected)
            {
                CreateConnectionForm();
            }
            else
            {
                connectionManager.DisconnectFromServer();
            }
        }

        private void CreateConnectionForm()
        {
            ConnectionForm connectionForm = new ConnectionForm(this);
            connectionForm.Show();
        }

        public void AddLog(string message)
        {
            //При такой реализации изменение из другого потока ломало это
            //logsRichTextBox.Text += message + "\r\n";

            Action action = () => logsRichTextBox.Text += message + "\r\n";
            if (logsRichTextBox.InvokeRequired)
            {
                logsRichTextBox.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public void SetConnectionStatus(bool connected)
        {
            //if (connected)
            //{
            //    button1.Text = "Disconnect";
            //    toolStripStatusLabel2.Text = "установлено";
            //    toolStripStatusLabel2.BackColor = Color.LimeGreen;
            //}
            //else
            //{
            //    button1.Text = "Connect";
            //    toolStripStatusLabel2.Text = "не установлено";
            //    toolStripStatusLabel2.BackColor = Color.Tomato;
            //}

            Action action = () => {
                if (connected)
                {
                    button1.Text = "Disconnect";
                    toolStripStatusLabel2.Text = "установлено";
                    toolStripStatusLabel2.BackColor = Color.LimeGreen;
                }
                else
                {
                    button1.Text = "Connect";
                    toolStripStatusLabel2.Text = "не установлено";
                    toolStripStatusLabel2.BackColor = Color.Tomato;
                }
            };

            if (button1.InvokeRequired && statusStrip1.InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (connectionManager.connected)
            {
                connectionManager.DisconnectFromServer();
            }
        }
    }
}
