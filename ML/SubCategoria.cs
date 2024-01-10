using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class SubCategoria
    {
        public int IdSubcategoria { get; set; }

        public string? NombreSubcategoria { set; get; }

        public ML.Categoria? Categoria { set; get; }

        public List<object>? Subcategorias { set; get; }
    }
}
