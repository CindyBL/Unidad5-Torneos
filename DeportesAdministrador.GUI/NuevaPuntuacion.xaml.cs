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
    /// Lógica de interacción para NuevaPuntuacion.xaml
    /// </summary>
    public partial class NuevaPuntuacion : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }

        IManejadorEquipo manejadorEquipo;
        IManejadorPuntuacion manejadorPuntuacion;
        accion accionPuntuacion;
        public NuevaPuntuacion()
        {
            InitializeComponent();
            manejadorEquipo = new ManejadorDeEquipo(new RepositorioEquipo());
            manejadorPuntuacion = new ManejadorDePuntuacion(new RepositorioPuntuacion());
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
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
            txbPuntos.Clear();
            txbPuntos.IsEnabled = habilitadas;
            cmbEquipo.IsEnabled = habilitadas;
            cmbEquipo.ItemsSource = null;
            cmbEquipo.ItemsSource = manejadorEquipo.Listar;

        }
        private void ActualizarTabla()
        {
            dtgPuntuacion.ItemsSource = null;
            dtgPuntuacion.ItemsSource = manejadorPuntuacion.Listar;
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
            accionPuntuacion = accion.Nuevo;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Puntuacion pro = dtgPuntuacion.SelectedItem as Puntuacion;
            if (pro != null)
            {
                HabilitarCajas(true);
                txbEquipoId.Text = pro.Id;
                txbPuntos.Text = pro.Puntos;
                cmbEquipo.Text = pro.Equipo;
                accionPuntuacion = accion.Editar;
                HabilitarBotones(false);
            }
            else
            {
                MessageBox.Show("Seleccione la puntuacion que desea editar", "Puntuacion", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionPuntuacion == accion.Nuevo)
            {
                Puntuacion pro = new Puntuacion()
                {
                    Puntos = txbPuntos.Text,
                    Equipo = cmbEquipo.Text,
                };
                if (manejadorPuntuacion.Agregar(pro))
                {
                    MessageBox.Show("La puntuacion se ha agregado correctamente", "Puntuacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("La puntuacion no se pudo agregar", "Puntuacion", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Puntuacion pro = dtgPuntuacion.SelectedItem as Puntuacion;
                pro.Puntos = txbPuntos.Text;
                pro.Equipo = cmbEquipo.Text;
                if (manejadorPuntuacion.Modificar(pro))
                {
                    MessageBox.Show("Puntuacion modificado correctamente", "Puntuacion", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("La puntuacion no se pudo actualizar", "Puntuacion", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Puntuacion pro = dtgPuntuacion.SelectedItem as Puntuacion;
            if (pro != null)
            {
                if (MessageBox.Show("Realmente deseas eliminar esta puntuacion?", "Puntuacion", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorPuntuacion.Eliminar(pro.Id))
                    {
                        MessageBox.Show("la puntuacion ha sido eliminado correctamente", "Puntuacion", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTabla();
                    }
                    else
                    {
                        MessageBox.Show("la puntuacion no se pudo eliminar", "Puntuacion", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }
    }
}
