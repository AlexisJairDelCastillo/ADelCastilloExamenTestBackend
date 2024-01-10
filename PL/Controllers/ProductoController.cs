using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        private IHostEnvironment environment;
        private IConfiguration configuration;

        public ProductoController(IHostEnvironment _environment, IConfiguration _configuration)
        {
            environment = _environment;
            configuration = _configuration;
        }

        public ActionResult Getall()
        {
            ML.Producto producto = new ML.Producto();

            ML.Result resultProducto = new ML.Result();
            resultProducto.Objects = new List<object>();

            using(HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.GetAsync("producto/getall");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach(var item in readTask.Result.Objects)
                    {
                        ML.Producto itemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(item.ToString());
                        resultProducto.Objects.Add(itemList);
                    }
                }

                producto.Productos = resultProducto.Objects;
            }

            return View(producto);
        }

        [HttpGet]
        public ActionResult Form(int? idProducto)
        {
            ML.Result resultCategoria = BL.Categoria.GetAll();

            ML.Producto producto = new ML.Producto();
            producto.TipoProducto = new ML.TipoProducto();
            producto.TipoProducto.SubCategoria = new ML.SubCategoria();
            producto.TipoProducto.SubCategoria.Categoria = new ML.Categoria();

            producto.TipoProducto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;

            if(idProducto == null)
            {
                ViewBag.Titulo = "Agregar nuevo registro.";
                ViewBag.Action = "Agregar";

                return View(producto);
            }
            else
            {
                ViewBag.Titulo = "Actualizar información.";
                ViewBag.Action = "Actualizar";

                ML.Result result = BL.Producto.GetById(idProducto.Value);

                if(result.Correct)
                {
                    producto = (ML.Producto)result.Object;

                    producto.TipoProducto = new ML.TipoProducto();
                    producto.TipoProducto.SubCategoria = new ML.SubCategoria();
                    producto.TipoProducto.SubCategoria.Categoria = new ML.Categoria();

                    producto.TipoProducto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;

                    ML.Result resultSubcategoria = BL.Subcategoria.GetByIdCategoria(producto.TipoProducto.SubCategoria.Categoria.IdCategoria);
                    ML.Result resultTipoProducto = BL.TipoProducto.GetByIdSubcategoria(producto.TipoProducto.SubCategoria.IdSubcategoria);

                    return View(producto);
                }
                else
                {
                    ViewBag.Titulo = "Error";
                    ViewBag.Message = result.Message;

                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            if(producto.IdProducto == 0)
            {
                ML.Result result = new ML.Result();

                using(HttpClient client = new HttpClient())
                {
                    string webApi = configuration["WebApi"];
                    client.BaseAddress = new Uri(webApi);

                    var postTask = client.PostAsJsonAsync<ML.Producto>("producto/add", producto);
                    postTask.Wait();

                    HttpResponseMessage resultTask = postTask.Result;

                    if (resultTask.IsSuccessStatusCode)
                    {
                        result.Correct = true;

                        ViewBag.Titulo = "El registro se inserto correctamente.";
                        ViewBag.Message = result.Message;

                        return View("Modal");
                    }
                    else
                    {
                        ViewBag.Titulo = "Ocurrio un error al insertar el registro.";
                        ViewBag.Message = result.Message;

                        return View("Modal");
                    }
                }
            }
            else
            {
                ML.Result result = new ML.Result();

                using(HttpClient client = new HttpClient())
                {
                    string webApi = configuration["WebApi"];
                    client.BaseAddress = new Uri(webApi);

                    Task<HttpResponseMessage> postTask = client.PutAsJsonAsync<ML.Producto>("producto/update/" + producto.IdProducto, producto);
                    postTask.Wait();

                    HttpResponseMessage resultTask = postTask.Result;

                    if(resultTask.IsSuccessStatusCode)
                    {
                        result.Correct = true;

                        ViewBag.Titulo = "El registro se actualizo correctamente.";
                        ViewBag.Message = result.Message;

                        return View("Modal");
                    }
                    else
                    {
                        ViewBag.Titulo = "Ocurrio un error al actualizar el registro.";
                        ViewBag.Message = result.Message;

                        return View("Modal");
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult Delete(ML.Producto producto)
        {
            ML.Result resultProducto = new ML.Result();
            int idProducto = producto.IdProducto;

            using(HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.DeleteAsync("producto/delete/" + idProducto);
                responseTask.Wait();

                var result = responseTask.Result;

                if(result.IsSuccessStatusCode ) {
                    ViewBag.Titulo = "El registro se elimino correctamente.";

                    return View("Modal");
                }
                else
                {
                    ViewBag.Titulo = "Ocurrio un error al eliminar el registro.";

                    return View("Modal");
                }
            }
        }

        [HttpGet]
        public JsonResult GetSubcategorias(int idCategoria)
        {
            ML.Result result = BL.Subcategoria.GetByIdCategoria(idCategoria);

            return Json(result);
        }

        [HttpGet]
        public JsonResult GetTipoProducto(int idSubcategoria)
        {
            ML.Result result = BL.TipoProducto.GetByIdSubcategoria(idSubcategoria);

            return Json(result);
        }
    }
}
