using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMMON.Entidades
{
    public  class Usuario : Base
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }

        public List<Dispositivo> Dispositivos { get; set; }
        public bool EsAdministrador { get; set; }
        public Usuario()
        {
            Dispositivos = new List<Dispositivo>();

        }
    }
}

