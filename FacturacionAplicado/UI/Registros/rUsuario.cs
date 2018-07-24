using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacturacionAplicado.Entidades;
using FacturacionAplicado.BLL;

namespace FacturacionAplicado.UI.Registros
{
    public partial class rUsuario : Form
    {
        public rUsuario()
        {
            InitializeComponent();
        }

        private void rUsuario_Load(object sender, EventArgs e)
        {

        }
        private void LlenarComboBox()
        {
            IDcomboBox.Items.Clear();
            foreach (var item in BLL.ClienteBLL.Buscar())
            {
                IDcomboBox.Items.Add(item.IdCliente);
            }


        }

        private void LimpiarProvider()
        {
            errorProvider.Clear();
            DemaserrorProvider.Clear();
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            IDcomboBox.Text = string.Empty;
            NombretextBox.Clear();
            UsuariotextBox.Clear();
            ContraseñatextBox.Clear();
            ConfirmartextBox.Clear();
            ComentariotextBox.Clear();
            LimpiarProvider();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            LimpiarProvider();
            if (SetError(2))
            {
                MessageBox.Show("Hay campos vacios");
                return;
            }
            Usuario customer = LlenaClase();

            if (IDcomboBox.Text == string.Empty)
            {
                if (BLL.UsuarioBLL.Guardar(customer))
                {
                    MessageBox.Show("Guardado!!");
                    IDcomboBox.DataSource = null;
                    LlenarComboBox();
                    Clear();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar!!");
                }
            }
            else
            {
                if (BLL.UsuarioBLL.Modificar(LlenaClase()))
                {
                    MessageBox.Show("Modificar!!");
                }
                else
                {
                    MessageBox.Show("No se pudo modificar!!");
                }
            }
        }

        private Usuario LlenaClase()
        {
            Usuario customer = new Usuario();
            if (IDcomboBox.Text == string.Empty)
            {
                customer.IdUsuario = 0;
            }
            else
            {
                customer.IdUsuario = Convert.ToInt32(IDcomboBox.Text);

            }
            customer.NombreUsuario = UsuariotextBox.Text;
            customer.Nombre = NombretextBox.Text;
            customer.Clave = ContraseñatextBox.Text;
            customer.Clave = ConfirmartextBox.Text;
            customer.Comentario = ComentariotextBox.Text;

            return customer;
        }

        private bool SetError(int error)
        {
            bool paso = false;

            if (error == 1 && IDcomboBox.Text == string.Empty)
            {
                errorProvider.SetError(IDcomboBox, "Llenar Id");
                paso = true;
            }
            if (error == 2 && NombretextBox.Text == string.Empty)
            {
                DemaserrorProvider.SetError(NombretextBox, "Llenar Nombre");
                paso = true;
            }
            if (error == 2 && UsuariotextBox.Text == string.Empty)
            {
                DemaserrorProvider.SetError(UsuariotextBox, "Llenar Direccion");
                paso = true;
            }
            if (error == 2 && ContraseñatextBox.Text == string.Empty)
            {

                DemaserrorProvider.SetError(ContraseñatextBox, "Llenar Contraseña");
                paso = true;
            }
            if (error == 2 && ConfirmartextBox.Text == string.Empty)
            {

                DemaserrorProvider.SetError(ConfirmartextBox, "Llenar Confirmacion de contraseña");
                paso = true;
            }
            if (error == 2 && ComentariotextBox.Text == string.Empty)
            {

                DemaserrorProvider.SetError(ComentariotextBox, "Llenar Confirmacion de contraseña");
                paso = true;
            }
            return paso;
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            LimpiarProvider();
            if (SetError(1))
            {
                MessageBox.Show("Llenar Campo Id");
                return;
            }

            if (BLL.UsuarioBLL.Eliminar(IDcomboBox.Text))
            {
                MessageBox.Show("Eliminado!!");
                IDcomboBox.DataSource = null;
                LlenarComboBox();
                Clear();
            }
            else
            {
                MessageBox.Show("No se pudo eliminar!!");
            }
        }

        private void IDcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenaCampos();
        }

        private void LlenaCampos()
        {
            int id = Convert.ToInt32(IDcomboBox.Text);
            foreach (var item in BLL.UsuarioBLL.Buscar())
            {
                if (id == item.IdUsuario)
                {
                    UsuariotextBox.Text = item.NombreUsuario;
                    NombretextBox.Text = item.Nombre;
                    ContraseñatextBox.Text = item.Clave;
                    ConfirmartextBox.Text = item.Clave;
                    ComentariotextBox.Text = item.Comentario;

                }
            }

        }
    }
}
