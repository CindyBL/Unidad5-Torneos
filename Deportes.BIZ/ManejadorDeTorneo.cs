using System;
using System.Collections.Generic;
using System.Text;
using Deporte.COMMON.Entidades;
using Deporte.COMMON.Interfaces;
using System.Linq;
using MongoDB.Bson;

namespace Deportes.BIZ
{
    public class ManejadorDeTorneo : IManejadorTorneo
    {
        IRepositorio<Torneo> repositorio;

        public ManejadorDeTorneo(IRepositorio<Torneo> repo)
        {
            repositorio = repo;
        }

        public List<Torneo> Listar => repositorio.Read;

        public bool Agregar(Torneo entidad)
        {
            return repositorio.Create(entidad);
        }

        public Torneo BuscarPorId(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Delete(id);
        }

        public bool Modificar(Torneo entidad)
        {
            return repositorio.Update(entidad);
        }
    }
}
