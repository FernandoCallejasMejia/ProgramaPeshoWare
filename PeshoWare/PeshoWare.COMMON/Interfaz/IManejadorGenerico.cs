﻿using PeshoWare.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeshoWare.COMMON.Interfaz
{
    public interface IManejadorGenerico<T> where T : Base
    {
        bool Agregar(T entidad);
        List<T> Listar { get; }
        bool Eliminar(string id);
        bool Modificar(T entidad);
        T BuscarId(string id);
    }
}
