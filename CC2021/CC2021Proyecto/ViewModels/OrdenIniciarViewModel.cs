using System.Collections.Generic;
using Dominio.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CC2021Proyecto.ViewModels
{
    public class OrdenIniciarViewModel
    {
        
        public  List<LineaDeTrabajo> Lineas { get; set; }
        public List<Modelo> Modelos { get; set; }
        public List<Color> Colores { get; set; }
        
    }
}