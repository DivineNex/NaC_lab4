using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewModules
{
    internal partial class ChartSerieSettingsForm : Form
    {
        private ChartManager chartManager;
        private ChartSerie serie;
        public ChartSerieSettingsForm(ChartManager chartManager, ChartSerie serie)
        {
            InitializeComponent();
            this.chartManager = chartManager;
            this.serie = serie;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();
            serie.color = colorDialog.Color;
            serie.chartSettingsSeriePanel.Refresh();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            serie.lineThickness = Convert.ToInt32(numericUpDown1.Value);
        }

        private void ChartSerieSettingsForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = serie.lineThickness;
        }

        private void ChartSerieSettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            serie.chartSettingsSeriePanel.Refresh();
        }
    }
}
