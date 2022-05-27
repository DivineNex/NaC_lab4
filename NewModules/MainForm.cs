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
using System.Windows.Threading;

namespace NewModules
{
    internal partial class MainForm : Form
    {
        private ChartManager chartManager;
        private static RegistrationModule registrationModule;
        private static GenerationModule generationModule;

        public List<Param> allParams;

        public TabPage tabPage
        {
            get { return tabPage1; }
        }

        public ConnectionManager connectionManager;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
            Login();
        }

        private void Init()
        {
            generationModule = new GenerationModule();
            registrationModule = new RegistrationModule(generationModule, this);

            generationModule.LoadAndParseParams();

            foreach (var par in generationModule.allParams)
            {
                par.rm = registrationModule;
            }

            AddLog("Инициализация прошла успешно");

            connectionManager = new ConnectionManager(this);
            chartManager = new ChartManager(this);
            allParams = new List<Param>();

            //автоконнект при запуске
            connectionManager.serverIP = "127.0.0.1";
            connectionManager.serverPort = 55555;
            connectionManager.ConnectToServer();
        }

        private void Login()
        {
            AuthAndRegForm authForm = new AuthAndRegForm(this);
            authForm.ShowDialog();
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
            Action action = () => {
                if (connected)
                {
                    button1.Text = "Disconnect";
                    toolStripStatusLabel2.Text = "установлено";
                    toolStripStatusLabel2.BackColor = Color.LimeGreen;
                    registrationModule.ConnectToServer();
                }
                else
                {
                    button1.Text = "Connect";
                    toolStripStatusLabel2.Text = "не установлено";
                    toolStripStatusLabel2.BackColor = Color.Tomato;
                    registrationModule.DisconnectFromServer();
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

        public void SetParamValue(string time, string paramName, float value)
        {
            foreach (var par in allParams)
            {
                if (par.Name == paramName)
                {
                    par.UpdateValue(time, value);

                    foreach (var serie in par.assignedSeries)
                    {
                        if (serie.Points.Count == 0)
                        {
                            float y = serie.Chart.lastAddedPointY;
                            serie.AddPoint(value, y);
                            serie.Chart.lastAddedPointY = y;
                        }
                        else
                        {
                            float y = serie.Points.Last().Y + serie.intervalCoeff * ClientChart.AXIS_Y_DEFAULT_STEP;
                            serie.AddPoint(value, y);
                            serie.Chart.lastAddedPointY = y;
                        }
                    }

                    break;
                }
            }
            UpdateTable();
        }

        private void UpdateTable()
        {
            Action action = () =>
            {
                textBox1.Clear();
                foreach (var par in allParams)
                {
                    textBox1.Text += $"\r\n{par.Name}\t{par.Value}";
                }
            };

            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chartManager.CreateChart(null, eChartOrientation.vertical);
        }

        private void logsRichTextBox_TextChanged(object sender, EventArgs e)
        {
            logsRichTextBox.SelectionStart = logsRichTextBox.Text.Length;
            logsRichTextBox.ScrollToCaret();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((logsRichTextBox.Visible) == true)
            {
                logsRichTextBox.Visible = false;
                tabControl1.Width += textBox1.Width - 15;
            }
            else if ((logsRichTextBox.Visible) == false)
            {
                tabControl1.Width -= textBox1.Width - 15;
                logsRichTextBox.Visible = true;
            }
                

            if ((label1.Visible) == true)
            {
                label1.Visible = false;
            }
            else label1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (generationModule.Active == false)
            {
                if (registrationModule.Connected)
                {
                    generationModule.Start();
                    AddLog("Модуль генерации запущен");
                }
                else
                {
                    AddLog("Сперва подключитесь к серверу");
                }
            }
            else
            {
                AddLog("Модуль генерации уже запущен");
            }
        }
    }
}
