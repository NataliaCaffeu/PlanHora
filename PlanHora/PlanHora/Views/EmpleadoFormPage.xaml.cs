using Microsoft.Maui.Controls;
using PlanHora.Models;
using PlanHora.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanHora.Views
{
    public partial class EmpleadoFormPage : ContentPage
    {
        private readonly DatabaseService _db;
        private readonly Empleado _empleado;
        private List<Local> _locales;

        // Constructor para nuevo empleado
        public EmpleadoFormPage()
        {
            InitializeComponent();
            _db = new DatabaseService();
            _empleado = new Empleado();
            CargarLocalesAsync();
        }

        // Constructor para editar empleado existente
        public EmpleadoFormPage(Empleado empleado)
        {
            InitializeComponent();
            _db = new DatabaseService();
            _empleado = empleado;
            CargarLocalesAsync();

            // Cargar datos en los campos
            NombreEntry.Text = empleado.Nombre;
            PuestoEntry.Text = empleado.Puesto;
            JornadaEntry.Text = empleado.JornadaSemanal.ToString();
        }

        private async Task CargarLocalesAsync()
        {
            _locales = await _db.GetLocalesPorUsuarioAsync(SessionService.UsuarioActual.Id);
            LocalPicker.ItemsSource = _locales;

            // Si es edición, seleccionar el local correspondiente
            if (_empleado.LocalId > 0)
            {
                var localSeleccionado = _locales.FirstOrDefault(l => l.Id == _empleado.LocalId);
                if (localSeleccionado != null)
                    LocalPicker.SelectedItem = localSeleccionado;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Validaciones
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

            if (LocalPicker.SelectedItem is not Local localSeleccionado)
            {
                await DisplayAlert("Error", "Debes seleccionar un local.", "OK");
                return;
            }

            // Asignar datos al empleado
            _empleado.Nombre = NombreEntry.Text.Trim();
            _empleado.Puesto = PuestoEntry.Text?.Trim() ?? "";
            _empleado.JornadaSemanal = jornada;
            _empleado.LocalId = localSeleccionado.Id;
            _empleado.UsuarioId = SessionService.UsuarioActual.Id;

            // Guardar en la base de datos
            await _db.SaveEmpleadoAsync(_empleado);

            await DisplayAlert("Éxito", "Empleado guardado correctamente.", "OK");
            await Navigation.PopAsync();
        }
    }
}


