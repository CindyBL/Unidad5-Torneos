using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace Deporte.COMMON.Entidades
{
    public class Torneo:Base
    {
        public DateTime FechaTorneo { get; set; }
        public string Tipo_Deporte { get; set; }
        public string Equipo1 { get; set; }
        public int Marcador_1 { get; set; }
        public string Equipo2 { get; set; }
        public int Marcador_2 { get; set; }
    }
}
