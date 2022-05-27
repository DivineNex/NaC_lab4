using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewModules
{
    internal class ChartDrawArea : Control
    {
        private ClientChart chart;
        private static readonly Color BACKGROUND_COLOR = SystemColors.Control;
        private Size size;

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.Style |= 0x00200000;  // Turn on WS_SIZEBOX
                return cp;
            }
        }

        public ChartDrawArea(ClientChart chart)
        {
            this.chart = chart;
            Parent = chart;
            Width = chart.Width - 60 - ClientChart.BORDER_THICKNESS * 2;
            Height = chart.Height - chart.infoPanel.Height - ClientChart.BORDER_THICKNESS * 2;
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Top = ClientChart.BORDER_THICKNESS + chart.infoPanel.Height;
            Left = ClientChart.BORDER_THICKNESS + chart.timeAxis.Width;
            BackColor = BACKGROUND_COLOR;
            DoubleBuffered = true;

            //VScrollBar vScroll = new VScrollBar();
            //vScroll.Width = 15;
            //vScroll.Dock = DockStyle.Right;
            //Controls.Add(vScroll);
            //vScroll.Show();

            chart.Controls.Add(this);
            Show();
            Paint += ChartDrawArea_Paint;
            Resize += ChartDrawArea_Resize;
            size = Size;
            MinimumSize = new Size(50, 200);
        }

        private void ChartDrawArea_Resize(object sender, EventArgs e)
        {
            List<PointF> interpolatedPoints = new List<PointF>();
            foreach (ChartSerie serie in chart.Series)
            {
                foreach (PointF point in serie.Points)
                {
                    float interpolatedX = (point.X * Width) / size.Width;

                    interpolatedPoints.Add(new PointF(interpolatedX, point.Y));
                }

                serie.Points.Clear();
                serie.Points.AddRange(interpolatedPoints);
                interpolatedPoints.Clear();
            }

            size = Size;
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
                    Pen pen = new Pen(serie.color, serie.lineThickness);

                    if (serie.Points.Count == 1)
                    {
                        e.Graphics.FillEllipse(brush, serie.Points[0].X, serie.Points[0].Y, 3, 3);
                    }
                    else
                    {
                        List<PointF> interpolatedPoints = new List<PointF>();

                        foreach (var point in serie.Points)
                        {
                            PointF newPoint = new PointF(point.X, Height - 15 - (chart.lastAddedPointY - point.Y)*chart.zoomCoeff);
                            interpolatedPoints.Add(newPoint);
                        }

                        e.Graphics.DrawLines(pen, interpolatedPoints.ToArray());
                        //e.Graphics.DrawCurve(pen, interpolatedPoints.ToArray());
                    }

                    DrawSerieTriangle(serie, brush, e);
                }
            }
        }

        private void DrawSerieTriangle(ChartSerie serie, Brush brush, PaintEventArgs e)
        {
            PointF[] arrayForDrawingTriangles = new PointF[3];

            arrayForDrawingTriangles[0].X = serie.Points.Last().X;
            arrayForDrawingTriangles[0].Y = Height - 15;
            arrayForDrawingTriangles[1].X = serie.Points.Last().X - 10;
            arrayForDrawingTriangles[1].Y = Height;
            arrayForDrawingTriangles[2].X = serie.Points.Last().X + 10;
            arrayForDrawingTriangles[2].Y = Height;

            e.Graphics.FillPolygon(brush, arrayForDrawingTriangles);
        }

        private void DrawGrid(PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 210, 210, 210));

            int horLinesCount = Height / ClientChart.AXIS_X_DEFAULT_STEP + 1;
            int vertLinesCount = Width / ClientChart.AXIS_Y_DEFAULT_STEP + 1;

            for (int i = 0; i < horLinesCount; i++)
            {
                e.Graphics.DrawLine(pen, new Point(0, Height - (i * ClientChart.AXIS_Y_DEFAULT_STEP) - 15),
                                         new Point(Width, Height - (i * ClientChart.AXIS_Y_DEFAULT_STEP) - 15));
            }

            for (int i = 0; i < vertLinesCount; i++)
            {
                e.Graphics.DrawLine(pen, new Point(i * ClientChart.AXIS_X_DEFAULT_STEP, 0),
                                         new Point(i * ClientChart.AXIS_X_DEFAULT_STEP, Height));
            }
        }
    }
}
