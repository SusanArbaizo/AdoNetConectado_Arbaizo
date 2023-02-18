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
    public partial class FrmTipoClienteEDIT_P : Form
    {
        int ID;
        string cadenaConexion = @"Server=localhost\sqlexpress;
                                 DataBase=BancoBD;
                                  Integrated Security=true";
        public FrmTipoClienteEDIT_P(int id = 0)
        {
            InitializeComponent();
            this.ID = id;
        }

        private void Aceptar_cambios(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("DEBE INGRESAR UN NOMBRE VALIDO", "SISTEMAS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult=DialogResult.OK;
        }

        private void cargarDatos(object sender, EventArgs e)
        {
            if(this.ID>0)
            {
                this.Text = "Editar";
                mostrarDatos();
            }

        }
        void mostrarDatos()
        {
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using(var comando = new SqlCommand("SELECT * FROM TipoCliente WHERE ID =@id",conexion))
                {
                    comando.Parameters.AddWithValue("@id", this.ID);
                    using (var reader = comando.ExecuteReader())
                    {
                        if(reader != null && reader.HasRows)
                        {
                            reader.Read();
                            txtNombre.Text = reader[1].ToString();
                            txtDescripcion.Text = reader[2].ToString();
                            chkEstado.Checked = reader[3].ToString()=="1" ? true : false;

                        }
                    }
                }
            }
        }
    }
}
