using System;
using System.Collections.Generic;

namespace DL;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? NombreProducto { get; set; }

    public string? NumMaterial { get; set; }

    public int? Inventario { get; set; }

    public int? IdTipoProducto { get; set; }

    public string? Tipo { get; set; }

    public virtual TipoProducto? IdTipoProductoNavigation { get; set; }
}
