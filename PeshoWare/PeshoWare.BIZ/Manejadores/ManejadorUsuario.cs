using PeshoWare.COMMON.Entidades;
using PeshoWare.COMMON.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeshoWare.BIZ.Manejadores
{
    public class ManejadorUsuario : IManejadorUsuario
    {
        IRepositorio<Usuario> repositorio;
        public ManejadorUsuario(IRepositorio<Usuario> repo)
        {
            repositorio = repo;
        }

        public List<Usuario> Listar => repositorio.Leer;

        public bool Agregar(Usuario entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Usuario BuscarId(string id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(string id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Usuario entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
