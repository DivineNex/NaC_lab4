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
    public partial class ChartSettingsForm : Form
    {
        private ClientChart chart;
        public string chartName;

        public ChartSettingsForm(ClientChart chart)
        {
            InitializeComponent();
            this.chart = chart;
        }

        public void Init()
        {
            chartName = chart.Name;
            InitLabels();
            InitButtons();
            InitTextBoxes();
        }

        private void InitLabels()
        {
            label1.Text = chartName;
            label1.Top = 5;
        }

        private void InitTextBoxes()
        {
            textBox1.Width = label1.Width;
        }

        private void InitButtons()
        {
            buttonEdit.Text = "";
            buttonEdit.Size = new System.Drawing.Size(30, 30);
            buttonEdit.Image = Image.FromFile(@"..\..\Res\IconEdit.png");
            buttonEdit.Top = 5;
            buttonEdit.Left = label1.Width + 15;
            buttonEdit.Click += ButtonEdit_Click;

            buttonAcceptEdit.Text = "";
            buttonAcceptEdit.Size = new System.Drawing.Size(30, 30);
            buttonAcceptEdit.Image = Image.FromFile(@"..\..\Res\IconAccept.png");
            buttonAcceptEdit.Visible = false;
            buttonAcceptEdit.Top = 5;
            buttonAcceptEdit.Left = label1.Width + 15;
            buttonAcceptEdit.Click += ButtonAcceptEdit_Click;
        }

        private void ButtonAcceptEdit_Click(object sender, EventArgs e)
        {
            buttonEdit.Visible = true;
            buttonAcceptEdit.Visible = false;
            label1.Text = textBox1.Text;
            chartName = label1.Text;
            textBox1.Visible = false;
            label1.Visible = true;
            chart.Title = chartName;
            buttonAcceptEdit.Left = label1.Width + 15;
            buttonEdit.Left = label1.Width + 15;
            textBox1.Width = label1.Width;
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            textBox1.Visible = true;
            buttonEdit.Visible = false;
            buttonAcceptEdit.Visible = true;
        }
    }
}
