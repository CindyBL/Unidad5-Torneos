using System;
using System.Collections.Generic;
using System.Text;
using Deporte.COMMON.Entidades;
using Deporte.COMMON.Interfaces;
using MongoDB.Bson;
using Deportes.BIZ;
using System.Linq;
using System.Collections;

namespace Deportes.BIZ
{
    class ManejadorTorneoEquipos : IManejadorEquipo
    {
        IRepositorio<Equipo> equi;
        public ManejadorTorneoEquipos(IRepositorio<Equipo> usuario)
        {
            this.equi = usuario;
        }

        public List<Equipo> Listar => equi.Read;

        public bool Agregar(Equipo entidad)
        {
            return equi.Create(entidad);
        }



        public int Aleatorios(string palabra)
        {
            int valor = ContadorDeBuscarEquipo(palabra);
            Random a = new Random();
            int v = 0;
            return v = a.Next(1, valor);
        }

        public Equipo Buscador(string Id)
        {
            return Listar.Where(e => e.Deporte == Id).SingleOrDefault();
        }

        public IEnumerable BuscarEquipos(string palabra)
        {
            return Listar.Where(e => e.Deporte == palabra).ToList();
        }

        public Equipo BuscarPorId(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public int ContadorDeBuscarEquipo(string palabra)
        {
            return Listar.Where(e => e.Deporte == palabra).ToList().Count();
        }



        public bool Eliminar(ObjectId id)
        {
            return equi.Delete(id);
        }

        public bool Modificar(Equipo entidad)
        {
            return equi.Update(entidad);
        }

        public bool VerificarSiEsNumero(string telefono)
        {
            foreach (var item in telefono)
            {
                if (!(char.IsNumber(item)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
