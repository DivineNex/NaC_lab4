using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        private void UpdateData()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateConnectionForm();
        }

        private void CreateConnectionForm()
        {
            ConnectionForm connectionForm = new ConnectionForm(this);
            connectionForm.Show();
        }

        public void AddLog(string message)
        {
            logsRichTextBox.Text += message + "\r\n";
        }

        public void SetConnectionStatus(bool connected)
        {
            if (connected)
            {
                toolStripStatusLabel2.Text = "установлено";
                toolStripStatusLabel2.BackColor = Color.LimeGreen;
            }
            else
            {
                toolStripStatusLabel2.Text = "не установлено";
                toolStripStatusLabel2.BackColor = Color.Tomato;
            }
        }
    }
}
