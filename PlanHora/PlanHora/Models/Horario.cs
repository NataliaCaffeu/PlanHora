using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanHora.Models
{
    public class Horario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public string Dia { get; set; } = string.Empty;
        public string HoraEntrada { get; set; } = string.Empty;
        public string HoraSalida { get; set; } = string.Empty;
    }
}
