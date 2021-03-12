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
<<<<<<< HEAD
        
=======
        public IList<Color> Coloress { get; set; }
        public string ColorSelected{ get; set; }
        public IList<Modelo> Modeloss { get; set; }
        public string ModeloSelected { get; set; }
        public IList<LineaDeTrabajo> Lineass { get; set; }
        public string LineaSelected{ get; set; }

>>>>>>> 5ea75cbb28bee9067aac1dff96635c04a71473ae
    }
}