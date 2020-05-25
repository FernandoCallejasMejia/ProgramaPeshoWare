using PeshoWare.COMMON.Entidades;
using PeshoWare.COMMON.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeshoWare.BIZ.Manejadores
{
    public class ManejadorPrestamo : IManejadorPrestamo
    {
        IRepositorio<Prestamo> repositorio;
        public ManejadorPrestamo(IRepositorio<Prestamo> repo)
        {
            repositorio = repo;
        }
        public List<Prestamo> Listar => repositorio.Leer;

        public bool Agregar(Prestamo entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Prestamo BuscarId(string id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(string id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Prestamo entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
