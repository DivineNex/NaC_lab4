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
        public const int BORDER_THICKNESS = 5;
        private static readonly Color BACKGROUND_COLOR = Color.FromArgb(255, 220, 220, 220);
        private static readonly Color BORDER_COLOR = Color.FromArgb(255, 80, 80, 80);
        private static readonly Color BUTTON_BACK_COLOR = Color.FromArgb(255, 220, 220, 220);
        private static readonly Color TEXT_COLOR = Color.FromArgb(255, 60, 60, 60);
        private static readonly string TEXT_FONT_FAMILY = "Arial";

        private Label titleLabel;

        public string Title
        {
            get { return titleLabel.Text; }
            set { titleLabel.Text = value; }
        }

        private eChartOrientation type;
        public ChartManager chartManager;
        private Button buttonSettings;
        private Button buttonClose;
        private ChartInfoPanel infoPanel;
        private ChartSettingsForm settingsForm;

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
            Init();
        }

        private void ClientChart_Paint(object sender, PaintEventArgs e)
        {
            DrawSeries(e);
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
            series = new List<ChartSerie>();

            OpenSettingsForm();
            DoubleBuffered = true;
            Paint += ClientChart_Paint;
        }

        public void Close()
        {
            chartManager.DeleteChart(this);
            this.Dispose();
        }

        public void DrawSeries(PaintEventArgs e)
        {
            foreach (ChartSerie serie in series)
            {
                if (serie.Points.Count != 0)
                {
                    Brush brush = new SolidBrush(serie.color);
                    Pen pen = new Pen(serie.color);
                    pen.Width = 3;

                    if (serie.Points.Count == 1)
                    {
                        e.Graphics.FillEllipse(brush, serie.Points[0].X, serie.Points[0].Y, 3, 3);
                    }
                    else
                    {
                        e.Graphics.DrawLines(pen, serie.Points.ToArray());
                    }
                }
            }
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

            buttonSettings.Location = new Point(Width - 66, 6);
            buttonClose.Location = new Point(Width - 36, 6);

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
            OpenSettingsForm();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitMainLabel()
        {
            titleLabel = new Label();
            titleLabel.Parent = this;
            titleLabel.Text = "График #" + (chartManager.allCharts.Count + 1).ToString();
            titleLabel.Location = new Point(10, 13);
            titleLabel.Font = new Font(TEXT_FONT_FAMILY, 16);
            titleLabel.ForeColor = TEXT_COLOR;
            titleLabel.AutoSize = true;
            titleLabel.BackColor = ChartInfoPanel.BACKGROUND_COLOR;
            Controls.Add(titleLabel);
            titleLabel.Show();
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
            foreach (Param prm in chartManager.MainForm.AllParams)
            {
                if (prm.name == paramName)
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
