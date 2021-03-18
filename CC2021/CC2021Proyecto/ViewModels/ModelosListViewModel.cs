using System.Collections.Generic;
using Dominio.Entities;

namespace CC2021Proyecto.ViewModels
{
    public class ModelosListViewModel
    {
        public IReadOnlyList<Modelo> Modelos { get; set; }
        public Usuario Usuario { get; set; }

    }
}