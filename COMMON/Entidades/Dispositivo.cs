using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMMON.Entidades
{
    public  class Dispositivo : Base
    {
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public DateTime FechaColocacion { get; set; }
        public string NombreRele1 { get; set; }
        public string NombreRele2 { get; set; }
        public string NombreRele3 { get; set; }
        public string NombreRele4 { get; set; }
        public bool EstatusRele1 { get; set; }
        public bool EstatusRele2 { get; set; }
        public bool EstatusRele3 { get; set; }
        public bool EstatusRele4 { get; set; }




    }
}