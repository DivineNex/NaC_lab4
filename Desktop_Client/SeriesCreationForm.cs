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

        public SeriesCreationForm(ClientChart chart)
        {
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
                chart.CreateSerie(listBoxAllParamsNames.SelectedItem.ToString());

                Dispose();
            }
            catch
            {

            }
        }
    }
}
