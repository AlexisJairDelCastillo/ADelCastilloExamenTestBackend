using System;
using System.Collections.Generic;

namespace DL;

public partial class TipoProducto
{
    public int IdTipoProducto { get; set; }

    public string? Tipo { get; set; }

    public int? IdSubcategoria { get; set; }

    public virtual SubCategorium? IdSubcategoriaNavigation { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
