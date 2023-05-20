using COMMON.Entidades;
using COMMON.Interfaces;
using COMMON.Validadores;
using DAL.Mongo;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispositivoController : GenericController<Dispositivo>
    {
        public DispositivoController() : base(new GenericRepositorio<Dispositivo>(new DispositivoValidator()))
        {

        }
    }
}
