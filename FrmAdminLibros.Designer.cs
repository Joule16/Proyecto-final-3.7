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