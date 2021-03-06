﻿using System;
using System.Collections.Generic;
using System.Text;
using Deporte.COMMON.Entidades;
using Deporte.COMMON.Interfaces;
using System.Linq;
using MongoDB.Bson;

namespace Deportes.BIZ
{
    public class ManejadorDeContraseña:IManejadorContraseña
    {
        IRepositorio<Contraseña> repositorio;

        public ManejadorDeContraseña(IRepositorio<Contraseña> repo)
        {
            repositorio = repo;
        }

        public List<Contraseña> Listar => repositorio.Read;

        public bool Agregar(Contraseña entidad)
        {
            return repositorio.Create(entidad);
        }

        public Contraseña BuscarPorId(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Delete(id);
        }

        public bool Modificar(Contraseña entidad)
        {
            return repositorio.Update(entidad);
        }
    }
}
