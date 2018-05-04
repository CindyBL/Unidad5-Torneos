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

        IManejadorDeporte manejadorDeporte;
        IManejadorEquipo manejadorEquipo;
        IManejadorTorneo manejadorTorneo;
        accion accionTorneo;

        List<DeporteEspecifico> vista = new List<DeporteEspecifico>();
        List<Aleatorios> lista = new List<Aleatorios>();
        List<Aleatorios> lista2 = new List<Aleatorios>();
        List<Torneo> torne = new List<Torneo>();
        List<DeporteEspecifico> nueva = new List<DeporteEspecifico>();
        string equipo1;
        string equipo2;
        public NuevoTorneo()
        {

            InitializeComponent();
            manejadorEquipo = new ManejadorDeEquipo(new RepositorioGenerico<Equipo>());
            manejadorTorneo = new ManejadorDeTorneo(new RepositorioGenerico<Torneo>());
            manejadorDeporte = new ManejadorDeDeportes(new RepositorioGenerico<Deportess>());
            HabilitarCajas(false);
            HabilitarBotones(true);
            ActualizarTabla();
        }

        private void HabilitarBotones(bool habilitados)
        {
            //btnNuevo.IsEnabled = habilitados;
            //btnEditar.IsEnabled = habilitados;
            //btnEliminar.IsEnabled = habilitados;
            //btnGuardar.IsEnabled = !habilitados;
            //btnCancelar.IsEnabled = !habilitados;
        }

        private void HabilitarCajas(bool habilitadas)
        {
            //cmbEquipo1.IsEnabled = habilitadas;
            //cmbEquipo1.ItemsSource = null;
            //cmbEquipo1.ItemsSource = manejadorEquipo.Listar;
            //cmbEquipo2.IsEnabled = habilitadas;
            //cmbEquipo2.ItemsSource = null;
            //cmbEquipo2.ItemsSource = manejadorEquipo.Listar;

        }
        private void ActualizarTabla()
        {
            dtgDatosDeporte.ItemsSource = null;
            dtgDatosDeporte.ItemsSource = manejadorTorneo.Listar;
            dtgDatosDeporte2.ItemsSource = null;
            dtgDatosDeporte2.ItemsSource = manejadorTorneo.Listar;
            dtgPrueba.ItemsSource = null;
            dtgPrueba.ItemsSource = manejadorTorneo.Listar;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CDeportes elegir = new CDeportes();
            elegir.Owner = this;
            elegir.Show();
        }

        private void LimpiarEditarTorneo(bool a)
        {
            cmbTipoDeporteTorneo.ItemsSource = "";

            cmbTipoDeporteTorneo.IsEnabled = a;
            ActualizarTabla();

            btnOrdenarTorneo.IsEnabled = a;
            //btnBuscadorTorneo.IsEnabled = a;
            //EliminacionDeTorneoCompleto.IsEnabled = a;
            btnCancelarTorneo.IsEnabled = a;
            btnEliminarTorneo.IsEnabled = !a;
            btnNuevoTorneo.IsEnabled = !a;

        }

        private void LimpiarTorneo(bool a)
        {
            cmbTipoDeporteTorneo.ItemsSource = "";
            cmbTipoDeporteTorneo.IsEnabled = a;
            btnOrdenarTorneo.IsEnabled = a;
            btnCancelarTorneo.IsEnabled = a;
            btnEliminarTorneo.IsEnabled = !a;
            btnNuevoTorneo.IsEnabled = !a;
            btnOrdenarTorneo.IsEnabled = a;
            //btnBuscadorTorneo.IsEnabled = a;
            //EliminacionDeTorneoCompleto.IsEnabled = a;
            btnCancelarTorneo.IsEnabled = a;
            btnEliminarTorneo.IsEnabled = !a;
            btnNuevoTorneo.IsEnabled = !a;
        }
        private void btnNuevoTorneo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarEditarTorneo(true);
            torne = new List<Torneo>();
            dtgDatosDeporte.ItemsSource = null; //modificado dtg Deporte
            dtgDatosDeporte.ItemsSource = torne; //modificado dtg Deporte
            cmbTipoDeporteTorneo.ItemsSource = manejadorDeporte.Listar;
            
        }


        public void PrimerEquipoTorneo()
        {
            int contador = 0;
            Aleatorios a = new Aleatorios();
            foreach (var item in lista)
            {
                contador++;
                if (contador == 1)
                {
                    equipo1 = item.Datos;
                    a.Datos = item.Datos;
                    lista.Remove(item);
                    lista2.Add(a);
                    break;
                }
            }

        }

        private void SegundoEquipotorneoImpar()
        {
            if (lista.Count >= 1)
            {
                Random r = new Random();
                int val = r.Next(1, lista.Count);
                int contador = 0;

                foreach (var item2 in lista)
                {
                    contador++;

                    if (contador == val)
                    {
                        equipo2 = item2.Datos;
                        lista.Remove(item2);
                        break;
                    }
                }
            }
        }

        private void AgregarListaNumerosImpares(string palabra)
        {
            PrimerEquipoTorneo();
            SegundoEquipotorneoImpar();
            Torneo a = new Torneo()
            {
                Equipo1 = equipo1,
                Equipo2 = equipo2,
                Marcador_1 = 0,
                Marcador_2 = 0,
                FechaTorneo=DateTime.Today,
                Tipo_Deporte = palabra.ToUpper(),
            };
            torne.Add(a);
            manejadorTorneo.Agregar(a);
            dtgPrueba.ItemsSource = null;
            dtgPrueba.ItemsSource = manejadorTorneo.Listar;
            dtgDatosDeporte.ItemsSource = null;
            dtgDatosDeporte.ItemsSource = torne; //deporte
        }

        private void CargarEquipos(string palabra)
        {
            foreach (var item in manejadorEquipo.Listar)
            {
                List<DeporteEspecifico> nueva = new List<DeporteEspecifico>();
                if (item.Deporte== palabra)
                {
                    DeporteEspecifico equipos = new DeporteEspecifico();
                    equipos.Nombre = item.NombreEquipo;
                    equipos.Tipo_De_Deporte = palabra;

                    nueva.Add(equipos);
                }
            }
            dtgDatosDeporte.ItemsSource = null;//deporte
           dtgDatosDeporte.ItemsSource = nueva;//deporte
        }


        private void btnOrdenarTorneo_Click(object sender, RoutedEventArgs e)
        {
            if (cmbTipoDeporteTorneo.SelectedItem == null)
            {
                MessageBox.Show("Error...No ha seleccionado ningun deporte!!!", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string palabra = cmbTipoDeporteTorneo.Text;
            if (manejadorEquipo.ContadorDeBuscarEquipo(palabra) <= 1)
            {
                MessageBox.Show("No se puede realizar los torneos con un solo equipo\nAgregue más equipos", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var item in manejadorEquipo.Listar)
            {
                if (item.Deporte== palabra)
                {
                    Aleatorios equipos = new Aleatorios();
                    equipos.Datos = item.NombreEquipo;
                    lista.Add(equipos);
                }
            }

            if (lista.Count % 2 == 0)
            {
                while (((lista.Count) / 2) > 0)
                {
                    AgregarListaNumerosImpares(palabra);
                }
            }
            else
            {
                while (((lista.Count) / 2) > 0)
                {
                    AgregarListaNumerosImpares(palabra);
                }
                UltimoElemntoRestante(palabra);
            }

            LimpiarTorneo(false);
        }

        private void UltimoElemntoRestante(string palabra)
        {
            PrimerEquipoTorneo();
            if (lista.Count >= 1)
            {
                Random r = new Random();
                int val = r.Next(2, lista2.Count);
                int contador = 0;

                foreach (var item2 in lista2)
                {
                    contador++;

                    if (contador == val)
                    {
                        equipo2 = item2.Datos;
                        break;
                    }
                }
            }
            Torneo a = new Torneo()
            {
                Equipo1 = equipo1,
                Equipo2 = equipo2,
                Tipo_Deporte = palabra.ToUpper(),
                Marcador_1 = 0,
                Marcador_2 = 0,
            };
            torne.Add(a);
            manejadorTorneo.Agregar(a);
            dtgPrueba.ItemsSource = null;
            dtgPrueba.ItemsSource = manejadorTorneo.Listar;
            dtgDatosDeporte.ItemsSource = null;//deporte
            dtgDatosDeporte.ItemsSource = torne;//deporte
        }

        private void btnBuscadorTorneo_Click(object sender, RoutedEventArgs e)
        {
            if (cmbTipoDeporteTorneo.SelectedItem != null)
            {
                List<DeporteEspecifico> vista = new List<DeporteEspecifico>();
                foreach (var item in manejadorEquipo.Listar)
                {
                    if (item.Deporte == cmbTipoDeporteTorneo.Text)
                    {
                        DeporteEspecifico s = new DeporteEspecifico()
                        {
                            Nombre = item.NombreEquipo,
                            Tipo_De_Deporte = cmbTipoDeporteTorneo.Text
                        };
                        vista.Add(s);
                    }
                }
                dtgDatosDeporte2.ItemsSource = null;//deporte2
                dtgDatosDeporte2.ItemsSource = vista;//deporte2
            }
            else
            {
                MessageBox.Show("Seleccione un deporte", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }

        private void btnEliminarTorneo_Click(object sender, RoutedEventArgs e)
        {
            if (dtgPrueba.SelectedItem != null)
            {
                Torneo a = dtgPrueba.SelectedItem as Torneo;
                if (MessageBox.Show("Esta realmente seguro de eliminar los equipos: " + a.Equipo1 + " & " + a.Equipo2, "Torneo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorTorneo.Eliminar(a.Id))
                    {
                        MessageBox.Show("Equipos eliminados", "Torneo", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTabla();
                    }
                    else
                    {
                        MessageBox.Show("No se puedo eliminar el los quipos", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado nada aun\nSeleccione uno", "Torneo Eliminación", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnCancelarTorneo_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Quiere cancelar la operacion?", "Torneo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ActualizarTabla();
                LimpiarEditarTorneo(false);
            }
        }

        private void EliminacionDeTorneoCompleto_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cmbTipoDeporteTorneo.Text))
            {
                MessageBox.Show("Seleccione el Deporte a eliminar", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (cmbTipoDeporteTorneo.SelectedItem != null)
            {
                if (MessageBox.Show("Realmente quiere eliminar el torneo", "Torneo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    foreach (var item in manejadorTorneo.Listar)
                    {
                    }
                    MessageBox.Show("El Torneo ha sido eliminado correctamente", "Torneo", MessageBoxButton.OK, MessageBoxImage.Information);
                    ActualizarTabla();
                    LimpiarEditarTorneo(true);
                    LimpiarTorneo(false);
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado el Deporte para  eliminar", "Torneo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
