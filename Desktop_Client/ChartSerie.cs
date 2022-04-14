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
        public readonly string name;
        private Param param;
        private List<Point> allPoints;

        public ChartSerie(Param param)
        {
            allPoints = new List<Point>();
            //задать имя в зависимости от названия параметра
        }

        public void AddPoint(int x, int y)
        {
            allPoints.Add(new Point(x, y));
        }
    }
}
