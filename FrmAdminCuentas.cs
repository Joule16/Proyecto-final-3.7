//Suñiga Maciel Joule Alexander
//Villa Olivares Ariel
//Nuñez Martinez Marco Antonio
//Equipo 2
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SistemaBiblioteca1.Models;

namespace SistemaBiblioteca1
{
    public partial class FrmAdminCuentas : Form
    {
        private bool acomodoListo = false;

        public FrmAdminCuentas()
        {
            InitializeComponent();
            ConfigurarColumnas();

            foreach (Control c in new Control[] { dgvCuentas, lblResumenCuentas, lblMensajeCuentas,
                lblMotivoBaneo, txtMotivoBaneo, btnActivarCuenta, btnDesactivarCuenta,
                btnBanearCuenta, btnQuitarBaneoCuenta, btnRecargarCuentas })
            {
                c.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AcomodarControles();
        }

        private void AcomodarControles()
        {
            if (!acomodoListo)
                return;

            int ancho = ClientSize.Width;
            int alto = ClientSize.Height;
            if (ancho < 300 || alto < 300)
                return;

            const int margen = 16;
            const int separacion = 10;
            const int altoBoton = 34;

            int anchoBoton = (ancho - 2 * margen - 4 * separacion) / 5;
            anchoBoton = Math.Max(90, Math.Min(150, anchoBoton));
            int yBotones = alto - margen - altoBoton;

            Button[] botones =
            {
                btnActivarCuenta, btnDesactivarCuenta, btnBanearCuenta,
                btnQuitarBaneoCuenta, btnRecargarCuentas
            };

            for (int i = 0; i < botones.Length; i++)
            {
                botones[i].SetBounds(margen + i * (anchoBoton + separacion), yBotones, anchoBoton, altoBoton);
            }

            int yMotivo = yBotones - separacion - txtMotivoBaneo.Height;
            lblMotivoBaneo.Location = new Point(margen, yMotivo + (txtMotivoBaneo.Height - lblMotivoBaneo.Height) / 2);
            int xCampo = lblMotivoBaneo.Right + separacion;
            txtMotivoBaneo.SetBounds(xCampo, yMotivo, ancho - margen - xCampo, txtMotivoBaneo.Height);

            int yMensaje = yMotivo - 6 - lblMensajeCuentas.Height;
            lblMensajeCuentas.SetBounds(margen, yMensaje, ancho - 2 * margen, lblMensajeCuentas.Height);
            lblResumenCuentas.Location = new Point(margen, yMensaje - 2 - lblResumenCuentas.Height);

            int yTabla = 56;
            int altoTabla = lblResumenCuentas.Top - 6 - yTabla;
            dgvCuentas.SetBounds(margen, yTabla, ancho - 2 * margen, Math.Max(80, altoTabla));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            EstilitarTabla();
            txtMotivoBaneo.Font = new Font("Segoe UI", 10F);
            CargarCuentas(null);
            MostrarMensaje("Selecciona una cuenta y elige una acción.", false);

            acomodoListo = true;
            AcomodarControles();
        }

        private void ConfigurarColumnas()
        {
            dgvCuentas.Columns.Add("colId", "ID");
            dgvCuentas.Columns.Add("colNombre", "Nombre");
            dgvCuentas.Columns.Add("colCorreo", "Correo");
            dgvCuentas.Columns.Add("colRol", "Rol");
            dgvCuentas.Columns.Add("colEstado", "Estado");
            dgvCuentas.Columns.Add("colSanciones", "Sanciones");
            dgvCuentas.Columns.Add("colMotivo", "Motivo del baneo");
        }

        private void EstilitarTabla()
        {
            Color fondo = Color.FromArgb(10, 17, 40);
            Color celda = Color.FromArgb(14, 28, 64);
            Color cian = Color.FromArgb(0, 212, 255);

            dgvCuentas.BackgroundColor = fondo;
            dgvCuentas.GridColor = Color.FromArgb(0, 110, 150);
            dgvCuentas.BorderStyle = BorderStyle.None;
            dgvCuentas.EnableHeadersVisualStyles = false;

            dgvCuentas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 80, 120);
            dgvCuentas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCuentas.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 80, 120);
            dgvCuentas.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            dgvCuentas.DefaultCellStyle.BackColor = celda;
            dgvCuentas.DefaultCellStyle.ForeColor = Color.White;
            dgvCuentas.DefaultCellStyle.SelectionBackColor = cian;
            dgvCuentas.DefaultCellStyle.SelectionForeColor = fondo;

