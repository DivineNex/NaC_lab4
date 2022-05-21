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
    public partial class AuthAndRegForm : Form
    {
        private MainForm mainForm;

        public AuthAndRegForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.connectionManager.ConnectToServer();

            string result;
            mainForm.connectionManager.SendAuthMessage(textBox1.Text, textBox2.Text, out result);
            textBox3.Text = result;

            if (result == "Авторизация выполнена успешно")
            {
                Close();
            }
        }
    }
}
