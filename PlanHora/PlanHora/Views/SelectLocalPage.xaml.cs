using Microsoft.Maui.Controls;
using PlanHora.Models;
using PlanHora.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanHora.Views
{
    public partial class SelectLocalPage : ContentPage
    {
        private readonly DatabaseService _db;
        private List<Local> _locales = new();

        public SelectLocalPage()
        {
            InitializeComponent();
            _db = new DatabaseService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //await CargarLocalesAsync();

            // Verificar que hay usuario logueado
            if (!SessionService.HaySesionActiva)
            {
                await DisplayAlert("Error", "Debes iniciar sesión primero.", "OK");
                await Shell.Current.GoToAsync("//LoginPage");
                return;
            }

            _locales = await _db.GetLocalesPorUsuarioAsync(SessionService.UsuarioActual.Id);
            LocalPicker.ItemsSource = _locales;
        }

        //private async Task CargarLocalesAsync()
        //{
        //    _locales = await _db.GetLocalesAsync();
        //    LocalPicker.ItemsSource = _locales;
        //}

        private async void OnLocalSelected(object sender, EventArgs e)
        {
            if (LocalPicker.SelectedItem is Local localSeleccionado)
            {
                await DisplayAlert("Local seleccionado", $"Has elegido: {localSeleccionado.Nombre}", "OK");

                // Navegación absoluta usando /// para que Shell la reconozca
                await Shell.Current.GoToAsync($"///HorariosPage?localId={localSeleccionado.Id}");

                // Limpiar selección si quieres que se pueda volver a elegir
                LocalPicker.SelectedItem = null;
            }
        }

        private async void OnAddLocalClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LocalFormPage());
        }

        private async void OnDeleteLocalClicked(object sender, EventArgs e)
        {
            // Obtener todos los locales
            var locales = await _db.GetLocalesPorUsuarioAsync(SessionService.UsuarioActual.Id);
            if (!locales.Any())
            {
                await DisplayAlert("Aviso", "No hay locales registrados.", "OK");
                return;
            }

            // Mostrar lista de locales
            string[] nombresLocales = locales.Select(l => l.Nombre).ToArray();
            string seleccionado = await DisplayActionSheet("Selecciona un local para borrar", "Cancelar", null, nombresLocales);

            if (string.IsNullOrEmpty(seleccionado) || seleccionado == "Cancelar")
                return;

            var localSeleccionado = locales.First(l => l.Nombre == seleccionado);

            // Confirmar eliminación
            bool confirmar = await DisplayAlert("Confirmar eliminación",
                $"¿Seguro que deseas borrar el local '{localSeleccionado.Nombre}'?\n\n" +
                "Esto eliminará también a todos los empleados y horarios asociados.",
                "Sí, borrar", "Cancelar");

            if (!confirmar)
                return;

            // Eliminar empleados asociados
            var empleados = (await _db.GetEmpleadosPorUsuarioAsync(SessionService.UsuarioActual.Id)).Where(e => e.LocalId == localSeleccionado.Id).ToList();
            foreach (var emp in empleados)
            {
                // También podrías eliminar los horarios de cada empleado si tu base lo permite
                await _db.DeleteEmpleadoAsync(emp);
            }

            // Eliminar el local
            await _db.DeleteLocalAsync(localSeleccionado);

            await DisplayAlert("Éxito", $"El local '{localSeleccionado.Nombre}' y sus empleados asociados han sido eliminados correctamente.", "OK");
        }

        private async void OnVerEmpleadosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EmpleadosPage());
        }

    }
}
