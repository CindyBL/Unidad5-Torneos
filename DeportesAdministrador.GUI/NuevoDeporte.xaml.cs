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
    /// Lógica de interacción para NuevoDeporte.xaml
    /// </summary>
    public partial class NuevoDeporte : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }

        IManejadorDeporte manejadorDeporte;
        accion accionDeporte;
        public NuevoDeporte()
        {
            InitializeComponent();
            manejadorDeporte = new ManejadorDeDeportes(new RepositorioDeportes());
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            dtgDeportes.ItemsSource = null;
            dtgDeportes.ItemsSource = manejadorDeporte.Listar;
        }

        private void HabilitarBotones(bool habilitados)
        {
            btnNuevo.IsEnabled = habilitados;
            btnEditar.IsEnabled = habilitados;
            btnEliminar.IsEnabled = habilitados;
            btnGuardar.IsEnabled = !habilitados;
            btnCancelar.IsEnabled = !habilitados;
        }

        private void HabilitarCajas(bool habilitadas)
        {
            txbNombreDeporte.Clear();
            txbNombreDeporte.IsEnabled = habilitadas;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CDeportes elegir = new CDeportes();
            elegir.Owner = this;
            elegir.Show();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(true);
            HabilitarBotones(false);
            accionDeporte = accion.Nuevo;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Deportess cli = dtgDeportes.SelectedItem as Deportess;
            if (cli != null)
            {
                HabilitarCajas(true);
                txbNombreDeporte.Text = cli.NombreDeporte;
                txbDeportesId.Text = cli.Id;
                accionDeporte = accion.Editar;
                HabilitarBotones(false);
            }
            else
            {
                MessageBox.Show("Seleccione el deporte que desea editar", "Deportes", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionDeporte == accion.Nuevo)
            {
                Deportess cli = new Deportess()
                {
                    NombreDeporte = txbNombreDeporte.Text,

                };
                if (manejadorDeporte.Agregar(cli))
                {
                    MessageBox.Show("Deporte agregado correctamente", "Deportes", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("El deporte no se puede agregar", "Deportes", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Deportess cli = dtgDeportes.SelectedItem as Deportess;
                cli.NombreDeporte = txbNombreDeporte.Text;
                if (manejadorDeporte.Modificar(cli))
                {
                    MessageBox.Show("Deporte modificado correctamente", "Deportes", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("El deporte no se pudo actualizar coreectamente", "Deportes", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Deportess cli = dtgDeportes.SelectedItem as Deportess;
            if (cli != null)
            {
                if (MessageBox.Show("Realmente deseas eliminar este deporte?", "Deportes", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorDeporte.Eliminar(cli.Id))
                    {
                        MessageBox.Show("El deporte ha sido eliminado correctamente", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTabla();
                    }
                    else
                    {
                        MessageBox.Show("El deporte no se pudo eliminar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
        }
    }
}
