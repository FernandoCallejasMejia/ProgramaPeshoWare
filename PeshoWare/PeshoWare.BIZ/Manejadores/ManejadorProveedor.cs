using PeshoWare.COMMON.Entidades;
using PeshoWare.COMMON.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeshoWare.BIZ.Manejadores
{
    public class ManejadorProveedor : IManejadorProveedor
    {
        IRepositorio<Proveedor> repositorio;
        public ManejadorProveedor(IRepositorio<Proveedor> repo)
        {
            repositorio = repo;
        }
        public List<Proveedor> Listar => repositorio.Leer;

        public bool Agregar(Proveedor entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Proveedor BuscarId(string id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(string id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Proveedor entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
