using System.ComponentModel.DataAnnotations;

namespace CC2021Proyecto.ValidationClass
{
    public class UsuarioValidation
    {
        [Required(ErrorMessage = "Porfavor ingrese el usuario")]
        [Display(Name = "Descripcion")]
        [StringLength(25)]
        public string User { get; set; }
        [Required(ErrorMessage = "Porfavor ingrese la contraseña")]
        [Display(Name = "Descripcion")]
        [StringLength(25)]
        public string Password { get; set; }
    }
}