using System;
using System.Collections.Generic;
using System.Text;

namespace Deporte.COMMON.Entidades
{
    public class DeporteTemporal
    {
        public string Id { get; set; }
        public string FechaProgramada { get; set; }
        public string TipoDeporte { get; set; }
        public string Equipo1 { get; set; }
        public int Marcador1 { get; set; }
        public string Equipo2 { get; set; }
        public int Marcador2 { get; set; }
    }
}
