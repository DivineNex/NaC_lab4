using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Client
{
    public class ChartDrawArea : Control
    {
        private ClientChart chart;
        private static readonly Color BACKGROUND_COLOR = SystemColors.Control;

        public ChartDrawArea(ClientChart chart)
        {
            this.chart = chart;
            Parent = chart;
            Width = chart.Width - 60 - ClientChart.BORDER_THICKNESS * 2;
            Height = chart.Height - chart.infoPanel.Height - ClientChart.BORDER_THICKNESS * 2;
            Top = ClientChart.BORDER_THICKNESS + chart.infoPanel.Height;
            Left = ClientChart.BORDER_THICKNESS + chart.timeAxis.Width;
            BackColor = BACKGROUND_COLOR;
            DoubleBuffered = true;

            VScrollBar vScroll = new VScrollBar();
            vScroll.Show();
            vScroll.Size = new Size(15, Height - ClientChart.BORDER_THICKNESS * 2);
            vScroll.Top = ClientChart.BORDER_THICKNESS;
            vScroll.Left = Width - vScroll.Width;
            Controls.Add(vScroll);

            chart.Controls.Add(this);
            Show();
            Paint += ChartDrawArea_Paint;
        }

        private void ChartDrawArea_Paint(object sender, PaintEventArgs e)
        {
            Draw(e);
        }

        private void Draw(PaintEventArgs e)
        {
            DrawGrid(e);

            foreach (ChartSerie serie in chart.Series)
            {
                if (serie.Points.Count != 0)
                {
                    Brush brush = new SolidBrush(serie.color);
                    Pen pen = new Pen(serie.color);
                    pen.Width = 3;

                    if (serie.Points.Count == 1)
                    {
                        e.Graphics.FillEllipse(brush, serie.Points[0].X, serie.Points[0].Y, 3, 3);
                    }
                    else
                    {
                        e.Graphics.DrawLines(pen, serie.Points.ToArray());
                    }

                    DrawSerieTriangle(serie, brush, e);
                }
            }
        }

        private void DrawSerieTriangle(ChartSerie serie, Brush brush, PaintEventArgs e)
        {
            PointF[] arrayForDrawingTriangles = new PointF[3];
            arrayForDrawingTriangles[0].X = serie.Points.Last().X;
            arrayForDrawingTriangles[0].Y = serie.Points.Last().Y - 2;
            arrayForDrawingTriangles[1].X = serie.Points.Last().X - 10;
            arrayForDrawingTriangles[1].Y = serie.Points.Last().Y + 10;
            arrayForDrawingTriangles[2].X = serie.Points.Last().X + 10;
            arrayForDrawingTriangles[2].Y = serie.Points.Last().Y + 10;
            e.Graphics.FillPolygon(brush, arrayForDrawingTriangles);
        }

        private void DrawGrid(PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 210, 210, 210));

            int horLinesCount = Height / chart.axisXStep + 1;
            int vertLinesCount = Width / chart.axisYStep + 1;

            for (int i = 0; i < horLinesCount; i++)
            {
                e.Graphics.DrawLine(pen, new Point(0, Height - (i * chart.axisYStep) - 15),
                                         new Point(Width, Height - (i * chart.axisYStep) - 15));
            }

            for (int i = 0; i < vertLinesCount; i++)
            {
                e.Graphics.DrawLine(pen, new Point(i * chart.axisXStep, 0),
                                         new Point(i * chart.axisXStep, Height));
            }
        }
    }
}
