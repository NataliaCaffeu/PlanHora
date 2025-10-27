using Microsoft.Maui.Controls;
using PlanHora.Models;
using PlanHora.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel.DataTransfer;


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
            var horarioEmpleado = horarios.FirstOrDefault(h => h.EmpleadoId == empleadoId);

            if (horarioEmpleado == null)
            {
                HorariosCollectionView.ItemsSource = null;
                return;
            }

            var items = new List<HorarioDia>
            {
                new HorarioDia { DiaSemana = "Lunes", HoraEntrada = horarioEmpleado.LunesEntrada, HoraSalida = horarioEmpleado.LunesSalida, EsLibre = string.IsNullOrEmpty(horarioEmpleado.LunesEntrada) && string.IsNullOrEmpty(horarioEmpleado.LunesSalida) },
                new HorarioDia { DiaSemana = "Martes", HoraEntrada = horarioEmpleado.MartesEntrada, HoraSalida = horarioEmpleado.MartesSalida, EsLibre = string.IsNullOrEmpty(horarioEmpleado.MartesEntrada) && string.IsNullOrEmpty(horarioEmpleado.MartesSalida) },
                new HorarioDia { DiaSemana = "Miércoles", HoraEntrada = horarioEmpleado.MiercolesEntrada, HoraSalida = horarioEmpleado.MiercolesSalida, EsLibre = string.IsNullOrEmpty(horarioEmpleado.MiercolesEntrada) && string.IsNullOrEmpty(horarioEmpleado.MiercolesSalida) },
                new HorarioDia { DiaSemana = "Jueves", HoraEntrada = horarioEmpleado.JuevesEntrada, HoraSalida = horarioEmpleado.JuevesSalida, EsLibre = string.IsNullOrEmpty(horarioEmpleado.JuevesEntrada) && string.IsNullOrEmpty(horarioEmpleado.JuevesSalida) },
                new HorarioDia { DiaSemana = "Viernes", HoraEntrada = horarioEmpleado.ViernesEntrada, HoraSalida = horarioEmpleado.ViernesSalida, EsLibre = string.IsNullOrEmpty(horarioEmpleado.ViernesEntrada) && string.IsNullOrEmpty(horarioEmpleado.ViernesSalida) },
                new HorarioDia { DiaSemana = "Sábado", HoraEntrada = horarioEmpleado.SabadoEntrada, HoraSalida = horarioEmpleado.SabadoSalida, EsLibre = string.IsNullOrEmpty(horarioEmpleado.SabadoEntrada) && string.IsNullOrEmpty(horarioEmpleado.SabadoSalida) },
                new HorarioDia { DiaSemana = "Domingo", HoraEntrada = horarioEmpleado.DomingoEntrada, HoraSalida = horarioEmpleado.DomingoSalida, EsLibre = string.IsNullOrEmpty(horarioEmpleado.DomingoEntrada) && string.IsNullOrEmpty(horarioEmpleado.DomingoSalida) },
            };


            HorariosCollectionView.ItemsSource = items.ToList();

        }

        private async void OnExportarPdfClicked(object sender, EventArgs e)
        {
            if (EmpleadoPicker.SelectedItem is not Empleado empleado)
            {
                await DisplayAlert("Error", "Debes seleccionar un empleado antes de exportar.", "OK");
                return;
            }

            if (HorariosCollectionView.ItemsSource is not IEnumerable<HorarioDia> enumerableHorarios)
            {
                await DisplayAlert("Error", "No hay horarios cargados para exportar.", "OK");
                return;
            }

            var horarios = enumerableHorarios.ToList();
            if (!horarios.Any())
            {
                await DisplayAlert("Error", "No hay horarios cargados para exportar.", "OK");
                return;
            }

            if (_localSeleccionado == null)
            {
                await DisplayAlert("Error", "No se ha cargado el local.", "OK");
                return;
            }

            try
            {
                var pdfService = new PdfService();
                var shareService = new ShareService();

                string pdfPath = await pdfService.GenerarCuadranteEmpleadoAsync(
                    empleado.Nombre,
                    horarios,
                    _localSeleccionado.Nombre
                );

                await shareService.CompartirArchivoAsync(pdfPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo generar el PDF:\n{ex.Message}", "OK");
            }
        }


    }


}
