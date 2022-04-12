using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace Desktop_Client
{
    class ChartManager : Control
    {
        private MainForm mainForm;
        
        public List<ClientChart> allCharts;



        public ChartManager(MainForm mainForm)
        {
            this.mainForm = mainForm;
            allCharts = new List<ClientChart>();
        }
        public void CreateChart(Param param, eChartTypes chartType)
        {
            ClientChart newChart = new ClientChart();
            allCharts.Add(newChart);
        }
        public void DeleteChart()
        {

        }
        public void UpdateChart()
        {

        }
    }
}
