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
    public partial class ConnectionForm : Form
    {
        ConnectionManager cm;

        public ConnectionForm(MainForm mainForm)
        {
            InitializeComponent();
            cm = mainForm.connectionManager;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cm.serverIP = textBox1.Text;
            cm.serverPort = Int32.Parse(textBox2.Text);
            cm.ConnectToServer();
            this.Close();
            this.Dispose();
        }
    }
}
