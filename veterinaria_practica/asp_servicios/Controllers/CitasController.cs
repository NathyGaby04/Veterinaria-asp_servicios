using Microsoft.AspNetCore.Mvc;
using asp_servicios.Nucleo;
using lib_repositorios;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CitasController : ControllerBase
    {

        [HttpGet(Name = "GetCitas")]
        public IEnumerable<Citas> Get()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=LAPTOP-1ITG8EDT\\SQLEXPRESS;database=db_veteri;Integrated Security=True;TrustServerCertificate=true;";
            conexion.Database.Migrate();
            conexion.Guardar(new Citas()

            {
                Fecha = DateTime.Parse("2024/11/24"),
                PacienteId = 4,
                Diagnostico = "",
            });

            conexion.SaveChanges();
            return conexion.Listar<Citas>();

        }

        //metodo que retorne una lista con las proximas citas
        [HttpPost]
        public List<DateTime> ProximasCita()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=LAPTOP-1ITG8EDT\\SQLEXPRESS;database=db_veteri;Integrated Security=True;TrustServerCertificate=true;";
            var lista = conexion.Listar<Citas>();
            var Fecha1 = DateTime.Today;
            List<DateTime> ProxCitas = new List<DateTime>();
            foreach (var x in lista)
            {
                if (x.Fecha > Fecha1)
                    ProxCitas.Add(x.Fecha);                  
            }
            return ProxCitas;

        }

        //metodo que retorna una lista con las citas por mes //FUNCIONA SOLO PARA ESTE AÑO
        [HttpPost]
        public Dictionary<int,int> CitasPorMes()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=LAPTOP-1ITG8EDT\\SQLEXPRESS;database=db_veteri;Integrated Security=True;TrustServerCertificate=true;";
            var lista = conexion.Listar<Citas>();
            var meses = DateTime.Today.Month;
            var contar = 0;
            Dictionary<int, int> Citaspormes = new Dictionary<int, int>();
            foreach (var x in lista)
            {
                    if (x.Fecha.Month == meses)
                    {
                        contar++;
                        Citaspormes.TryAdd(meses, contar);
                        meses++;
                    }  
            }
            return Citaspormes;
        }

        //Contar citas por diagnostico 
        [HttpPost]
        public Dictionary<string, int> CitasPorDiagnostico()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=LAPTOP-1ITG8EDT\\SQLEXPRESS;database=db_veteri;Integrated Security=True;TrustServerCertificate=true;";
            var lista = conexion.Listar<Citas>();
            int contar = 0;
            int contar2 = 0;
            string tipodiag = "El paciente se encuentra en buen estado solo tiene fiebre, por ende debe tomar el medicamento proporcionado";
            string tipodiag2 = "El paciente se encuentra en buen estado";
            Dictionary<string, int> Citaspordiag = new Dictionary<string, int>();
            foreach (var x in lista)
            {
                if (x.Diagnostico.Equals(tipodiag) )
                {
                    contar++;

                }
                else
                {
                    if (x.Diagnostico.Equals(tipodiag2))
                    {
                        contar2++;
                    }
                }
            }
            Citaspordiag.TryAdd(tipodiag, contar);
            Citaspordiag.TryAdd(tipodiag2, contar2);
            return Citaspordiag;
        }
    }
}
