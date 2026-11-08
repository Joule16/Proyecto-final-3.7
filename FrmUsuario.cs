//Suñiga Maciel Joule Alexander
//Villa Olivares Ariel
//Martinez Marco Antonio
//Equipo 2
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SistemaBiblioteca1.Models;

namespace SistemaBiblioteca1
{
    public partial class FrmUsuario : FrmBase, IPanelCRUD
    {
        public FrmUsuario()
        {
            InitializeComponent();

            ConfigurarCampoLetras(txtNombreUsuario, "Nombre");
            ConfigurarCampoLetras(txtApellidoUsuario, "Apellido");
            ConfigurarCampoTexto(txtEmailUsuario, "Correo electrónico", t => (!t.Contains('@') || !t.Contains('.')) ? "El correo debe contener '@' y un punto." : null);
            ConfigurarCampoEntero(txtTelefonoUsuario, "Teléfono", t => t.Length != 10 ? "El teléfono debe tener exactamente 10 dígitos." : null);
            ConfigurarCampoTexto(txtIdUniversitarioUsuario, "ID universitario");
            ConfigurarCampoLista(cmbRolUsuario, "Rol");

            txtContrasenaUsuario.TextChanged += (s, e) => epValidaciones.SetError(txtContrasenaUsuario, "");
        }

        public void EjecutarGuardar()
        {
            if (!ValidarFormulario())
                return;

            try
            {
                if (!ValidarContrasena(true))
                    return;

                Usuario usuario = CrearUsuarioDesdeControles();
                usuario.EstablecerContrasena(txtContrasenaUsuario.Text);
                usuario.InsertarRegistro(usuario);

                MessageBox.Show("Usuario guardado correctamente.", "Guardar",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarControles();
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException || ex is FormatException)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void EjecutarBuscar(string id, ErrorProvider alerta)
        {
            alerta.SetError(txtId, string.Empty);

            try
            {
                Usuario usuario = new Usuario().ConsultarRegistro(id) as Usuario;

                if (usuario == null)
                {
                    alerta.SetError(txtId, "No existe un usuario con ese id.");
                    return;
                }

                txtId.Text = usuario.Id.ToString();
                txtNombreUsuario.Text = usuario.Nombre;
                txtApellidoUsuario.Text = usuario.Apellido;
                txtEmailUsuario.Text = usuario.Email;
                txtTelefonoUsuario.Text = usuario.Telefono;
                txtIdUniversitarioUsuario.Text = usuario.IdUniversitario;
                cmbRolUsuario.SelectedIndex = (int)usuario.Rol;
                chkEstadoUsuario.Checked = usuario.EsActivo;

                txtContrasenaUsuario.Clear();
            }
            catch (ArgumentException ex)
            {
                alerta.SetError(txtId, ex.Message);
            }
        }

        public void EjecutarActualizar()
        {
            if (!ValidarFormulario())
                return;

            try
            {
                if (!ValidarContrasena(false))
                    return;

                Usuario usuario = CrearUsuarioDesdeControles();

                Usuario existente = new Usuario().ConsultarRegistro(txtId.Text) as Usuario;
                if (existente != null)
                {
                    usuario.ContadorSanciones = existente.ContadorSanciones;
                    usuario.EstaBaneado = existente.EstaBaneado;
                    usuario.FechaRegistro = existente.FechaRegistro;
                    usuario.ContrasenaHash = existente.ContrasenaHash;
                }

                if (txtContrasenaUsuario.Text.Length > 0)
                    usuario.EstablecerContrasena(txtContrasenaUsuario.Text);

                usuario.ActualizarRegistro(usuario);

                MessageBox.Show("Usuario actualizado correctamente.", "Actualizar",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException || ex is FormatException)
            {
                MessageBox.Show(ex.Message, "No se pudo actualizar",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void EjecutarEliminar(StatusStrip barraEstado)
        {
            if (!ValidarIdObligatorio())
            {
                MostrarEstado(barraEstado, "Escribe o busca el ID del registro que deseas eliminar.");
                return;
            }

            try
            {
                new Usuario().EliminarRegistro(txtId.Text);
                LimpiarControles();
                MostrarEstado(barraEstado, "Usuario eliminado correctamente.");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                MostrarEstado(barraEstado, ex.Message);
            }
        }

        private Usuario CrearUsuarioDesdeControles()
        {
            if (!int.TryParse(txtId.Text, out int id))
                throw new FormatException("El id debe ser un número entero.");

            return new Usuario(id, txtNombreUsuario.Text, txtApellidoUsuario.Text, txtEmailUsuario.Text,
                               txtTelefonoUsuario.Text, txtIdUniversitarioUsuario.Text, chkEstadoUsuario.Checked,
                               (RolUsuario)cmbRolUsuario.SelectedIndex);
        }

        private void LimpiarControles()
        {
            LimpiarErrores();
            txtId.Clear();
            txtNombreUsuario.Clear();
            txtApellidoUsuario.Clear();
            txtEmailUsuario.Clear();
            txtTelefonoUsuario.Clear();
            txtIdUniversitarioUsuario.Clear();
            txtContrasenaUsuario.Clear();
            cmbRolUsuario.SelectedIndex = -1;
            chkEstadoUsuario.Checked = false;
        }

        private bool ValidarContrasena(bool obligatoria)
        {
            string texto = txtContrasenaUsuario.Text;
            string? mensaje = null;

            if (texto.Length == 0)
            {
                if (obligatoria)
                    mensaje = "La contraseña es obligatoria.";
            }
            else
            {
                try
                {
                    Usuario.ValidarFormatoContrasena(texto);
                }
                catch (ArgumentException ex)
                {
                    mensaje = ex.Message;
                }
            }

            if (mensaje != null)
                return MostrarErrorCampo(txtContrasenaUsuario, mensaje);

            epValidaciones.SetError(txtContrasenaUsuario, "");
            return true;
        }

        private void MostrarEstado(StatusStrip barraEstado, string mensaje)
        {
            if (barraEstado.Items.Count > 0)
                barraEstado.Items[0].Text = mensaje;
        }

        private void lblTelefonoUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}