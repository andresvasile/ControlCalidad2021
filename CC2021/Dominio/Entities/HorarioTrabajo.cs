using System;
using System.Collections.Generic;

namespace Dominio.Entities
{
    public class HorarioTrabajo : BaseEntity
    {
        public DateTime Inicio{ get; set; }
        public DateTime Fin { get; set; }
        public int OrdenDeProduccionId { get; set; }
        public OrdenDeProduccion OrdenDeProduccion{ get; set; }
        public Hermanado Hermanado{ get; set; }
        public List<Hallazgo> Hallazgos{ get; set; }
        public List<Primera> ParesPrimera{ get; set; }
        public Turno Turno{ get; set; }

        public HorarioTrabajo()
        {
            if(Hallazgos ==null) Hallazgos = new List<Hallazgo>();
            if(ParesPrimera==null) ParesPrimera = new List<Primera>();
        }

    }
}