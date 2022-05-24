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

        public ClientChart Chart
        {
            get { return chart; }
        }

        public readonly string name;
        public Param param;
        private List<PointF> allPoints;
        public Color color;
        Random random = new Random();
        public ChartInfoPanelSerie seriePanel;
        public ChartSettingsSeriePanel chartSettingsSeriePanel;
        public float maxValue;
        public float minValue;
        public float intervalCoeff;
        public int lineThickness = 1;

        public List<PointF> Points
        {
            get { return allPoints; }
        }

        public ChartSerie(Param param, ClientChart chart)
        {
            if (param.Interval < chart.minInterval)
                chart.minInterval = param.Interval;

            intervalCoeff = param.Interval / 1000;

            Width = chart.Width - ClientChart.BORDER_THICKNESS * 2;
            Height = chart.drawArea.Height - ClientChart.BORDER_THICKNESS * 2;
            Left = ClientChart.BORDER_THICKNESS;
            Top = ClientChart.BORDER_THICKNESS;
            allPoints = new List<PointF>();
            this.param = param;
            this.chart = chart;
            name = param.Name;
            minValue = param.MinValue;
            maxValue = param.MaxValue;
            color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            Parent = chart;
            seriePanel = new ChartInfoPanelSerie(this, chart.infoPanel);
            
            InitSerieSettingsPanel();
        }

        public void AddPoint(float x, float y)
        {
            float interpolatedX = InterpolatePointX(param.MinValue, 20, param.MaxValue, chart.drawArea.Width - 35, x);
            allPoints.Add(new PointF(interpolatedX, y));
        }

        public void InitSerieSettingsPanel()
        {
            ChartSettingsSeriePanel seriePanel = new ChartSettingsSeriePanel(chart, this);
        }

        private float InterpolatePointX(float x1, float y1, float x2, float y2, float x)
        {
            return y1 + (y2 - y1) * (x - x1) / (x2 - x1);
        }
    }
}
