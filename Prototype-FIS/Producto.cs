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
    public partial class Producto : Form
    {
        private SqlConnection conexiondb = new SqlConnection("Data Source =DESKTOP-TKHF31C;Initial Catalog = Inventarios; Integrated Security = true");
        Conexion sql = new Conexion();
        public Producto()
        {
            InitializeComponent();
        }

        private void Producto_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(300, 200);
            dgv.DataSource = sql.MostrarDatos("Productos");
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "Insert into Productos(IDProducto, Nombre, Tipo, LimiteInfStock, Cantidad,"
                +"TipoDeUnidad, IDCompañia) values (@ID, @Nombre,@Tipo,@Limite,@Cantidad,@TipoUnidad,@IDCompañia)";
            string ID = textBox1.Text;
            string nombre = textBox3.Text;
            string tipo = comboBox1.Text;
            string tipoUnidad = comboBox2.Text;
            string limite = textBox2.Text;
            string cantidad = textBox4.Text;
            string IDCompañia = textBox5.Text;
            //Checa dentro de la base si el ID ya existe antes de insertarlo y si algun texto fue dejado vacio.
            if (!IDExists(ID) && (!string.IsNullOrWhiteSpace(textBox1.Text)
                && textBox1.Text.Length == 4 && textBox5.Text.Length ==4
                && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrWhiteSpace(textBox3.Text)
                && !string.IsNullOrWhiteSpace(textBox4.Text)
                && !string.IsNullOrWhiteSpace(textBox5.Text)))
            {
                if(foreignIDExists(IDCompañia,"Compañia","IDCompañia"))
                {
                    SqlCommand cmd = new SqlCommand(query, conexiondb);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Tipo", tipo);
                    cmd.Parameters.AddWithValue("@Limite", limite);
                    cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                    cmd.Parameters.AddWithValue("TipoUnidad", tipoUnidad);
                    cmd.Parameters.AddWithValue("IDCompañia", IDCompañia);
                    conexiondb.Open(); //Abre la conexión
                    cmd.ExecuteNonQuery(); //Ejecuta la query dentro de la conexión a la base de datos
                    conexiondb.Close();//Cierra la conexión.
                    dgv.DataSource = sql.MostrarDatos("Productos"); //Actualiza la tabla
                    clearTextBox();
                }
                else
                {
                    MessageBox.Show("El ID de compañia [" + IDCompañia+"] no existe");
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
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }
        private bool IDExists(string ID)
        {
            string query = "SELECT COUNT(*) from Productos where IDProducto like '" + ID + "'";
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
            string query = "SELECT COUNT(*) from "+table+" where "+value+" like '" + ID + "'";
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
