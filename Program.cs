//   Equipo 2
//   NUÑEZ Martinez Marco Antonio
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
// =============================

using SistemaBiblioteca1.Models;
using System.Text;

namespace SistemaBiblioteca1
{
    internal static class Program
    {
        private const string CORREO_ADMIN_INICIAL = "admin@gmail.com";
        private const string CONTRASENA_ADMIN_INICIAL = "Admin1234";

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try
            {
                CrearAdministradorInicial();
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                MessageBox.Show("No se pudo crear la cuenta de administrador inicial:\n" + ex.Message,
                                "Cuenta inicial", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (FrmLogin login = new FrmLogin())
            {
                if (login.ShowDialog() != DialogResult.OK)
                    return;
            }

            Application.Run(new FrmMaestro());
        }

        private static void CrearAdministradorInicial()
        {
            Usuario admin = new Usuario(1, "Administrador", "Sistema", CORREO_ADMIN_INICIAL,
                                        "3300000000", "ADMIN", true, RolUsuario.Administrador);
            admin.EstablecerContrasena(CONTRASENA_ADMIN_INICIAL);
            admin.InsertarRegistro(admin);
        }
    }
}