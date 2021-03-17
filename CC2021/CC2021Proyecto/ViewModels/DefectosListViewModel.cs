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
        public Turno Turno{ get; set; }
        public IReadOnlyList<Hallazgo> Hallazgos { get; set; }
        public IReadOnlyList<Primera> Primeras { get; set; }
        public int CantidadPrimera{ get; set; }
        public int DefectosObservadosIzquierdo{ get; set; }
        public int DefectosObservadosDerecho{ get; set; }
        public int DefectosReprocesosIzquierdo { get; set; }
        public int DefectosReprocesosDerecho { get; set; }
        public Defecto def { get; set; }

    }
}