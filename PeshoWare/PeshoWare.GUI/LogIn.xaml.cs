using PeshoWare.BIZ.Manejadores;
using PeshoWare.COMMON.Entidades;
using PeshoWare.COMMON.Interfaz;
using PeshoWare.DAL.Repositorios;
using PeshoWare.GUI;
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

namespace PeshoWare.GUI
{
    /// <summary>
    /// Lógica de interacción para LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        IManejadorUsuario manejadorUsuario;

        public LogIn()
        {
            InitializeComponent();
            manejadorUsuario = new ManejadorUsuario(new RepositorioUsuario());
            cmbUsuarioLog.ItemsSource = null;
            cmbUsuarioLog.ItemsSource = manejadorUsuario.Listar;
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (cmbUsuarioLog.Text == "")
            {
                MessageBox.Show("Error", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(txbContraseniaLog .Password))
            {
                MessageBox.Show("Favor de ingresar la contraseña", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            if (string.IsNullOrEmpty(txbContraseniaLog .Password))
            {
                MessageBox.Show("No ha ingresado la contraseña", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (cmbUsuarioLog .SelectedItem != null)
            {
                Usuario a = cmbUsuarioLog .SelectedItem as Usuario;
                if (txbContraseniaLog .Password == a.Contrasenia)
                {
                    Tienda b = new Tienda();
                    b.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Contraseña Incorrecta", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun usuario", "Usario", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
    }
}
