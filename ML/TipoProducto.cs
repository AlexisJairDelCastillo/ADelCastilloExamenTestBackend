using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class TipoProducto
    {
        public int IdTipoProducto { get; set; }

        public string? Tipo { get; set; }

        public ML.SubCategoria? SubCategoria { get; set; }

        public List<object>? TiposProductos { get; set; }
    }
}
