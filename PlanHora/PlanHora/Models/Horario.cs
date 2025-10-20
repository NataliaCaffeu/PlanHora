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
        public int LocalId { get; set; }

        public string LunesEntrada { get; set; } = string.Empty;
        public string LunesSalida { get; set; } = string.Empty;
        public string MartesEntrada { get; set; } = string.Empty;
        public string MartesSalida { get; set; } = string.Empty;
        public string MiercolesEntrada { get; set; } = string.Empty;
        public string MiercolesSalida { get; set; } = string.Empty;
        public string JuevesEntrada { get; set; } = string.Empty;
        public string JuevesSalida { get; set; } = string.Empty;
        public string ViernesEntrada { get; set; } = string.Empty;
        public string ViernesSalida { get; set; } = string.Empty;
        public string SabadoEntrada { get; set; } = string.Empty;
        public string SabadoSalida { get; set; } = string.Empty;
        public string DomingoEntrada { get; set; } = string.Empty;
        public string DomingoSalida { get; set; } = string.Empty;
    }
}
