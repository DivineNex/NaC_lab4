﻿using System;
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
        public ChartInfoPanel infoPanel;
        public ChartTimeAxis timeAxis;
        public ChartDrawArea drawArea;
        private ChartSettingsForm settingsForm;
        public int axisYStep;
        public int axisXStep;

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
            drawArea.Refresh();
            DrawBorders(e);
        }

        private void Init()
        {
            Width = 500;
            Height = 500;
            BackColor = BACKGROUND_COLOR;
            ResizeRedraw = true;
            InitButtons();

            axisYStep = 20;
            axisXStep = 20;

            series = new List<ChartSerie>();
            infoPanel = new ChartInfoPanel(this);
            timeAxis = new ChartTimeAxis(this);
            drawArea = new ChartDrawArea(this);

            OpenSettingsForm();
            DoubleBuffered = true;
            Paint += ClientChart_Paint;
            Show();
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

            buttonSettings.Parent = this;
            buttonClose.Parent = this;
            buttonScreenshot.Parent = this;
            buttonWarnings.Parent = this;

            buttonSettings.Size = new Size(30, 30);
            buttonClose.Size = new Size(30, 30);
            buttonScreenshot.Size = new Size(30, 30);
            buttonWarnings.Size = new Size(30, 30);

            buttonSettings.Location = new Point(BORDER_THICKNESS, BORDER_THICKNESS);
            buttonClose.Location = new Point(BORDER_THICKNESS, BORDER_THICKNESS+30);
            buttonScreenshot.Location = new Point(BORDER_THICKNESS+30, BORDER_THICKNESS);
            buttonWarnings.Location = new Point(BORDER_THICKNESS+30, BORDER_THICKNESS+30);

            buttonSettings.BackColor = BUTTON_BACK_COLOR;
            buttonClose.BackColor = BUTTON_BACK_COLOR;
            buttonScreenshot.BackColor = BUTTON_BACK_COLOR;
            buttonWarnings.BackColor = BUTTON_BACK_COLOR;

            buttonSettings.Image = Image.FromFile(@"..\..\Res\IconSettings.png");
            buttonClose.Image = Image.FromFile(@"..\..\Res\IconClose.png");
            buttonScreenshot.Image = Image.FromFile(@"..\..\Res\IconScreenshot.png");
            buttonWarnings.Image = Image.FromFile(@"..\..\Res\IconWarnings.png");

            buttonSettings.Click += ButtonSettings_Click;
            buttonClose.Click += ButtonClose_Click;
            buttonScreenshot.Click += ButtonScreenshot_Click;
            buttonWarnings.Click += ButtonWarnings_Click;

            Controls.Add(buttonSettings);
            Controls.Add(buttonClose);
            Controls.Add(buttonScreenshot);
            Controls.Add(buttonWarnings);

            buttonSettings.Show();
            buttonClose.Show();
            buttonScreenshot.Show();
            buttonWarnings.Show();
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
