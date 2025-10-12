using Microsoft.Maui.Controls;
using PlanHora.Models;
using PlanHora.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanHora.Views
{
    public partial class HorariosPage : ContentPage
    {
        private readonly DatabaseService _db;

        public HorariosPage()
        {
            InitializeComponent();
            _db = new DatabaseService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var empleados = await _db.GetEmpleadosAsync();
            EmpleadoPicker.ItemsSource = empleados;
        }

        private async void EmpleadoPicker_SelectedIndexChanged(object? sender, EventArgs? e)
        {
            if (EmpleadoPicker.SelectedItem is Empleado empleado)
            {
                await LoadHorarioEmpleadoAsync(empleado);
            }
        }

        private async Task LoadHorarioEmpleadoAsync(Empleado empleado)
        {
            var horarios = await _db.GetHorariosAsync();
            var empleadoHorarios = horarios.Where(h => h.EmpleadoId == empleado.Id).ToList();

            // Limpiar campos
            EntradaLunes.Text = SalidaLunes.Text = "";
            EntradaMartes.Text = SalidaMartes.Text = "";
            EntradaMiercoles.Text = SalidaMiercoles.Text = "";
            EntradaJueves.Text = SalidaJueves.Text = "";
            EntradaViernes.Text = SalidaViernes.Text = "";
            EntradaSabado.Text = SalidaSabado.Text = "";
            EntradaDomingo.Text = SalidaDomingo.Text = "";

            foreach (var h in empleadoHorarios)
            {
                switch (h.Dia)
                {
                    case "Lunes":
                        EntradaLunes.Text = h.HoraEntrada;
                        SalidaLunes.Text = h.HoraSalida;
                        break;
                    case "Martes":
                        EntradaMartes.Text = h.HoraEntrada;
                        SalidaMartes.Text = h.HoraSalida;
                        break;
                    case "Miércoles":
                        EntradaMiercoles.Text = h.HoraEntrada;
                        SalidaMiercoles.Text = h.HoraSalida;
                        break;
                    case "Jueves":
                        EntradaJueves.Text = h.HoraEntrada;
                        SalidaJueves.Text = h.HoraSalida;
                        break;
                    case "Viernes":
                        EntradaViernes.Text = h.HoraEntrada;
                        SalidaViernes.Text = h.HoraSalida;
                        break;
                    case "Sábado":
                        EntradaSabado.Text = h.HoraEntrada;
                        SalidaSabado.Text = h.HoraSalida;
                        break;
                    case "Domingo":
                        EntradaDomingo.Text = h.HoraEntrada;
                        SalidaDomingo.Text = h.HoraSalida;
                        break;
                }
            }
        }

        private async void OnSaveClicked(object? sender, EventArgs? e)
        {
            if (EmpleadoPicker.SelectedItem is not Empleado empleado)
            {
                await DisplayAlert("Error", "Selecciona un empleado antes de guardar.", "OK");
                return;
            }

            // Traer horarios existentes del empleado
            var allHorarios = await _db.GetHorariosAsync();
            var empleadoHorarios = allHorarios.Where(h => h.EmpleadoId == empleado.Id).ToList();

            var dias = new List<(string Dia, Entry Entrada, Entry Salida)>
            {
                ("Lunes", EntradaLunes, SalidaLunes),
                ("Martes", EntradaMartes, SalidaMartes),
                ("Miércoles", EntradaMiercoles, SalidaMiercoles),
                ("Jueves", EntradaJueves, SalidaJueves),
                ("Viernes", EntradaViernes, SalidaViernes),
                ("Sábado", EntradaSabado, SalidaSabado),
                ("Domingo", EntradaDomingo, SalidaDomingo)
            };

            foreach (var (dia, entrada, salida) in dias)
            {
                var horarioExistente = empleadoHorarios.FirstOrDefault(h => h.Dia == dia);
                if (horarioExistente != null)
                {
                    // Actualizar horario existente usando su Id
                    horarioExistente.HoraEntrada = entrada.Text;
                    horarioExistente.HoraSalida = salida.Text;
                    await _db.SaveHorarioAsync(horarioExistente);
                }
                else
                {
                    // Crear nuevo horario solo si no existe
                    var nuevoHorario = new Horario
                    {
                        EmpleadoId = empleado.Id,
                        Dia = dia,
                        HoraEntrada = entrada.Text,
                        HoraSalida = salida.Text
                    };
                    await _db.SaveHorarioAsync(nuevoHorario);
                }
            }

            await DisplayAlert("Éxito", "Horario guardado correctamente.", "OK");

            // Refrescar los campos para mostrar los datos guardados
            await LoadHorarioEmpleadoAsync(empleado);
        }
    }
}
