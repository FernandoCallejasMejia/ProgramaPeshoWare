﻿using LiteDB;
using PeshoWare.COMMON.Entidades;
using PeshoWare.COMMON.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeshoWare.DAL.Repositorios
{
    public class RepositorioTicket : IRepositorio<InventarioVenta>
    {
        private string DBName = "Inventario.db";
        private string TableName = "Tickets";
        public List<InventarioVenta> Leer
        {
            get
            {
                List<InventarioVenta> datos = new List<InventarioVenta>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<InventarioVenta>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Crear(InventarioVenta entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<InventarioVenta>(TableName);
                    coleccion.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Editar(InventarioVenta entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<InventarioVenta>(TableName);
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
                    var coleccion = db.GetCollection<InventarioVenta>(TableName);
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
