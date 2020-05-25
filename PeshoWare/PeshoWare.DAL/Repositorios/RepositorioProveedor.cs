using LiteDB;
using PeshoWare.COMMON.Entidades;
using PeshoWare.COMMON.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeshoWare.DAL.Repositorios
{
    public class RepositorioProveedor : IRepositorio<Proveedor>
    {
        private string DBName = "Inventario.db";
        private string TableName = "Proveedores";
        public List<Proveedor> Leer
        {
            get
            {
                List<Proveedor> datos = new List<Proveedor>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Proveedor>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Crear(Proveedor entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Proveedor>(TableName);
                    coleccion.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Editar(Proveedor entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Proveedor>(TableName);
                    coleccion.Update(entidadModificada);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Eliminar(string id)
        {
            try
            {
                int r;
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Proveedor>(TableName);
                    r = coleccion.Delete(e => e.Id == id);
                }
                return r > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
