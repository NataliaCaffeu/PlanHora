namespace PlanHora.Models
{
    public class HorarioDia
    {
        public string DiaSemana { get; set; } = string.Empty;
        public string HoraEntrada { get; set; } = string.Empty;
        public string HoraSalida { get; set; } = string.Empty;
        public bool EsLibre { get; set; } = false;

    }
}
