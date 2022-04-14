using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Client
{
    internal class ChartSerie : Control
    {
        private ClientChart chart;
        public readonly string name;
        private Param param;
        private List<Point> allPoints;
        private ChartSettingsForm settingsForm;

        public ChartSerie(Param param, ClientChart chart, ChartSettingsForm settingsForm)
        {
            this.settingsForm = settingsForm;
            allPoints = new List<Point>();
            this.param = param;
            this.chart = chart;
            name = param.name;
            ChartSettingsSeriePanel seriePanel = new ChartSettingsSeriePanel(chart, settingsForm, this);
        }

        public void AddPoint(int x, int y)
        {
            allPoints.Add(new Point(x, y));
        }

        public void UpdateValue()
        {
            //Нужно ли?
        }
    }
}
