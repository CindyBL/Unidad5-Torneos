using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using Deporte.COMMON.Interfaces;
using Deporte.COMMON.Entidades;
using System.Linq;

namespace Deportes.DAL
{
    public class RepositorioPuntuacion : IRepositorio<Puntuacion>
    {
        private string DBName = "Puntuacio.db";
        private string TableName = "Puntuacion";
        public List<Puntuacion> Read
        {
            get
            {
                List<Puntuacion> datos = new List<Puntuacion>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Puntuacion>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Create(Puntuacion entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Puntuacion>(TableName);
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
                    var coleccion = db.GetCollection<Puntuacion>(TableName);
                    r = coleccion.Delete(e => e.Id == id);
                }
                return r > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Puntuacion entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Puntuacion>(TableName);
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
