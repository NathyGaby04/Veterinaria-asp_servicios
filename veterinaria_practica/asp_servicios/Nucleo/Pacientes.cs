using System.ComponentModel.DataAnnotations;

namespace asp_servicios.Nucleo
{
    public class Pacientes
    {
        [Key]public int PacientesId {  get; set; }
        public string? Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Raza {  get; set; }
        public bool Estado { get; set; }

    }
}
