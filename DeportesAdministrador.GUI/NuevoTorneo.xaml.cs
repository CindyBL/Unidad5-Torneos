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
    /// Lógica de interacción para NuevoTorneo.xaml
    /// </summary>
    public partial class NuevoTorneo : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }

        IManejadorEquipo manejadorEquipo;
        IManejadorTorneo manejadorTorneo;
        accion accionTorneo;
        public NuevoTorneo()
        {
            InitializeComponent();
            manejadorEquipo = new ManejadorDeEquipo(new RepositorioGenerico<Equipo>());
            manejadorTorneo = new ManejadorDeTorneo(new RepositorioGenerico<Torneo>());
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
            cmbEquipo1.IsEnabled = habilitadas;
            cmbEquipo1.ItemsSource = null;
            cmbEquipo1.ItemsSource = manejadorEquipo.Listar;
            cmbEquipo2.IsEnabled = habilitadas;
            cmbEquipo2.ItemsSource = null;
            cmbEquipo2.ItemsSource = manejadorEquipo.Listar;

        }
        private void ActualizarTabla()
        {
            dtgTorneo.ItemsSource = null;
            dtgTorneo.ItemsSource = manejadorTorneo.Listar;
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
            accionTorneo = accion.Nuevo;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Torneo pro = dtgTorneo.SelectedItem as Torneo;
            if (pro != null)
            {
                HabilitarCajas(true);
                txbEquipoId.Text = pro.Id.ToString();
                cmbEquipo1.Text = pro.Equipo1;
                cmbEquipo2.Text = pro.Equipo2;
                accionTorneo = accion.Editar;
                HabilitarBotones(false);
            }
            else
            {
                MessageBox.Show("Seleccione el torneo que desea eliminar", "Torneo", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionTorneo == accion.Nuevo)
            {
                Torneo pro = new Torneo()
                {
                    Equipo1 = cmbEquipo1.Text,
                    Equipo2 = cmbEquipo2.Text,
                };
                if (manejadorTorneo.Agregar(pro))
                {
                    MessageBox.Show("Torneo agregado correctamente", "Torneo", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("El Torneo no se pudo agregar", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Torneo pro = dtgTorneo.SelectedItem as Torneo;
                pro.Equipo1 = cmbEquipo1.Text;
                pro.Equipo2 = cmbEquipo2.Text;
                if (manejadorTorneo.Modificar(pro))
                {
                    MessageBox.Show("Torneo modificado correctamente", "Torneo", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("El torneo no se pudo actualizar", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Torneo pro = dtgTorneo.SelectedItem as Torneo;
            if (pro != null)
            {
                if (MessageBox.Show("Realmente deseas eliminar este torneo?", "Torneo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorTorneo.Eliminar(pro.Id))
                    {
                        MessageBox.Show("El torneo ha sido eliminado correctamente", "Torneo", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTabla();
                    }
                    else
                    {
                        MessageBox.Show("El torneo no se pudo eliminar", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
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
