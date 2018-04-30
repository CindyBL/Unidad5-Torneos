using System;
using System.Collections.Generic;
using System.Text;
using Deporte.COMMON.Entidades;

namespace Deporte.COMMON.Interfaces
{
    public interface IRepositorio<T> where T : Base
    {
        bool Create(T entidad);
        List<T> Read { get; }
        bool Update(T entidadModificada);
        bool Delete(string id);
    }
}
