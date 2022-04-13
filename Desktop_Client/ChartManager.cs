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
            Parent = mainForm.tabPage;
            Show();
            BackColor = Color.DarkGray;
            UpdateSize();
            Paint += ChartManager_Paint;
        }

        private void ChartManager_Paint(object sender, PaintEventArgs e)
        {
            DrawBorders(e);
        }

        public void CreateChart(Param param, eChartOrientation chartType)
        {
            ClientChart newChart = new ClientChart(this, chartType);
            newChart.Parent = this;
            Controls.Add(newChart);
            allCharts.Add(newChart);
            Controls[Controls.Count-1].BringToFront();
        }

        public void DeleteChart(ClientChart chart)
        {
            allCharts.Remove(chart);
        }

        public void UpdateChart()
        {

        }

        private void UpdateSize()
        {
            Width = mainForm.tabPage.Width;
            Height = mainForm.tabPage.Height;
        }

        public void DrawBorders(PaintEventArgs e)
        {
            Pen blackPen = new Pen(Color.FromArgb(255, 80, 80, 80), 5);
            Rectangle rect = new Rectangle(0, 0, Width-1, Height-1);
            e.Graphics.DrawRectangle(blackPen, rect);
        }
    }
}
