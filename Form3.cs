using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace AdoNetConectado_2
{

    public partial class Form3 : Form
    {
        int id;
        string nombre;
        string descripcion;
        string estado;
        string cadenaConexion = @"Server=localhost\sqlexpress;
                                 DataBase=BancoBD;
                                  Integrated Security=true";
       
        public Form3(int id, string nombre, string descripcion, string estado)
        {
            InitializeComponent();
            this.id = id;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.estado = estado;

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            txtnombre.Text = nombre;
            txtdescripcion.Text = descripcion;
            txtestado.Text = estado;
        }

        private void ACEPTAR_CLICK(object sender, EventArgs e)
        {
            string query = "UPDATE TipoCliente SET Nombre=@nombre, Descripcion=@descripcion, Estado=@estado WHERE Id=@id";
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", txtnombre.Text);
                    comando.Parameters.AddWithValue("@descripcion", txtdescripcion.Text);
                    comando.Parameters.AddWithValue("@estado", txtestado.Text);
                    comando.Parameters.AddWithValue("@id", id);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Los datos se actualizaron correctamente");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar los datos");
                    }


                }

            }
        }

        private void SALIR_CLICK(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtestado_TextChanged(object sender, EventArgs e)
        {
            int result;
            if (!int.TryParse(txtestado.Text, out result) || (result != 0 && result != 1))
            {
                MessageBox.Show("Por favor ingrese solo 0(DESACTIVADO) o 1(ACTIVO)");
                txtestado.Text = "";
            }
        }
    }
}
