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
        private ClientChart chart;

        public ChartTimeAxis(ClientChart chart)
        {
            this.chart = chart;
            Parent = chart;
            Width = 60;
            Height = chart.Height - ClientChart.BORDER_THICKNESS * 2 - 60;
            Top = ClientChart.BORDER_THICKNESS + 60;
            Left = ClientChart.BORDER_THICKNESS;
            BackColor = BACKGROUND_COLOR;
            DoubleBuffered = true;

            chart.Controls.Add(this);
            Show();

            Paint += ChartTimeAxis_Paint;
        }

        private void ChartTimeAxis_Paint(object sender, PaintEventArgs e)
        {
            DrawTimeStamps(e);
        }

        private void DrawTimeStamps(PaintEventArgs e)
        {
            if (chart.timeStamps.Count > 0)
            {
                Brush brush = new SolidBrush(Color.Black);
                Font font = new Font(ClientChart.TEXT_FONT_FAMILY, 10);
                PointF stampPoint = new PointF();

                for (int i = 0; i < chart.timeStamps.Count; i++)
                {
                    stampPoint.X = 2;
                    stampPoint.Y = Height - (chart.timeStamps.Count - i) * chart.axisYStep - 5;
                    e.Graphics.DrawString(chart.timeStamps[i], font, brush, stampPoint);
                }
            }
        }
    }
}
