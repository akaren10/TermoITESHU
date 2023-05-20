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
    }
}
