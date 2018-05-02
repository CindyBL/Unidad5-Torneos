using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Deportes.BIZ;
using Deporte.COMMON.Interfaces;
using Deportes.DAL;
using LiteDB;
using Deporte.COMMON.Entidades;


namespace DeportesAdministrador.GUI
{
    /// <summary>
    /// Lógica de interacción para NuevaContraseña.xaml
    /// </summary>
    public partial class NuevaContraseña : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }
        IManejadorContraseña manejadorContraseña;
        accion accionn;
        public NuevaContraseña()
        {
            InitializeComponent();
            manejadorContraseña = new ManejadorDeContraseña(new RepositorioGenerico<Contraseña>());
            //HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dtgContraseña.ItemsSource = null;
            dtgContraseña.ItemsSource = manejadorContraseña.Listar;
        }

        private void HabilitarBotones(bool habilitados)
        {
        }

        //private void HabilitarCajas(bool habilitadas)
        //{
        //    txbNuevaContraseña.Clear();
        //    txbNuevaContraseña.IsEnabled = habilitadas;
        //}

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CDeportes elegir = new CDeportes();
            elegir.Owner = this;
            elegir.Show();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (accionn == accion.Nuevo)
            {
                Contraseña emp = new Contraseña()
                {
                    // Identificador = txbEmpleadoId.Text,
                    Usuario = txbUsuario.Text,
                    NuevaContraseña = txbNuevaContraseña.Text,




                };
                if (manejadorContraseña.Agregar(emp))
                {
                    MessageBox.Show("la contraseña se ha cambiado correctamente  ", "Deportes", MessageBoxButton.OK, MessageBoxImage.Information);
                    //HabilitarCajas(false);
                    ActualizarTabla();
                }
                else
                {
                    MessageBox.Show("la contraseña no se pudo cambiar  ", "Deportes", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Contraseña emp = dtgContraseña.SelectedItem as Contraseña;
                emp.Usuario = txbUsuario.Text;
                emp.NuevaContraseña = txbNuevaContraseña.Text;


                if (manejadorContraseña.Modificar(emp))
                {
                    MessageBox.Show("la contraseña ha sido modificada exitosañente  ", "Deportes", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                }
                else
                {
                    MessageBox.Show("El Usuario no se pudo actualizar  ", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Contraseña pro = dtgContraseña.SelectedItem as Contraseña;
            if (pro != null)
            {
                txbEquipoId.Text = pro.Id.ToString();
                txbUsuario.Text = pro.Usuario;
                txbNuevaContraseña.Text = pro.NuevaContraseña;
                accionn = accion.Editar;
                HabilitarBotones(false);
            }
            else
            {
                MessageBox.Show("Seleccione el usuario que desea editar", "Contraseña", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txbNuevaContraseña.Clear();
        }
    }
}
