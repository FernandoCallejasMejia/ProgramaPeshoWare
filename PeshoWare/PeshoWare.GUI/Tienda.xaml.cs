using Microsoft.Win32;
using PeshoWare.BIZ.Manejadores;
using PeshoWare.COMMON.Entidades;
using PeshoWare.COMMON.Interfaz;
using PeshoWare.DAL.Repositorios;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para Tienda.xaml
    /// </summary>
    public partial class Tienda : Window
    {
        enum accion
        {
            Nuevo,
            Editar
        }

        IManejadorProveedor ManejadorProveedor;
        IManejadorArticulo ManejadorArticulo;
        IManejadorTicket ManejadorTicket;
        List<Venta> venta;

        accion accionProveedor;
        accion accionArticulo;

        public Tienda()
        {
            InitializeComponent();
            ManejadorProveedor = new ManejadorProveedor(new RepositorioProveedor());
            ManejadorArticulo = new ManejadorArticulo(new RepositorioArticulo());
            ManejadorTicket = new ManejadorTicket(new RepositorioTicket());
            venta = new List<Venta>();

            BotonesProveedorEdicion(false);
            LimpiarCamposProveedor();
            ActualizarTablaProveedor();

            BotonesArticuloEdicion(false);
            LimpiarCamposArticulo();
            ActualizarTablaArticulo();

            BotonesVentaEdicion(false);
            LimpiarCamposVenta();
            ActualizarTablas();

            TablaReportes();
            LimpiarCamposReportes();
            BotonesReportesEdicion(false);
        }

        private void BotonesReportesEdicion(bool value)
        {
            dtgTablaCompra.IsEnabled = value;
            txbTotalAlmacen.IsEnabled = value;
            txbFechaAlmacen.IsEnabled = value;
            txbFolioAlmacen.IsEnabled = value;
			
            TablaReportes();
        }

        private void LimpiarCamposReportes()
        {
            dtgTablaCompra.ItemsSource = null;
            txbTotalAlmacen.Clear();
            txbFechaAlmacen.Clear();
            TablaReportes();
            txbFolioAlmacen.Clear();
        }

        private void TablaReportes()
        {
            dtgAlmacenReportes.ItemsSource = null;
            dtgAlmacenReportes.ItemsSource = ManejadorTicket.Listar;
        }

        private void ActualizarTablas()
        {
            txbFechaVenta.Text = DateTime.Now.ToString();
            dtgVentaAlmacen.ItemsSource = null;
            dtgVentaAlmacen.ItemsSource = ManejadorArticulo.Listar;
            dtgVenta.ItemsSource = null;
            dtgVenta.ItemsSource = venta;
        }

        private void LimpiarCamposVenta()
        {
            dtgVenta.ItemsSource = null;
            venta = new List<Venta>();
            ActualizarTablas();
            txbCantidadVP.Clear();
            txbFolioVenta.Clear();
        }

        private void BotonesVentaEdicion(bool value)
        {
            btnAgregarVenta.IsEnabled = value;
            btnEliminarVenta.IsEnabled = value;
            btnCancelarVenta.IsEnabled = value;
            btnRealizarVenta.IsEnabled = value;
            btnNuevaVenta.IsEnabled = !value;
            txbCantidadVP.IsEnabled = value;
            txbFechaVenta.IsEnabled = value;
            txbFolioVenta.IsEnabled = value;
			txbBuscar.IsEnabled = value;
		}

        private void ActualizarTablaArticulo()
        {
            dtgProductos.ItemsSource = null;
            dtgProductos.ItemsSource = ManejadorArticulo.Listar;
            cmbProveedor.ItemsSource = null;
            cmbProveedor.ItemsSource = ManejadorProveedor.Listar;
        }

        private void LimpiarCamposArticulo()
        {
            txbNombreProducto.Clear();
            txbPrecioCompra.Clear();
            txbPrecioVenta.Clear();
            txbDescripcion.Clear();
            txbCantidad.Clear();
            cmbProveedor.Text = "";
            txbProductoId.Text = "";
            imgArticulo.Source = null;
        }

        private void BotonesArticuloEdicion(bool value)
        {
            btnNuevoProducto.IsEnabled = !value;
            btnGuardarProducto.IsEnabled = value;
            btnEditarProducto.IsEnabled = !value;
            btnCancelarProducto.IsEnabled = value;
            btnEliminarProducto.IsEnabled = !value;
            btnImagen.IsEnabled = value;
            txbNombreProducto.IsEnabled = value;
            txbDescripcion.IsEnabled = value;
            txbCantidad.IsEnabled = value;
            txbPrecioCompra.IsEnabled = value;
            txbPrecioVenta.IsEnabled = value;
            cmbProveedor.IsEnabled = value;
        }

        private void ActualizarTablaProveedor()
        {
            dtgProveedores.ItemsSource = null;
            dtgProveedores.ItemsSource = ManejadorProveedor.Listar;
            cmbProveedor.ItemsSource = ManejadorProveedor.Listar;
        }

        private void LimpiarCamposProveedor()
        {
            txbNombreProveedor.Clear();
            txbProveedorCorreo.Clear();
            txbProveedorDireccion.Clear();
            txbProveedorTelefono.Clear();
            txbProveedorId.Text = "";
        }

        private void BotonesProveedorEdicion(bool value)
        {
            btnNuevoProveedor.IsEnabled = !value;
            btnGuardarProveedor.IsEnabled = value;
            btnEditarProveedor.IsEnabled = !value;
            btnCancelarProveedor.IsEnabled = value;
            btnEliminarProveedor.IsEnabled = !value;
            txbNombreProveedor.IsEnabled = value;
            txbProveedorCorreo.IsEnabled = value;
            txbProveedorTelefono.IsEnabled = value;
            txbProveedorDireccion.IsEnabled = value;
        }

        private void BtnNuevoProveedor_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposProveedor();
            BotonesProveedorEdicion(true);
            accionProveedor = accion.Nuevo;
        }

        private void BtnEditarProveedor_Click(object sender, RoutedEventArgs e)
        {
            Proveedor pro = dtgProveedores.SelectedItem as Proveedor;
            if (pro != null)
            {
                txbProveedorId.Text = pro.Id;
                txbNombreProveedor.Text = pro.NombreProveedor;
                txbProveedorCorreo.Text = pro.CorreoProveedor;
                txbProveedorTelefono.Text = pro.TelefonoProveedor;
                txbProveedorDireccion.Text = pro.DireccionProveedor;
                accionProveedor = accion.Editar;
                BotonesProveedorEdicion(true);
            }
            else
            {
                MessageBox.Show("No ha seleccionado nada", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BtnGuardarProveedor_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNombreProveedor.Text) || string.IsNullOrEmpty(txbProveedorTelefono.Text) || string.IsNullOrEmpty(txbProveedorDireccion.Text))
            {
                MessageBox.Show("Falta el nombre del proveedor, el telefono o la direccion", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            foreach (var item in txbProveedorTelefono.Text)
            {
                if (!(char.IsNumber(item)))
                {
                    MessageBox.Show("Error en el Telefono, solo ingrese numeros", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (accionProveedor == accion.Nuevo)
            {
                Proveedor pro = new Proveedor()
                {
                    NombreProveedor = txbNombreProveedor.Text,
                    CorreoProveedor = txbProveedorCorreo.Text,
                    TelefonoProveedor = txbProveedorTelefono.Text,
                    DireccionProveedor = txbProveedorDireccion.Text
                };
                if (ManejadorProveedor.Agregar(pro))
                {
                    MessageBox.Show("Proveedor agregado correctamente", "inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposProveedor();
                    ActualizarTablaProveedor();
                    BotonesProveedorEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Proveedor no pudo ser agregado", "inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Proveedor pro = dtgProveedores.SelectedItem as Proveedor;
                pro.NombreProveedor = txbNombreProveedor.Text;
                pro.CorreoProveedor = txbProveedorCorreo.Text;
                pro.TelefonoProveedor = txbProveedorTelefono.Text;
                pro.DireccionProveedor = txbProveedorDireccion.Text;
                if (ManejadorProveedor.Modificar(pro))
                {
                    MessageBox.Show("Proveedor modificado correctamente", "inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposProveedor();
                    ActualizarTablaProveedor();
                    BotonesProveedorEdicion(false);
                }
                else
                {
                    MessageBox.Show("El Proveedor no pudo ser actualizado", "inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnCancelarProveedor_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposProveedor();
            BotonesProveedorEdicion(false);
        }

        private void BtnEliminarProveedor_Click(object sender, RoutedEventArgs e)
        {
            Proveedor pro = dtgProveedores.SelectedItem as Proveedor;
            if (pro != null)
            {
                if (MessageBox.Show("Realmente desea eliminar este Proveedor?", "Inventarios", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (ManejadorProveedor.Eliminar(pro.Id))
                    {
                        MessageBox.Show("Proveedor eliminado", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaProveedor();
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

        private void BtnNuevoProducto_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposArticulo();
            BotonesArticuloEdicion(true);
            accionArticulo = accion.Nuevo;
        }

        public byte[] ImageToByte(ImageSource image)
        {
            if (image != null)
            {
                MemoryStream memStream = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image as BitmapSource));
                encoder.Save(memStream);
                return memStream.ToArray();
            }
            else
            {
                return null;
            }
        }

        public ImageSource ByteToImage(byte[] imageData)
        {
            if (imageData == null)
            {
                return null;
            }
            else
            {
                BitmapImage biImg = new BitmapImage();
                MemoryStream ms = new MemoryStream(imageData);
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();
                ImageSource imgSrc = biImg as ImageSource;
                return imgSrc;
            }
        }

        private void BtnEditarProducto_Click(object sender, RoutedEventArgs e)
        {
            Articulo ar = dtgProductos.SelectedItem as Articulo;
            if (ar != null)
            {
                txbProductoId.Text = ar.Id;
                txbNombreProducto.Text = ar.NombreArticulo;
                txbDescripcion.Text = ar.Descripcion;
                cmbProveedor.Text = ar.N_Proveedor;
                txbPrecioCompra.Text = ar.CompraArticulo;
                txbPrecioVenta.Text = ar.VentaArticulo;
                txbCantidad.Text = ar.CantidadArticulo;
                imgArticulo.Source = ByteToImage(ar.ImagenArticulo);
                accionArticulo = accion.Editar;
                BotonesArticuloEdicion(true);
            }
            else
            {
                MessageBox.Show("No ha seleccionado nada", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BtnGuardarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNombreProducto.Text) || string.IsNullOrEmpty(cmbProveedor.Text) || string.IsNullOrEmpty(txbPrecioCompra.Text) || string.IsNullOrEmpty(txbPrecioVenta.Text) || string.IsNullOrEmpty(txbCantidad.Text))
            {
                MessageBox.Show("Faltan datos", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            foreach (var item in txbPrecioCompra.Text)
            {
                if (!(char.IsNumber(item)))
                {
                    MessageBox.Show("Error en el precio de compra, solo ingrese numeros", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            foreach (var item in txbPrecioVenta.Text)
            {
                if (!(char.IsNumber(item)))
                {
                    MessageBox.Show("Error en el precio de venta, solo ingrese numeros", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            foreach (var item in txbCantidad.Text)
            {
                if (!(char.IsNumber(item)))
                {
                    MessageBox.Show("Error en la cantidad, solo ingrese numeros", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (accionArticulo == accion.Nuevo)
            {
                Articulo ar = new Articulo()
                {
                    NombreArticulo = txbNombreProducto.Text,
                    Descripcion = txbDescripcion.Text,
                    CompraArticulo = txbPrecioCompra.Text,
                    VentaArticulo = txbPrecioVenta.Text,
                    CantidadArticulo = txbCantidad.Text,
                    N_Proveedor = cmbProveedor.Text,
                    ImagenArticulo = ImageToByte(imgArticulo.Source)
                };
                if (ManejadorArticulo.Agregar(ar))
                {
                    MessageBox.Show("Producto agregado correctamente", "inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposArticulo();
                    ActualizarTablaArticulo();
                    BotonesArticuloEdicion(false);
                    ActualizarTablas();
                }
                else
                {
                    MessageBox.Show("El Producto no pudo ser agregado", "inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Articulo ar = dtgProductos.SelectedItem as Articulo;
                ar.NombreArticulo = txbNombreProducto.Text;
                ar.N_Proveedor = cmbProveedor.Text;
                ar.Descripcion = txbDescripcion.Text;
                ar.CompraArticulo = txbPrecioCompra.Text;
                ar.VentaArticulo = txbPrecioVenta.Text;
                ar.CantidadArticulo = txbCantidad.Text;
                ar.ImagenArticulo = ImageToByte(imgArticulo.Source);
                if (ManejadorArticulo.Modificar(ar))
                {
                    MessageBox.Show("Producto modificado correctamente", "inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimpiarCamposArticulo();
                    ActualizarTablaArticulo();
                    BotonesArticuloEdicion(false);
                    ActualizarTablas();
                }
                else
                {
                    MessageBox.Show("El Producto no pudo ser actualizado", "inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnCancelarProducto_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposArticulo();
            BotonesArticuloEdicion(false);
        }

        private void BtnEliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            Articulo ar = dtgProductos.SelectedItem as Articulo;
            if (ar != null)
            {
                if (MessageBox.Show("Realmente desea eliminar este Producto?", "Inventarios", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (ManejadorArticulo.Eliminar(ar.Id))
                    {
                        MessageBox.Show("Producto eliminado", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarTablaArticulo();
                        ActualizarTablas();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el Producto", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado nada", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BtnImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Title = "seleccione una imagen";
            dialogo.Filter = "Archivos de Imagen|*.jpg; *.jpeg; *.png";
            if (dialogo.ShowDialog().Value)
            {
                imgArticulo.Source = new BitmapImage(new Uri(dialogo.FileName));
            }
        }

        private void BtnAgregarVenta_Click(object sender, RoutedEventArgs e)
        {
            Articulo a = dtgVentaAlmacen.SelectedItem as Articulo;
            Venta v = new Venta();
            if (a != null)
            {
                if (string.IsNullOrEmpty(txbCantidadVP.Text))
                {
                    MessageBox.Show("No ha llenado el Campo de cantidad", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                foreach (var item in txbCantidadVP.Text)
                {
                    if (!(char.IsNumber(item)))
                    {
                        MessageBox.Show("Valor invalido, intente de nuevo", "Ventas", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                v.CantidadVenta = int.Parse(txbCantidadVP.Text);
                v.ProductoVenta = a.NombreArticulo;
                v.DescripcionVenta = a.Descripcion;
                
                v.PrecioVenta = float.Parse(a.VentaArticulo);
                v.TotalVenta = v.CantidadVenta * v.PrecioVenta;
                if (int.Parse(a.CantidadArticulo) < int.Parse(txbCantidadVP.Text))
                {
                    MessageBox.Show("No hay suficientes productos", "Ventas", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                venta.Add(v);
                dtgVenta.ItemsSource = null;
                dtgVenta.ItemsSource = venta;
                a.CantidadArticulo = (int.Parse(a.CantidadArticulo) - int.Parse(txbCantidadVP.Text)) + "";
                ManejadorArticulo.Modificar(a);
                ActualizarTablas();
                ActualizarTablaArticulo();
                txbCantidadVP.Clear();
            }
            else
            {
                MessageBox.Show("No selecciono nada en la tabla", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BtnEliminarVenta_Click(object sender, RoutedEventArgs e)
        {
            Venta v = dtgVenta.SelectedItem as Venta;
            if (v != null)
            {
                foreach (var item in venta)
                {
                    if (item.ProductoVenta == v.ProductoVenta && item.PrecioVenta == v.PrecioVenta)
                    {
                        Articulo articulo = ManejadorArticulo.Listar.Where(p => p.NombreArticulo == item.ProductoVenta).SingleOrDefault();
                        articulo.CantidadArticulo = (int.Parse(articulo.CantidadArticulo) + item.CantidadVenta).ToString();
                        ManejadorArticulo.Modificar(articulo);
                    }
                }
                if (v != null)
                {
                    venta.Remove(v);
                    ActualizarTablaArticulo();
                    ActualizarTablas();
                    dtgVenta.ItemsSource = null;
                    dtgVenta.ItemsSource = venta;
                };
            }
            else
            {
                MessageBox.Show("No selecciono nada en la tabla en Venta", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BtnCancelarVenta_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Realmente esta seguro de cancelar la venta", "Inventarios", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in venta)
                {
                    Articulo articulo = ManejadorArticulo.Listar.Where(p=>p.NombreArticulo==item.ProductoVenta).SingleOrDefault();
                    articulo.CantidadArticulo = (int.Parse(articulo.CantidadArticulo) + item.CantidadVenta).ToString();
                    ManejadorArticulo.Modificar(articulo);
                }
                ActualizarTablaArticulo();
                ActualizarTablas();
                BotonesVentaEdicion(false);
                dtgVenta.ItemsSource = null;
                venta = new List<Venta>();
                txbFolioVenta.Clear();
                txbFechaVenta.Clear();
            }
        }

        private void BtnRealizarVenta_Click(object sender, RoutedEventArgs e)
        {
            BotonesVentaEdicion(false);
            if (venta.Count <= 0)
            {
                MessageBox.Show("No a seleccionado un producto", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            float SubTotal = 0;
            foreach (Venta item in venta)
            {
                SubTotal += item.TotalVenta;
            }
            float IVa = 0.16f;
            float IvaIncluido = IVa * SubTotal;
            float TotalVenta = IvaIncluido + SubTotal;

            Ticket reporte = new Ticket(txbFolioVenta.Text + ".but");
            string datos = "", elementos = "", informacion = "";
            datos = string.Format("PESHOWARE\n \nFolio {0}\n \nFecha: {1}\n \nProducto\n   \nPrecio\n \nCantidad\n \nTotal\n-----------------\n", txbFolioVenta.Text, txbFechaVenta.Text);
            foreach (Venta item in venta)
            {
                elementos += string.Format("\n{0}      {1}     {2}     {3}\n", item.ProductoVenta, item.PrecioVenta, item.CantidadVenta, item.TotalVenta);
            }
            informacion = string.Format("\nSubtotal: ${0}\nIva: ${1}\n Total: ${2}\n\n   ¡¡¡Vuelva pronto!!!", SubTotal.ToString(), IvaIncluido.ToString(), TotalVenta.ToString());
            reporte.Guardar(datos + elementos + informacion);
            MessageBox.Show("Subtotal: " + SubTotal.ToString() + " \nIva " + (IvaIncluido).ToString() + " \nTotal " + TotalVenta.ToString() + "\nReporte Guardado con Exito: " + txbFolioVenta.Text + ".txt", "Total de la Venta", MessageBoxButton.OK, MessageBoxImage.Information);
            try
            {
                InventarioVenta Ventas = new InventarioVenta()
                {
                    Folio = txbFolioVenta.Text,
                    Fecha = txbFechaVenta.Text,
                    Iva_Pago = float.Parse(IvaIncluido.ToString()),
                    Subtotal_Pago = float.Parse(SubTotal.ToString()),
                    Total_Pago = TotalVenta,
                    Producto_Venta = venta,

                };
                ManejadorTicket.Agregar(Ventas);
                TablaReportes();
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo generar la lista de Inventario de Ventas", "Ventas", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            BotonesVentaEdicion(false);
            LimpiarCamposVenta();
        }

        private void BtnNuevaVenta_Click(object sender, RoutedEventArgs e)
        {
            BotonesVentaEdicion(true);
            LimpiarCamposVenta();
            Random a = new Random();
            int folio = a.Next(200000, 99999999);
            txbFolioVenta.Text = folio.ToString();
        }

        private void DtgAlmacenReportes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InventarioVenta a = dtgAlmacenReportes.SelectedItem as InventarioVenta;
            if (a != null)
            {
                BotonesReportesEdicion(true);
                txbFechaAlmacen.Text = a.Fecha;
                dtgTablaCompra.ItemsSource = null;
                dtgTablaCompra.ItemsSource = a.Producto_Venta;
                txbTotalAlmacen.Text = a.Total_Pago.ToString();
                txbFolioAlmacen.Text = a.Folio;
            }
            else
            {
                MessageBox.Show("No se pudo seleccionar la opcion, intente de nuevo", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BtnLimpiarReportes_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposReportes();
        }

        private void BtnEliminarReporte_Click(object sender, RoutedEventArgs e)
        {
            InventarioVenta a = dtgAlmacenReportes.SelectedItem as InventarioVenta;
            if (a != null)
            {
                if (MessageBox.Show("Realmente desea eliminar el campo", "Inventarios", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ManejadorTicket.Eliminar(a.Id);
                    TablaReportes();
                    LimpiarCamposReportes();
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un venta de la tabla , intente de nuevo", "Inventarios", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BtnProductosRegresar_Click(object sender, RoutedEventArgs e)
        {
            Menu a = new Menu();
            a.Show();
            this.Close();
        }

        private void BtnVentasRegresar_Click(object sender, RoutedEventArgs e)
        {
            Menu a = new Menu();
            a.Show();
            this.Close();
        }

        private void BtnProveedoresRegresar_Click(object sender, RoutedEventArgs e)
        {
            Menu a = new Menu();
            a.Show();
            this.Close();
        }

		private void txbBuscar_TextChanged(object sender, TextChangedEventArgs e)
		{
			dtgVentaAlmacen.ItemsSource = null;
			if (string.IsNullOrEmpty(txbBuscar.Text))
				dtgVentaAlmacen.ItemsSource = ManejadorArticulo.Listar;
			else
				dtgVentaAlmacen.ItemsSource = ManejadorArticulo.Listar.Where(p => p.NombreArticulo == txbBuscar .Text);
		}
	}
}
