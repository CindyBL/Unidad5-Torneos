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
    /// Lógica de interacción para NuevaPuntuacion.xaml
    /// </summary>
    public partial class NuevaPuntuacion : Window
    {
        public NuevaPuntuacion()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CDeportes elegir = new CDeportes();
            elegir.Owner = this;
            elegir.Show();
        }
    }
}
