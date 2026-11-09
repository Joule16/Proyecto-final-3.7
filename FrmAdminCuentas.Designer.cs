// Proyecto Biblioteca - Equipo #2
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
//   NUÑEZ Martinez Marco Antonio
// ===============================
namespace SistemaBiblioteca1
{
    partial class FrmAdminCuentas
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
            lblTituloCuentas = new Label();
            dgvCuentas = new DataGridView();
            lblResumenCuentas = new Label();
            lblMensajeCuentas = new Label();
            lblMotivoBaneo = new Label();
            txtMotivoBaneo = new TextBox();
            btnActivarCuenta = new BotonPersona3();
            btnDesactivarCuenta = new BotonPersona3();
            btnBanearCuenta = new BotonPersona3();
            btnQuitarBaneoCuenta = new BotonPersona3();
            btnRecargarCuentas = new BotonPersona3();
            ((System.ComponentModel.ISupportInitialize)dgvCuentas).BeginInit();
            SuspendLayout();

            lblTituloCuentas.AutoSize = true;
            lblTituloCuentas.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblTituloCuentas.Location = new Point(16, 12);
            lblTituloCuentas.Margin = new Padding(2, 0, 2, 0);
            lblTituloCuentas.Name = "lblTituloCuentas";
            lblTituloCuentas.Size = new Size(210, 32);
            lblTituloCuentas.TabIndex = 0;
            lblTituloCuentas.Text = "Cuentas de usuario";

            dgvCuentas.AllowUserToAddRows = false;
            dgvCuentas.AllowUserToDeleteRows = false;
            dgvCuentas.AllowUserToResizeRows = false;
            dgvCuentas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCuentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCuentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCuentas.Location = new Point(16, 56);
            dgvCuentas.Margin = new Padding(2, 2, 2, 2);
            dgvCuentas.MultiSelect = false;
            dgvCuentas.Name = "dgvCuentas";
            dgvCuentas.ReadOnly = true;
            dgvCuentas.RowHeadersVisible = false;
            dgvCuentas.RowHeadersWidth = 51;
            dgvCuentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCuentas.Size = new Size(828, 300);
            dgvCuentas.TabIndex = 1;

            lblResumenCuentas.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblResumenCuentas.AutoSize = true;
            lblResumenCuentas.Location = new Point(16, 366);
            lblResumenCuentas.Margin = new Padding(2, 0, 2, 0);
            lblResumenCuentas.Name = "lblResumenCuentas";
            lblResumenCuentas.Size = new Size(86, 20);
            lblResumenCuentas.TabIndex = 2;
            lblResumenCuentas.Text = "Total: 0";

            lblMensajeCuentas.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblMensajeCuentas.Location = new Point(16, 392);
            lblMensajeCuentas.Margin = new Padding(2, 0, 2, 0);
            lblMensajeCuentas.Name = "lblMensajeCuentas";
            lblMensajeCuentas.Size = new Size(828, 24);
            lblMensajeCuentas.TabIndex = 3;

            lblMotivoBaneo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblMotivoBaneo.AutoSize = true;
            lblMotivoBaneo.Location = new Point(16, 432);
            lblMotivoBaneo.Margin = new Padding(2, 0, 2, 0);
            lblMotivoBaneo.Name = "lblMotivoBaneo";
            lblMotivoBaneo.Size = new Size(131, 20);
            lblMotivoBaneo.TabIndex = 4;
            lblMotivoBaneo.Text = "Motivo del baneo:";

            txtMotivoBaneo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtMotivoBaneo.Location = new Point(160, 428);
            txtMotivoBaneo.Margin = new Padding(2, 2, 2, 2);
            txtMotivoBaneo.MaxLength = 200;
            txtMotivoBaneo.Name = "txtMotivoBaneo";
            txtMotivoBaneo.PlaceholderText = "Obligatorio para banear";
            txtMotivoBaneo.Size = new Size(684, 27);
            txtMotivoBaneo.TabIndex = 5;

