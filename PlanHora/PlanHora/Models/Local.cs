using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanHora.Models
{
    public class Local
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public string Apertura { get; set; } = string.Empty;
        public string Cierre { get; set; } = string.Empty;
    }
}
