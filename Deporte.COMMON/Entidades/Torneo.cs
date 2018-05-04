using System;
using System.Collections.Generic;
using System.Text;

namespace Deporte.COMMON.Entidades
{
    public class Torneo:Base
    {
        public string Equipo1 { get; set; }
        public string Equipo2 { get; set; }
        public DateTime FechaTorneo { get; set; }
    }
}
