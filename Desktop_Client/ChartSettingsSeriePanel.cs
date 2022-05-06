using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Client
{
    public class ChartSettingsSeriePanel : Control
    {
        private Label label;
        private Button buttonSettings;
        private Button buttonClose;
        private ClientChart chart;
        private ChartSerie serie;

        public ChartSettingsSeriePanel(ClientChart chart, ChartSerie serie)
        {
            this.chart = chart;
            this.serie = serie;
            Height = 30;
            Width = chart.SettingsForm.SeriesPanel.Width;
            Top = chart.SettingsForm.SeriesPanel.Controls.Count * Height;
            BackColor = Color.Thistle;
            Parent = chart.SettingsForm.SeriesPanel;
            chart.SettingsForm.SeriesPanel.Controls.Add(this);
            serie.chartSettingsSeriePanel = this;
            Paint += ChartSettingsSeriePanel_Paint;

            InitLabel();
            InitButtons();
        }

        private void ChartSettingsSeriePanel_Paint(object sender, PaintEventArgs e)
        {
            DrawColorCircle(e);
            DrawBorders(e);
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
            buttonClose.Image = Image.FromFile(@"..\..\Res\IconClose.png");
            buttonClose.Click += ButtonClose_Click;
            Controls.Add(buttonClose);

            buttonSettings = new Button();
            buttonSettings.Size = new Size(30, 30);
            buttonSettings.Parent = this;
            buttonSettings.Left = Width - 60;
            buttonSettings.Top = 0;
            buttonSettings.Image = Image.FromFile(@"..\..\Res\IconSettings.png");
            buttonSettings.Click += ButtonSettings_Click;
            Controls.Add(buttonSettings);
        }


        private void DrawColorCircle(PaintEventArgs e)
        {
            Brush brush = new SolidBrush(serie.color);          
            e.Graphics.FillEllipse(brush, Width - 90, 4, 20, 20);
        }

        private void DrawBorders(PaintEventArgs e)
        {
            Pen blackPen = new Pen(Color.DarkGray);
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            e.Graphics.DrawRectangle(blackPen, rect);
        }

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            ChartSerieSettingsForm serieSettingsForm = new ChartSerieSettingsForm(chart.chartManager, serie);
            serieSettingsForm.ShowDialog();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            chart.Series.Remove(serie);
            chart.infoPanel.seriesPanels.Remove(serie.seriePanel);
            serie.seriePanel.Dispose();
            chart.infoPanel.UpdateSeriePanelsLocation();
            chart.SettingsForm.SeriesPanel.Controls.Remove(this);
            chart.SettingsForm.UpdateSeriesPanelsPosition();

            if (chart.Series.Count == 0)
                chart.Refresh();
            Dispose();
        }
    }
}
