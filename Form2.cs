using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AdoNetConectado_2
{
    public partial class Form2 : Form
    {
        
        string cadenaConexion = @"Server=localhost\sqlexpress;
                                 DataBase=BancoBD;
                                  Integrated Security=true";
        DataRow fila;
       
        public Form2(DataRow filaEditar = null)
        {
            InitializeComponent();
            
        }
        

        //public void AgregarRegistro(int id, string nombre, string descripcion)
        //{
        //    using (var conexion = new SqlConnection(cadenaConexion))
        //    {
        //        conexion.Open();
        //        using (var comando = new SqlCommand("INSERT INTO TipoCliente (Nombre, Descripcion) VALUES (@nombre, @descripcion)", conexion))
        //        {
        //            comando.Parameters.AddWithValue("@nombre", nombre);
        //            comando.Parameters.AddWithValue("@descripcion", descripcion);
        //            comando.ExecuteNonQuery();
        //        }
        //    }
        //}

        private void mostrarDatos()
        {
            //using (var conexion = new SqlConnection(cadenaConexion))
            //{
            //    conexion.Open();
            //    using (var comando = new SqlCommand("SELECT * FROM TipoCliente WHERE ID = @id", conexion))
            //    {
            //        comando.Parameters.AddWithValue("@id", this.ID);
            //        using (var reader = comando.ExecuteReader())
            //        {
            //            if (reader != null && reader.HasRows)
            //            {
            //                reader.Read();
            //                txtnombre.Text = reader[1].ToString();
            //                txtdescripcion.Text = reader[2].ToString();
            //                chkEstado.Checked = reader[3].ToString() == "1" ? true : false;
            //            }
            //        }
            //    }
            //}
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Aceptar(object sender, EventArgs e)
        {

           
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("INSERT INTO TipoCliente(Nombre, Descripcion, Estado) VALUES (@nombre, @descripcion, @estado)", conexion))
                {
                    comando.Parameters.AddWithValue("@nombre", txtnombre.Text);
                    comando.Parameters.AddWithValue("@descripcion", txtdescripcion.Text);
                    comando.Parameters.AddWithValue("@estado", txtestado.Text);

                    int filasAfectadas = comando.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Datos insertados correctamente.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al insertar datos.");
                    }
                }
            }






        }

        private void Cancelar_CLICK(object sender, EventArgs e)
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
