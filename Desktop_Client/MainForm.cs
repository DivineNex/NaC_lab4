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

namespace Desktop_Client
{
    public partial class MainForm : Form
    {
        public static List<string> allGettingParamsNames;
        private ChartManager chartManager;
        private List<Param> allParams;

        public List<Param> AllParams
        {
            get { return allParams; }
        }

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
        }

        private void Init()
        {
            connectionManager = new ConnectionManager(this);
            allGettingParamsNames = new List<string>();
            chartManager = new ChartManager(this);
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

        public void InitializeParamsArray()
        {
            allParams = new List<Param>();

            foreach (var paramName in allGettingParamsNames)
            {
                Param newParam = new Param() { name = paramName };
                newParam.UpdateValue(0);
                allParams.Add(newParam);
            }
        }

        public void SetParamValue(string paramName, float value)
        {
            foreach (var par in allParams)
            {
                if (par.name == paramName)
                {
                    par.UpdateValue(value);

                    foreach (var serie in par.assignedSeries)
                    {
                        serie.AddPoint(value, serie.Height - 10);
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
                    textBox1.Text += $"\r\n{par.name}\t{par.Value}";
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
                tabControl1.Width = 1121;
                tabControl1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                chartManager.Width = 1121;
                chartManager.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
            else
                if ((logsRichTextBox.Visible) == false)
            {
                logsRichTextBox.Visible = true;
                tabControl1.Width = 824;
                chartManager.Width = 824;
            }
                

            if ((label1.Visible) == true)
            {
                label1.Visible = false;
            }
            else label1.Visible = true;
        }

        private void tabControl1_Resize(object sender, EventArgs e)
        {        
            
        }
    }
}
