//   Equipo 2
//   NUÑEZ Martinez Marco Antonio
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
// =============================

using SistemaBiblioteca1.Datos;
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

            if (!IniciarBaseDatos())
                return;

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

        private static bool IniciarBaseDatos()
        {
            if (!ConexionBD.Inicializar(out string mensaje))
            {
                DialogResult respuesta = MessageBox.Show(
                    mensaje + "\n\nSi continúas, el sistema trabajará SIN base de datos y la información se perderá al cerrar.\n\n¿Deseas continuar de todos modos?",
                    "Base de datos", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                return respuesta == DialogResult.Yes;
            }

            try
            {
                RepositorioBD.CargarTodo();
                return true;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                MessageBox.Show("Se conectó con MariaDB, pero no se pudieron cargar los datos:\n" + ex.Message +
                                "\n\nRevisa que la base de datos se haya creado con el script BibliotecaDB_MariaDB.sql.",
                                "Base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static void CrearAdministradorInicial()
        {
            int mayorId = 0;

            foreach (Usuario existente in Usuario.ObtenerTodos())
            {
                if (string.Equals(existente.Email, CORREO_ADMIN_INICIAL, StringComparison.OrdinalIgnoreCase))
                    return;

                if (existente.Id > mayorId)
                    mayorId = existente.Id;
            }

            Usuario admin = new Usuario(mayorId + 1, "Administrador", "Sistema", CORREO_ADMIN_INICIAL,
                                        "3300000000", "ADMIN", true, RolUsuario.Administrador);
            admin.EstablecerContrasena(CONTRASENA_ADMIN_INICIAL);
            admin.InsertarRegistro(admin);
        }
    }
}