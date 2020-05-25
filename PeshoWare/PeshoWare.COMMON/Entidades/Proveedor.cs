using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeshoWare.COMMON.Entidades
{
    public class Proveedor : Base
    {
        public string NombreProveedor { get; set; }
        public string TelefonoProveedor { get; set; }
        public string CorreoProveedor { get; set; }
        public string DireccionProveedor { get; set; }
        public override string ToString()
        {
            return NombreProveedor;
        }
    }
}
