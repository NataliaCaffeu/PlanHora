using PlanHora.Models;
using PlanHora.Services;
using Microsoft.Maui.Controls;

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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Cargar empleados desde la base de datos
            EmpleadosCollection.ItemsSource = await _db.GetEmpleadosAsync();
        }

        private async void OnAddEmpleadoClicked(object sender, EventArgs e)
        {
            // añadir un empleado de prueba
            var empleado = new Empleado
            {
                Nombre = "Empleado Prueba",
                Puesto = "Camarero",
                JornadaSemanal = 40,
                LocalId = 1 
            };
            await _db.SaveEmpleadoAsync(empleado);
            EmpleadosCollection.ItemsSource = await _db.GetEmpleadosAsync();
        }
    }
}
