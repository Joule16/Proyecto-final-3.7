//   Equipo #2
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
//   NUÑEZ Martinez Marco Antonio
// ===============================
namespace SistemaBiblioteca1
{
    partial class FrmAdminLibros
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTituloLibros = new Label();
            lblBuscarLibro = new Label();
            txtBuscarLibro = new TextBox();
            dgvLibros = new DataGridView();
            lblResumenLibros = new Label();
            lblMensajeLibros = new Label();
            btnDarDeBajaLibro = new BotonPersona3();
            btnRestaurarLibro = new BotonPersona3();
            btnRecargarLibros = new BotonPersona3();
            ((System.ComponentModel.ISupportInitialize)dgvLibros).BeginInit();
            SuspendLayout();

            lblTituloLibros.AutoSize = true;
            lblTituloLibros.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblTituloLibros.Location = new Point(16, 12);
            lblTituloLibros.Margin = new Padding(2, 0, 2, 0);
            lblTituloLibros.Name = "lblTituloLibros";
            lblTituloLibros.Size = new Size(245, 32);
            lblTituloLibros.TabIndex = 0;
            lblTituloLibros.Text = "Catálogo general de libros";

            lblBuscarLibro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblBuscarLibro.AutoSize = true;
            lblBuscarLibro.Location = new Point(520, 18);
            lblBuscarLibro.Margin = new Padding(2, 0, 2, 0);
            lblBuscarLibro.Name = "lblBuscarLibro";
            lblBuscarLibro.Size = new Size(63, 20);
            lblBuscarLibro.TabIndex = 1;
            lblBuscarLibro.Text = "Buscar:";

            txtBuscarLibro.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscarLibro.Location = new Point(590, 14);
            txtBuscarLibro.Margin = new Padding(2, 2, 2, 2);
            txtBuscarLibro.Name = "txtBuscarLibro";
            txtBuscarLibro.Size = new Size(254, 27);
            txtBuscarLibro.TabIndex = 2;
            txtBuscarLibro.TextChanged += txtBuscarLibro_TextChanged;

            dgvLibros.AllowUserToAddRows = false;
            dgvLibros.AllowUserToDeleteRows = false;
            dgvLibros.AllowUserToResizeRows = false;
            dgvLibros.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvLibros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLibros.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLibros.Location = new Point(16, 56);
            dgvLibros.Margin = new Padding(2, 2, 2, 2);
            dgvLibros.MultiSelect = false;
            dgvLibros.Name = "dgvLibros";
            dgvLibros.ReadOnly = true;
            dgvLibros.RowHeadersVisible = false;
            dgvLibros.RowHeadersWidth = 51;
            dgvLibros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLibros.Size = new Size(828, 300);
            dgvLibros.TabIndex = 3;

            lblResumenLibros.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblResumenLibros.AutoSize = true;
            lblResumenLibros.Location = new Point(16, 366);
            lblResumenLibros.Margin = new Padding(2, 0, 2, 0);
            lblResumenLibros.Name = "lblResumenLibros";
            lblResumenLibros.Size = new Size(86, 20);
            lblResumenLibros.TabIndex = 4;
            lblResumenLibros.Text = "Total: 0";

            lblMensajeLibros.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblMensajeLibros.Location = new Point(16, 392);
            lblMensajeLibros.Margin = new Padding(2, 0, 2, 0);
            lblMensajeLibros.Name = "lblMensajeLibros";
            lblMensajeLibros.Size = new Size(828, 24);
            lblMensajeLibros.TabIndex = 5;

            btnDarDeBajaLibro.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDarDeBajaLibro.Location = new Point(16, 424);
            btnDarDeBajaLibro.Margin = new Padding(2, 2, 2, 2);
            btnDarDeBajaLibro.Name = "btnDarDeBajaLibro";
            btnDarDeBajaLibro.Size = new Size(170, 38);
            btnDarDeBajaLibro.TabIndex = 6;
            btnDarDeBajaLibro.Text = "Dar de baja";
            btnDarDeBajaLibro.UseVisualStyleBackColor = true;
            btnDarDeBajaLibro.Click += btnDarDeBajaLibro_Click;

            btnRestaurarLibro.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRestaurarLibro.Location = new Point(202, 424);
            btnRestaurarLibro.Margin = new Padding(2, 2, 2, 2);
            btnRestaurarLibro.Name = "btnRestaurarLibro";
            btnRestaurarLibro.Size = new Size(170, 38);
            btnRestaurarLibro.TabIndex = 7;
            btnRestaurarLibro.Text = "Restaurar";
            btnRestaurarLibro.UseVisualStyleBackColor = true;
            btnRestaurarLibro.Click += btnRestaurarLibro_Click;

            btnRecargarLibros.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRecargarLibros.Location = new Point(388, 424);
            btnRecargarLibros.Margin = new Padding(2, 2, 2, 2);
            btnRecargarLibros.Name = "btnRecargarLibros";
            btnRecargarLibros.Size = new Size(170, 38);
            btnRecargarLibros.TabIndex = 8;
            btnRecargarLibros.Text = "Actualizar lista";
            btnRecargarLibros.UseVisualStyleBackColor = true;
            btnRecargarLibros.Click += btnRecargarLibros_Click;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 480);
            Controls.Add(lblTituloLibros);
            Controls.Add(lblBuscarLibro);
            Controls.Add(txtBuscarLibro);
            Controls.Add(dgvLibros);
            Controls.Add(lblResumenLibros);
            Controls.Add(lblMensajeLibros);
            Controls.Add(btnDarDeBajaLibro);
            Controls.Add(btnRestaurarLibro);
            Controls.Add(btnRecargarLibros);
            Margin = new Padding(2, 2, 2, 2);
            Name = "FrmAdminLibros";
            Text = "Administración - Catálogo de libros";
            ((System.ComponentModel.ISupportInitialize)dgvLibros).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTituloLibros;
        private Label lblBuscarLibro;
        private TextBox txtBuscarLibro;
        private DataGridView dgvLibros;
        private Label lblResumenLibros;
        private Label lblMensajeLibros;
        private BotonPersona3 btnDarDeBajaLibro;
        private BotonPersona3 btnRestaurarLibro;
        private BotonPersona3 btnRecargarLibros;
    }
}