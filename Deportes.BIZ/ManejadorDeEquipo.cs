using System;
using System.Collections.Generic;
using System.Text;
using Deporte.COMMON.Entidades;
using Deporte.COMMON.Interfaces;
using System.Linq;
using MongoDB.Bson;

namespace Deportes.BIZ
{
    public class ManejadorDeEquipo: IManejadorEquipo
    {
        IRepositorio<Equipo> repositorio;

        public ManejadorDeEquipo(IRepositorio<Equipo> repo)
        {
            repositorio = repo;
        }

        public List<Equipo> Listar => repositorio.Read;

        public bool Agregar(Equipo entidad)
        {
            return repositorio.Create(entidad);
        }

        public Equipo BuscarPorId(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Delete(id);
        }

        public bool Modificar(Equipo entidad)
        {
            return repositorio.Update(entidad);
        }
    }
}
