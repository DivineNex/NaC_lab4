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
    class ClientChart : Control
    {
        public const int BORDER_THICKNESS = 5;
        private static readonly Color BACKGROUND_COLOR = Color.FromArgb(255, 220, 220, 220);
        private static readonly Color BORDER_COLOR = Color.FromArgb(255, 80, 80, 80);
        private static readonly Color BUTTON_BACK_COLOR = Color.FromArgb(255, 220, 220, 220);
        private static readonly Color TEXT_COLOR = Color.FromArgb(255, 60, 60, 60);
        private static readonly string TEXT_FONT_FAMILY = "Arial";

        private Label label;
        private eChartOrientation type;
        private ChartManager chartManager;
        private List<Point> allPoints;
        private Button buttonSettings;
        private Button buttonClose;
        private ChartInfoPanel infoPanel;

        public ClientChart(ChartManager chartManager, eChartOrientation orientation)
        {
            this.chartManager = chartManager;
            Init();

            Paint += ClientChart_Paint;
        }

        private void ClientChart_Paint(object sender, PaintEventArgs e)
        {
            DrawBorders(e);
        }

        private void Init()
        {
            Width = 500;
            Height = 500;
            BackColor = BACKGROUND_COLOR;
            Show();
            InitButtons();
            InitMainLabel();

            infoPanel = new ChartInfoPanel(this);
            allPoints = new List<Point>();
        }

        public void Close()
        {
            chartManager.DeleteChart(this);
            this.Dispose();
        }

        public void Draw()
        {
        
        }

        public void UpdateValue()
        {

        }

        public void AddPoint(int x, int y)
        {
            allPoints.Add(new Point(x, y));
        }

        public void DrawBorders(PaintEventArgs e)
        {
            Pen blackPen = new Pen(BORDER_COLOR, BORDER_THICKNESS);
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            e.Graphics.DrawRectangle(blackPen, rect);
        }

        private void InitButtons()
        {
            buttonSettings = new Button();
            buttonClose = new Button();

            buttonSettings.Parent = this;
            buttonClose.Parent = this;

            buttonSettings.Size = new Size(30, 30);
            buttonClose.Size = new Size(30, 30);

            buttonSettings.Location = new Point(Width - 35, 5);
            buttonClose.Location = new Point(Width - 65, 5);

            buttonSettings.BackColor = BUTTON_BACK_COLOR;
            buttonClose.BackColor = BUTTON_BACK_COLOR;

            buttonSettings.Image = Image.FromFile(@"..\..\Res\IconChartSettingsButton.png");
            buttonClose.Image = Image.FromFile(@"..\..\Res\IconChartCloseButton.png");

            buttonSettings.Click += ButtonSettings_Click;
            buttonClose.Click += ButtonClose_Click;

            Controls.Add(buttonSettings);
            Controls.Add(buttonClose);

            buttonSettings.Show();
            buttonClose.Show();
        }

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            //Появляется форма настройки графика
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitMainLabel()
        {
            label = new Label();
            label.Parent = this;
            label.Text = "График #" + (chartManager.allCharts.Count+1).ToString();
            label.Location = new Point(10, 13);
            label.Font = new Font(TEXT_FONT_FAMILY, 16);
            label.ForeColor = TEXT_COLOR;
            label.AutoSize = true;
            label.BackColor = ChartInfoPanel.BACKGROUND_COLOR;
            Controls.Add(label);
            label.Show();
        }
    }
}
