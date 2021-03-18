using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace CC2021Proyecto.ViewModels
{
    public class ColoresListViewModel
    {
        public IReadOnlyList<Color> Colores { get; set; }
        public Usuario Usuario { get; set; }
    }
}
