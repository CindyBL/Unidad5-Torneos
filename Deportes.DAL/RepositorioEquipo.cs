using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using Deporte.COMMON.Interfaces;
using Deporte.COMMON.Entidades;
using System.Linq;

namespace Deportes.DAL
{
    public class RepositorioEquipo: IRepositorio<Equipo>
    {
        private string DBName = "Equipo.db";
    private string TableName = "Equipo";
    public List<Equipo> Read
    {
        get
        {
            List<Equipo> datos = new List<Equipo>();
            using (var db = new LiteDatabase(DBName))
            {
                datos = db.GetCollection<Equipo>(TableName).FindAll().ToList();
            }
            return datos;
        }
    }

    public bool Create(Equipo entidad)
    {
        entidad.Id = Guid.NewGuid().ToString();
        try
        {
            using (var db = new LiteDatabase(DBName))
            {
                var coleccion = db.GetCollection<Equipo>(TableName);
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
                var coleccion = db.GetCollection<Equipo>(TableName);
                r = coleccion.Delete(e => e.Id == id);
            }
            return r > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Update(Equipo entidadModificada)
    {
        try
        {
            using (var db = new LiteDatabase(DBName))
            {
                var coleccion = db.GetCollection<Equipo>(TableName);
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
