using Microsoft.Maui.Controls;
using PlanHora.Models;
using PlanHora.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanHora.Views
{
    public partial class EmpleadosDeletePage : ContentPage
    {
        private readonly DatabaseService _db;
        private List<Local> _locales = new();
        private List<Empleado> _empleados = new();

        public EmpleadosDeletePage()
        {
            InitializeComponent();
            _db = new DatabaseService();
            _ = CargarLocalesAsync();
        }

        private async Task CargarLocalesAsync()
        {
            _locales = await _db.GetLocalesAsync();
            LocalPicker.ItemsSource = _locales;
        }

        private async void OnLocalSelected(object sender, EventArgs e)
        {
            if (LocalPicker.SelectedItem is Local localSeleccionado)
            {
                _empleados = (await _db.GetEmpleadosAsync())
                             .Where(emp => emp.LocalId == localSeleccionado.Id)
                             .ToList();

                EmpleadosCollection.ItemsSource = _empleados;
            }
        }

        private async void OnBorrarEmpleadoClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BindingContext is Empleado emp)
            {
                bool confirmar = await DisplayAlert("Confirmar",
                    $"¿Deseas borrar al empleado {emp.Nombre}?", "Sí", "No");

                if (confirmar)
                {
                    await _db.DeleteEmpleadoAsync(emp);

                    if (LocalPicker.SelectedItem is Local localSeleccionado)
                    {
                        _empleados = (await _db.GetEmpleadosAsync())
                                     .Where(x => x.LocalId == localSeleccionado.Id)
                                     .ToList();
                        EmpleadosCollection.ItemsSource = _empleados;
                    }
                }
            }
        }
    }
}
