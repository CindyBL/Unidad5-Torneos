using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using Deporte.COMMON.Interfaces;
using Deporte.COMMON.Entidades;
using System.Linq;

namespace Deportes.DAL
{
    public class RepositorioDeportes : IRepositorio<Deportess>
    {
        private string DBName = "Deportes.db";
        private string TableName = "Deportess";
        public List<Deportess> Read
        {
            get
            {
                List<Deportess> datos = new List<Deportess>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Deportess>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Create(Deportess entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Deportess>(TableName);
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
                    var coleccion = db.GetCollection<Deportess>(TableName);
                    r = coleccion.Delete(e => e.Id == id);
                }
                return r > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Deportess entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Deportess>(TableName);
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
