namespace SistemaBiblioteca1
{
    partial class FrmEjemplar
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


        private Label lblIdLibroEjemplar;
        private Label lblCodigoInventario;
        private TextBox txtIdLIbroEjemplar;
        private TextBox txtCodigoInventario;
        private Label lblCondicion;
        private CheckBox chkDisponible;
        private CheckBox chkEstadoEjemplar;
        private ComboBox cmbCondicion;
    }
}