using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeshoWare.COMMON.Entidades
{
    public class Articulo : Base
    {
        public string NombreArticulo { get; set; }
        public string Descripcion { get; set; }
        public string CompraArticulo { get; set; }
        public string VentaArticulo { get; set; }
        public string CantidadArticulo { get; set; }
        public string N_Proveedor { get; set; }
        public byte[] ImagenArticulo { get; set; }
        public override string ToString()
        {
            return NombreArticulo;
        }
    }
}
