using System;
using System.Collections.Generic;
using System.Text;

namespace Deporte.COMMON.Entidades
{
    public class Contraseña:Base
    {
        public string Usuario { get; set; }
        public string NuevaContraseña { get; set; }
        public override string ToString()
        {
            return Usuario;
        }
    }
}
