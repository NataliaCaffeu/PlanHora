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
            var locales = await _db.GetLocalesAsync();
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
            var empleados = (await _db.GetEmpleadosAsync()).Where(emp => emp.LocalId == localSeleccionado.Id).ToList();
            if (!empleados.Any())
            {
                await DisplayAlert("Aviso", "No hay empleados en este local.", "OK");
                return;
            }

            // Mostrar lista con opci�n de borrar
            foreach (var empleado in empleados)
            {
                bool borrar = await DisplayAlert("Borrar empleado",
                    $"�Deseas borrar a {empleado.Nombre} ({empleado.Puesto})?", "S�", "No");

                if (borrar)
                {
                    await _db.SaveEmpleadoAsync(new Empleado { Id = empleado.Id }); // Aqu� cambiar�amos a Delete
                }
            }
        }
    }
}
