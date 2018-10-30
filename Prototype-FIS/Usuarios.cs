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
    public partial class Usuarios : Form
    {
        private SqlConnection conexiondb = new SqlConnection("Data Source =DESKTOP-TKHF31C;Initial Catalog = Inventarios; Integrated Security = true");
        Conexion sql = new Conexion();
        public Usuarios()
        {
            InitializeComponent();
        }

       

        private void Usuarios_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(300, 200);
            dgv.DataSource = sql.MostrarDatos("Usuarios");
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string query = "Insert into Usuarios(IDUsuario, Nombre, Apellidos, Email, Contraseña,FechaDeNacimiento,Sexo)"
                + "values (@ID, @Nombre,@Apellidos,@Email,@Contraseña,@Fecha,@Sexo)";
            string ID = textBox1.Text;
            string nombre = textBox2.Text;
            string apellidos = textBox3.Text;
            string email = textBox4.Text;
            string contraseña = textBox5.Text;
            DateTime parsedDate = DateTime.Parse(dateTimePicker1.Text);
            string sexo="Error";
            if (radioButton1.Checked)
            {
                sexo = radioButton1.Text;
            }
            else if(radioButton2.Checked)
            {
                sexo = radioButton2.Text;
            }
            //Checa dentro de la base si el ID ya existe antes de insertarlo y si algun texto fue dejado vacio.
            if (!IDExists(ID) && (!string.IsNullOrWhiteSpace(textBox1.Text)
                && textBox1.Text.Length == 4
                && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrWhiteSpace(textBox3.Text)
                && !string.IsNullOrWhiteSpace(textBox4.Text)
                && !string.IsNullOrWhiteSpace(textBox5.Text))) 
            {
                SqlCommand cmd = new SqlCommand(query, conexiondb);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Apellidos", apellidos);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Contraseña", contraseña);
                cmd.Parameters.AddWithValue("@Fecha", parsedDate);
                cmd.Parameters.AddWithValue("@Sexo", sexo);
                conexiondb.Open(); //Abre la conexión
                cmd.ExecuteNonQuery(); //Ejecuta la query dentro de la conexión a la base de datos
                conexiondb.Close();//Cierra la conexión.
                dgv.DataSource = sql.MostrarDatos("Usuarios"); //Actualiza la tabla
                clearTextBox();
            }
            else if(IDExists(ID))
            {
                MessageBox.Show("El ID ["+ID+"] ya existe o no se han ingresado todos los valores necesarios");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IDExists(textBox6.Text))
            {
                sql.Eliminar(textBox6.Text, "IDUsuario", "Usuarios");
                MessageBox.Show("Datos eliminados");
                dgv.DataSource = sql.MostrarDatos("Usuarios");
                clearTextBox();
            }
            else
            {
                MessageBox.Show("El ID ["+textBox6.Text+"] no se encontro");
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
            string query = "SELECT COUNT(*) from Usuarios where IDUsuario like '" + ID + "'";
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            new Menu().Show(); this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ID = textBox1.Text;
            string nombre = textBox2.Text;
            string apellidos = textBox3.Text;
            string email = textBox4.Text;
            string contraseña = textBox5.Text;
            DateTime parsedDate = DateTime.Parse(dateTimePicker1.Text);
            string sexo = "Error";
            if (radioButton1.Checked)
            {
                sexo = radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                sexo = radioButton2.Text;
            }
            string query = "Update Usuarios SET Nombre =@Nombre, Apellidos =@Apellidos,"
                +"Email=@Email, Contraseña=@Contraseña,FechaDeNacimiento=@Fecha,Sexo = @Sexo "
                +"WHERE IDUsuario = @ID";
            //Checa dentro de la base si el ID ya existe antes de insertarlo y si algun texto fue dejado vacio.
            if (IDExists(ID) && (!string.IsNullOrWhiteSpace(textBox1.Text)
                && textBox1.Text.Length == 4
                && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrWhiteSpace(textBox3.Text)
                && !string.IsNullOrWhiteSpace(textBox4.Text)
                && !string.IsNullOrWhiteSpace(textBox5.Text)))
            {
                DialogResult dialogResult = MessageBox.Show("Esto remplazara todos los valores del usuario = "+ID
                    +" ¿Esta seguro?","Warning", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand(query, conexiondb);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellidos", apellidos);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Contraseña", contraseña);
                    cmd.Parameters.AddWithValue("@Fecha", parsedDate);
                    cmd.Parameters.AddWithValue("@Sexo", sexo);
                    conexiondb.Open(); //Abre la conexión
                    cmd.ExecuteNonQuery(); //Ejecuta la query dentro de la conexión a la base de datos
                    conexiondb.Close();//Cierra la conexión.
                    dgv.DataSource = sql.MostrarDatos("Usuarios"); //Actualiza la tabla
                    clearTextBox();
                    MessageBox.Show("Se han actualizado los valores para el usuario [" + ID + "]");
                }
            }
            else if (!IDExists(ID))
            {
                MessageBox.Show("El ID [" + ID + "] no existe o no se han ingresado todos los valores necesarios");
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if(dgv == null)
            {
                return;
            }
            else
            {
                DataGridViewRow fila = dgv.Rows[e.RowIndex]; //esta fila sera igual a la de la fial seleccionada
                textBox1.Text = Convert.ToString(fila.Cells[0].Value);
                textBox2.Text = Convert.ToString(fila.Cells[1].Value);
                textBox3.Text = Convert.ToString(fila.Cells[2].Value);
                textBox4.Text = Convert.ToString(fila.Cells[3].Value);
                textBox5.Text = Convert.ToString(fila.Cells[4].Value);
            }
        }
    }
}
