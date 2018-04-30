using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using Deporte.COMMON.Interfaces;
using Deporte.COMMON.Entidades;
using System.Linq;

namespace Deportes.DAL
{
    public class RepositorioTorneo : IRepositorio<Torneo>
    {
        private string DBName = "Torneo.db";
        private string TableName = "Torneo";
        public List<Torneo> Read
        {
            get
            {
                List<Torneo> datos = new List<Torneo>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Torneo>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Create(Torneo entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Torneo>(TableName);
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
                    var coleccion = db.GetCollection<Torneo>(TableName);
                    r = coleccion.Delete(e => e.Id == id);
                }
                return r > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Torneo entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Torneo>(TableName);
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
