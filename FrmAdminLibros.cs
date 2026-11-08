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
    public partial class FrmAdminLibros : Form
    {
        private bool acomodoListo = false;

        public FrmAdminLibros()
        {
            InitializeComponent();
            ConfigurarColumnas();

            foreach (Control c in new Control[] { dgvLibros, lblResumenLibros, lblMensajeLibros,
                lblBuscarLibro, txtBuscarLibro, btnDarDeBajaLibro, btnRestaurarLibro, btnRecargarLibros })
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

            int anchoBuscador = Math.Max(120, Math.Min(254, ancho / 3));
            txtBuscarLibro.SetBounds(ancho - margen - anchoBuscador, 14, anchoBuscador, txtBuscarLibro.Height);
            lblBuscarLibro.Location = new Point(
                txtBuscarLibro.Left - separacion - lblBuscarLibro.Width,
                txtBuscarLibro.Top + (txtBuscarLibro.Height - lblBuscarLibro.Height) / 2);

            int anchoBoton = (ancho - 2 * margen - 2 * separacion) / 3;
            anchoBoton = Math.Max(90, Math.Min(170, anchoBoton));
            int yBotones = alto - margen - altoBoton;

            Button[] botones = { btnDarDeBajaLibro, btnRestaurarLibro, btnRecargarLibros };
            for (int i = 0; i < botones.Length; i++)
            {
                botones[i].SetBounds(margen + i * (anchoBoton + separacion), yBotones, anchoBoton, altoBoton);
            }

            int yMensaje = yBotones - 6 - lblMensajeLibros.Height;
            lblMensajeLibros.SetBounds(margen, yMensaje, ancho - 2 * margen, lblMensajeLibros.Height);
            lblResumenLibros.Location = new Point(margen, yMensaje - 2 - lblResumenLibros.Height);

            int yTabla = 56;
            int altoTabla = lblResumenLibros.Top - 6 - yTabla;
            dgvLibros.SetBounds(margen, yTabla, ancho - 2 * margen, Math.Max(80, altoTabla));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            EstilizarTabla();
            txtBuscarLibro.Font = new Font("Segoe UI", 10F);
            CargarLibros(null);
            MostrarMensaje("Selecciona un libro y elige una acción.", false);

            acomodoListo = true;
            AcomodarControles();
        }

        private void ConfigurarColumnas()
        {
            dgvLibros.Columns.Add("colId", "ID");
            dgvLibros.Columns.Add("colTitulo", "Título");
            dgvLibros.Columns.Add("colAutor", "Autor");
            dgvLibros.Columns.Add("colCategoria", "Categoría");
            dgvLibros.Columns.Add("colIsbn", "ISBN");
            dgvLibros.Columns.Add("colAnio", "Año");
            dgvLibros.Columns.Add("colPuntoEntrega", "Punto de entrega");
            dgvLibros.Columns.Add("colEstado", "Estado");
        }

        private void EstilizarTabla()
        {
            Color fondo = Color.FromArgb(10, 17, 40);
            Color celda = Color.FromArgb(14, 28, 64);
            Color cian = Color.FromArgb(0, 212, 255);

            dgvLibros.BackgroundColor = fondo;
            dgvLibros.GridColor = Color.FromArgb(0, 110, 150);
            dgvLibros.BorderStyle = BorderStyle.None;
            dgvLibros.EnableHeadersVisualStyles = false;

            dgvLibros.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 80, 120);
            dgvLibros.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLibros.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 80, 120);
            dgvLibros.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            dgvLibros.DefaultCellStyle.BackColor = celda;
            dgvLibros.DefaultCellStyle.ForeColor = Color.White;
            dgvLibros.DefaultCellStyle.SelectionBackColor = cian;
            dgvLibros.DefaultCellStyle.SelectionForeColor = fondo;

            dgvLibros.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvLibros.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvLibros.RowTemplate.Height = 28;
        }

        private static string NombreAutor(int idAutor)
        {
            Autor? autor = new Autor().ConsultarRegistro(idAutor.ToString()) as Autor;
            return autor == null ? "(autor #" + idAutor + ")" : autor.Nombre + " " + autor.Apellido;
        }

        private static string NombreCategoria(int idCategoria)
        {
            Categoria? categoria = new Categoria().ConsultarRegistro(idCategoria.ToString()) as Categoria;
            return categoria == null ? "(categoría #" + idCategoria + ")" : categoria.Nombre;
        }

        private int? IdSeleccionadoActual()
        {
            if (dgvLibros.SelectedRows.Count == 0)
                return null;

            return (int)dgvLibros.SelectedRows[0].Cells["colId"].Value;
        }

        private void CargarLibros(int? idSeleccionado)
        {
            dgvLibros.Rows.Clear();

            List<Libro> libros = Libro.ObtenerTodos();
            string filtro = txtBuscarLibro.Text.Trim();

            Dictionary<int, string> autores = new Dictionary<int, string>();
            Dictionary<int, string> categorias = new Dictionary<int, string>();

            int disponibles = 0;
            int mostrados = 0;

            foreach (Libro libro in libros)
            {
                if (libro.EsActivo)
                    disponibles++;

                if (!autores.ContainsKey(libro.IdAutor))
                    autores[libro.IdAutor] = NombreAutor(libro.IdAutor);
                if (!categorias.ContainsKey(libro.IdCategoria))
                    categorias[libro.IdCategoria] = NombreCategoria(libro.IdCategoria);

                string autor = autores[libro.IdAutor];
                string categoria = categorias[libro.IdCategoria];

                if (filtro.Length > 0 &&
                    libro.Titulo.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) < 0 &&
                    libro.Isbn.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) < 0 &&
                    autor.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) < 0 &&
                    libro.PuntoEntrega.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    continue;
                }

                int fila = dgvLibros.Rows.Add(
                    libro.Id,
                    libro.Titulo,
                    autor,
                    categoria,
                    libro.Isbn,
                    libro.AnioPublicacion,
                    libro.PuntoEntrega.Length > 0 ? libro.PuntoEntrega : "(ciudad del dueño)",
                    libro.EsActivo ? "Disponible" : "Dado de baja");

                if (!libro.EsActivo)
                    dgvLibros.Rows[fila].DefaultCellStyle.ForeColor = Color.FromArgb(170, 170, 170);

                mostrados++;
            }

            string resumen = "Total: " + libros.Count + "   |   Disponibles: " + disponibles +
                             "   |   Dados de baja: " + (libros.Count - disponibles);
            if (filtro.Length > 0)
                resumen += "   |   Mostrando: " + mostrados;
            lblResumenLibros.Text = resumen;

            AjustarColumnas();

            dgvLibros.ClearSelection();

            if (idSeleccionado.HasValue)
            {
                foreach (DataGridViewRow fila in dgvLibros.Rows)
                {
                    if ((int)fila.Cells["colId"].Value == idSeleccionado.Value)
                    {
                        fila.Selected = true;
                        dgvLibros.CurrentCell = fila.Cells["colTitulo"];
                        break;
                    }
                }
            }
        }

        private void AjustarColumnas()
        {
            const int relleno = 14;

            dgvLibros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            foreach (DataGridViewColumn col in dgvLibros.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.MinimumWidth = 20;
            }

            dgvLibros.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            foreach (DataGridViewColumn col in dgvLibros.Columns)
                col.Width = col.Width + relleno;

            DataGridViewColumn ultima = dgvLibros.Columns[dgvLibros.Columns.Count - 1];
            ultima.MinimumWidth = ultima.Width;
            ultima.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            foreach (DataGridViewRow fila in dgvLibros.Rows)
            {
                foreach (DataGridViewCell celda in fila.Cells)
                    celda.ToolTipText = Convert.ToString(celda.Value) ?? string.Empty;
            }
        }

        private Libro? ObtenerSeleccionado()
        {
            int? id = IdSeleccionadoActual();
            if (!id.HasValue)
            {
                MostrarMensaje("Selecciona un libro de la tabla.", true);
                return null;
            }

            return new Libro().ConsultarRegistro(id.Value.ToString()) as Libro;
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblMensajeLibros.ForeColor = esError ? Color.FromArgb(255, 107, 107) : Color.FromArgb(0, 212, 255);
            lblMensajeLibros.Text = mensaje;
        }

        private void txtBuscarLibro_TextChanged(object? sender, EventArgs e)
        {
            CargarLibros(IdSeleccionadoActual());
        }

        private void btnDarDeBajaLibro_Click(object? sender, EventArgs e)
        {
            Libro? libro = ObtenerSeleccionado();
            if (libro == null)
                return;

            if (!libro.EsActivo)
            {
                MostrarMensaje("Ese libro ya está dado de baja.", true);
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Dar de baja el libro '" + libro.Titulo + "'?\n\n" +
                "Dejará de aparecer como disponible en el catálogo, pero su registro se conserva y puedes restaurarlo.",
                "Dar de baja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                libro.DarDeBaja();
                CargarLibros(libro.Id);
                MostrarMensaje("El libro '" + libro.Titulo + "' quedó dado de baja.", false);
            }
            catch (InvalidOperationException ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        private void btnRestaurarLibro_Click(object? sender, EventArgs e)
        {
            Libro? libro = ObtenerSeleccionado();
            if (libro == null)
                return;

            try
            {
                libro.Restaurar();
                CargarLibros(libro.Id);
                MostrarMensaje("El libro '" + libro.Titulo + "' volvió al catálogo.", false);
            }
            catch (InvalidOperationException ex)
            {
                MostrarMensaje(ex.Message, true);
            }
        }

        private void btnRecargarLibros_Click(object? sender, EventArgs e)
        {
            CargarLibros(IdSeleccionadoActual());
            MostrarMensaje("Lista actualizada.", false);
        }
    }
}