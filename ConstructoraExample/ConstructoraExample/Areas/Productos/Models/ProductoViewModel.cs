using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConstructoraExample.Areas.Productos.Models
{
    public class ProductoViewModel
    {
        public int numero { get; set; }
        public string nombre { get; set; }
        public float precio { get; set; }
        public string comentario { get; set; }
    }
}