using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop_Client
{
    public partial class SeriesCreationForm : Form
    {
        private ClientChart chart;
        private MainForm mainForm;
        private ChartSettingsForm settingsForm;

        public SeriesCreationForm(ClientChart chart, ChartSettingsForm settingsForm)
        {
            this.settingsForm = settingsForm;
            this.chart = chart;
            mainForm = chart.chartManager.MainForm;
            InitializeComponent();
            InitParams();
        }

        private void InitParams()
        {
            foreach(Param param in mainForm.allParams)
                listBoxAllParamsNames.Items.Add(param.Name);
        }

        private void listBoxAllParamsNames_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                foreach (var serie in chart.Series)
                {
                    if (serie.name == listBoxAllParamsNames.SelectedItem.ToString())
                    {
                        MessageBox.Show("Для этого параметра уже существует серия");
                        return;
                    }
                }
                chart.CreateSerie(listBoxAllParamsNames.SelectedItem.ToString());
                Close();
            }
            catch
            {

            }
        }
    }
}
