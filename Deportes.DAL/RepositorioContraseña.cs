using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using Deporte.COMMON.Interfaces;
using Deporte.COMMON.Entidades;
using System.Linq;

namespace Deportes.DAL
{
    public class RepositorioContraseña : IRepositorio<Contraseña>
    {
        private string DBName = "Contraseña.db";
        private string TableName = "Contraseña";
        public List<Contraseña> Read
        {
            get
            {
                List<Contraseña> datos = new List<Contraseña>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Contraseña>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Create(Contraseña entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Contraseña>(TableName);
                    coleccion.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                int r;
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Contraseña>(TableName);
                    r = coleccion.Delete(e => e.Id == id);
                }
                return r > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Contraseña entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Contraseña>(TableName);
                    coleccion.Update(entidadModificada);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
