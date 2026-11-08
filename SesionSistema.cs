//   Equipo #2
//   NUÑEZ Martinez Marco Antonio
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
// ===============================
using System;

namespace SistemaBiblioteca1
{
    public static class SesionSistema
    {
        public static string UsuarioActivo { get; private set; } = string.Empty;
        public static string RolActivo { get; private set; } = string.Empty;
        public static DateTime HoraInicioSesion { get; private set; }

        public static bool HaySesion
        {
            get { return UsuarioActivo.Length > 0; }
        }

        public static void Iniciar(string usuarioActivo, string rolActivo)
        {
            if (string.IsNullOrWhiteSpace(usuarioActivo) || string.IsNullOrWhiteSpace(rolActivo))
                throw new ArgumentException("La sesión necesita un usuario y un rol.");

            UsuarioActivo = usuarioActivo;
            RolActivo = rolActivo;
            HoraInicioSesion = DateTime.Now;
        }

        public static void Cerrar()
        {
            UsuarioActivo = string.Empty;
            RolActivo = string.Empty;
            HoraInicioSesion = default;
        }
    }
}