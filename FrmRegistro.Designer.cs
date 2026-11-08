//   Equipo #2
//   NUÑEZ Martinez Marco Antonio
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
// ===============================
namespace SistemaBiblioteca1
{
    partial class FrmRegistro
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
            lblTituloRegistro = new Label();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblApellido = new Label();
            txtApellido = new TextBox();
            lblCorreo = new Label();
            txtCorreo = new TextBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            lblIdUniversitario = new Label();
            txtIdUniversitario = new TextBox();
            lblContrasenaReg = new Label();
            txtContrasenaReg = new TextBox();
            lblConfirmar = new Label();
            txtConfirmar = new TextBox();
            lblMensajeRegistro = new Label();
            btnCrearCuenta = new BotonPersona3();
            btnCancelarRegistro = new BotonPersona3();
            SuspendLayout();
            // 
            // lblTituloRegistro
            // 
            lblTituloRegistro.AutoSize = true;
            lblTituloRegistro.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            lblTituloRegistro.Location = new Point(28, 20);
            lblTituloRegistro.Margin = new Padding(2, 0, 2, 0);
            lblTituloRegistro.Name = "lblTituloRegistro";
            lblTituloRegistro.Size = new Size(150, 37);
            lblTituloRegistro.TabIndex = 0;
            lblTituloRegistro.Text = "Crear cuenta";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(30, 76);
            lblNombre.Margin = new Padding(2, 0, 2, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(72, 20);
            lblNombre.TabIndex = 1;
            lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(30, 102);
            txtNombre.Margin = new Padding(2, 2, 2, 2);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(170, 27);
            txtNombre.TabIndex = 2;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(220, 76);
            lblApellido.Margin = new Padding(2, 0, 2, 0);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(76, 20);
            lblApellido.TabIndex = 3;
            lblApellido.Text = "Apellido:";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(220, 102);
            txtApellido.Margin = new Padding(2, 2, 2, 2);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(170, 27);
            txtApellido.TabIndex = 4;
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Location = new Point(30, 146);
            lblCorreo.Margin = new Padding(2, 0, 2, 0);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(131, 20);
            lblCorreo.TabIndex = 5;
            lblCorreo.Text = "Correo electrónico:";
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(30, 172);
            txtCorreo.Margin = new Padding(2, 2, 2, 2);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(360, 27);
            txtCorreo.TabIndex = 6;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(30, 216);
            lblTelefono.Margin = new Padding(2, 0, 2, 0);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(154, 20);
            lblTelefono.TabIndex = 7;
            lblTelefono.Text = "Teléfono (10 dígitos):";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(30, 242);
            txtTelefono.Margin = new Padding(2, 2, 2, 2);
            txtTelefono.MaxLength = 10;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(170, 27);
            txtTelefono.TabIndex = 8;
            // 
            // lblIdUniversitario
            // 
            lblIdUniversitario.AutoSize = true;
            lblIdUniversitario.Location = new Point(220, 216);
            lblIdUniversitario.Margin = new Padding(2, 0, 2, 0);
            lblIdUniversitario.Name = "lblIdUniversitario";
            lblIdUniversitario.Size = new Size(129, 20);
            lblIdUniversitario.TabIndex = 9;
            lblIdUniversitario.Text = "ID universitario:";
            // 
            // txtIdUniversitario
            // 
            txtIdUniversitario.Location = new Point(220, 242);
            txtIdUniversitario.Margin = new Padding(2, 2, 2, 2);
            txtIdUniversitario.Name = "txtIdUniversitario";
            txtIdUniversitario.Size = new Size(170, 27);
            txtIdUniversitario.TabIndex = 10;
            // 
            // lblContrasenaReg
            // 
            lblContrasenaReg.AutoSize = true;
            lblContrasenaReg.Location = new Point(30, 286);
            lblContrasenaReg.Margin = new Padding(2, 0, 2, 0);
            lblContrasenaReg.Name = "lblContrasenaReg";
            lblContrasenaReg.Size = new Size(300, 20);
            lblContrasenaReg.TabIndex = 11;
            lblContrasenaReg.Text = "Contraseña (mínimo 8, letras y números):";
            // 
            // txtContrasenaReg
            // 
            txtContrasenaReg.Location = new Point(30, 312);
            txtContrasenaReg.Margin = new Padding(2, 2, 2, 2);
            txtContrasenaReg.MaxLength = 64;
            txtContrasenaReg.Name = "txtContrasenaReg";
            txtContrasenaReg.Size = new Size(360, 27);
            txtContrasenaReg.TabIndex = 12;
            txtContrasenaReg.UseSystemPasswordChar = true;
            // 
            // lblConfirmar
            // 
            lblConfirmar.AutoSize = true;
            lblConfirmar.Location = new Point(30, 356);
            lblConfirmar.Margin = new Padding(2, 0, 2, 0);
            lblConfirmar.Name = "lblConfirmar";
            lblConfirmar.Size = new Size(158, 20);
            lblConfirmar.TabIndex = 13;
            lblConfirmar.Text = "Confirmar contraseña:";
            // 
            // txtConfirmar
            // 
            txtConfirmar.Location = new Point(30, 382);
            txtConfirmar.Margin = new Padding(2, 2, 2, 2);
            txtConfirmar.MaxLength = 64;
            txtConfirmar.Name = "txtConfirmar";
            txtConfirmar.Size = new Size(360, 27);
            txtConfirmar.TabIndex = 14;
            txtConfirmar.UseSystemPasswordChar = true;
            // 
            // lblMensajeRegistro
            // 
            lblMensajeRegistro.Location = new Point(30, 424);
            lblMensajeRegistro.Margin = new Padding(2, 0, 2, 0);
            lblMensajeRegistro.Name = "lblMensajeRegistro";
            lblMensajeRegistro.Size = new Size(360, 44);
            lblMensajeRegistro.TabIndex = 15;
            // 
            // btnCrearCuenta
            // 
            btnCrearCuenta.Location = new Point(30, 480);
            btnCrearCuenta.Margin = new Padding(2, 2, 2, 2);
            btnCrearCuenta.Name = "btnCrearCuenta";
            btnCrearCuenta.Size = new Size(170, 38);
            btnCrearCuenta.TabIndex = 16;
            btnCrearCuenta.Text = "Crear cuenta";
            btnCrearCuenta.UseVisualStyleBackColor = true;
            btnCrearCuenta.Click += btnCrearCuenta_Click;
            // 
            // btnCancelarRegistro
            // 
            btnCancelarRegistro.DialogResult = DialogResult.Cancel;
            btnCancelarRegistro.Location = new Point(220, 480);
            btnCancelarRegistro.Margin = new Padding(2, 2, 2, 2);
            btnCancelarRegistro.Name = "btnCancelarRegistro";
            btnCancelarRegistro.Size = new Size(170, 38);
            btnCancelarRegistro.TabIndex = 17;
            btnCancelarRegistro.Text = "Cancelar";
            btnCancelarRegistro.UseVisualStyleBackColor = true;
            // 
            // FrmRegistro
            // 
            AcceptButton = btnCrearCuenta;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelarRegistro;
            ClientSize = new Size(420, 540);
            Controls.Add(lblTituloRegistro);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblApellido);
            Controls.Add(txtApellido);
            Controls.Add(lblCorreo);
            Controls.Add(txtCorreo);
            Controls.Add(lblTelefono);
            Controls.Add(txtTelefono);
            Controls.Add(lblIdUniversitario);
            Controls.Add(txtIdUniversitario);
            Controls.Add(lblContrasenaReg);
            Controls.Add(txtContrasenaReg);
            Controls.Add(lblConfirmar);
            Controls.Add(txtConfirmar);
            Controls.Add(lblMensajeRegistro);
            Controls.Add(btnCrearCuenta);
            Controls.Add(btnCancelarRegistro);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(2, 2, 2, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmRegistro";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Crear cuenta";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTituloRegistro;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblApellido;
        private TextBox txtApellido;
        private Label lblCorreo;
        private TextBox txtCorreo;
        private Label lblTelefono;
        private TextBox txtTelefono;
        private Label lblIdUniversitario;
        private TextBox txtIdUniversitario;
        private Label lblContrasenaReg;
        private TextBox txtContrasenaReg;
        private Label lblConfirmar;
        private TextBox txtConfirmar;
        private Label lblMensajeRegistro;
        private BotonPersona3 btnCrearCuenta;
        private BotonPersona3 btnCancelarRegistro;
    }
}