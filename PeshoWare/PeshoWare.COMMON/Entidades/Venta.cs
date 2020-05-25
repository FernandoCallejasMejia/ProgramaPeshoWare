using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeshoWare.COMMON.Entidades
{
    public class Venta
    {
        public string ProductoVenta { get; set; }
        public string DescripcionVenta { get; set; }
        public float PrecioVenta { get; set; }
        //public string Stock { get; set; }
        public int CantidadVenta { get; set; }
        public float TotalVenta { get; set; }
    }
}
