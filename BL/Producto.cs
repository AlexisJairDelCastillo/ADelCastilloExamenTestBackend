using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenTestBackendContext context = new DL.AdelCastilloExamenTestBackendContext())
                {
                    var query = context.Productos.FromSqlRaw("ProductoGetAll").ToList();

                    if(query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach(DL.Producto resultProducto in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = resultProducto.IdProducto;
                            producto.NombreProducto = resultProducto.NombreProducto;
                            producto.NumMaterial = resultProducto.NumMaterial;
                            producto.Inventario = resultProducto.Inventario;
                            producto.TipoProducto = new ML.TipoProducto();
                            producto.TipoProducto.IdTipoProducto = resultProducto.IdTipoProducto.Value;
                            producto.TipoProducto.Tipo = resultProducto.Tipo;

                            result.Objects.Add(producto);
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

        public static ML.Result GetById(int idProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenTestBackendContext context = new DL.AdelCastilloExamenTestBackendContext())
                {
                    var query = context.Productos.FromSqlRaw($"ProductoGetById {idProducto}").AsEnumerable().FirstOrDefault();

                    if(query != null)
                    {
                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = query.IdProducto;
                        producto.NombreProducto = query.NombreProducto;
                        producto.NumMaterial = query.NumMaterial;
                        producto.Inventario = query.Inventario;
                        producto.TipoProducto = new ML.TipoProducto();
                        producto.TipoProducto.IdTipoProducto = query.IdTipoProducto.Value;
                        producto.TipoProducto.Tipo = query.Tipo;

                        result.Object = producto;
                    }

                    result.Correct = true;
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar el registro." + ex.Message;
            }

            return result;
        }

        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenTestBackendContext context = new DL.AdelCastilloExamenTestBackendContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"ProductoAdd '{producto.NombreProducto}', '{producto.NumMaterial}', {producto.Inventario}, {producto.TipoProducto.IdTipoProducto}");

                    if(query > 0)
                    {
                        result.Correct = true;
                        result.Message = "El registro se inserto correctamente.";
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al insertar los registros." + ex.Message;
            }

            return result;
        }

        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenTestBackendContext context = new DL.AdelCastilloExamenTestBackendContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"ProductoUpdate {producto.IdProducto}, '{producto.NombreProducto}', '{producto.NumMaterial}', {producto.Inventario}, {producto.TipoProducto.IdTipoProducto}");

                    if(query > 0 )
                    {
                        result.Correct = true;
                        result.Message = "El registro se actualizo correctamente.";
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al actualizar el registro." + ex.Message;
            }

            return result;
        }

        public static ML.Result Delete(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenTestBackendContext context = new DL.AdelCastilloExamenTestBackendContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"ProductoDelete {producto.IdProducto}");

                    if(query > 0 )
                    {
                        result.Correct = true;
                        result.Message = "El registro se elimino correctamente.";
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al eliminar el registro." + ex.Message;
            }

            return result;
        }
    }
}
