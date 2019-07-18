using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConstructoraExample.Areas.Empleado.Models
{
    public class EmpleadoViewModel
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string sexo { get; set; }
        public double salario { get; set; }

    }
}