using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Prototype_FIS
{
    public partial class Compañias : Form
    {
        private SqlConnection conexiondb = new SqlConnection("Data Source =DESKTOP-TKHF31C;Initial Catalog = Inventarios; Integrated Security = true");
        Conexion sql = new Conexion();
        public Compañias()
        {
            InitializeComponent();
        }
        private void Compañias_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(300, 200);
            dgv.DataSource = sql.MostrarDatos("Compañia");
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "Insert into Compañia (IDCompañia, Nombre, CodigoPostal, Telefono)"
                + " values (@ID, @Nombre,@Codigo,@Telefono)";
            string ID = textBox1.Text;
            string nombre = textBox4.Text;
            string codigo = textBox3.Text;
            string telefono = textBox2.Text;
            //Checa dentro de la base si el ID ya existe antes de insertarlo y si algun texto fue dejado vacio.
            if (!IDExists(ID) && (!string.IsNullOrWhiteSpace(textBox1.Text)
                && textBox1.Text.Length == 4 
                && !string.IsNullOrWhiteSpace(textBox1.Text)
                && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrWhiteSpace(textBox3.Text)
                && !string.IsNullOrWhiteSpace(textBox4.Text)))
            {
                SqlCommand cmd = new SqlCommand(query, conexiondb);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                conexiondb.Open(); //Abre la conexión
                cmd.ExecuteNonQuery(); //Ejecuta la query dentro de la conexión a la base de datos
                conexiondb.Close();//Cierra la conexión.
                dgv.DataSource = sql.MostrarDatos("Compañia"); //Actualiza la tabla
                clearTextBox();
            }
            else if (IDExists(ID))
            {
                MessageBox.Show("El ID [" + ID + "] ya existe o no se han ingresado todos los valores necesarios");
            }
        }
        private void clearTextBox()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
        }
        private bool IDExists(string ID)
        {
            string query = "SELECT COUNT(*) from Compañia where IDCompañia like '" + ID + "'";
            SqlCommand cmd = new SqlCommand(query, conexiondb);
            conexiondb.Open(); //Abre la conexión
            int userCount = (int)cmd.ExecuteScalar();
            conexiondb.Close();//Cierra la conexión.
            if (userCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Menu().Show(); this.Close();
        }
    }
}
