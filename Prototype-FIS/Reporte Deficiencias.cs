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
    public partial class Reporte_Deficiencias : Form
    {
        public Reporte_Deficiencias()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Menu().Show(); this.Close();
        }
    }
}
