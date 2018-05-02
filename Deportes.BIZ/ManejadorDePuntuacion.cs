using System;
using System.Collections.Generic;
using System.Text;
using Deporte.COMMON.Entidades;
using Deporte.COMMON.Interfaces;
using System.Linq;
using MongoDB.Bson;

namespace Deportes.BIZ
{
    public class ManejadorDePuntuacion:IManejadorPuntuacion
    {
        IRepositorio<Puntuacion> repositorio;

        public ManejadorDePuntuacion(IRepositorio<Puntuacion> repo)
        {
            repositorio = repo;
        }

        public List<Puntuacion> Listar => repositorio.Read;

        public bool Agregar(Puntuacion entidad)
        {
            return repositorio.Create(entidad);
        }

        public Puntuacion BuscarPorId(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Delete(id);
        }

        public bool Modificar(Puntuacion entidad)
        {
            return repositorio.Update(entidad);
        }
    }
}
