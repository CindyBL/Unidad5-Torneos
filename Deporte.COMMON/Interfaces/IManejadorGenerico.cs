using System;
using System.Collections.Generic;
using System.Text;
using Deporte.COMMON.Entidades;
using MongoDB.Bson;

namespace Deporte.COMMON.Interfaces
{
    public interface IManejadorGenerico<T> where T : Base
    {
        bool Agregar(T entidad);
        List<T> Listar { get; }
        bool Eliminar(ObjectId id);
        bool Modificar(T entidad);
        T BuscarPorId(ObjectId id);
    }
}
