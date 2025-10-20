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
                // Convertir apertura y cierre del local a TimeSpan
                if (!TimeSpan.TryParse(_local.HoraApertura, out var apertura))
                {
                    await DisplayAlert("Error", "Horario de apertura del local inválido.", "OK");
                    return;
                }

                if (!TimeSpan.TryParse(_local.HoraCierre, out var cierre))
                {
                    await DisplayAlert("Error", "Horario de cierre del local inválido.", "OK");
                    return;
                }

                // Diccionario para recorrer los días
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

                foreach (var dia in dias)
                {
                    if (dia.Value.libre.IsChecked)
                    {
                        SetHorarioDia(dia.Key, "", "");
                        continue;
                    }

                    if (!TimeSpan.TryParse(dia.Value.entrada.Text, out var entrada))
                    {
                        await DisplayAlert("Error", $"Entrada inválida para {dia.Key}", "OK");
                        return;
                    }

                    if (!TimeSpan.TryParse(dia.Value.salida.Text, out var salida))
                    {
                        await DisplayAlert("Error", $"Salida inválida para {dia.Key}", "OK");
                        return;
                    }

                    if (entrada >= salida)
                    {
                        await DisplayAlert("Error", $"La salida de {dia.Key} debe ser posterior a la entrada.", "OK");
                        return;
                    }

                    if (entrada < apertura)
                    {
                        await DisplayAlert("Error", $"La entrada de {dia.Key} no puede ser antes de la apertura del local", "OK");
                        return;
                    }

                    if (salida > cierre)
                    {
                        await DisplayAlert("Error", $"La salida de {dia.Key} no puede ser después del cierre del local", "OK");
                        return;
                    }

                    double horasDia = (salida - entrada).TotalHours;

                    if (horasDia > 9)
                    {
                        await DisplayAlert("Error", $"No se pueden trabajar más de 9 horas en {dia.Key}", "OK");
                        return;
                    }

                    horasTotales += horasDia;

                    SetHorarioDia(dia.Key, dia.Value.entrada.Text, dia.Value.salida.Text);
                }

                if (horasTotales > _empleado.JornadaSemanal)
                {
                    await DisplayAlert("Error", $"La jornada total ({horasTotales}h) excede la jornada semanal del empleado ({_empleado.JornadaSemanal}h).", "OK");
                    return;
                }

                await _db.SaveHorarioAsync(_horario);
                await DisplayAlert("Éxito", "Horario guardado correctamente", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
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
