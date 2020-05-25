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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PeshoWare.GUI
{
    /// <summary>
    /// Lógica de interacción para SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            Loadprogressbar();
            pb1.ValueChanged += Pb1_valueChanged;
        }

        private void Pb1_valueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (pb1.Value == 100)
            {
                Menu m = new Menu();
                m.Show();
                this.Close();
            }
        }

        private void Loadprogressbar()
        {
            Duration dur = new Duration(TimeSpan.FromSeconds(30));
            DoubleAnimation dblani = new DoubleAnimation(200.0, dur);
            pb1.BeginAnimation(System.Windows.Controls.Primitives.RangeBase.ValueProperty, dblani);
        }
    }
}
