using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace NewModules
{
    internal class ChartManager : Control
    {
        private MainForm mainForm;
        private Timer timer;
        private const int MILLISECONDS_PER_UPDATE = 100;
        private List<ClientChart> allCharts;

        public List<ClientChart> AllCharts
        {
            get { return allCharts; }
        }

        public MainForm MainForm
        {
            get { return mainForm; }
        }

        public ChartManager(MainForm mainForm)
        {
            this.mainForm = mainForm;
            allCharts = new List<ClientChart>();
            Parent = mainForm.tabPage;
            Show();
            BackColor = Color.DarkGray;
            UpdateSize();
            Paint += ChartManager_Paint;
            timer = new Timer();
            timer.Interval = MILLISECONDS_PER_UPDATE;
            timer.Tick += Timer_Tick;
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            //временно выключен на период тестирования
            timer.Start();
            Resize += ChartManager_Resize;
        }

        private void ChartManager_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < allCharts.Count; i++)
            {
                allCharts[i].Width = mainForm.tabPage.Width / allCharts.Count;
                allCharts[i].Height = mainForm.tabPage.Height;
                allCharts[i].Left = i * allCharts[i].Width;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (allCharts.Count > 0)
                UpdateCharts();
        }

        private void ChartManager_Paint(object sender, PaintEventArgs e)
        {
            //DrawBorders(e);
        }

        public void CreateChart(Param param, eChartOrientation chartType)
        {
            ClientChart newChart = new ClientChart(this, chartType);
        }

        public void DeleteChart(ClientChart chart)
        {
            allCharts.Remove(chart);
        }

        public void UpdateCharts()
        {
            foreach (ClientChart chart in allCharts)
            {
                if (chart.Series.Count > 0)
                    chart.Refresh();
            }
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
