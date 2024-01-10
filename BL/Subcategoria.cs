using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Subcategoria
    {
        public static ML.Result GetByIdCategoria(int idCategoria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.AdelCastilloExamenTestBackendContext context = new DL.AdelCastilloExamenTestBackendContext())
                {
                    var query = context.SubCategoria.FromSqlRaw($"SubcategoriaGetByIdCategoria {idCategoria}").ToList();

                    if(query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach(DL.SubCategorium resultSubcategoria in query)
                        {
                            ML.SubCategoria subcategoria = new ML.SubCategoria();
                            subcategoria.IdSubcategoria = resultSubcategoria.IdSubcategoria;
                            subcategoria.NombreSubcategoria = resultSubcategoria.NombreSubcategoria;
                            subcategoria.Categoria = new ML.Categoria();
                            subcategoria.Categoria.IdCategoria = resultSubcategoria.IdCategoria.Value;

                            result.Objects.Add(subcategoria);
                        }
                    }

                    result.Correct = true;
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar los registros." + ex.Message;
            }

            return result;
        }
    }
}
