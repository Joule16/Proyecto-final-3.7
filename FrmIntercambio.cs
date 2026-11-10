//Suñiga Maciel Joule Alexander
//Villa Olivares Ariel
//Nuñez Martinez Marco Antonio
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
    public partial class FrmIntercambio : FrmBase, IPanelCRUD
    {
        public FrmIntercambio()
        {
            InitializeComponent();

            ConfigurarCampoEntero(txtIdUsuarioIntercambio, "ID solicitante");
            ConfigurarCampoEntero(txtIdLibroSolicitadoIntercambio, "ID libro solicitado");
            ConfigurarCampoLista(cmbEstatusIntercambio, "Estatus");

            txtIdLibroOfrecidoIntercambio.KeyPress += SoloDigitos_KeyPress;
            txtIdLibroOfrecidoIntercambio.TextChanged += (s, e) => epValidaciones.SetError(txtIdLibroOfrecidoIntercambio, "");

            chkDonacionIntercambio.CheckedChanged += chkDonacionIntercambio_CheckedChanged;
            cmbEstatusIntercambio.SelectedIndexChanged += cmbEstatusIntercambio_SelectedIndexChanged;

            cmbEstatusIntercambio.SelectedIndex = (int)EstatusIntercambio.Pendiente;
        }

        public void EjecutarGuardar()
        {
            if (!ValidarFormulario())
                return;

            if (!ValidarLibroOfrecido())
                return;

            try
            {
                Intercambio intercambio = CrearIntercambioDesdeControles();
                intercambio.InsertarRegistro(intercambio);

                MessageBox.Show("Intercambio guardado correctamente.", "Guardar",
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
                Intercambio? intercambio = new Intercambio().ConsultarRegistro(id) as Intercambio;

                if (intercambio == null)
                {
                    alerta.SetError(txtId, "No existe un intercambio con ese id.");
                    return;
                }

                txtId.Text = intercambio.Id.ToString();
                txtIdUsuarioIntercambio.Text = intercambio.IdUsuarioSolicitante.ToString();
                txtIdLibroSolicitadoIntercambio.Text = intercambio.IdLibroSolicitado.ToString();

                chkDonacionIntercambio.Checked = intercambio.EsDonacion;
                txtIdLibroOfrecidoIntercambio.Text = intercambio.IdLibroOfrecido.HasValue
                    ? intercambio.IdLibroOfrecido.Value.ToString()
                    : string.Empty;

                dtpFechaSolicitudIntercambio.Value = intercambio.FechaSolicitud.Date;
                cmbEstatusIntercambio.SelectedIndex = (int)intercambio.Estatus;

                if (intercambio.FechaConclusion.HasValue)
                {
                    dtpFechaConclusionIntercambio.Value = intercambio.FechaConclusion.Value.Date;
                    dtpFechaConclusionIntercambio.Checked = true;
                }
                else
                {
                    dtpFechaConclusionIntercambio.Checked = false;
                }

                txtPuntoEncuentroIntercambio.Text = intercambio.PuntoEncuentro;
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

            if (!ValidarLibroOfrecido())
                return;

            try
            {
                Intercambio intercambio = CrearIntercambioDesdeControles();

                Intercambio? existente = new Intercambio().ConsultarRegistro(txtId.Text) as Intercambio;
                if (existente != null)
                    intercambio.FechaRegistro = existente.FechaRegistro;

                intercambio.ActualizarRegistro(intercambio);

                MessageBox.Show("Intercambio actualizado correctamente.", "Actualizar",
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
                new Intercambio().EliminarRegistro(txtId.Text);
                LimpiarControles();
                MostrarEstado(barraEstado, "Intercambio eliminado correctamente.");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                MostrarEstado(barraEstado, ex.Message);
            }
        }

        private Intercambio CrearIntercambioDesdeControles()
        {
            int id = LeerEntero(txtId, "El id");
            int idSolicitante = LeerEntero(txtIdUsuarioIntercambio, "El ID solicitante");
            int idSolicitado = LeerEntero(txtIdLibroSolicitadoIntercambio, "El ID del libro solicitado");

            int? idOfrecido = null;
            if (!chkDonacionIntercambio.Checked)
                idOfrecido = LeerEntero(txtIdLibroOfrecidoIntercambio, "El ID del libro ofrecido");

            DateTime? fechaConclusion = null;
            if (dtpFechaConclusionIntercambio.Checked)
                fechaConclusion = dtpFechaConclusionIntercambio.Value.Date;

            return new Intercambio(id, idSolicitante, idSolicitado, idOfrecido,
                                   dtpFechaSolicitudIntercambio.Value.Date,
                                   (EstatusIntercambio)cmbEstatusIntercambio.SelectedIndex,
                                   fechaConclusion,
                                   txtPuntoEncuentroIntercambio.Text);
        }

        private static int LeerEntero(TextBox txt, string nombre)
        {
            if (!int.TryParse(txt.Text.Trim(), out int valor))
                throw new FormatException(nombre + " debe ser un número entero.");
            return valor;
        }

        private bool ValidarLibroOfrecido()
        {
            if (chkDonacionIntercambio.Checked)
            {
                epValidaciones.SetError(txtIdLibroOfrecidoIntercambio, "");
                return true;
            }

            string texto = txtIdLibroOfrecidoIntercambio.Text.Trim();
            string? mensaje = null;

            if (texto.Length == 0)
                mensaje = "Escribe el ID del libro ofrecido o marca la casilla de donación.";
            else if (!int.TryParse(texto, out int idOfrecido) || idOfrecido <= 0)
                mensaje = "El ID del libro ofrecido debe ser un número entero mayor que cero.";

            if (mensaje != null)
                return MostrarErrorCampo(txtIdLibroOfrecidoIntercambio, mensaje);

            epValidaciones.SetError(txtIdLibroOfrecidoIntercambio, "");
            return true;
        }

        private void chkDonacionIntercambio_CheckedChanged(object? sender, EventArgs e)
        {
            bool esDonacion = chkDonacionIntercambio.Checked;

            txtIdLibroOfrecidoIntercambio.Enabled = !esDonacion;

            if (esDonacion)
            {
                txtIdLibroOfrecidoIntercambio.Clear();
                epValidaciones.SetError(txtIdLibroOfrecidoIntercambio, "");
            }
        }

        private void cmbEstatusIntercambio_SelectedIndexChanged(object? sender, EventArgs e)
        {
            dtpFechaConclusionIntercambio.Checked =
                cmbEstatusIntercambio.SelectedIndex == (int)EstatusIntercambio.Concluido;
        }

        private void SoloDigitos_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void LimpiarControles()
        {
            LimpiarErrores();
            txtId.Clear();
            txtIdUsuarioIntercambio.Clear();
            txtIdLibroSolicitadoIntercambio.Clear();
            chkDonacionIntercambio.Checked = false;
            txtIdLibroOfrecidoIntercambio.Clear();
            dtpFechaSolicitudIntercambio.Value = DateTime.Today;
            cmbEstatusIntercambio.SelectedIndex = (int)EstatusIntercambio.Pendiente;
            dtpFechaConclusionIntercambio.Checked = false;
            txtPuntoEncuentroIntercambio.Clear();
        }

        private void MostrarEstado(StatusStrip barraEstado, string mensaje)
        {
            if (barraEstado.Items.Count > 0)
                barraEstado.Items[0].Text = mensaje;
        }
    }
}