using System;
using System.Collections.Generic;
using System.Text;

namespace Deporte.COMMON.Entidades
{
    public class Equipo:Base
    {
        public string Deporte { get; set; }
        public string NombreEquipo { get; set; }
        public override string ToString()
        {
            return string.Format("{0} ({1})", NombreEquipo,Deporte);
        }
    }
}
