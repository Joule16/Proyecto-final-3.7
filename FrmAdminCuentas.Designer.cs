//   Equipo #2
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