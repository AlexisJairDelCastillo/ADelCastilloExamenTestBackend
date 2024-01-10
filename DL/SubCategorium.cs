using System;
using System.Collections.Generic;

namespace DL;

public partial class SubCategorium
{
    public int IdSubcategoria { get; set; }

    public string? NombreSubcategoria { get; set; }

    public int? IdCategoria { get; set; }

    public virtual Categorium? IdCategoriaNavigation { get; set; }

    public virtual ICollection<TipoProducto> TipoProductos { get; set; } = new List<TipoProducto>();
}
