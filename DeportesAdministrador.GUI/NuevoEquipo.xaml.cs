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
    /// Lógica de interacción para NuevoEquipo.xaml
    /// </summary>
    public partial class NuevoEquipo : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }

        IManejadorEquipo manejadorEquipo;
        IManejadorDeporte manejadorDeporte;
        accion accionEquipo;
        public NuevoEquipo()
        {
            InitializeComponent();
            manejadorEquipo= new ManejadorDeEquipo(new RepositorioEquipo());
            manejadorDeporte = new ManejadorDeDeportes(new RepositorioDeportes());
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
            txbNombreEquipo.Clear();
            txbNombreEquipo.IsEnabled = habilitadas;
            cmbDeporte.IsEnabled = habilitadas;
            cmbDeporte.ItemsSource = null;
            cmbDeporte.ItemsSource = manejadorDeporte.Listar;

        }
        private void ActualizarTabla()
        {
            dtgEquipos.ItemsSource = null;
            dtgEquipos.ItemsSource = manejadorEquipo.Listar;
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
            accionEquipo = accion.Nuevo;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            Equipo pro = dtgEquipos.SelectedItem as Equipo;
            if (pro != null)
            {
                HabilitarCajas(true);
                txbEquipoId.Text = pro.Id;
                txbNombreEquipo.Text = pro.NombreEquipo;
                cmbDeporte.Text = pro.Deporte;
                accionEquipo = accion.Editar;
                HabilitarBotones(false);
            }
            else
            {
                MessageBox.Show("Seleccione el equipo que desea editar", "Equipo", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCajas(false);
            HabilitarBotones(true);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Equipo pro = dtgEquipos.SelectedItem as Equipo;
            if (pro != null)
            {
                if (MessageBox.Show("Realmente deseas eliminar este equipo?", "Equipos", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorEquipo.Eliminar(pro.Id))
                    {
                        MessageBox.Show("El equipo ha sido eliminado correctamente", "Equipos", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTabla();
                    }
                    else
                    {
                        MessageBox.Show("El equipo no se pudo eliminar", "Equipos", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (accionEquipo == accion.Nuevo)
            {
                Equipo pro = new Equipo()
                {
                    NombreEquipo = txbNombreEquipo.Text,
                    Deporte = cmbDeporte.Text,
                };
                if (manejadorEquipo.Agregar(pro))
                {
                    MessageBox.Show("Equipo agregado correctamente", "Equipo", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("El equipo no se pudo agregar", "Equipos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Equipo pro = dtgEquipos.SelectedItem as Equipo;
                pro.NombreEquipo = txbNombreEquipo.Text;
                pro.Deporte = cmbDeporte.Text;
                if (manejadorEquipo.Modificar(pro))
                {
                    MessageBox.Show("Equipo modificado correctamente", "Equipos", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    HabilitarBotones(true);
                    HabilitarCajas(false);
                }
                else
                {
                    MessageBox.Show("El equipo no se pudo actualizar", "Farmacia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
