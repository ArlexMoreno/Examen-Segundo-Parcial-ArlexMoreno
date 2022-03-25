using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos.Accesos;
using Datos.Entidades;

namespace Examen_SegundoParcial
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            UsuarioDA usuarioDa = new UsuarioDA();
            Usuario usuario = new Usuario();

            usuario = usuarioDa.Login(UsuarioTextBox.Text, ClaveTextBox.Text);
            if (usuario == null)
            {
                MessageBox.Show("Datos erroneos");
                return;
            }
            else if (!usuario.EstaActivo)
            {
                MessageBox.Show("Usuario Inactivo");
                return;
            }

            FrmMenu frmMenu = new FrmMenu();
            frmMenu.Show();
            this.Hide();

        }
        private void CancelarButton_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

       
    }
}
