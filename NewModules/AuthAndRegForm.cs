using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewModules
{
    internal partial class AuthAndRegForm : Form
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

        private void AuthAndRegForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainForm.connectionManager.loggedStatus != "logged")
            {
                mainForm.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.connectionManager.ConnectToServer();

            string result;
            mainForm.connectionManager.SendRegistrationMessage(textBox6.Text, textBox5.Text, out result);
            textBox4.Text = result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox5.PasswordChar == '\0')
            {
                textBox5.PasswordChar = '*';
                button3.Text = "Show";
            }
            else
            {
                textBox5.PasswordChar = '\0';
                button3.Text = "Hide";
            }
        }
    }
}
