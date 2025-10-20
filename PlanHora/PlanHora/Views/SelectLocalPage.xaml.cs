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
            await CargarLocalesAsync();
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
    }
}
