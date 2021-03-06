﻿using System;
using System.Collections.Generic;
using System.Text;
using Deporte.COMMON.Entidades;
using Deporte.COMMON.Interfaces;
using System.Linq;
using MongoDB.Bson;

namespace Deportes.BIZ
{
    public class ManejadorDeDeportes:IManejadorDeporte
    {
        IRepositorio<Deportess> repositorio;

        public ManejadorDeDeportes(IRepositorio<Deportess> repo)
        {
            repositorio = repo;
        }

        public List<Deportess> Listar => repositorio.Read;

        public bool Agregar(Deportess entidad)
        {
            return repositorio.Create(entidad);
        }

        public Deportess BuscarPorId(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Delete(id);
        }

        public bool Modificar(Deportess entidad)
        {
            return repositorio.Update(entidad);
        }
    }
}
