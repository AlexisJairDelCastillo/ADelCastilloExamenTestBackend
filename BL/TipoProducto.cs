using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TipoProducto
    {
        public static ML.Result GetByIdSubcategoria(int idSubcategoria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenTestBackendContext context = new DL.AdelCastilloExamenTestBackendContext())
                {
                    var query = context.TipoProductos.FromSqlRaw($"TipoProductoGetByIdSubcategoria {idSubcategoria}").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DL.TipoProducto resultTipoProducto in query)
                        {
                            ML.TipoProducto tipoProducto = new ML.TipoProducto();
                            tipoProducto.IdTipoProducto = resultTipoProducto.IdTipoProducto;
                            tipoProducto.Tipo = resultTipoProducto.Tipo;
                            tipoProducto.SubCategoria = new ML.SubCategoria();
                            tipoProducto.SubCategoria.IdSubcategoria = resultTipoProducto.IdSubcategoria.Value;

                            result.Objects.Add(tipoProducto);
                        }
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar los registros." + ex.Message;
            }

            return result;
        }
    }
}
