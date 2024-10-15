using System.ComponentModel.DataAnnotations;

namespace asp_servicios.Nucleo
{
    public class Citas
    {
        [Key] public int CitasId { get; set; }
        public DateTime Fecha { get; set; }
        public int PacienteId { get; set; }
        public string? Diagnostico { get; set; }

    }
}
