using Microsoft.Maui.Controls;
using PlanHora.Models;
using PlanHora.Services;
using System;

namespace PlanHora.Views
{
    public partial class LocalFormPage : ContentPage
    {
        private readonly DatabaseService _db;
        private readonly Local _local;

        public LocalFormPage(Local? local = null)
        {
            InitializeComponent();
            _db = new DatabaseService();
            _local = local ?? new Local();

            if (local != null)
            {
                NombreEntry.Text = local.Nombre;
                AperturaEntry.Text = local.Apertura;
                CierreEntry.Text = local.Cierre;
            }
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NombreEntry.Text))
            {
                await DisplayAlert("Error", "El nombre es obligatorio.", "OK");
                return;
            }

            _local.Nombre = NombreEntry.Text.Trim();
            _local.Apertura = AperturaEntry.Text?.Trim() ?? "08:00";
            _local.Cierre = CierreEntry.Text?.Trim() ?? "22:00";

            await _db.SaveLocalAsync(_local);
            await DisplayAlert("Éxito", "Local guardado correctamente.", "OK");
            await Navigation.PopAsync();
        }
    }
}
