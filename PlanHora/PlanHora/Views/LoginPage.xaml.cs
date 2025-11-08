using PlanHora.Models;
using PlanHora.Services;

namespace PlanHora.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly DatabaseService _db;

        public LoginPage()
        {
            InitializeComponent();
            _db = new DatabaseService();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var usuario = await _db.GetUsuarioAsync(UsernameEntry.Text, PasswordEntry.Text);

            if (usuario != null)
            {
                SessionService.IniciarSesion(usuario);
                await DisplayAlert("Bienvenido", $"Hola {usuario.NombreUsuario}!", "OK");
                await Shell.Current.GoToAsync("//SelectLocalPage");
                UsernameEntry.Text = string.Empty;
                PasswordEntry.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Error", "Usuario o contraseña incorrectos.", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }

        //funcion del boton usado para pruebas
        //private async void OnSkipLoginClicked(object sender, EventArgs e)
        //{
        //    // Entrada libre para pruebas
        //    await Shell.Current.GoToAsync("//SelectLocalPage");
        //    UsernameEntry.Text = string.Empty;
        //    PasswordEntry.Text = string.Empty;
        //}
    }
}
