using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Client
{
    internal class ChartSettingsSeriePanel : Control
    {
        private Label label;
        private Button buttonSettings;
        private Button buttonClose;
        private ClientChart chart;
        private ChartSettingsForm settingsForm;
        private ChartSerie serie;

        public ChartSettingsSeriePanel(ClientChart chart, ChartSettingsForm settingsForm, ChartSerie serie)
        {
            this.chart = chart;
            this.settingsForm = settingsForm;
            this.serie = serie;

            Width = settingsForm.SeriesPanel.Width;
            Height = 30;
            BackColor = Color.Thistle;
            Parent = settingsForm.SeriesPanel;
            settingsForm.SeriesPanel.Controls.Add(this);
            InitLabel();
            InitButtons();
        }

        private void InitLabel()
        {
            label = new Label();
            label.AutoSize = true;
            label.Parent = this;
            label.Left = 10;
            label.Top = 3;
            label.Font = new Font("Arial", 14);
            label.Text = serie.name;

            Controls.Add(label);
        }

        private void InitButtons()
        {
            buttonClose = new Button();
            buttonClose.Size = new Size(30, 30);
            buttonClose.Parent = this;
            buttonClose.Left = Width - 30;
            buttonClose.Top = 0;
            buttonClose.Image = Image.FromFile(@"..\..\Res\IconChartCloseButton.png");
            buttonClose.Click += ButtonClose_Click;
            Controls.Add(buttonClose);

            buttonSettings = new Button();
            buttonSettings.Size = new Size(30, 30);
            buttonSettings.Parent = this;
            buttonSettings.Left = Width - 60;
            buttonSettings.Top = 0;
            buttonSettings.Image = Image.FromFile(@"..\..\Res\IconChartSettingsButton.png");
            buttonSettings.Click += ButtonSettings_Click;
            Controls.Add(buttonSettings);
        }

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            //
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
