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
        private static readonly Color BACKGROUND_COLOR = Color.DarkGray;
        private ClientChart chart;

        public ChartTimeAxis(ClientChart chart)
        {
            this.chart = chart;
            Parent = chart;
            Width = 60;
            Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left;
            Height = chart.Height - ClientChart.BORDER_THICKNESS * 2 - 90;
            Top = ClientChart.BORDER_THICKNESS + 90;
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
                List<PointF> stampPoints = new List<PointF>();

                PointF tsPoint = new PointF();
                tsPoint.X = 2;
                tsPoint.Y = Height - 22;
                stampPoints.Add(tsPoint);

                for (int i = chart.timeStamps.Count - 1; i > 0; i--)
                {
                    tsPoint.X = 2;
                    tsPoint.Y = stampPoints[0].Y - ClientChart.AXIS_Y_DEFAULT_STEP * chart.zoomCoeff;
                    stampPoints.Insert(0, tsPoint);
                }

                for (int i = 0; i < chart.timeStamps.Count; i++)
                {
                    e.Graphics.DrawString(chart.timeStamps[i], font, brush, stampPoints[i]);
                }
            }
        }
    }
}
