using COMMON.Entidades;
using COMMON.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.api
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class GenericController<T> : ControllerBase where T : Base
    {

        protected IGenericRepositorio<T> repositorio;

        public GenericController(IGenericRepositorio<T> repo)
        {
            repositorio = repo;
        }

        // GET: api/<GenericController>
        [HttpGet]
        public ActionResult<IEnumerable<T>> Get()
        {
            var datos = repositorio.Get;
            if (datos != null)
            {
                return Ok(datos);
            }
            else
            {
                return BadRequest(repositorio.Error);
            }

        }

        // GET api/<GenericController>/5
        [HttpGet("{id}")]
        public ActionResult<T> Get(string id)
        {
            var dato=repositorio.GetById(id);
            if (dato != null)
            {
                return Ok(dato);
            }
            else
            {
                return BadRequest(repositorio.Error);
            }
        }

        // POST api/<GenericController>
        [HttpPost]
        public ActionResult<T> Post([FromBody] T value)
        {
            var dato = repositorio.Insert(value);
            if (dato != null)
            {
                return Ok(dato);
            }
            else
            {
                return BadRequest(repositorio.Error);
            }
        }

        // PUT api/<GenericController>/5
        [HttpPut]
        public ActionResult<T> Put([FromBody] T value)
        {
            var dato = repositorio.Update(value);
            if (dato != null)
            {
                return Ok(dato);
            }
            else
            {
                return BadRequest(repositorio.Error);
            }
        }

        // DELETE api/<GenericController>/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(string id)
        {
            var dato = repositorio.Delete(id);
            if (dato)
            {
                return Ok(dato);
            }
            else
            {
                return BadRequest(repositorio.Error);
            }
        }
    }
}