            dgvCuentas.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvCuentas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvCuentas.RowTemplate.Height = 28;
        }

        private void CargarCuentas(int? idSeleccionado)
        {
            dgvCuentas.Rows.Clear();

            List<Usuario> usuarios = Usuario.ObtenerTodos();
            int activas = 0;
            int desactivadas = 0;
            int baneadas = 0;

            foreach (Usuario u in usuarios)
            {
                int fila = dgvCuentas.Rows.Add(
                    u.Id,
                    u.Nombre + " " + u.Apellido,
                    u.Email,
                    u.Rol.ToString(),
                    TextoEstado(u),
                    u.ContadorSanciones,
                    u.EstaBaneado ? u.MotivoBaneo : string.Empty);

                if (u.EstaBaneado)
                    dgvCuentas.Rows[fila].DefaultCellStyle.ForeColor = Color.FromArgb(255, 140, 140);
                else if (!u.EsActivo)
                    dgvCuentas.Rows[fila].DefaultCellStyle.ForeColor = Color.FromArgb(170, 170, 170);

                if (u.EstaBaneado) baneadas++;
                else if (u.EsActivo) activas++;
                else desactivadas++;
            }

            lblResumenCuentas.Text = "Total: " + usuarios.Count + "   |   Activas: " + activas +
                                     "   |   Desactivadas: " + desactivadas +
                                     "   |   Baneadas: " + baneadas;

            AjustarColumnas();

            dgvCuentas.ClearSelection();

            if (idSeleccionado.HasValue)
            {
                foreach (DataGridViewRow fila in dgvCuentas.Rows)
                {
                    if ((int)fila.Cells["colId"].Value == idSeleccionado.Value)
                    {
                        fila.Selected = true;
                        dgvCuentas.CurrentCell = fila.Cells["colNombre"];
                        break;
                    }
                }
            }
        }

        private void AjustarColumnas()
        {
            const int relleno = 14;

            dgvCuentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            foreach (DataGridViewColumn col in dgvCuentas.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.MinimumWidth = 20;
            }

            dgvCuentas.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            foreach (DataGridViewColumn col in dgvCuentas.Columns)
                col.Width = col.Width + relleno;

            DataGridViewColumn ultima = dgvCuentas.Columns[dgvCuentas.Columns.Count - 1];
            ultima.MinimumWidth = ultima.Width;
            ultima.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            foreach (DataGridViewRow fila in dgvCuentas.Rows)
            {
                foreach (DataGridViewCell celda in fila.Cells)
                    celda.ToolTipText = Convert.ToString(celda.Value) ?? string.Empty;
            }
        }

        private static string TextoEstado(Usuario u)
        {
            if (u.EstaBaneado)
                return "Baneada";

            return u.EsActivo ? "Activa" : "Desactivada";
        }

        private Usuario? ObtenerSeleccionado()
        {
            if (dgvCuentas.SelectedRows.Count == 0)
            {
                MostrarMensaje("Selecciona una cuenta de la tabla.", true);
                return null;
            }

            int id = (int)dgvCuentas.SelectedRows[0].Cells["colId"].Value;
            return new Usuario().ConsultarRegistro(id.ToString()) as Usuario;
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensajeCuentas.ForeColor = esError ? Color.FromArgb(255, 107, 107) : Color.FromArgb(0, 212, 255);
            lblMensajeCuentas.Text = mensaje;
        }

