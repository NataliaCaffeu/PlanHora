using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PlanHora.Models;


namespace PlanHora.Services
{
    public class PdfService
    {
        public async Task<string> GenerarCuadranteEmpleadoAsync(string empleadoNombre, List<HorarioDia> horarios, string localNombre)
        {
            string fileName = $"Cuadrante_{empleadoNombre}_{DateTime.Now:yyyyMMddHHmm}.pdf";
            string filePath = Path.Combine(FileSystem.CacheDirectory, fileName);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.PageColor(QuestPDF.Helpers.Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text($"Cuadrante Semanal - {empleadoNombre} ({localNombre})")
                        .FontSize(18).Bold().AlignCenter();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2); // Día
                            columns.RelativeColumn(3); // Horas
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Día").Bold();
                            header.Cell().Text("Horario").Bold();
                        });

                        foreach (var h in horarios)
                        {
                            table.Cell().Text(h.DiaSemana);
                            table.Cell().Text(
                                h.EsLibre ? "Libre" :
                                $"{h.HoraEntrada} - {h.HoraSalida}"
                            );
                        }
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}");
                });
            });

            await Task.Run(() => document.GeneratePdf(filePath));
            return filePath;
        }
    }
}
