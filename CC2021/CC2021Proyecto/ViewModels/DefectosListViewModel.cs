using System;
using System.Collections.Generic;
using Dominio.Entities;

namespace CC2021Proyecto.ViewModels
{
    public class DefectosListViewModel
    {
        public IReadOnlyList<Defecto> DefectosObservados { get; set; }
        public IReadOnlyList<Defecto> DefectosReprocesos { get; set; }
        public OrdenDeProduccion Orden{ get; set; }
        public DateTime Hora{ get; set; }
        public string Mensaje{ get; set; }

    }
}