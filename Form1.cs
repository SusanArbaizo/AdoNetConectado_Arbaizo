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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AdoNetConectado_2
{
    public partial class Form1 : Form
    {
        string cadenaConexion = @"Server=localhost\sqlexpress;
                                 DataBase=BancoBD;
                                  Integrated Security=true";
        public Form1()
        {
            InitializeComponent();
        }

        private void CargarFormulario(object sender, EventArgs e)
        {
            cargarDatos();

        }
        private void cargarDatos()
        {
            //------------------   
            //var conexion = new SqlConnection(cadenaConexion);
            //conexion.Open();
            //var querySql = "SELECT * FROM TipoCliente";
            //var comando = new SqlCommand(querySql, conexion);
            //var reader = comando.ExecuteReader();//
            //if (reader != null && reader.HasRows)
            //{
            //    reader.Read();
            //    dgvDatos.Rows.Add(reader[0], reader[1]);
            //}
            //--------------------

            // dgvDatos.Rows.Clear();


            //
            dgvDatos.Rows.Clear();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("SELECT * FROM TipoCliente", conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            while (lector.Read())
                            {
                                dgvDatos.Rows.Add(lector[0], lector[1], lector[2], lector[3]);
                            }
                        }
                    }
                }
            }
            

        }

        private void Click_Insertar(object sender, EventArgs e)
        {

            FrmTipoClienteEDIT_P frm = new FrmTipoClienteEDIT_P();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                string nombre = frm.Controls["txtNombre"].Text;
                string descripcion = frm.Controls["txtDescripcion"].Text;
                //OPERADOR TERNARIO
                var estado = ((CheckBox)frm.Controls["chkEstado"]).Checked==true ? 1: 0;

                using (var conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    using(var comando = new SqlCommand("INSERT INTO TipoCliente (Nombre, Descripcion, Estado)"
                        + "VALUES (@nombre, @descripcion, @estado)",conexion))
                    {
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        comando.Parameters.AddWithValue("@descripcion", descripcion);
                        comando.Parameters.AddWithValue("@estado", estado);
                        int resultado = comando.ExecuteNonQuery();
                        if (resultado > 0)
                        {
                            MessageBox.Show("DATOS REGISTRADOS", "SISTEMAS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("NO SE HA PODIDO REGISTRAR LOS DATOS", "SISTEMAS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }


            }
            cargarDatos();



            ////---------------------
            //dgvDatos.Rows.Clear();
            //Form2 form = new Form2();
            //form.ShowDialog();

            //cargarDatos();
            ////----------------------


        }

        private void Actualizar_CLICK(object sender, EventArgs e)
        {
            //------------------------
            dgvDatos.Rows.Clear();
            cargarDatos();
            //-------------------------
        }

        private void Editar_CLICK(object sender, EventArgs e)
        {
            //VALIDAMOS QUE EXISTAN FILAS PARA EDITAR
            if (dgvDatos.RowCount> 0 && dgvDatos.CurrentRow != null )
            {
                //TOMAMOS EL ID DE LA FILA SELECCIONADA
                
                int idTipo = int.Parse(dgvDatos.CurrentRow.Cells[0].Value.ToString());
                var frm = new FrmTipoClienteEDIT_P(idTipo);
                if(frm.ShowDialog() ==DialogResult.OK)
                {
                    string nombre = frm.Controls["txtNombre"].Text;
                    string descripcion = frm.Controls["txtDescripcion"].Text;
                    //operador ternario
                    var estado = ((CheckBox)frm.Controls["chkEstado"]).Checked == true ? 1 : 0;
                    using(var conexion = new SqlConnection(cadenaConexion))
                    {
                        conexion.Open();
                        using(var comando = new SqlCommand("UPDATE TipoCliente SET Nombre = @nombre, "+
                            "Descripcion = @descripcion, Estado= @estado WHERE ID=@id", conexion))
                        {
                            comando.Parameters.AddWithValue("@nombre", nombre);
                            comando.Parameters.AddWithValue("@descripcion", descripcion);
                            comando.Parameters.AddWithValue("@estado", estado);
                            comando.Parameters.AddWithValue("@id", idTipo);
                            int resultado = comando.ExecuteNonQuery();
                            if (resultado > 0)
                            {
                                MessageBox.Show("Datos actualizados.", "Sistemas",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No se ha podido actualizar los datos.", "Sistemas",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }

            cargarDatos();

            ////--------------------------------------
            //if (dgvDatos.SelectedRows.Count != 1)
            //{
            //    MessageBox.Show("Debe seleccionar una sola fila para editar");
            //    return;
            //}

            //int id = Convert.ToInt32(dgvDatos.SelectedRows[0].Cells[0].Value);
            //string nombre = dgvDatos.SelectedRows[0].Cells[1].Value.ToString();
            //string descripcion = dgvDatos.SelectedRows[0].Cells[2].Value.ToString();
            //string estado = dgvDatos.SelectedRows[0].Cells[3].Value.ToString();

            //Form3 form = new Form3(id, nombre, descripcion, estado);
            //form.ShowDialog();
            //cargarDatos();
            ////-------------------------------------------------------





            //if (dgvDatos.SelectedRows.Count != 1)
            //{
            //    MessageBox.Show("Debe seleccionar una sola fila para editar");
            //    return;
            //}

            //int id = Convert.ToInt32(dgvDatos.SelectedRows[0].Cells[0].Value);
            //string nombre = dgvDatos.SelectedRows[0].Cells[1].Value.ToString();
            //string descripcion = dgvDatos.SelectedRows[0].Cells[2].Value.ToString();
            //string estado = dgvDatos.SelectedRows[0].Cells[3].Value.ToString();

            //Form3 form = new Form3(id, nombre, descripcion, estado);
            //form.ShowDialog();
            //cargarDatos();





            //if (dgvDatos.SelectedRows.Count == 1)
            //{
            //    MessageBox.Show("Una fila seleccionada");
            //    //Abrir tercer formulario
            //}
            //else
            //{
            //    MessageBox.Show("Debe seleccionar una sola fila");
            //}



            //if (dgvDatos.SelectedRows.Count > 0)
            //{
            //    int id = Convert.ToInt32(dgvDatos.SelectedRows[0].Cells[0].Value);
            //    string nombre = dgvDatos.SelectedRows[0].Cells[1].Value.ToString();
            //    string descripcion = dgvDatos.SelectedRows[0].Cells[2].Value.ToString();
            //    string estado = dgvDatos.SelectedRows[0].Cells[3].Value.ToString();

            //    Form3 form = new Form3(id, nombre, descripcion, estado);
            //    form.ShowDialog();
            //    cargarDatos();
            //}
            //else
            //{
            //    MessageBox.Show("Debe seleccionar una fila para editar");
            //}

        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Eliminar_CLICK(object sender, EventArgs e)
        {
            //----------------------------------------------------------
            if (dgvDatos.SelectedRows.Count != 1)
            {
                MessageBox.Show("Debe seleccionar una sola fila para eliminar");
                return;
            }
            int id = Convert.ToInt32(dgvDatos.SelectedRows[0].Cells[0].Value);

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("DELETE FROM TipoCliente WHERE id = @id", conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    comando.ExecuteNonQuery();
                }
            }

            dgvDatos.Rows.Clear();
            cargarDatos();
            //-------------------------------------------------------------
        }
    }
}


    

    
