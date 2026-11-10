//   Equipo #2
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
//   NUÑEZ Martinez Marco Antonio
namespace SistemaBiblioteca1
{
    partial class FrmReporteMasivo
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

        private Panel pnlControles;
        private Label lblCantidad;
        private NumericUpDown nudCantidad;
        private Label lblRetardo;
        private NumericUpDown nudRetardo;
        private Button btnIniciar;
        private Button btnDetener;
        private Button btnLineal;
        private Button btnPruebaUI;
        private Label lblClics;
        private Label lblInfo;
        private Panel pnlProgreso;
        private ProgressBar pbProgreso;
        private Label lblPorcentaje;
        private Label lblEstado;
        private Label lblResumen;
        private DataGridView dgvResultados;
        private DataGridViewTextBoxColumn colIdPrestamo;
        private DataGridViewTextBoxColumn colIdUsuario;
        private DataGridViewTextBoxColumn colDiasRetraso;
        private DataGridViewTextBoxColumn colRecargo;
        private DataGridViewTextBoxColumn colMonto;
    }
}
