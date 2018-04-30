using System;
using System.Collections.Generic;
using System.Text;

namespace Deporte.COMMON.Entidades
{
    public class Deportess:Base
    {
        public string NombreDeporte { get; set; }
        public override string ToString()
        {
            return string.Format("{0}", NombreDeporte);
        }
    }
}
