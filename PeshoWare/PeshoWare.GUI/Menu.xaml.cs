using PeshoWare.BIZ.Manejadores;
using PeshoWare.COMMON.Interfaz;
using PeshoWare.DAL.Repositorios;
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
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        IManejadorUsuario manejadorUsuario;

        public Menu()
        {
            InitializeComponent();
            manejadorUsuario = new ManejadorUsuario(new RepositorioUsuario());
        }

        private void BtnTienda_Click(object sender, RoutedEventArgs e)
        {
            LogIn abrir = new LogIn();
            abrir.Show();
            this.Close();
        }

        private void BtnAdministrador_Click(object sender, RoutedEventArgs e)
        {
            LoginAdministrador abrir = new LoginAdministrador ();
            abrir.Show();
            this.Close();
        }

        private void btnAcercade_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("PeshoWare \nVersion = 1.1 \n2019-12-1 \n--By Neotech--", "PeshoWare", MessageBoxButton.OK, MessageBoxImage.Information );
            return;
        }
    }
}
