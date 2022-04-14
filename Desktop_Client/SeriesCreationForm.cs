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

        public SeriesCreationForm(ClientChart chart)
        {
            InitializeComponent();
            InitParams();
            this.chart = chart;
        }

        private void InitParams()
        {
            foreach(var paramName in MainForm.allGettingParamsNames)
                listBoxAllParamsNames.Items.Add(paramName);
        }

        private void listBoxAllParamsNames_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxAllParamsNames.Items.Count > 0)
            {
                chart.CreateSerie(listBoxAllParamsNames.SelectedItem.ToString());
                Dispose();
            }
            else
            {
                MessageBox.Show("Нет параметров, чтобы создать серию");
            }
        }
    }
}
