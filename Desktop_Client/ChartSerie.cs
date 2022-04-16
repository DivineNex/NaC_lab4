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

        public List<PointF> Points
        {
            get { return allPoints; }
        }


        public ChartSerie(Param param, ClientChart chart)
        {
            Width = chart.Width - ClientChart.BORDER_THICKNESS * 2;
            Height = chart.Height - ClientChart.BORDER_THICKNESS * 2;
            Left = ClientChart.BORDER_THICKNESS;
            Top = ClientChart.BORDER_THICKNESS;
            allPoints = new List<PointF>();
            this.param = param;
            this.chart = chart;
            name = param.name;
            color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            InitSerieSettingsPanel();
        }

        public void AddPoint(float x, float y)
        {
            for (int i = 0; i < allPoints.Count; i++)
            {
                PointF interPoint = allPoints[i];
                interPoint.Y -= 20;
                allPoints[i] = interPoint;
            }

            allPoints.Add(new PointF(x, y));
        }

        public void InitSerieSettingsPanel()
        {
            ChartSettingsSeriePanel seriePanel = new ChartSettingsSeriePanel(chart, this);
        }
    }
}
