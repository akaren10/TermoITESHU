using COMMON.Entidades;
using COMMON.Validadores;
using DAL.Mongo;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturaController : GenericController<Lectura>
    {
        public LecturaController() : base(new GenericRepositorio<Lectura>(new LecturaValidator()))
        {

        }
        [HttpGet("ObtenerLecturasDeDispositivo/{id}/{cantidad}")]
        public ActionResult<List<Lectura>> ObtenerLecturassDeDispositivo(string id, int cantidad)
        {
            if(cantidad == -1)
            {
                return Ok(repositorio.Query(l=>l.IdDispositivo==id).ToList());
            }
            else
            {
                return Ok(repositorio.Query(l => l.IdDispositivo == id).OrderByDescending(e => e.FechaHora).Take(cantidad).ToList());
            }
        }
        [HttpPost("ObtenerHistoricoDeLecturas")]
        public ActionResult<List<Lectura>> ObtenerHistoricoDeLecturas([FromBody] IntervaloModel datos)
        {
            try
            {
                var data = repositorio.Query(l => l.IdDispositivo == datos.IdDispositivo && l.FechaHora.ToUniversalTime() >= datos.Desde && l.FechaHora.ToUniversalTime() <=datos.Hasta);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
