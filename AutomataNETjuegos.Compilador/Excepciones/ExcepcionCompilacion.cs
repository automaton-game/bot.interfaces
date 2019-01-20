using System;
using System.Collections.Generic;

namespace AutomataNETjuegos.Compilador.Excepciones
{
    public class ExcepcionCompilacion : Exception
    {
        public IList<ErrorCompilacion> ErroresCompilacion { get; set; }
    }
}
