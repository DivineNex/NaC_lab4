using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Client
{
    internal class ChartInfoPanel : Control
    {
        public static readonly Color BACKGROUND_COLOR = Color.DarkGray;

        public ChartInfoPanel(ClientChart chart)
        {
            Parent = chart;
            Width = chart.Width - ClientChart.BORDER_THICKNESS*2;
            Height = 50;
            Top = ClientChart.BORDER_THICKNESS;
            Left = ClientChart.BORDER_THICKNESS;
            BackColor = BACKGROUND_COLOR;
            chart.Controls.Add(this);
            Show();
        }
    }
}
