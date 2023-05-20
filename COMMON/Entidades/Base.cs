using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMMON.Entidades
{
    public abstract class Base
    {
        public abstract string Id { get; set; }
        public DateTime FechaHora { get; set; }
        public bool Habilitado { get; set; }

    }
}