using Microsoft.Maui.Controls;
using PlanHora.Services;
using PlanHora.Models;
using System;
using System.Threading.Tasks;

namespace PlanHora.Views
{
    public partial class RegisterPage : ContentPage
    {
        private readonly DatabaseService _db;

        public RegisterPage()
        {
            InitializeComponent();
            _db = new DatabaseService();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var usuario = UsuarioEntry.Text?.Trim();
            var contrasena = ContrasenaEntry.Text?.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                await DisplayAlert("Error", "Por favor completa todos los campos.", "OK");
                return;
            }

            var nuevoUsuario = new Usuario
            {
                NombreUsuario = usuario,
                Contrasena = contrasena
            };

            await _db.SaveUsuarioAsync(nuevoUsuario);

            await DisplayAlert("Éxito", "Usuario registrado correctamente.", "OK");
            await Shell.Current.GoToAsync("//LoginPage");

        }
    }
}
