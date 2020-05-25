using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeshoWare.COMMON.Entidades
{
    public class Prestamo : Base
    {
        public string N_ClientePrestamo { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaPago { get; set; }
        public float Monto { get; set; }
        public float Abono { get; set; }
    }
}
