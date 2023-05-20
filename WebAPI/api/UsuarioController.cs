using COMMON.Entidades;
using COMMON.Validadores;
using DAL.Mongo;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : GenericController<Usuario>
    {
        public UsuarioController() : base(new GenericRepositorio<Usuario>(new UsuarioValidator()))
        {
        }
    }
}
