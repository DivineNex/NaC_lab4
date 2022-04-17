using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Client
{
    public class ChartSerie : Control
    {
        private ClientChart chart;
        public readonly string name;
        private Param param;
        private List<PointF> allPoints;
        public Color color;
        Random random = new Random();
        private double maxValue;
        private double minValue;

        public List<PointF> Points
        {
            get { return allPoints; }
        }

        public ChartSerie(Param param, ClientChart chart)
        {
            //if (param.Interval < chart.timer.Interval)
            //    chart.timer.Interval = param.Interval;
            //if (!chart.timer.Enabled)
            //    chart.timer.Enabled = true;
            if (param.Interval < chart.minInterval)
                chart.minInterval = param.Interval;

            Width = chart.Width - ClientChart.BORDER_THICKNESS * 2;
            Height = chart.drawArea.Height - ClientChart.BORDER_THICKNESS * 2;
            Left = ClientChart.BORDER_THICKNESS;
            Top = ClientChart.BORDER_THICKNESS;
            allPoints = new List<PointF>();
            this.param = param;
            this.chart = chart;
            name = param.Name;

            color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            InitSerieSettingsPanel();
        }

        public void AddPoint(float x, float y)
        {
            //for (int i = 0; i < allPoints.Count; i++)
            //{
            //    PointF interPoint = allPoints[i];
            //    interPoint.Y -= chart.axisYStep;
            //    allPoints[i] = interPoint;
            //}

            ////string time = DateTime.Now.ToString("HH:mm:ss");
            ////if (!chart.timeStamps.Contains(time))
            ////    chart.timeStamps.Add(time);

            if (param.Interval == chart.minInterval)
            {
                foreach (var serie in chart.Series)
                {
                    for (int i = 0; i < serie.Points.Count; i++)
                    {
                        PointF interPoint = serie.Points[i];
                        interPoint.Y -= chart.axisYStep;
                        serie.Points[i] = interPoint;
                    }
                }
            }

            string time = DateTime.Now.ToString("HH:mm:ss");
            if (!chart.timeStamps.Contains(time))
                chart.timeStamps.Add(time);

            float interpolatedX = InterpolateX(param.MinValue, 10, param.MaxValue, chart.drawArea.Width-20, x);

            allPoints.Add(new PointF(interpolatedX, y));
        }

        public void InitSerieSettingsPanel()
        {
            ChartSettingsSeriePanel seriePanel = new ChartSettingsSeriePanel(chart, this);
        }

        private float InterpolateX(float x1, float y1, float x2, float y2, float x)
        {
            return y1 + (y2 - y1) * (x - x1) / (x2 - x1);
        }
    }
}
