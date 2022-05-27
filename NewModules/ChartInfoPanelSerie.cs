using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewModules
{
    internal class ChartInfoPanelSerie : Control
    {
        private ChartSerie serie;
        private string minValue;
        private string maxValue;
        private string serieTitle;

        public ChartInfoPanelSerie(ChartSerie serie, ChartInfoPanel infoPanel)
        {
            this.serie = serie;
            Parent = infoPanel;
            Width = infoPanel.Width - 20;
            Height = 20;    
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Left = 10;
            BackColor = Color.FromArgb(255, 190, 190, 190);
            DoubleBuffered = true;

            minValue = serie.minValue.ToString();
            maxValue = serie.maxValue.ToString();
            serieTitle = serie.name;

            infoPanel.Controls.Add(this);
            infoPanel.seriesPanels.Add(this);
            Show();
            Paint += ChartInfoPanelSerie_Paint;

            infoPanel.UpdateSeriePanelsLocation();
        }

        private void ChartInfoPanelSerie_Paint(object sender, PaintEventArgs e)
        {
            Brush brush = new SolidBrush(serie.color);
            DrawMarker(e, brush);
            DrawLabels(e, brush);
        }

        private void DrawMarker(PaintEventArgs e, Brush brush)
        {
            float markerWidht = 6;
            try
            {
                RectangleF rect = new RectangleF(serie.Points.Last().X - markerWidht / 2 - Left, 0, markerWidht, Height);

                e.Graphics.FillRectangle(brush, rect);
            }
            catch { }
        }

        private void DrawLabels(PaintEventArgs e, Brush brush)
        {
            Font font = new Font(ClientChart.TEXT_FONT_FAMILY, 10);

            PointF minValuePoint = new PointF(0, 2);
            PointF maxValuePoint = new PointF(Width - e.Graphics.MeasureString(maxValue, font).Width, 2);
            PointF serieTitlePoint = new PointF(Width / 2 - e.Graphics.MeasureString(serieTitle, font).Width / 2, 2);

            e.Graphics.DrawString(minValue, font, brush, minValuePoint);
            e.Graphics.DrawString(maxValue, font, brush, maxValuePoint);
            e.Graphics.DrawString(serieTitle, font, brush, serieTitlePoint);
        }
    }
}
