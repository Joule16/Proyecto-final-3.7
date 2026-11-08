//   Equipo #2
//   NUÑEZ Martinez Marco Antonio
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
// ===============================
namespace SistemaBiblioteca1
{
    partial class FrmLogin
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
            lblTituloLogin = new Label();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblContrasena = new Label();
            txtContrasena = new TextBox();
            lblMensajeLogin = new Label();
            btnIngresar = new BotonPersona3();
            btnSalir = new BotonPersona3();
            btnRegistrarse = new BotonPersona3();
            lblAutoria = new Label();
            SuspendLayout();

            lblTituloLogin.AutoSize = true;
            lblTituloLogin.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            lblTituloLogin.Location = new Point(28, 20);
            lblTituloLogin.Margin = new Padding(2, 0, 2, 0);
            lblTituloLogin.Name = "lblTituloLogin";
            lblTituloLogin.Size = new Size(160, 37);
            lblTituloLogin.TabIndex = 0;
            lblTituloLogin.Text = "Iniciar sesión";

            lblUsuario.AutoSize = true;
            lblUsuario.Location = new Point(30, 80);
            lblUsuario.Margin = new Padding(2, 0, 2, 0);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(135, 20);
            lblUsuario.TabIndex = 1;
            lblUsuario.Text = "Usuario o correo:";

            txtUsuario.Location = new Point(30, 106);
            txtUsuario.Margin = new Padding(2, 2, 2, 2);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(360, 27);
            txtUsuario.TabIndex = 2;

            lblContrasena.AutoSize = true;
            lblContrasena.Location = new Point(30, 150);
            lblContrasena.Margin = new Padding(2, 0, 2, 0);
            lblContrasena.Name = "lblContrasena";
            lblContrasena.Size = new Size(95, 20);
            lblContrasena.TabIndex = 3;
            lblContrasena.Text = "Contraseña:";

            txtContrasena.Location = new Point(30, 176);
            txtContrasena.Margin = new Padding(2, 2, 2, 2);
            txtContrasena.MaxLength = 64;
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(360, 27);
            txtContrasena.TabIndex = 4;
            txtContrasena.UseSystemPasswordChar = true;

            lblMensajeLogin.Location = new Point(30, 214);
            lblMensajeLogin.Margin = new Padding(2, 0, 2, 0);
            lblMensajeLogin.Name = "lblMensajeLogin";
            lblMensajeLogin.Size = new Size(360, 44);
            lblMensajeLogin.TabIndex = 5;

            btnIngresar.Location = new Point(30, 270);
            btnIngresar.Margin = new Padding(2, 2, 2, 2);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(170, 38);
            btnIngresar.TabIndex = 6;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = true;
            btnIngresar.Click += btnIngresar_Click;

            btnSalir.DialogResult = DialogResult.Cancel;
            btnSalir.Location = new Point(220, 270);
            btnSalir.Margin = new Padding(2, 2, 2, 2);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(170, 38);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;

            btnRegistrarse.Location = new Point(30, 320);
            btnRegistrarse.Margin = new Padding(2, 2, 2, 2);
            btnRegistrarse.Name = "btnRegistrarse";
            btnRegistrarse.Size = new Size(360, 38);
            btnRegistrarse.TabIndex = 8;
            btnRegistrarse.Text = "¿No tienes cuenta? Regístrate";
            btnRegistrarse.UseVisualStyleBackColor = true;
            btnRegistrarse.Click += btnRegistrarse_Click;

            lblAutoria.Location = new Point(30, 376);
            lblAutoria.Margin = new Padding(2, 0, 2, 0);
            lblAutoria.Name = "lblAutoria";
            lblAutoria.Size = new Size(360, 92);
            lblAutoria.TabIndex = 9;
            lblAutoria.Text = "Equipo #2\r\nNUÑEZ Martinez Marco Antonio\r\nSUÑIGA Maciel Joule Alexander\r\nVILLA Olivarez Ariel";

            AcceptButton = btnIngresar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnSalir;
            ClientSize = new Size(420, 484);
            Controls.Add(lblTituloLogin);
            Controls.Add(lblUsuario);
            Controls.Add(txtUsuario);
            Controls.Add(lblContrasena);
            Controls.Add(txtContrasena);
            Controls.Add(lblMensajeLogin);
            Controls.Add(btnIngresar);
            Controls.Add(btnSalir);
            Controls.Add(btnRegistrarse);
            Controls.Add(lblAutoria);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(2, 2, 2, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Iniciar sesión";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTituloLogin;
        private Label lblUsuario;
        private TextBox txtUsuario;
        private Label lblContrasena;
        private TextBox txtContrasena;
        private Label lblMensajeLogin;
        private BotonPersona3 btnIngresar;
        private BotonPersona3 btnSalir;
        private BotonPersona3 btnRegistrarse;
        private Label lblAutoria;
    }
}