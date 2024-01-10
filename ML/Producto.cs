using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Producto
    {
        public int IdProducto { get; set; }

        public string? NombreProducto { get; set; }

        public string? NumMaterial { get; set; }

        public int? Inventario { get; set; }

        public ML.TipoProducto? TipoProducto { get; set; }

        public List<object>? Productos { get; set; }
    }
}
