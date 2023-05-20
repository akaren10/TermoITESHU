using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace COMMON.Entidades
{
    public abstract class Lectura : Base
    {
        public string IdDispositivo { get; set; }
        public float Temperatura { get; set; }
        public float Humedad { get; set; }
        public float Luminosidad { get; set; }
        public bool EstatusRele1 { get; set; }
        public bool EstatusRele2 { get; set; }
        public bool EstatusRele3 { get; set; }
        public bool EstatusRele4 { get; set; }

    }
}

