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
        private List<Point> allPoints;
        public Color color;
        Random random = new Random();

        public ChartSerie(Param param, ClientChart chart)
        {
            allPoints = new List<Point>();
            this.param = param;
            this.chart = chart;
            name = param.name;
            color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            InitSerieSettingsPanel();
        }

        public void AddPoint(int x, int y)
        {
            allPoints.Add(new Point(x, y));
        }

        public void UpdateValue()
        {
            //Нужно ли?
        }

        public void InitSerieSettingsPanel()
        {
            ChartSettingsSeriePanel seriePanel = new ChartSettingsSeriePanel(chart, this);
        }
    }
}
