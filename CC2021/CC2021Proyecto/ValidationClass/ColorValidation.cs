using System.ComponentModel.DataAnnotations;

namespace CC2021Proyecto.ValidationClass
{
    public class ColorValidation
    {
        [Required(ErrorMessage = "Porfavor ingrese la descripción")]
        [Display(Name = "Descripcion")]
        [StringLength(25)]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Porfavor ingrese el codigo")]
        [Display(Name = "Codigo")]
        public int Codigo { get; set; }
    }
}