using Microsoft.AspNetCore.Mvc;
using asp_servicios.Nucleo;
using lib_repositorios;
using Microsoft.EntityFrameworkCore;
namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PacientesController : ControllerBase
    {

        [HttpGet(Name = "GetPacientes")]
        public IEnumerable<Pacientes> Get()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=LAPTOP-1ITG8EDT\\SQLEXPRESS;database=db_veteri;Integrated Security=True;TrustServerCertificate=true;";
            conexion.Database.Migrate();
            conexion.Guardar(new Pacientes()

            {
                Nombre = "Roberto",
                FechaNacimiento = DateTime.Parse("2022/02/04"),
                Raza = "Perro Pastor Aleman",
                Estado = false,
            });

            conexion.SaveChanges();
            return conexion.Listar<Pacientes>();

        }

        //Este metodo nos retorna una lista con la edad de todos los pacientes que tengamos, independiente de su estado
        [HttpPost]
        public List<int> EdadPaciente()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=LAPTOP-1ITG8EDT\\SQLEXPRESS;database=db_veteri;Integrated Security=True;TrustServerCertificate=true;";
            var lista = conexion.Listar<Pacientes>();
            var Fecha = DateTime.Today;
            List<int> Edad = new List<int>();
            foreach (var x in lista)
            {
                Edad.Add(Fecha.Subtract(x.FechaNacimiento).Days / 365);
            }
            return Edad;

        }

        // sumar edades de todos los pacientes
        [HttpPost]
        public int SumaEdadPaciente()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=LAPTOP-1ITG8EDT\\SQLEXPRESS;database=db_veteri;Integrated Security=True;TrustServerCertificate=true;";
            var lista = EdadPaciente();
            int edades = 0;

            foreach (var x in lista)
            {
                edades = edades + x;

            }
            return edades;
        }

        //promedio de todas las edades
        [HttpPost]
        public decimal PromEdadPaciente()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=LAPTOP-1ITG8EDT\\SQLEXPRESS;database=db_veteri;Integrated Security=True;TrustServerCertificate=true;";
            var lista = EdadPaciente();
            int edades = 0;
            decimal tot = 0;
            decimal prom = 0;

            foreach (var x in lista)
            {
                edades = edades + x;
                tot ++;

            }
            return prom = (edades/tot);

        }

        //sumattoria de todas las fechas de cumpleaños de los pacientes ?????



        //promedio de años de nacimiento de los pacientes
        [HttpPost]
        public decimal PromAñosNacimientoPaciente()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=LAPTOP-1ITG8EDT\\SQLEXPRESS;database=db_veteri;Integrated Security=True;TrustServerCertificate=true;";
            var lista = conexion.Listar<Pacientes>();
            decimal Fecha = 0;
            decimal contar = 0;
            int años = 0;
            foreach (var x in lista)
            {
                años = años + x.FechaNacimiento.Year;
                contar++;
            }
            return Fecha = años / contar ;

        }
        
    }
}
