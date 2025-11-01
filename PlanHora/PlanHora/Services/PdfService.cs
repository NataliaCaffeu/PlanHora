using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using PlanHora.Models;
using SkiaSharp;

namespace PlanHora.Services
{
    public class PdfService
    {
        const float A4_WIDTH = 595;
        const float A4_HEIGHT = 842;

        public async Task<string> GenerarCuadranteEmpleadoAsync(string nombreEmpleado, List<HorarioDia> horarios, string nombreLocal)
        {
            string fileName = $"Cuadrante_{SanitizeFileName(nombreEmpleado)}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string filePath = Path.Combine(FileSystem.CacheDirectory, fileName);

            await Task.Run(() =>
            {
                using (var stream = File.OpenWrite(filePath))
                using (var document = SKDocument.CreatePdf(stream))
                {
                    if (document == null)
                        throw new Exception("No se pudo crear el documento PDF.");

                    using (var canvas = document.BeginPage((int)A4_WIDTH, (int)A4_HEIGHT))
                    {
                        canvas.Clear(SKColors.White);

                        float y = 40;
                        float margin = 40;
                        float colDiaX = margin;
                        float colHorarioX = margin + 180;
                        float rowHeight = 22;

                        var titlePaint = new SKPaint { Typeface = SKTypeface.Default, TextSize = 18, IsAntialias = true, Color = SKColors.Black, TextAlign = SKTextAlign.Center };
                        var textPaint = new SKPaint { Typeface = SKTypeface.Default, TextSize = 12, IsAntialias = true, Color = SKColors.Black };

                        // Título
                        canvas.DrawText($"Cuadrante Semanal - {nombreEmpleado}", A4_WIDTH / 2, y, titlePaint);
                        y += 28;

                        canvas.DrawText($"Local: {nombreLocal}", margin, y, textPaint);
                        y += 24;

                        // Encabezados
                        canvas.DrawText("Día", colDiaX, y, textPaint);
                        canvas.DrawText("Horario", colHorarioX, y, textPaint);
                        y += rowHeight;

                        // Filas
                        foreach (var h in horarios)
                        {
                            canvas.DrawText(h.DiaSemana, colDiaX, y, textPaint);
                            string horarioTexto = h.EsLibre ? "Libre" : $"{h.HoraEntrada} - {h.HoraSalida}";
                            canvas.DrawText(horarioTexto, colHorarioX, y, textPaint);
                            y += rowHeight;
                        }

                        // Pie de página
                        canvas.DrawText($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}", A4_WIDTH / 2, A4_HEIGHT - 30, textPaint);

                        document.EndPage();
                    }

                    document.Close();
                }
            });

            return filePath;
        }

        private string SanitizeFileName(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }
    }
}
