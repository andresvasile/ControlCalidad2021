using System.ComponentModel.DataAnnotations;

namespace CC2021Proyecto.ValidationClass
{
    public class ModeloValidation
    {
        [Required(ErrorMessage = "Porfavor ingrese el sku")]
        [Display(Name = "Sku")]
        [StringLength(25)]
        public string Sku { get; set; }
        [Required(ErrorMessage = "Porfavor ingrese la denominación")]
        [Display(Name = "Denominación")]
        [StringLength(25)]
        public string Denominacion { get; set; }
        [Required(ErrorMessage = "Porfavor ingrese el Objetivo")]
        [Display(Name = "Objetivo")]
        public int Objetivo { get; set; }
    }
}