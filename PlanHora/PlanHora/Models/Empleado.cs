using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanHora.Models
{
    public class Empleado
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public string Puesto { get; set; } = string.Empty;
        public int LocalId { get; set; }
        public int JornadaSemanal { get; set; }
        public string? Observaciones { get; set; }
        public int UsuarioId { get; set; }

    }
}
