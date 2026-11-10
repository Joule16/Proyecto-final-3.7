namespace SistemaBiblioteca1
{
    partial class FrmCategoria
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


        private Label lblNombreCategoria;
        private Label lblDescripcionCategoria;
        private TextBox txtNombreCategoria;
        private TextBox txtDescripcionCategoria;
        private CheckBox chkRestringidaMenores;
        private CheckBox chkEstadoCategoria;
    }
}