        private void btnActivarCuenta_Click(object? sender, EventArgs e)
        {
            Usuario? usuario = ObtenerSeleccionado();
            if (usuario == null)
                return;

            try
            {
                usuario.Activar();
            }
            catch (InvalidOperationException ex)
            {
                MostrarMensaje(ex.Message, true);
                return;
            }

            CargarCuentas(usuario.Id);

            string mensaje = "La cuenta de " + usuario.Nombre + " " + usuario.Apellido + " quedó activa.";
            if (usuario.EstaBaneado)
                mensaje += " Sigue baneada: no podrá entrar hasta que le quites el baneo.";

            MostrarMensaje(mensaje, false);
        }

        private void btnDesactivarCuenta_Click(object? sender, EventArgs e)
        {
            Usuario? usuario = ObtenerSeleccionado();
            if (usuario == null)
                return;

            if (Sesion.UsuarioActual != null && usuario.Id == Sesion.UsuarioActual.Id)
            {
                MostrarMensaje("No puedes desactivar tu propia cuenta.", true);
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Desactivar la cuenta de " + usuario.Nombre + " " + usuario.Apellido + "?\n\n" +
                "No podrá iniciar sesión, pero su historial se conserva.",
                "Desactivar cuenta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                usuario.Desactivar();
            }
            catch (InvalidOperationException ex)
            {
                MostrarMensaje(ex.Message, true);
                return;
            }

            CargarCuentas(usuario.Id);
            MostrarMensaje("La cuenta de " + usuario.Nombre + " " + usuario.Apellido + " quedó desactivada.", false);
        }

        private void btnBanearCuenta_Click(object? sender, EventArgs e)
        {
            Usuario? usuario = ObtenerSeleccionado();
            if (usuario == null)
                return;

            if (Sesion.UsuarioActual != null && usuario.Id == Sesion.UsuarioActual.Id)
            {
                MostrarMensaje("No puedes banear tu propia cuenta.", true);
                return;
            }

            if (usuario.EstaBaneado)
            {
                MostrarMensaje("La cuenta de " + usuario.Nombre + " " + usuario.Apellido + " ya está baneada.", true);
                return;
            }

            string motivo = txtMotivoBaneo.Text.Trim();
            if (motivo.Length == 0)
            {
                MostrarMensaje("Escribe el motivo del baneo antes de banear.", true);
                txtMotivoBaneo.Focus();
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Banear la cuenta de " + usuario.Nombre + " " + usuario.Apellido + "?\n\n" +
                "Motivo: " + motivo + "\n\n" +
                "Es una sanción: no podrá iniciar sesión hasta que le quites el baneo. Su historial se conserva.",
                "Banear cuenta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                usuario.Banear(motivo);
                txtMotivoBaneo.Clear();
                CargarCuentas(usuario.Id);
                MostrarMensaje("La cuenta de " + usuario.Nombre + " " + usuario.Apellido + " quedó baneada.", false);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        private void btnQuitarBaneoCuenta_Click(object? sender, EventArgs e)
        {
            Usuario? usuario = ObtenerSeleccionado();
            if (usuario == null)
                return;

            try
            {
                usuario.QuitarBaneo();
                CargarCuentas(usuario.Id);

                string mensaje = "Se quitó el baneo de " + usuario.Nombre + " " + usuario.Apellido +
                                 ". Sus sanciones se reiniciaron.";
                if (!usuario.EsActivo)
                    mensaje += " La cuenta sigue desactivada.";

                MostrarMensaje(mensaje, false);
            }
            catch (InvalidOperationException ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        private void btnRecargarCuentas_Click(object? sender, EventArgs e)
        {
            int? id = null;
            if (dgvCuentas.SelectedRows.Count > 0)
                id = (int)dgvCuentas.SelectedRows[0].Cells["colId"].Value;

            CargarCuentas(id);
            MostrarMensaje("Lista actualizada.", false);
        }
    }
}