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
    /// Lógica de interacción para LoginAdministrador.xaml
    /// </summary>
    public partial class LoginAdministrador : Window
    {
        public LoginAdministrador()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbContraseniaLog.Password) || string.IsNullOrEmpty( txbUsuarioLog.Text))
            {
                MessageBox.Show("Favor  de llenar los dos campos ", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }

            if (txbUsuarioLog.Text == "Administrador" && txbContraseniaLog.Password == "admin")
            {
                Administrador b = new Administrador();
                b.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Lo sentimos el usuario o contraseña no es la correcta ", "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
