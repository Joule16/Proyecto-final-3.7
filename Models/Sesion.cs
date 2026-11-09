//NUÑEZ Martinez Marco Antonio
//SUÑIGA Maciel Joule Alexander
//VILLA Olivarez Ariel
//Equipo 2
//=============================
using System;
using SistemaBiblioteca1.Models;

namespace SistemaBiblioteca1
{
    public static class Sesion
    {
        public static Usuario? UsuarioActual { get; private set; }

        public static bool HaySesion
        {
            get { return UsuarioActual != null || SesionSistema.HaySesion; }
        }

        public static bool EsAdministrador
        {
            get { return SesionSistema.RolActivo == UsuariosSistema.ROL_ADMINISTRADOR; }
        }

        public static void Iniciar(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario), "No hay usuario para iniciar sesión.");

            UsuarioActual = usuario;

            string rol = usuario.EsAdministrador()
                ? UsuariosSistema.ROL_ADMINISTRADOR
                : UsuariosSistema.ROL_OPERADOR;

            SesionSistema.Iniciar(usuario.Nombre + " " + usuario.Apellido, rol);
        }

        public static void Cerrar()
        {
            UsuarioActual = null;
            SesionSistema.Cerrar();
        }
    }
}