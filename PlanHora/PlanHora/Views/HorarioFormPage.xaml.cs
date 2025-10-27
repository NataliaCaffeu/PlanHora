using Microsoft.Maui.Controls;
using PlanHora.Models;
using PlanHora.Services;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PlanHora.Views
{
    public partial class HorarioFormPage : ContentPage
    {
        private readonly DatabaseService _db;
        private readonly Horario _horario;
        private readonly Empleado _empleado;
        private readonly Local _local;

        public HorarioFormPage(Empleado empleado, Local local, Horario? horarioExistente = null)
        {
            InitializeComponent();
            _db = new DatabaseService();

            _empleado = empleado;
            _local = local;

            _horario = horarioExistente ?? new Horario
            {
                EmpleadoId = empleado.Id,
                LocalId = local.Id
            };

            EmpleadoLabel.Text = $"Empleado: {empleado.Nombre}";
        }

        private void OnDiaLibreCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox cb)
            {
                if (cb.IsChecked)
                {
                    switch (cb.ClassId)
                    {
                        case "Lunes": EntradaLunes.Text = ""; SalidaLunes.Text = ""; break;
                        case "Martes": EntradaMartes.Text = ""; SalidaMartes.Text = ""; break;
                        case "Miercoles": EntradaMiercoles.Text = ""; SalidaMiercoles.Text = ""; break;
                        case "Jueves": EntradaJueves.Text = ""; SalidaJueves.Text = ""; break;
                        case "Viernes": EntradaViernes.Text = ""; SalidaViernes.Text = ""; break;
                        case "Sabado": EntradaSabado.Text = ""; SalidaSabado.Text = ""; break;
                        case "Domingo": EntradaDomingo.Text = ""; SalidaDomingo.Text = ""; break;
                    }
                }
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                // Validar hora de apertura y cierre del local
                if (!TryParseHoraSimple(_local.HoraApertura, out var apertura))
                {
                    await DisplayAlert("Error", "Horario de apertura del local inválido.", "OK");
                    return;
                }

                if (!TryParseHoraSimple(_local.HoraCierre, out var cierre))
                {
                    await DisplayAlert("Error", "Horario de cierre del local inválido.", "OK");
                    return;
                }

                // Diccionario con los días y sus controles
                Dictionary<string, (Entry entrada, Entry salida, CheckBox libre)> dias = new()
                {
                    { "Lunes", (EntradaLunes, SalidaLunes, LibreLunes) },
                    { "Martes", (EntradaMartes, SalidaMartes, LibreMartes) },
                    { "Miercoles", (EntradaMiercoles, SalidaMiercoles, LibreMiercoles) },
                    { "Jueves", (EntradaJueves, SalidaJueves, LibreJueves) },
                    { "Viernes", (EntradaViernes, SalidaViernes, LibreViernes) },
                    { "Sabado", (EntradaSabado, SalidaSabado, LibreSabado) },
                    { "Domingo", (EntradaDomingo, SalidaDomingo, LibreDomingo) },
                };

                double horasTotales = 0;
                var resumenDias = new List<string>();

                foreach (var dia in dias)
                {
                    if (dia.Value.libre.IsChecked)
                    {
                        SetHorarioDia(dia.Key, "", "");
                        resumenDias.Add($"{dia.Key}: Libre");
                        continue;
                    }

                    if (!TryParseHoraSimple(dia.Value.entrada.Text, out var entrada))
                    {
                        await DisplayAlert("Error", $"Entrada inválida para {dia.Key}. Usa formato HH:mm o simplemente la hora (ej: 9 o 9:30)", "OK");
                        return;
                    }

                    if (!TryParseHoraSimple(dia.Value.salida.Text, out var salida))
                    {
                        await DisplayAlert("Error", $"Salida inválida para {dia.Key}. Usa formato HH:mm o simplemente la hora (ej: 17 o 17:30)", "OK");
                        return;
                    }

                    if (entrada >= salida)
                    {
                        await DisplayAlert("Error", $"La salida de {dia.Key} debe ser posterior a la entrada.", "OK");
                        return;
                    }

                    if (entrada < apertura)
                    {
                        await DisplayAlert("Error", $"La entrada de {dia.Key} no puede ser antes de la apertura del local.", "OK");
                        return;
                    }

                    if (salida > cierre)
                    {
                        await DisplayAlert("Error", $"La salida de {dia.Key} no puede ser después del cierre del local.", "OK");
                        return;
                    }

                    double horasDia = (salida - entrada).TotalHours;

                    if (horasDia > 9)
                    {
                        await DisplayAlert("Error", $"No se pueden trabajar más de 9 horas en {dia.Key}.", "OK");
                        return;
                    }

                    horasTotales += horasDia;
                    resumenDias.Add($"{dia.Key}: {horasDia:F1}h");

                    SetHorarioDia(dia.Key, dia.Value.entrada.Text, dia.Value.salida.Text);
                }

                // Validar que al menos un día tenga horario
                bool tieneHorarios = dias.Any(d =>
                    !string.IsNullOrWhiteSpace(d.Value.entrada.Text) &&
                    !string.IsNullOrWhiteSpace(d.Value.salida.Text) &&
                    !d.Value.libre.IsChecked);

                if (!tieneHorarios)
                {
                    await DisplayAlert("Aviso", "No se puede guardar un horario vacío. Ingresa al menos un día trabajado.", "OK");
                    return;
                }

                // Validar jornada semanal
                if (horasTotales > _empleado.JornadaSemanal)
                {
                    await DisplayAlert("Error", $"La jornada total ({horasTotales:F1}h) excede la jornada semanal del empleado ({_empleado.JornadaSemanal}h).", "OK");
                    return;
                }

                // Verificar si ya existe un horario para este empleado y local
                var horarios = await _db.GetHorariosAsync();
                var existente = horarios.FirstOrDefault(h => h.EmpleadoId == _empleado.Id && h.LocalId == _local.Id);

                if (existente != null)
                {
                    // Sobrescribir el existente
                    _horario.Id = existente.Id;
                    await _db.SaveHorarioAsync(_horario); // tu SaveHorarioAsync hace Insert o Update según Id
                }
                else
                {
                    await _db.SaveHorarioAsync(_horario);
                }

                // Mostrar resumen
                string resumen = string.Join(" — ", resumenDias);
                await DisplayAlert(
                    "Éxito",
                    $"Horario guardado correctamente.\n\nTotal semanal: {horasTotales:F1}h\n\n{resumen}",
                    "OK"
                );

                //await DisplayAlert("Éxito", "Horario guardado correctamente.", "OK");

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        // Función para convertir la hora en distintos formatos válidos
        private bool TryParseHoraSimple(string? texto, out TimeSpan hora)
        {
            hora = default;

            if (string.IsNullOrWhiteSpace(texto))
                return false;

            texto = texto.Trim();

            // Si pone solo "9" - 09:00
            if (int.TryParse(texto, out int h) && h >= 0 && h < 24)
            {
                hora = new TimeSpan(h, 0, 0);
                return true;
            }

            // Si pone "9:30" o "09:30"
            if (TimeSpan.TryParseExact(texto, new[] { "h\\:mm", "hh\\:mm" }, CultureInfo.InvariantCulture, out hora))
                return true;

            return false;
        }

        private void SetHorarioDia(string dia, string entrada, string salida)
        {
            switch (dia)
            {
                case "Lunes": _horario.LunesEntrada = entrada; _horario.LunesSalida = salida; break;
                case "Martes": _horario.MartesEntrada = entrada; _horario.MartesSalida = salida; break;
                case "Miercoles": _horario.MiercolesEntrada = entrada; _horario.MiercolesSalida = salida; break;
                case "Jueves": _horario.JuevesEntrada = entrada; _horario.JuevesSalida = salida; break;
                case "Viernes": _horario.ViernesEntrada = entrada; _horario.ViernesSalida = salida; break;
                case "Sabado": _horario.SabadoEntrada = entrada; _horario.SabadoSalida = salida; break;
                case "Domingo": _horario.DomingoEntrada = entrada; _horario.DomingoSalida = salida; break;
            }
        }


    }
}
