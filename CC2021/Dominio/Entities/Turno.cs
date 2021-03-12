using System;

namespace Dominio.Entities
{
    public class Turno : BaseEntity
    {
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public string Descripcion { get; set; }

        public bool EsTurnoValido(DateTime fechaActual)
        {
            var HoraInicio = Inicio.TimeOfDay;
            var HoraFin = Fin.TimeOfDay;
            var hora = fechaActual.TimeOfDay;

            if (HoraInicio > HoraFin)
            {
                return HoraInicio >= hora && hora <= HoraFin;
            }

            return !(HoraInicio >= hora && hora <= HoraFin);
            
        }
    }

    
}