            btnActivarCuenta.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnActivarCuenta.Location = new Point(16, 468);
            btnActivarCuenta.Margin = new Padding(2, 2, 2, 2);
            btnActivarCuenta.Name = "btnActivarCuenta";
            btnActivarCuenta.Size = new Size(150, 38);
            btnActivarCuenta.TabIndex = 6;
            btnActivarCuenta.Text = "Activar";
            btnActivarCuenta.UseVisualStyleBackColor = true;
            btnActivarCuenta.Click += btnActivarCuenta_Click;

            btnDesactivarCuenta.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDesactivarCuenta.Location = new Point(176, 468);
            btnDesactivarCuenta.Margin = new Padding(2, 2, 2, 2);
            btnDesactivarCuenta.Name = "btnDesactivarCuenta";
            btnDesactivarCuenta.Size = new Size(150, 38);
            btnDesactivarCuenta.TabIndex = 7;
            btnDesactivarCuenta.Text = "Desactivar";
            btnDesactivarCuenta.UseVisualStyleBackColor = true;
            btnDesactivarCuenta.Click += btnDesactivarCuenta_Click;

            btnBanearCuenta.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBanearCuenta.Location = new Point(336, 468);
            btnBanearCuenta.Margin = new Padding(2, 2, 2, 2);
            btnBanearCuenta.Name = "btnBanearCuenta";
            btnBanearCuenta.Size = new Size(150, 38);
            btnBanearCuenta.TabIndex = 8;
            btnBanearCuenta.Text = "Banear";
            btnBanearCuenta.UseVisualStyleBackColor = true;
            btnBanearCuenta.Click += btnBanearCuenta_Click;

            btnQuitarBaneoCuenta.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnQuitarBaneoCuenta.Location = new Point(496, 468);
            btnQuitarBaneoCuenta.Margin = new Padding(2, 2, 2, 2);
            btnQuitarBaneoCuenta.Name = "btnQuitarBaneoCuenta";
            btnQuitarBaneoCuenta.Size = new Size(160, 38);
            btnQuitarBaneoCuenta.TabIndex = 9;
            btnQuitarBaneoCuenta.Text = "Quitar baneo";
            btnQuitarBaneoCuenta.UseVisualStyleBackColor = true;
            btnQuitarBaneoCuenta.Click += btnQuitarBaneoCuenta_Click;

            btnRecargarCuentas.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRecargarCuentas.Location = new Point(666, 468);
            btnRecargarCuentas.Margin = new Padding(2, 2, 2, 2);
            btnRecargarCuentas.Name = "btnRecargarCuentas";
            btnRecargarCuentas.Size = new Size(170, 38);
            btnRecargarCuentas.TabIndex = 10;
            btnRecargarCuentas.Text = "Actualizar lista";
            btnRecargarCuentas.UseVisualStyleBackColor = true;
            btnRecargarCuentas.Click += btnRecargarCuentas_Click;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 524);
            Controls.Add(lblTituloCuentas);
            Controls.Add(dgvCuentas);
            Controls.Add(lblResumenCuentas);
            Controls.Add(lblMensajeCuentas);
            Controls.Add(lblMotivoBaneo);
            Controls.Add(txtMotivoBaneo);
            Controls.Add(btnActivarCuenta);
            Controls.Add(btnDesactivarCuenta);
            Controls.Add(btnBanearCuenta);
            Controls.Add(btnQuitarBaneoCuenta);
            Controls.Add(btnRecargarCuentas);
            Margin = new Padding(2, 2, 2, 2);
            Name = "FrmAdminCuentas";
            Text = "Administración - Cuentas de usuario";
            ((System.ComponentModel.ISupportInitialize)dgvCuentas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTituloCuentas;
        private DataGridView dgvCuentas;
        private Label lblResumenCuentas;
        private Label lblMensajeCuentas;
        private Label lblMotivoBaneo;
        private TextBox txtMotivoBaneo;
        private BotonPersona3 btnActivarCuenta;
        private BotonPersona3 btnDesactivarCuenta;
        private BotonPersona3 btnBanearCuenta;
        private BotonPersona3 btnQuitarBaneoCuenta;
        private BotonPersona3 btnRecargarCuentas;
    }
}