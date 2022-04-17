using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Client
{
    public class ChartInfoPanelSerie : Control
    {
        private Label serieTitle;
        private Label minValueLabel;
        private Label maxValueLabel;

        public ChartInfoPanelSerie(ChartSerie serie, ChartInfoPanel infoPanel)
        {
            Parent = infoPanel;
            infoPanel.Height += 20;
            Width = infoPanel.Width - 20;
            Height = 20;    
            Top = infoPanel.seriesPanels.Count * Height + 20;
            Left = 10;
            BackColor = Color.FromArgb(255, 190, 190, 190);
            DoubleBuffered = true;

            serieTitle = new Label();
            serieTitle.Text = serie.name;
            serieTitle.Font = new Font(ClientChart.TEXT_FONT_FAMILY, 10);
            serieTitle.ForeColor = serie.color;
            serieTitle.AutoSize = true;
            serieTitle.TextAlign = ContentAlignment.MiddleCenter;
            serieTitle.Location = new Point(infoPanel.Width/2 - serieTitle.Width/2 - 40, 2);
            serieTitle.BackColor = Color.FromArgb(255, 190, 190, 190);

            minValueLabel = new Label();
            minValueLabel.Text = serie.minValue.ToString();
            minValueLabel.Font = new Font(ClientChart.TEXT_FONT_FAMILY, 10);
            minValueLabel.ForeColor = serie.color;
            minValueLabel.AutoSize = true;
            minValueLabel.TextAlign = ContentAlignment.MiddleCenter;
            minValueLabel.Location = new Point(0, 2);
            minValueLabel.BackColor = Color.FromArgb(255, 190, 190, 190);

            maxValueLabel = new Label();
            maxValueLabel.Text = serie.maxValue.ToString();
            maxValueLabel.Font = new Font(ClientChart.TEXT_FONT_FAMILY, 10);
            maxValueLabel.ForeColor = serie.color;
            maxValueLabel.Size = new Size(Width, Height);
            maxValueLabel.Top = 2;
            maxValueLabel.TextAlign = ContentAlignment.MiddleRight;
            maxValueLabel.BackColor = Color.FromArgb(255, 190, 190, 190);

            Controls.Add(serieTitle);
            Controls.Add(minValueLabel);
            Controls.Add(maxValueLabel);
            infoPanel.Controls.Add(this);
            infoPanel.seriesPanels.Add(this);
            Show();
        }
    }
}
