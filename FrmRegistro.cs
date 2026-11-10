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
    public partial class FrmRegistro : Form
    {
        private const bool CUENTA_ACTIVA_AL_REGISTRARSE = true;

        public string CorreoRegistrado { get; private set; } = string.Empty;

        public FrmRegistro()
        {
            InitializeComponent();

            TemaPersona3.Aplicar(this);

            lblMensajeRegistro.ForeColor = Color.FromArgb(255, 107, 107);
            lblMensajeRegistro.Text = string.Empty;

            foreach (TextBox caja in new TextBox[] { txtNombre, txtApellido, txtCorreo, txtTelefono,
                                                     txtIdUniversitario, txtContrasenaReg, txtConfirmar })
            {
                caja.TextChanged += (s, e) => lblMensajeRegistro.Text = string.Empty;
            }

            txtTelefono.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            };
        }

        private void btnCrearCuenta_Click(object? sender, EventArgs e)
        {
            try
            {
                if (txtContrasenaReg.Text != txtConfirmar.Text)
                    throw new ArgumentException("Las contraseñas no coinciden.");

                Usuario usuario = new Usuario(SiguienteId(), txtNombre.Text, txtApellido.Text,
                                              txtCorreo.Text, txtTelefono.Text, txtIdUniversitario.Text,
                                              CUENTA_ACTIVA_AL_REGISTRARSE, RolUsuario.Usuario);

                if (ExisteCorreo(usuario.Email))
                    throw new InvalidOperationException("Ya existe una cuenta con ese correo.");

                usuario.EstablecerContrasena(txtContrasenaReg.Text);
                usuario.InsertarRegistro(usuario);

                CorreoRegistrado = usuario.Email;
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                lblMensajeRegistro.Text = ex.Message;
            }
        }

        private static int SiguienteId()
        {
            int mayor = 0;
            foreach (Usuario u in Usuario.ObtenerTodos())
            {
                if (u.Id > mayor)
                    mayor = u.Id;
            }
            return mayor + 1;
        }

        private static bool ExisteCorreo(string correo)
        {
            foreach (Usuario u in Usuario.ObtenerTodos())
            {
                if (string.Equals(u.Email, correo, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}