using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Client
{
    public class ChartInfoPanel : Control
    {
        private static readonly Color BACKGROUND_COLOR = Color.DarkGray;

        public Label labelTitle;

        public ChartInfoPanel(ClientChart chart)
        {
            Parent = chart;
            Width = chart.Width - 60 - ClientChart.BORDER_THICKNESS*2;
            Height = 60;
            Top = ClientChart.BORDER_THICKNESS;
            Left = ClientChart.BORDER_THICKNESS + 60;
            BackColor = BACKGROUND_COLOR;

            labelTitle = new Label();
            labelTitle.Text = "График #" + (chart.chartManager.allCharts.Count + 1).ToString();
            labelTitle.Font = new Font(ClientChart.TEXT_FONT_FAMILY, 12);
            labelTitle.ForeColor = ClientChart.TEXT_COLOR;
            labelTitle.Location = new Point(20, 5);
            labelTitle.AutoSize = true;
            labelTitle.BackColor = BACKGROUND_COLOR;

            chart.Controls.Add(this);
            Show();
            Controls.Add(labelTitle);
            labelTitle.Show();
        }
    }
}
