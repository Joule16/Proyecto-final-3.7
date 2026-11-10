namespace SistemaBiblioteca1
{
    partial class FrmPago
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

        private Label lblIdSancion;
        private Label lblMontoPago;
        private Label lblFechaPago;
        private Label lblMetodoPago;
        private CheckBox chkEstadoPago;
        private TextBox txtSancionPago;
        private TextBox txtMontoPago;
        private DateTimePicker dtpFechaPago;
        private ComboBox cmbMetodoPago;
    }
}