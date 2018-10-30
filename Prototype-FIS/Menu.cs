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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Salidas().Show(); this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Usuarios().Show(); this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            new Proyectos().Show(); this.Close();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(300, 200);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Producto().Show(); this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Entradas().Show(); this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Corte().Show(); this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            new Compañias().Show(); this.Close();
        }
    }
}
