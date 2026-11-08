//   Equipo #2
//   NUÑEZ Martinez Marco Antonio
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
// ===============================
using System;
using System.Collections.Generic;

namespace SistemaBiblioteca1
{
    public class CuentaSistema
    {
        public string NombreUsuario { get; }
        public string Contrasena { get; }
        public string NombreCompleto { get; }
        public string Rol { get; }

        public CuentaSistema(string nombreUsuario, string contrasena, string nombreCompleto, string rol)
        {
            NombreUsuario = nombreUsuario;
            Contrasena = contrasena;
            NombreCompleto = nombreCompleto;
            Rol = rol;
        }
    }

    public static class UsuariosSistema
    {
        public const string ROL_ADMINISTRADOR = "Administrador";
        public const string ROL_OPERADOR = "Operador";
        public const string ROL_INVITADO = "Invitado";

        private static readonly List<CuentaSistema> cuentas = new List<CuentaSistema>
        {
            new CuentaSistema("admin",    "Admin123", "Administrador del Sistema", ROL_ADMINISTRADOR),
            new CuentaSistema("operador", "Oper123",  "Operador de Biblioteca",    ROL_OPERADOR),
            new CuentaSistema("invitado", "Invit123", "Invitado",                  ROL_INVITADO)
        };

        public static bool ExisteUsuario(string nombreUsuario)
        {
            foreach (CuentaSistema cuenta in cuentas)
            {
                if (string.Equals(cuenta.NombreUsuario, nombreUsuario, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public static CuentaSistema? Validar(string nombreUsuario, string contrasena)
        {
            foreach (CuentaSistema cuenta in cuentas)
            {
                if (string.Equals(cuenta.NombreUsuario, nombreUsuario, StringComparison.OrdinalIgnoreCase)
                    && cuenta.Contrasena == contrasena)
                {
                    return cuenta;
                }
            }
            return null;
        }
    }
}