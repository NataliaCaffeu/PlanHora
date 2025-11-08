using PlanHora.Models;
using PlanHora.Services;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanHora.Views
{
    public partial class EmpleadosPage : ContentPage
    {
        private readonly DatabaseService _db;

        public EmpleadosPage()
        {
            InitializeComponent();
            _db = new DatabaseService();
        }

        private async void OnAddEmpleadoClicked(object sender, EventArgs e)
        {
            // Abrir el formulario de empleado sin LocalId fijo
            await Navigation.PushAsync(new EmpleadoFormPage());
        }

        private async void OnDeleteEmpleadoClicked(object sender, EventArgs e)
        {
            // Obtener locales
            var locales = await _db.GetLocalesPorUsuarioAsync(SessionService.UsuarioActual.Id);
            if (!locales.Any())
            {
                await DisplayAlert("Error", "No hay locales registrados.", "OK");
                return;
            }

            // Pedir seleccionar local
            string[] nombresLocales = locales.Select(l => l.Nombre).ToArray();
            string seleccionado = await DisplayActionSheet("Selecciona un local", "Cancelar", null, nombresLocales);

            if (string.IsNullOrEmpty(seleccionado) || seleccionado == "Cancelar")
                return;

            var localSeleccionado = locales.First(l => l.Nombre == seleccionado);

            // Obtener empleados de ese local
            var empleados = (await _db.GetEmpleadosPorUsuarioAsync(SessionService.UsuarioActual.Id)).Where(emp => emp.LocalId == localSeleccionado.Id).ToList();
            if (!empleados.Any())
            {
                await DisplayAlert("Aviso", "No hay empleados en este local.", "OK");
                return;
            }

            // Mostrar lista con opción de borrar
            foreach (var empleado in empleados)
            {
                bool borrar = await DisplayAlert("Borrar empleado",
                    $"¿Deseas borrar a {empleado.Nombre} ({empleado.Puesto})?", "Sí", "No");

                if (borrar)
                {
                    await _db.DeleteEmpleadoAsync(empleado);
                }
            }
        }
    }
}
