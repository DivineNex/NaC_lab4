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
    public class ClientChart : Control
    {
        public const int AXIS_X_DEFAULT_STEP = 10;
        public const int AXIS_Y_DEFAULT_STEP = 10;
        public const int BORDER_THICKNESS = 2;
        private static readonly Color BACKGROUND_COLOR = Color.FromArgb(255, 220, 220, 220);
        private static readonly Color BORDER_COLOR = Color.FromArgb(255, 80, 80, 80);
        private static readonly Color BUTTON_BACK_COLOR = Color.FromArgb(255, 220, 220, 220);
        public static readonly Color TEXT_COLOR = Color.FromArgb(255, 60, 60, 60);
        public static readonly string TEXT_FONT_FAMILY = "Arial";

        //private eChartOrientation type;
        public ChartManager chartManager;
        private Button buttonSettings;
        private Button buttonClose;
        private Button buttonScreenshot;
        private Button buttonWarnings;
        private Button buttonZoomIn;
        private Button buttonZoomOut;
        public ChartInfoPanel infoPanel;
        public ChartTimeAxis timeAxis;
        public ChartDrawArea drawArea;
        private ChartSettingsForm settingsForm;
        public List<string> timeStamps;
        public Timer timer;
        public int minInterval = Int32.MaxValue;
        public float zoomCoeff;
        public float lastAddedPointY;

        private List<ChartSerie> series;

        public List<ChartSerie> Series
        {
            get { return series; }
        }

        public ChartSettingsForm SettingsForm
        {
            get { return settingsForm; }
        }

        public ClientChart(ChartManager chartManager, eChartOrientation orientation)
        {
            this.chartManager = chartManager;
            chartManager.AllCharts.Add(this);
            chartManager.Controls.Add(this);
            InitChartWidth();
            Init();
        }

        private void ClientChart_Paint(object sender, PaintEventArgs e)
        {
            drawArea.Refresh();
            infoPanel.RefreshPanels();
            timeAxis.Refresh();
            DrawBorders(e);
        }

        private void Init()
        {
            Parent = chartManager;
            zoomCoeff = 1;
            
            BackColor = BACKGROUND_COLOR;
            ResizeRedraw = true;
            InitButtons();

            series = new List<ChartSerie>();
            infoPanel = new ChartInfoPanel(this);
            timeAxis = new ChartTimeAxis(this);
            drawArea = new ChartDrawArea(this);
            timeStamps = new List<string>();

            OpenSettingsForm();
            DoubleBuffered = true;
            Paint += ClientChart_Paint;
            Show();
            chartManager.Controls[chartManager.Controls.Count - 1].BringToFront();
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
        }

        private void InitChartWidth()
        {
            for (int i = 0; i < chartManager.AllCharts.Count; i++)
            {
                chartManager.AllCharts[i].Width = chartManager.MainForm.tabPage.Width / chartManager.AllCharts.Count;
                chartManager.AllCharts[i].Height = chartManager.MainForm.tabPage.Height;
                chartManager.AllCharts[i].Left = i * chartManager.AllCharts[i].Width;
            }
        }

        public void Close()
        {
            chartManager.DeleteChart(this);
            this.Dispose();
        }

        public void DrawBorders(PaintEventArgs e)
        {
            Pen blackPen = new Pen(BORDER_COLOR, BORDER_THICKNESS);
            Rectangle rect = new Rectangle(BORDER_THICKNESS/2, BORDER_THICKNESS/2, Width - BORDER_THICKNESS, Height - BORDER_THICKNESS);
            e.Graphics.DrawRectangle(blackPen, rect);
        }

        private void InitButtons()
        {
            buttonSettings = new Button();
            buttonClose = new Button();
            buttonScreenshot = new Button();
            buttonWarnings = new Button();
            buttonZoomIn = new Button();
            buttonZoomOut = new Button();

            buttonSettings.Parent = this;
            buttonClose.Parent = this;
            buttonScreenshot.Parent = this;
            buttonWarnings.Parent = this;
            buttonZoomIn.Parent = this;
            buttonZoomOut.Parent = this;

            buttonSettings.Size = new Size(30, 30);
            buttonClose.Size = new Size(30, 30);
            buttonScreenshot.Size = new Size(30, 30);
            buttonWarnings.Size = new Size(30, 30);
            buttonZoomIn.Size = new Size(30, 30);
            buttonZoomOut.Size = new Size(30, 30);

            buttonSettings.Location = new Point(BORDER_THICKNESS, BORDER_THICKNESS);
            buttonClose.Location = new Point(BORDER_THICKNESS, BORDER_THICKNESS + 30);
            buttonScreenshot.Location = new Point(BORDER_THICKNESS + 30, BORDER_THICKNESS);
            buttonWarnings.Location = new Point(BORDER_THICKNESS + 30, BORDER_THICKNESS + 30);
            buttonZoomIn.Location = new Point(BORDER_THICKNESS, BORDER_THICKNESS + 60);
            buttonZoomOut.Location = new Point(BORDER_THICKNESS + 30, BORDER_THICKNESS + 60);

            buttonSettings.BackColor = BUTTON_BACK_COLOR;
            buttonClose.BackColor = BUTTON_BACK_COLOR;
            buttonScreenshot.BackColor = BUTTON_BACK_COLOR;
            buttonWarnings.BackColor = BUTTON_BACK_COLOR;
            buttonZoomIn.BackColor = BUTTON_BACK_COLOR;
            buttonZoomOut.BackColor = BUTTON_BACK_COLOR;

            buttonSettings.Image = Image.FromFile(@"..\..\Res\IconSettings.png");
            buttonClose.Image = Image.FromFile(@"..\..\Res\IconClose.png");
            buttonScreenshot.Image = Image.FromFile(@"..\..\Res\IconScreenshot.png");
            buttonWarnings.Image = Image.FromFile(@"..\..\Res\IconWarnings.png");
            buttonZoomIn.Image = Image.FromFile(@"..\..\Res\IconZoomIn.png");
            buttonZoomOut.Image = Image.FromFile(@"..\..\Res\IconZoomOut.png");

            buttonSettings.Click += ButtonSettings_Click;
            buttonClose.Click += ButtonClose_Click;
            buttonScreenshot.Click += ButtonScreenshot_Click;
            buttonWarnings.Click += ButtonWarnings_Click;
            buttonZoomIn.Click += ButtonZoomIn_Click;
            buttonZoomOut.Click += ButtonZoomOut_Click;

            Controls.Add(buttonSettings);
            Controls.Add(buttonClose);
            Controls.Add(buttonScreenshot);
            Controls.Add(buttonWarnings);
            Controls.Add(buttonZoomIn);
            Controls.Add(buttonZoomOut);

            buttonSettings.Show();
            buttonClose.Show();
            buttonScreenshot.Show();
            buttonWarnings.Show();
            buttonZoomIn.Show();
            buttonZoomOut.Show();
        }

        private void ButtonZoomOut_Click(object sender, EventArgs e)
        {
            zoomCoeff *= 0.8f;
        }

        private void ButtonZoomIn_Click(object sender, EventArgs e)
        {
            zoomCoeff *= 1.2f;
        }

        private void ButtonWarnings_Click(object sender, EventArgs e)
        {
            //
        }

        private void ButtonScreenshot_Click(object sender, EventArgs e)
        {
            //
        }

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            OpenSettingsForm();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void OpenSettingsForm()
        {
            settingsForm = new ChartSettingsForm(this);
            settingsForm.StartPosition = FormStartPosition.CenterScreen;
            settingsForm.Init();
            settingsForm.Show();
        }

        public void CreateSerie(string paramName)
        {
            foreach (Param prm in chartManager.MainForm.allParams)
            {
                if (prm.Name == paramName)
                {
                    ChartSerie newSerie = new ChartSerie(prm, this);
                    newSerie.Visible = true;
                    series.Add(newSerie);
                    prm.assignedSeries.Add(newSerie);
                    return;
                }
            }
        }
    }
}
