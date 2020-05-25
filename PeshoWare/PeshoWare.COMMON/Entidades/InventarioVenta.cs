using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeshoWare.COMMON.Entidades
{
    public class InventarioVenta : Base
    {
        public string Fecha { get; set; }
        public string Folio { get; set; }
        public List<Venta> Producto_Venta { get; set; }
        public float Subtotal_Pago { get; set; }
        public float Iva_Pago { get; set; }
        public float Total_Pago { get; set; }
    }
}
