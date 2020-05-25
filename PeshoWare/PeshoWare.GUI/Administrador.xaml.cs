using PeshoWare.BIZ.Manejadores;
using PeshoWare.COMMON.Entidades;
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
    /// Lógica de interacción para Administrador.xaml
    /// </summary>
    public partial class Administrador : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }

        IManejadorUsuario ManejadorUsuario;
        IManejadorCliente ManejadorCliente;
        IManejadorPrestamo ManejadorPrestamo;

        accion accionUsuario;
        accion accionCliente;
        accion accionPrestamo;
        accion accionPago;

        public Administrador()
        {
            InitializeComponent();
            ManejadorUsuario = new ManejadorUsuario(new RepositorioUsuario());
            ManejadorCliente = new ManejadorCliente(new RepositorioCliente());
            ManejadorPrestamo = new ManejadorPrestamo(new RepositorioPrestamo());

            BotonesUsuarioEdicion(false);
            LimpiarCamposUsuario();
            ActualizarTablaUsuario();

            BotonesClienteEdicion(false);
            LimpiarCamposCliente();
            ActualizarTablaCliente();

            BotonesPrestamoEdicion(false);
            LimpiarCamposPrestamo();
            ActualizarTablaPrestamo();

            BotonesPagoEdicion(false);
            LimpiarCamposPago();
            ActualizarTablaPago();
        }

        private void BotonesPagoEdicion(bool value)
        {
            btnPagoCancelar.IsEnabled = value;
            btnPagoEditar.IsEnabled = !value;
            //btnPagoEliminar.IsEnabled = !value;
            btnPagoGuardar.IsEnabled = value;
            //btnPagoNuevo.IsEnabled = !value;
            txtClientePago.IsEnabled = value;
            txbAbono.IsEnabled = value;
            txbFechaPago.IsEnabled = value;
        }

        private void ActualizarTablaPago()
        {
            dtgPagos.ItemsSource = null;
            dtgPagos.ItemsSource = ManejadorPrestamo.Listar;
            dtgPrestamos.ItemsSource = null;
            dtgPrestamos.ItemsSource = ManejadorPrestamo.Listar;
        }

        private void LimpiarCamposPago()
        {
            txtClientePago.Text = "";
            txbAbono.Clear();
            txbFechaPago.Text = "";
            txbPagoId.Text = "";
        }

        private void ActualizarTablaPrestamo()
        {
            dtgPrestamos.ItemsSource = null;
            dtgPrestamos.ItemsSource = ManejadorPrestamo.Listar;
            cmbClientePrestamo.ItemsSource = null;
            cmbClientePrestamo.ItemsSource = ManejadorCliente.Listar;
        }

        private void LimpiarCamposPrestamo()
        {
            cmbClientePrestamo.Text = "";
            txbMonto.Clear();
            txbFechaPrestamo.Clear();
            txbPrestamoId.Text = "";
        }

        private void BotonesPrestamoEdicion(bool value)
        {
            btnPrestamoCancelar.IsEnabled = value;
            //btnPrestamoEditar.IsEnabled = !value;
            btnPrestamoGuardar.IsEnabled = value;
            btnPrestamoNuevo.IsEnabled = !value;
            btnPrestamoEliminar.IsEnabled = !value;
            cmbClientePrestamo.IsEnabled = value;
            txbMonto.IsEnabled = value;
            txbFechaPrestamo.IsEnabled = value;
        }

        private void ActualizarTablaCliente()
        {
            dtgClientes.ItemsSource = null;
            dtgClientes.ItemsSource = ManejadorCliente.Listar;
            cmbClientePrestamo.ItemsSource = null;
            cmbClientePrestamo.ItemsSource = ManejadorCliente.Listar;
        }

        private void LimpiarCamposCliente()
        {
            txbNombreCliente.Clear();
            txbApellidosCliente.Clear();
            txbTelefonoCliente.Clear();
            txbCorreoCliente.Clear();
            txbClienteId.Text = "";
        }

        private void BotonesClienteEdicion(bool value)
        {
            btnClienteCancelar.IsEnabled = value;
            btnClienteEditar.IsEnabled = !value;
            btnClienteEliminar.IsEnabled = !value;
            btnClienteGuardar.IsEnabled = value;
            btnClienteNuevo.IsEnabled = !value;
            txbNombreCliente.IsEnabled = value;
            txbApellidosCliente.IsEnabled = value;
            txbTelefonoCliente.IsEnabled = value;
            txbCorreoCliente.IsEnabled = value;
        }

        private void ActualizarTablaUsuario()
        {
            dtgUsuarios.ItemsSource = null;
            dtgUsuarios.ItemsSource = ManejadorUsuario.Listar;
        }

        private void LimpiarCamposUsuario()
        {
            txbNombreUsuario.Clear();
            txbContraseniaUsuario.Clear();
            cmbUsuarioTipo.Text="";
            txbUsuarioId.Text = "";
        }

        private void BotonesUsuarioEdicion(bool value)
        {
            btnUsuarioCancelar.IsEnabled = value;
            btnUsuarioEditar.IsEnabled = !value;
            btnUsuarioEliminar.IsEnabled = !value;
            btnUsuarioGuardar.IsEnabled = value;
            btnUsuarioNuevo.IsEnabled = !value;
            txbNombreUsuario.IsEnabled = value;
            txbContraseniaUsuario.IsEnabled = value;
            cmbUsuarioTipo.IsEnabled = value;
        }

        private void btnUsuarioNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposUsuario();
            BotonesUsuarioEdicion(true);
            accionUsuario = accion.Nuevo;
        }

        private void btnUsuarioEditar_Click(object sender, RoutedEventArgs e)
        {
            Usuario usu = dtgUsuarios.SelectedItem as Usuario;
            if (usu != null)
            {
                txbUsuarioId.Text = usu.Id;
                txbNombreUsuario.Text = usu.NombreUsuario;
                txbContraseniaUsuario.Text = usu.Contrasenia;
                cmbUsuarioTipo.Text = usu.UsuarioTipo;
                accionUsuario = accion.Editar;
                BotonesUsuarioEdicion(true);
            }
            else
            {
                MessageBox.Show("No ha seleccionado nada", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void btnUsuarioGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNombreUsuario.Text) || string.IsNullOrEmpty(txbContraseniaUsuario.Text) || string.IsNullOrEmpty(cmbUsuarioTipo.Text))
            {
                MessageBox.Show("Faltan datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (accionUsuario == accion.Nuevo)
            {
                Usuario usu = new Usuario()
                {
                    NombreUsuario = txbNombreUsuario.Text,
                    UsuarioTipo = cmbUsuarioTipo.Text,
                    Contrasenia = txbContraseniaUsuario.Text
                };
                if (ManejadorUsuario.Agregar(usu))
                {
                    MessageBox.Show("Usuario agregado correctamente", "inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposUsuario();
                    ActualizarTablaUsuario();
                    BotonesUsuarioEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Usuario no pudo ser agregado", "inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Usuario usu = dtgUsuarios.SelectedItem as Usuario;
                usu.NombreUsuario = txbNombreUsuario.Text;
                usu.UsuarioTipo = cmbUsuarioTipo.Text;
                usu.Contrasenia = txbContraseniaUsuario.Text;
                if (ManejadorUsuario.Modificar(usu))
                {
                    MessageBox.Show("Usuario modificado correctamente", "inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposUsuario();
                    ActualizarTablaUsuario();
                    BotonesUsuarioEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Usuario no pudo ser actualizado", "inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnUsuarioCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposUsuario();
            BotonesUsuarioEdicion(false);
        }

        private void btnUsuarioEliminar_Click(object sender, RoutedEventArgs e)
        {
            Usuario usu = dtgUsuarios.SelectedItem as Usuario;
            if (usu != null)
            {
                if (MessageBox.Show("Realmente desea eliminar este Usuario?", "Inventarios", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (ManejadorUsuario.Eliminar(usu.Id))
                    {
                        MessageBox.Show("Usuario eliminado", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaUsuario();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el Usuario", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado nada", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BtnClienteNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposCliente();
            BotonesClienteEdicion(true);
            accionCliente = accion.Nuevo;
        }

        private void BtnClienteEditar_Click(object sender, RoutedEventArgs e)
        {
            Cliente cli = dtgClientes.SelectedItem as Cliente;
            if (cli != null)
            {
                txbClienteId.Text = cli.Id;
                txbNombreCliente.Text = cli.NombreCliente;
                txbApellidosCliente.Text = cli.ApellidosCliente;
                txbTelefonoCliente.Text = cli.TelefonoCliente;
                txbCorreoCliente.Text = cli.CorreoCliente;
                accionCliente = accion.Editar;
                BotonesClienteEdicion(true);
            }
            else
            {
                MessageBox.Show("No ha seleccionado nada", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BtnClienteGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNombreCliente.Text) || string.IsNullOrEmpty(txbApellidosCliente.Text) || string.IsNullOrEmpty(txbTelefonoCliente.Text))
            {
                MessageBox.Show("Faltan datos, asegurese de agregar el telefono", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            foreach (var item in txbTelefonoCliente.Text)
            {
                if (!(char.IsNumber(item)))
                {
                    MessageBox.Show("Error en el Telefono, solo ingrese numeros", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (accionCliente == accion.Nuevo)
            {
                Cliente cli = new Cliente()
                {
                    NombreCliente = txbNombreCliente.Text,
                    ApellidosCliente = txbApellidosCliente.Text,
                    TelefonoCliente = txbTelefonoCliente.Text,
                    CorreoCliente = txbCorreoCliente.Text
                };
                if (ManejadorCliente.Agregar(cli))
                {
                    MessageBox.Show("Cliente agregado correctamente", "inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposCliente();
                    ActualizarTablaCliente();
                    BotonesClienteEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Cliente no pudo ser agregado", "inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Cliente cli = dtgClientes.SelectedItem as Cliente;
                cli.NombreCliente = txbNombreCliente.Text;
                cli.ApellidosCliente = txbApellidosCliente.Text;
                cli.TelefonoCliente = txbTelefonoCliente.Text;
                cli.CorreoCliente = txbCorreoCliente.Text;
                if (ManejadorCliente.Modificar(cli))
                {
                    MessageBox.Show("Cliente modificado correctamente", "inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposCliente();
                    ActualizarTablaCliente();
                    BotonesClienteEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Cliente no pudo ser actualizado", "inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnClienteCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposCliente();
            BotonesClienteEdicion(false);
        }

        private void BtnClienteEliminar_Click(object sender, RoutedEventArgs e)
        {
            Cliente cli = dtgClientes.SelectedItem as Cliente;
            if (cli != null)
            {
                if (MessageBox.Show("Realmente desea eliminar este Cliente?", "Inventarios", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (ManejadorCliente.Eliminar(cli.Id))
                    {
                        MessageBox.Show("Cliente eliminado", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaCliente();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el Cliente", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado nada", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BtnPrestamoNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposPrestamo();
            BotonesPrestamoEdicion(true);
            accionPrestamo = accion.Nuevo;
        }

       /* private void BtnPrestamoEditar_Click(object sender, RoutedEventArgs e)
        {
            Prestamo pre = dtgPrestamos.SelectedItem as Prestamo;
            if (pre != null)
            {
                txbPrestamoId.Text = pre.Id;
                cmbClientePrestamo.Text = pre.N_ClientePrestamo;
                txbMonto.Text = pre.Monto.ToString();
                txbFechaPrestamo.Text = pre.FechaPrestamo.ToString();
                accionPrestamo = accion.Editar;
                ActualizarTablaPago();
                BotonesPrestamoEdicion(true);
            }
            else
            {
                MessageBox.Show("No ha seleccionado nada", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        */
        private void BtnPrestamoGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cmbClientePrestamo.Text) || string.IsNullOrEmpty(txbMonto.Text))
            {
                MessageBox.Show("Faltan datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            foreach (var item in txbMonto.Text)
            {
                if (!(char.IsNumber(item)))
                {
                    MessageBox.Show("Error en el Monto, solo ingrese numeros", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (accionPrestamo == accion.Nuevo)
            {
                Prestamo pre = new Prestamo()
                {
                    N_ClientePrestamo = cmbClientePrestamo.Text,
                    Monto = float.Parse(txbMonto.Text),
                    FechaPrestamo = DateTime.Now
                };
                if (ManejadorPrestamo.Agregar(pre))
                {
                    MessageBox.Show("Prestamo agregado correctamente", "inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposPrestamo();
                    ActualizarTablaPrestamo();
                    ActualizarTablaPago();
                    BotonesPrestamoEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Prestamo no pudo ser agregado", "inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Prestamo pre = dtgPrestamos.SelectedItem as Prestamo;
                pre.N_ClientePrestamo = cmbClientePrestamo.Text;
                pre.Monto = float.Parse(txbMonto.Text);
                pre.FechaPrestamo = DateTime.Now;
                if (ManejadorPrestamo.Modificar(pre))
                {
                    MessageBox.Show("Prestamo modificado correctamente", "inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposPrestamo();
                    ActualizarTablaPrestamo();
                    ActualizarTablaPago();
                    BotonesPrestamoEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Prestamo no pudo ser actualizado", "inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnPrestamoCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposPrestamo();
            BotonesPrestamoEdicion(false);
        }

      
        private void BtnPagoEditar_Click(object sender, RoutedEventArgs e)
        {
            Prestamo pag = dtgPagos.SelectedItem as Prestamo;
            if (pag != null)
            {
                txbPagoId.Text = pag.Id;
                txtClientePago.Text = pag.N_ClientePrestamo;
                txbAbono.Text = "0";
                txbFechaPago.Text = pag.FechaPago.ToString();
                //txbMonto.Text = pag.Monto.ToString();

                accionPago = accion.Editar;
                BotonesPagoEdicion(true);
            }
            else
            {
                MessageBox.Show("Por favor seleccion el CLIENTE que se le realizara el Abono o Pago", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BtnPagoGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtClientePago.Text) || string.IsNullOrEmpty(txbAbono.Text))
            {
                MessageBox.Show("Faltan datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            foreach (var item in txbAbono.Text)
            {
                if (!(char.IsNumber(item)))
                {
                    MessageBox.Show("Error en el Abono, solo ingrese numeros", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (accionPago == accion.Nuevo)
            {
                Prestamo pag = new Prestamo()
                {
                    N_ClientePrestamo = txtClientePago.Text,
                    Abono = float.Parse(txbAbono.Text),
                    // Monto = float.Parse(txbAbono.Text) - float.Parse(txbMonto.Text),
                    FechaPago = DateTime.Now
                };
                //if (int.Parse(pag.Monto.ToString()) < int.Parse(txbAbono.Text))
                //{
                //    MessageBox.Show("El Abono sobrepasa el monto", "inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                //    return;
                //}
                if (ManejadorPrestamo.Agregar(pag))
                {
                    MessageBox.Show("Pago agregado correctamente", "inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposPago();
                    ActualizarTablaPago();
                    BotonesPagoEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Pago no pudo ser agregado", "inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Prestamo pag = dtgPagos.SelectedItem as Prestamo;
                pag.N_ClientePrestamo = txtClientePago.Text;
                pag.Abono = float.Parse(txbAbono.Text);
                //pag.Monto = float.Parse(pag.Monto);
                pag.Monto = pag.Monto - int.Parse(txbAbono.Text);
                pag.FechaPago = DateTime.Now;

                if (ManejadorPrestamo.Modificar(pag))
                {
                    MessageBox.Show("Pago agregado correctamente", "inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposPago();
                    ActualizarTablaPago();
                    BotonesPagoEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Pago no pudo ser actualizado", "inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnPagoCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposPago();
            BotonesPagoEdicion(false);
        }

        /*private void BtnPagoEliminar_Click(object sender, RoutedEventArgs e)
        {
            
        }
        */

        private void BtnUsuarioRegresar_Click(object sender, RoutedEventArgs e)
        {
            Menu a = new Menu();
            a.Show();
            this.Close();
        }

        private void BtnClienteRegresar_Click(object sender, RoutedEventArgs e)
        {
            Menu a = new Menu();
            a.Show();
            this.Close();
        }

        private void BtnPrestamosRegresar_Click(object sender, RoutedEventArgs e)
        {
            Menu a = new Menu();
            a.Show();
            this.Close();
        }

        private void BtnPagosRegresar_Click(object sender, RoutedEventArgs e)
        {
            Menu a = new Menu();
            a.Show();
            this.Close();
        }

        private void btnPrestamoEliminar_Click_1(object sender, RoutedEventArgs e)
        {
            Prestamo pre = dtgPrestamos.SelectedItem as Prestamo;
            if (pre != null)
            {
                if (MessageBox.Show("Realmente desea eliminar este Prestamo?", "Inventarios", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (ManejadorPrestamo.Eliminar(pre.Id))
                    {
                        MessageBox.Show("Prestamo eliminado", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaPrestamo();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el Prestamo", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado nada", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ActualizarTablaPago();
        }
    }
}
