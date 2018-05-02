using System;
using System.Collections.Generic;
using System.Text;
using Deporte.COMMON.Entidades;
using MongoDB.Bson;

namespace Deporte.COMMON.Interfaces
{
    public interface IRepositorio<T> where T : Base
    {
        bool Create(T entidad);
        List<T> Read { get; }
        bool Update(T entidadModificada);
        bool Delete(ObjectId id);
    }
}
