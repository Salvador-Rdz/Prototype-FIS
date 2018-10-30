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
    public partial class Proyectos : Form
    {
        private SqlConnection conexiondb = new SqlConnection("Data Source =DESKTOP-TKHF31C;Initial Catalog = Inventarios; Integrated Security = true");
        Conexion sql = new Conexion();

        public Proyectos()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Menu().Show(); this.Close();
        }

        private void Proyectos_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(300, 200);
            dgv.DataSource = sql.MostrarDatos("Proyectos");
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "Insert into Proyectos(IDProyecto, Nombre, TipoDeProyecto, IDUsuario)"
                + "values (@ID, @Nombre,@Tipo,@IDUsuario)";
            string ID = textBox1.Text;
            string nombre = textBox3.Text;
            string tipo = comboBox1.Text;
            string IDUsuario = textBox4.Text;
            //Checa dentro de la base si el ID ya existe antes de insertarlo y si algun texto fue dejado vacio.
            if (!IDExists(ID) && (!string.IsNullOrWhiteSpace(textBox1.Text)
                && textBox1.Text.Length == 4
                && !string.IsNullOrWhiteSpace(textBox3.Text)
                && !string.IsNullOrWhiteSpace(textBox4.Text)))
            {
                if(foreignIDExists(IDUsuario,"Usuarios","IDUsuario"))
                {
                    SqlCommand cmd = new SqlCommand(query, conexiondb);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Tipo", tipo);
                    cmd.Parameters.AddWithValue("@IDUsuario", IDUsuario);
                    conexiondb.Open(); //Abre la conexión
                    cmd.ExecuteNonQuery(); //Ejecuta la query dentro de la conexión a la base de datos
                    conexiondb.Close();//Cierra la conexión.
                    dgv.DataSource = sql.MostrarDatos("Proyectos"); //Actualiza la tabla
                    clearTextBox();
                }
                else
                {
                    MessageBox.Show("El ID de el Usuario [" + IDUsuario + "] no existe");
                    return;
                }
            }
            else if (IDExists(ID))
            {
                MessageBox.Show("El ID [" + ID + "] ya existe o no se han ingresado todos los valores necesarios");
            }
        }
        private void clearTextBox()
        {
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
        }
        private bool IDExists(string ID)
        {
            string query = "SELECT COUNT(*) from Proyectos where IDProyecto like '" + ID + "'";
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
        private bool foreignIDExists(string ID, string table, string value)
        {
            string query = "SELECT COUNT(*) from " + table + " where " + value + " like '" + ID + "'";
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (IDExists(textBox6.Text))
            {
                sql.Eliminar(textBox6.Text, "IDProyecto", "Proyectos");
                MessageBox.Show("Datos eliminados");
                dgv.DataSource = sql.MostrarDatos("Proyectos");
                clearTextBox();
            }
            else
            {
                MessageBox.Show("El ID [" + textBox6.Text + "] no se encontro");
            }
        }
    }
}
