using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeshoWare.COMMON.Entidades
{
    public class Cliente : Base
    {
        public string NombreCliente { get; set; }
        public string ApellidosCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public string CorreoCliente { get; set; }
        public override string ToString()
        {
            return string.Format("{0} {1}", NombreCliente, ApellidosCliente);
        }
    }
}
