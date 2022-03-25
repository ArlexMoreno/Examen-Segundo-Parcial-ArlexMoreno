using Datos.Accesos;
using Datos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen_SegundoParcial
{
    public partial class FrmPedidos : Form
    {
        public FrmPedidos()
        {
            InitializeComponent();
        }

        ProductoDA productoDA = new ProductoDA();
        Pedido pedido = new Pedido();
        Producto producto;

        List<Pedido> detallePedidosLista = new List<Pedido>();

        decimal subTotal = decimal.Zero;
        decimal isv = decimal.Zero;
        decimal totalAPagar = decimal.Zero;
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FrmPedidos_Load(object sender, EventArgs e)
        {
            PedidosDataGridView.DataSource = detallePedidosLista;
        }

        private void IdentidadMaskedTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void CodigoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                producto = new Producto();
                producto = productoDA.GetProductoPorCodigo(CodigoTextBox.Text);
                DescripcionTextBox.Text = producto.Descripcion;
                CantidadTextBox.Focus();
            }
            else
            {
                producto = null;
                DescripcionTextBox.Clear();
                CantidadTextBox.Clear();
            }
        }

        private void CantidadTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(CantidadTextBox.Text))
            {
                Pedido detallePedido = new Pedido();
                detallePedido.Codigo = producto.Codigo;
                detallePedido.Descripcion = producto.Descripcion;
                detallePedido.Cantidad = Convert.ToInt32(CantidadTextBox.Text);
                detallePedido.Precio = producto.Precio;                             
                detallePedido.SubTotal = producto.Precio * Convert.ToInt32(CantidadTextBox.Text);
                detallePedido.ISV = detallePedido.SubTotal * 0.15M;
                detallePedido.Total = detallePedido.SubTotal + detallePedido.ISV;


                subTotal += detallePedido.SubTotal;
                isv = subTotal * 0.15M;
                totalAPagar = subTotal + isv; 

                SubTotalTextBox.Text = subTotal.ToString();
                ImpuestoTextBox.Text = isv.ToString();
                TotalTextBox.Text = totalAPagar.ToString();
                
                detallePedidosLista.Add(detallePedido);
                PedidosDataGridView.DataSource = null;
                PedidosDataGridView.DataSource = detallePedidosLista;
            }
        }
    }
}
