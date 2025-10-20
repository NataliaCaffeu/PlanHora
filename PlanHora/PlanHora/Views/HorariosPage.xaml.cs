using Microsoft.Maui.Controls;
using PlanHora.Models;
using PlanHora.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanHora.Views
{
    [QueryProperty(nameof(LocalId), "localId")]
    public partial class HorariosPage : ContentPage
    {
        private readonly DatabaseService _db;
        private int _localId;
        private Local _localSeleccionado;
        private List<Empleado> _empleados = new();

        public int LocalId
        {
            get => _localId;
            set
            {
                _localId = value;
                // Cargar el objeto Local desde la base de datos
                LoadLocalAsync(_localId);
                LoadEmpleadosAsync();
            }
        }

        public HorariosPage()
        {
            InitializeComponent();
            _db = new DatabaseService();
        }

        private async void LoadLocalAsync(int localId)
        {
            var locales = await _db.GetLocalesAsync();
            _localSeleccionado = locales.FirstOrDefault(l => l.Id == localId);

            if (_localSeleccionado == null)
            {
                await DisplayAlert("Error", "No se encontró el local seleccionado.", "OK");
                return;
            }
        }

        private async void LoadEmpleadosAsync()
        {
            if (_localId <= 0)
                return;

            // Obtener los empleados del local
            _empleados = await _db.GetEmpleadosAsync();
            var empleadosLocal = _empleados.Where(e => e.LocalId == _localId).ToList();

            EmpleadoPicker.ItemsSource = empleadosLocal;

            // Vaciar selección y lista de horarios al cambiar de local
            EmpleadoPicker.SelectedItem = null;
            HorariosCollectionView.ItemsSource = null;

            if (!empleadosLocal.Any())
            {
                await DisplayAlert("Aviso", "Este local no tiene empleados registrados aún.", "OK");
            }
        }

        private async void OnAddHorarioClicked(object sender, EventArgs e)
        {
            if (EmpleadoPicker.SelectedItem is not Empleado empleadoSeleccionado)
            {
                await DisplayAlert("Error", "Debes seleccionar un empleado antes de agregar horarios.", "OK");
                return;
            }

            if (_localSeleccionado == null)
            {
                await DisplayAlert("Error", "No se ha cargado correctamente el local.", "OK");
                return;
            }

            // Abrir HorarioFormPage pasando el empleado y el local
            await Navigation.PushAsync(new HorarioFormPage(empleadoSeleccionado, _localSeleccionado));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (EmpleadoPicker.SelectedItem is Empleado empleado)
                await CargarHorariosAsync(empleado.Id);
        }

        private async void OnEmpleadoSelected(object sender, EventArgs e)
        {
            if (EmpleadoPicker.SelectedItem is Empleado empleadoSeleccionado)
                await CargarHorariosAsync(empleadoSeleccionado.Id);
        }

        private async Task CargarHorariosAsync(int empleadoId)
        {
            var horarios = await _db.GetHorariosAsync();
            HorariosCollectionView.ItemsSource = horarios
                .Where(h => h.EmpleadoId == empleadoId)
                .ToList();
        }
    }
}
