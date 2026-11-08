//   Equipo #2
//   NUÑEZ Martinez Marco Antonio
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
// ===============================
using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaBiblioteca1.Models;

namespace SistemaBiblioteca1
{
    public partial class FrmLogin : Form
    {
        private static readonly Color ColorError = Color.FromArgb(255, 107, 107);
        private static readonly Color ColorExito = Color.FromArgb(0, 212, 255);

        public FrmLogin()
        {
            InitializeComponent();

            TemaPersona3.Aplicar(this);

            lblMensajeLogin.ForeColor = ColorError;
            lblMensajeLogin.Text = string.Empty;

            txtUsuario.TextChanged += (s, e) => lblMensajeLogin.Text = string.Empty;
            txtContrasena.TextChanged += (s, e) => lblMensajeLogin.Text = string.Empty;
        }

        private void btnIngresar_Click(object? sender, EventArgs e)
        {
            string usuarioEscrito = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text;

            if (usuarioEscrito.Length == 0 || contrasena.Length == 0)
            {
                MostrarMensaje("Escribe tu usuario y tu contraseña.", true);
                return;
            }

            CuentaSistema? cuenta = UsuariosSistema.Validar(usuarioEscrito, contrasena);
            if (cuenta != null)
            {
                SesionSistema.Iniciar(cuenta.NombreCompleto, cuenta.Rol);
                DialogResult = DialogResult.OK;
                return;
            }

            if (UsuariosSistema.ExisteUsuario(usuarioEscrito))
            {
                MostrarError("Usuario o contraseña incorrectos.");
                return;
            }

            try
            {
                Usuario usuario = Usuario.Autenticar(usuarioEscrito, contrasena);
                Sesion.Iniciar(usuario);

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                MostrarError(ex.Message);
            }
        }

        private void btnRegistrarse_Click(object? sender, EventArgs e)
        {
            using (FrmRegistro registro = new FrmRegistro())
            {
                if (registro.ShowDialog(this) == DialogResult.OK)
                {
                    txtUsuario.Text = registro.CorreoRegistrado;
                    txtContrasena.Clear();
                    txtContrasena.Focus();

                    MostrarMensaje("Cuenta creada. Ahora escribe tu contraseña para entrar.", false);
                }
            }
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensajeLogin.ForeColor = esError ? ColorError : ColorExito;
            lblMensajeLogin.Text = mensaje;
        }

        private void MostrarError(string mensaje)
        {
            txtContrasena.Clear();
            txtContrasena.Focus();
            MostrarMensaje(mensaje, true);
        }
    }
}