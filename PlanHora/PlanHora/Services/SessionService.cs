using PlanHora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanHora.Services
{
    internal class SessionService
    {
        public static Usuario? UsuarioActual { get; private set; }

        public static void IniciarSesion(Usuario usuario)
        {
            UsuarioActual = usuario;
        }

        public static void CerrarSesion()
        {
            UsuarioActual = null;
        }

        public static bool HaySesionActiva => UsuarioActual != null;
    }
}
