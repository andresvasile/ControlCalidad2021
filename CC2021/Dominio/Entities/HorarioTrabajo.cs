using System;
using System.Collections.Generic;

namespace Dominio.Entities
{
    public class HorarioTrabajo : BaseEntity
    {
        public DateTime Inicio{ get; set; }
        public DateTime Fin { get; set; }
        public int OrdenDeProduccionId { get; set; }
        public virtual OrdenDeProduccion OrdenDeProduccion{ get; set; }
        public virtual Hermanado Hermanado{ get; set; }
        public virtual List<Hallazgo> Hallazgos{ get; set; }
        public virtual List<Primera> ParesPrimera{ get; set; }
        public virtual Turno Turno{ get; set; }

        public HorarioTrabajo()
        {
            if(Hallazgos ==null) Hallazgos = new List<Hallazgo>();
            if(ParesPrimera==null) ParesPrimera = new List<Primera>();
        }

    }
}