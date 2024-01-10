using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Producto.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("getbyid/{idProducto}")]
        public IActionResult GetById(int idProducto)
        {
            ML.Result result = BL.Producto.GetById(idProducto);

            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] ML.Producto producto)
        {
            ML.Result result = BL.Producto.Add(producto);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("update/{idProducto}")]
        public IActionResult Update(int idProducto, [FromBody] ML.Producto producto)
        {
            ML.Result result = BL.Producto.Update(producto);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("delete/{idProducto}")]
        public IActionResult Delete(int idProducto)
        {
            ML.Producto producto = new ML.Producto();
            producto.IdProducto = Convert.ToInt32(idProducto);
            ML.Result result = BL.Producto.Delete(producto);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
