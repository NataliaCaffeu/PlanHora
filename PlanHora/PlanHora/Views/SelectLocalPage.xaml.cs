using Microsoft.Maui.Controls;
using PlanHora.Models;
using PlanHora.Services;
using System;
using System.Linq;

namespace PlanHora.Views
{
    public partial class SelectLocalPage : ContentPage
    {
        private readonly DatabaseService _db;

        public SelectLocalPage()
        {
            InitializeComponent();
            _db = new DatabaseService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LocalesCollection.ItemsSource = await _db.GetLocalesAsync();
        }

        private async void OnAddLocalClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LocalFormPage());
        }

        private async void OnLocalSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Local local)
            {
                await DisplayAlert("Seleccionado", $"Has elegido: {local.Nombre}", "OK");
            }
        }
    }
}
