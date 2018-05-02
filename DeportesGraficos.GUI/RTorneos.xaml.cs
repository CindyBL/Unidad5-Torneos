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
using LiteDB;
using Deporte.COMMON.Interfaces;
using Deportes.DAL;
using Deporte.COMMON.Entidades;

namespace DeportesGraficos.GUI
{
    /// <summary>
    /// Lógica de interacción para RTorneos.xaml
    /// </summary>
    public partial class RTorneos : Window
    {
        IManejadorDeporte manejadorDeporte;
        IManejadorEquipo manejadorEquipo;

        public RTorneos()
        {
            InitializeComponent();
            manejadorDeporte = new ManejadorDeDeportes(new RepositorioDeportes());
            manejadorEquipo = new ManejadorDeEquipo(new RepositorioEquipo());
            cmbDeporte.ItemsSource = manejadorDeporte.Listar;
            //cmbEquipo.ItemsSource = manejadorEquipo.Listar;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ir = new MainWindow();
            ir.Show();
            this.Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbDeporte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbDeporte.SelectedItem != null)
            {
                dtgTablaEquipos.ItemsSource = null;
                dtgTablaEquipos.ItemsSource = manejadorEquipo.Listar;
            }
        }
    }
}
