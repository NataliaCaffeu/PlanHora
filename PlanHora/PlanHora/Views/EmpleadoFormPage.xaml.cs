using Microsoft.Maui.Controls;
using PlanHora.Models;
using PlanHora.Services;
using System;

namespace PlanHora.Views
{
    public partial class EmpleadoFormPage : ContentPage
    {
        private readonly DatabaseService _db;
        private readonly Empleado _empleado;

        public EmpleadoFormPage(Empleado? empleado = null)
        {
            InitializeComponent();
            _db = new DatabaseService();
            _empleado = empleado ?? new Empleado();

            if (empleado != null)
            {
                NombreEntry.Text = empleado.Nombre;
                PuestoEntry.Text = empleado.Puesto;
                JornadaEntry.Text = empleado.JornadaSemanal.ToString();
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NombreEntry.Text))
            {
                await DisplayAlert("Error", "El nombre es obligatorio.", "OK");
                return;
            }

            if (!int.TryParse(JornadaEntry.Text, out int jornada) || jornada <= 0)
            {
                await DisplayAlert("Error", "La jornada semanal debe ser un número positivo.", "OK");
                return;
            }

            _empleado.Nombre = NombreEntry.Text.Trim();
            _empleado.Puesto = PuestoEntry.Text?.Trim() ?? "";
            _empleado.JornadaSemanal = jornada;

            await _db.SaveEmpleadoAsync(_empleado);
            await DisplayAlert("Éxito", "Empleado guardado correctamente.", "OK");
            await Navigation.PopAsync();
        }
    }
}
