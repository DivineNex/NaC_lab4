using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Client
{
    public class ChartTimeAxis : Control
    {
        private static readonly Color BACKGROUND_COLOR = Color.PeachPuff;

        public ChartTimeAxis(ClientChart chart)
        {
            Parent = chart;
            Width = 60;
            Height = chart.Height - ClientChart.BORDER_THICKNESS * 2 - 60;
            Top = ClientChart.BORDER_THICKNESS + 60;
            Left = ClientChart.BORDER_THICKNESS;
            BackColor = BACKGROUND_COLOR;

            chart.Controls.Add(this);
            Show();
        }
    }
}
