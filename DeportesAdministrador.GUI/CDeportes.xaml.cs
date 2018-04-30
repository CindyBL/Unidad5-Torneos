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

namespace DeportesAdministrador.GUI
{
    /// <summary>
    /// Lógica de interacción para CDeportes.xaml
    /// </summary>
    public partial class CDeportes : Window
    {
        public CDeportes()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow elegir = new MainWindow();
            elegir.Owner = this;
            elegir.Show();
        }

        private void btnDeporte_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            NuevoDeporte elegir = new NuevoDeporte();
            elegir.Owner = this;
            elegir.Show();
        }

        private void btnEquipo_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            NuevoEquipo elegir = new NuevoEquipo();
            elegir.Owner = this;
            elegir.Show();
        }

        private void btnTorneo_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            NuevoTorneo elegir = new NuevoTorneo();
            elegir.Owner = this;
            elegir.Show();
        }

        private void btnPuntuacion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPuntuacion_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            NuevaPuntuacion elegir = new NuevaPuntuacion();
            elegir.Owner = this;
            elegir.Show();
        }

        private void btnContraseña_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            NuevaContraseña elegir = new NuevaContraseña();
            elegir.Owner = this;
            elegir.Show();
        }
    }
}
