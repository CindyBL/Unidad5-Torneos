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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Deportes.BIZ;
using Deporte.COMMON.Entidades;
using Deporte.COMMON.Interfaces;
using Deportes.DAL;


namespace DeportesAdministrador.GUI
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IManejadorContraseña manejadorContraseña;
        public MainWindow()
        {
            InitializeComponent();
            manejadorContraseña = new ManejadorDeContraseña(new RepositorioGenerico<Contraseña>());
            cmbUsuario.ItemsSource = null;
            cmbUsuario.ItemsSource = manejadorContraseña.Listar;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbUsuario.Text == "")

            {
                MessageBox.Show("No ha seleccionado su usuario", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (string.IsNullOrEmpty(txbContraseña.Password))
            {
                MessageBox.Show("Aun no a ingresado la contraseña", "Inicio", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cmbUsuario.SelectedItem != null)

            {
                Contraseña b = cmbUsuario.SelectedItem as Contraseña;
                if (txbContraseña.Password == b.NuevaContraseña)
                {
                    CDeportes ir = new CDeportes();
                    ir.Show();
                    this.Close();
                    MainWindow s = new MainWindow();
                }
                else
                {
                    MessageBox.Show("Contraceña incorrecta", "Inicio", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("No a  seleccionado ningun usuario", "Inicio", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }
    }
}
