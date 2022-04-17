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

        public Panel SeriesPanel
        {
            get { return panel1; }
        }

        public ChartSettingsForm(ClientChart chart)
        {
            InitializeComponent();
            this.chart = chart;
        }

        public void Init()
        {
            InitLabels();
            InitButtons();
            InitTextBoxes();
            InitSeriesPanels();
        }

        private void InitLabels()
        {
            labelTitle.Text = chart.infoPanel.labelTitle.Text;
            labelTitle.Left = 10;
            labelTitle.Top = 5;
        }

        private void InitTextBoxes()
        {
            textBoxEditTitle.Width = labelTitle.Width - 5;
            textBoxEditTitle.Visible = false;
            textBoxEditTitle.Text = chart.infoPanel.labelTitle.Text;
        }

        private void InitButtons()
        {
            buttonEdit.Text = "";
            buttonEdit.Size = new Size(30, 30);
            buttonEdit.Image = Image.FromFile(@"..\..\Res\IconEdit.png");
            buttonEdit.Visible = true;
            buttonEdit.Click += ButtonEdit_Click;

            buttonAcceptEdit.Text = "";
            buttonAcceptEdit.Size = new Size(30, 30);
            buttonAcceptEdit.Image = Image.FromFile(@"..\..\Res\IconAccept.png");
            buttonAcceptEdit.Visible = false;
            buttonAcceptEdit.Click += ButtonAcceptEdit_Click;

            UpdateEditLabelButtonsPosition();
        }

        private void InitSeriesPanels()
        {
            foreach (ChartSerie serie in chart.Series)
            {
                serie.InitSerieSettingsPanel();
            }
        }

        private void ButtonAcceptEdit_Click(object sender, EventArgs e)
        {
            if (textBoxEditTitle.Text.Length > 0)
            {
                chart.infoPanel.labelTitle.Text = textBoxEditTitle.Text;
                labelTitle.Text = chart.infoPanel.labelTitle.Text;
            }
            else
            {
                MessageBox.Show("Название графика не может быть пустым!");
                textBoxEditTitle.Focus();
                return;
            }

            textBoxEditTitle.Visible = false;
            labelTitle.Visible = true;
            UpdateEditLabelButtonsPosition();
            textBoxEditTitle.Width = labelTitle.Width - 5;
            buttonEdit.Visible = true;
            buttonAcceptEdit.Visible = false;
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            labelTitle.Visible = false;
            textBoxEditTitle.Visible = true;
            buttonEdit.Visible = false;
            buttonAcceptEdit.Visible = true;
        }

        private void UpdateEditLabelButtonsPosition()
        {
            buttonEdit.Top = 5;
            buttonAcceptEdit.Top = 5;
            buttonAcceptEdit.Left = labelTitle.Width + 15;
            buttonEdit.Left = labelTitle.Width + 15;
        }

        private void buttonAddSerie_Click(object sender, EventArgs e)
        {
            SeriesCreationForm seriesCreationForm = new SeriesCreationForm(chart);
            seriesCreationForm.ShowDialog();
        }

        public void UpdateSeriesPanelsPosition()
        {
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                panel1.Controls[i].Top = i * 30;
            }
        }
    }
}
