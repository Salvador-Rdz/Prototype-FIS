using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype_FIS
{
    public partial class Reporte_Mensual_Anual : Form
    {
        public Reporte_Mensual_Anual()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Menu().Show(); this.Close();
        }
    }
}
