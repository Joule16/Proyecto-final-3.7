//   Equipo #2
//   NUÑEZ Martinez Marco Antonio
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
using SistemaBiblioteca1.Datos;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaBiblioteca1
{
    public partial class FrmMaestro : Form
    {
        private IPanelCRUD vistaActiva;
        private MenuPersona3 menuModulos;
        private GuiaPersona3 guia;
        private Menup btnHamburguesa;
        private EscaladorUI escalador;
        private FondoSubmarino fondoSubmarino;

        private MdiClient areaMdi;
        private MenuStrip msPrincipal;
        private ToolStripMenuItem mnuVentana;
        private ToolStripMenuItem mnuCascada;
        private ToolStripMenuItem mnuMosaicoVertical;
        private ToolStripMenuItem mnuMosaicoHorizontal;
        private ToolStripMenuItem mnuRestaurar;
        private ToolStripMenuItem mnuProcesos;
        private ToolStripMenuItem mnuReporteMasivo;
        private ToolStripMenuItem mnuAdministracion;
        private ToolStripMenuItem mnuCuentasAdmin;
        private ToolStripMenuItem mnuCatalogoAdmin;
        private ToolStripStatusLabel tsslCreditos;
        private ToolStripStatusLabel tsslSesion;

        private bool soloConsulta;
        private bool puedeEliminar = true;

        private readonly string[] modulos = ModulosPermitidos();

        public FrmMaestro()
        {
            InitializeComponent();

            this.Text = "Sistema de Biblioteca  |  " + (ConexionBD.Habilitada
                ? "Base de datos: " + ConexionBD.Destino
                : "Sin base de datos (modo memoria)");

            this.AutoValidate = AutoValidate.EnableAllowFocusChange;

            this.IsMdiContainer = true;
            foreach (Control c in Controls)
            {
                if (c is MdiClient)
                {
                    areaMdi = (MdiClient)c;
                    break;
                }
            }

            pnlContenedorVistas.Visible = false;

            TemaPersona3.Aplicar(this);
            fondoSubmarino = new FondoSubmarino(this);

            if (areaMdi != null)
                fondoSubmarino.CubrirMdi(areaMdi);

            cmbModulo.Items.AddRange(modulos);
            ConfigurarMenuLateral();
            ConfigurarHamburguesa();
            ConfigurarGuia();
            ConectarFondo();
            ConfigurarMenuVentana();
            ConfigurarCreditos();
            ConfigurarSesion();
            OrdenarAcople();

            this.MdiChildActivate += FrmMaestro_MdiChildActivate;

            Load += (s, e) =>
            {
                AplicarRestriccionesPorRol();

                AcomodarGuia();

                escalador = EscaladorUI.Aplicar(this);

                escalador.EscalaCambio += (s2, e2) => menuModulos.FactorEscala = escalador.Escala;
                menuModulos.FactorEscala = escalador.Escala;
            };
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (fondoSubmarino != null)
                fondoSubmarino.PintarFondo(e.Graphics, e.ClipRectangle);
            else
                base.OnPaintBackground(e);
        }

        private void ConfigurarMenuLateral()
        {
            ClientSize = new Size(1080, 600);

            cmbModulo.Visible = false;
            lblModulo.Visible = false;

            lblIdBusqueda.Visible = false;
            txtIdBusqueda.Visible = false;
            btnMasterBuscar.Visible = false;
            pnlInferior.Visible = false;

            menuModulos = new MenuPersona3
            {
                Dock = DockStyle.Left,
                Width = 230,
                Items = modulos
            };

            menuModulos.SelectedIndexChanged += (s, e) =>
            {
                if (menuModulos.SelectedIndex >= 0)
                    cmbModulo.SelectedIndex = menuModulos.SelectedIndex;
            };

            Controls.Add(menuModulos);
        }

        private void ConfigurarHamburguesa()
        {
            btnHamburguesa = new Menup
            {
                Location = new Point(12, 8),
                Abierto = true
            };

            btnHamburguesa.Click += (s, e) =>
            {
                menuModulos.Alternar();
                btnHamburguesa.Abierto = menuModulos.Expandido;
            };

            pnlSuperior.Controls.Add(btnHamburguesa);
        }

        private void ConfigurarGuia()
        {
            guia = new GuiaPersona3
            {
                Text = "¿Qué módulo deseas usar?",
                Size = new Size(420, 44)
            };

            pnlSuperior.Controls.Add(guia);
        }

        private void ConectarFondo()
        {
            fondoSubmarino.Registrar(menuModulos);
            fondoSubmarino.Registrar(pnlSuperior);

            menuModulos.FondoExterno = (g, c) => fondoSubmarino.PintarEnControl(g, c);

            pnlSuperior.Paint += (s, e) =>
            {
                fondoSubmarino.PintarEnControl(e.Graphics, pnlSuperior);

                using (var tinte = new SolidBrush(Color.FromArgb(90, 10, 17, 40)))
                {
                    e.Graphics.FillRectangle(tinte, pnlSuperior.ClientRectangle);
                }
            };
        }

        private void AcomodarGuia()
        {
            guia.Left = pnlSuperior.ClientSize.Width - guia.Width - 12;
            guia.Top = 6;
            guia.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }

        private void ConfigurarMenuVentana()
        {
            msPrincipal = new MenuStrip();
            mnuProcesos = new ToolStripMenuItem("Procesos");
            mnuReporteMasivo = new ToolStripMenuItem("Reporte masivo de sanciones...");
            mnuVentana = new ToolStripMenuItem("Ventana");
            mnuCascada = new ToolStripMenuItem("Cascada");
            mnuMosaicoVertical = new ToolStripMenuItem("Mosaico vertical");
            mnuMosaicoHorizontal = new ToolStripMenuItem("Mosaico horizontal");
            mnuRestaurar = new ToolStripMenuItem("Restaurar tamaño");

            mnuVentana.DropDownItems.Add(mnuCascada);
            mnuVentana.DropDownItems.Add(mnuMosaicoVertical);
            mnuVentana.DropDownItems.Add(mnuMosaicoHorizontal);
            mnuVentana.DropDownItems.Add(mnuRestaurar);
            mnuProcesos.DropDownItems.Add(mnuReporteMasivo);
            msPrincipal.Items.Add(mnuProcesos);
            msPrincipal.Items.Add(mnuVentana);

            if (Sesion.EsAdministrador)
            {
                mnuAdministracion = new ToolStripMenuItem("Administración");
                mnuCuentasAdmin = new ToolStripMenuItem("Cuentas de usuario...");
                mnuCatalogoAdmin = new ToolStripMenuItem("Catálogo de libros...");
                mnuAdministracion.DropDownItems.Add(mnuCuentasAdmin);
                mnuAdministracion.DropDownItems.Add(mnuCatalogoAdmin);
                msPrincipal.Items.Insert(1, mnuAdministracion);
                mnuCuentasAdmin.Click += (s, e) => AbrirAdminCuentas();
                mnuCatalogoAdmin.Click += (s, e) => AbrirAdminLibros();
            }

            msPrincipal.MdiWindowListItem = mnuVentana;
            this.MainMenuStrip = msPrincipal;

            mnuCascada.Click += mnuCascada_Click;
            mnuMosaicoVertical.Click += mnuMosaicoVertical_Click;
            mnuMosaicoHorizontal.Click += mnuMosaicoHorizontal_Click;
            mnuRestaurar.Click += mnuRestaurar_Click;
            mnuReporteMasivo.Click += mnuReporteMasivo_Click;

            Color fondo = Color.FromArgb(8, 24, 60);
            msPrincipal.BackColor = fondo;
            msPrincipal.ForeColor = Color.White;
            mnuVentana.DropDown.BackColor = fondo;
            mnuVentana.DropDown.ForeColor = Color.White;
            mnuProcesos.DropDown.BackColor = fondo;
            mnuProcesos.DropDown.ForeColor = Color.White;

            if (mnuAdministracion != null)
            {
                mnuAdministracion.DropDown.BackColor = fondo;
                mnuAdministracion.DropDown.ForeColor = Color.White;
            }

            msPrincipal.Dock = DockStyle.Top;
            Controls.Add(msPrincipal);
        }

        private void ConfigurarCreditos()
        {
            tsslCreditos = new ToolStripStatusLabel
            {
                Alignment = ToolStripItemAlignment.Right,
                ForeColor = Color.White,
                Text = string.Join("   |   ", label1.Text, label2.Text, label3.Text, label4.Text)
            };
            statusStrip1.Items.Add(tsslCreditos);
        }

        private void ConfigurarSesion()
        {
            if (!SesionSistema.HaySesion)
                return;

            tsslSesion = new ToolStripStatusLabel
            {
                ForeColor = Color.White,
                Text = "Usuario: " + SesionSistema.UsuarioActivo +
                       "   |   Rol: " + SesionSistema.RolActivo +
                       "   |   Inicio de sesión: " + SesionSistema.HoraInicioSesion.ToString("dd/MM/yyyy HH:mm:ss")
            };
            statusStrip1.Items.Add(tsslSesion);
        }

        private void AplicarRestriccionesPorRol()
        {
            string rol = SesionSistema.RolActivo;

            if (rol == UsuariosSistema.ROL_ADMINISTRADOR)
            {
                soloConsulta = false;
                puedeEliminar = true;
            }
            else if (rol == UsuariosSistema.ROL_OPERADOR)
            {
                mnuReporteMasivo.Enabled = false;
                soloConsulta = false;
                puedeEliminar = false;
            }
            else
            {
                mnuProcesos.Visible = false;
                soloConsulta = true;
                puedeEliminar = false;
            }
        }

        private static string[] ModulosPermitidos()
        {
            string[] todos =
            {
                "Administrador", "Autor", "Categoria", "Editorial", "Ejemplar", "Intercambio",
                "Libro", "Pago", "Prestamo", "Sancion", "Usuario"
            };

            string rol = SesionSistema.RolActivo;

            if (rol == UsuariosSistema.ROL_ADMINISTRADOR)
                return todos;

            if (rol == UsuariosSistema.ROL_OPERADOR)
                return Array.FindAll(todos, m => m != "Administrador");

            return new string[] { "Autor", "Categoria", "Editorial", "Ejemplar", "Libro" };
        }

        private void AplicarPermisosEnBotones(Control padre)
        {
            foreach (Control c in padre.Controls)
            {
                if (c is Button)
                {
                    string texto = c.Text.Trim().ToLower();

                    if (texto.Contains("eliminar") && !puedeEliminar)
                        c.Enabled = false;

                    if ((texto.Contains("guardar") || texto.Contains("actualizar")) && soloConsulta)
                        c.Enabled = false;
                }

                AplicarPermisosEnBotones(c);
            }
        }

        private void OrdenarAcople()
        {
            menuModulos.SendToBack();
            statusStrip1.SendToBack();
            pnlSuperior.SendToBack();
            msPrincipal.SendToBack();

            if (areaMdi != null)
                areaMdi.BringToFront();
        }

        private void mnuReporteMasivo_Click(object sender, EventArgs e)
        {
            AbrirReporteMasivo();
        }

        private void AbrirReporteMasivo()
        {
            foreach (Form abierto in this.MdiChildren)
            {
                if (abierto is FrmReporteMasivo)
                {
                    if (abierto.WindowState == FormWindowState.Minimized)
                    {
                        abierto.WindowState = FormWindowState.Normal;
                    }
                    abierto.Activate();
                    return;
                }
            }

            FrmReporteMasivo vista = new FrmReporteMasivo();

            TemaPersona3.Aplicar(vista);
            vista.AutoValidate = AutoValidate.EnableAllowFocusChange;

            if (areaMdi != null)
            {
                int desfase = (this.MdiChildren.Length % 4) * 25;
                vista.StartPosition = FormStartPosition.Manual;
                vista.Size = new Size(areaMdi.ClientSize.Width * 85 / 100, areaMdi.ClientSize.Height * 85 / 100);
                vista.Location = new Point(10 + desfase, 10 + desfase);
            }

            vista.MdiParent = this;
            vista.Show();

            tsslEstado.Text = "Proceso activo: Reporte masivo de sanciones";
        }

        private void AbrirAdminCuentas()
        {
            AbrirPantallaAdmin<FrmAdminCuentas>("Administración: cuentas de usuario");
        }

        private void AbrirAdminLibros()
        {
            AbrirPantallaAdmin<FrmAdminLibros>("Administración: catálogo de libros");
        }

        private void AbrirPantallaAdmin<T>(string estado) where T : Form, new()
        {
            if (!Sesion.EsAdministrador)
            {
                MessageBox.Show("Solo las cuentas con rol Administrador pueden entrar a esta pantalla.",
                                "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (Form abierto in this.MdiChildren)
            {
                if (abierto is T)
                {
                    if (abierto.WindowState == FormWindowState.Minimized)
                    {
                        abierto.WindowState = FormWindowState.Normal;
                    }
                    abierto.Activate();
                    return;
                }
            }

            T vista = new T();

            TemaPersona3.Aplicar(vista);
            vista.AutoValidate = AutoValidate.EnableAllowFocusChange;

            if (areaMdi != null)
            {
                int desfase = (this.MdiChildren.Length % 4) * 25;
                vista.StartPosition = FormStartPosition.Manual;
                vista.Size = new Size(areaMdi.ClientSize.Width * 85 / 100, areaMdi.ClientSize.Height * 85 / 100);
                vista.Location = new Point(10 + desfase, 10 + desfase);
            }

            vista.MdiParent = this;
            vista.Show();

            tsslEstado.Text = estado;
        }

        private void mnuCascada_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void mnuMosaicoVertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void mnuMosaicoHorizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void mnuRestaurar_Click(object sender, EventArgs e)
        {
            if (areaMdi == null)
                return;

            int numero = 0;
            foreach (Form hija in this.MdiChildren)
            {
                hija.WindowState = FormWindowState.Normal;

                int desfase = (numero % 4) * 25;
                hija.Size = new Size(areaMdi.ClientSize.Width * 75 / 100, areaMdi.ClientSize.Height * 75 / 100);
                hija.Location = new Point(10 + desfase, 10 + desfase);
                numero++;
            }
        }

        private void FrmMaestro_MdiChildActivate(object sender, EventArgs e)
        {
            vistaActiva = this.ActiveMdiChild as IPanelCRUD;

            if (vistaActiva == null)
            {
                guia.Text = "¿Qué módulo deseas usar?";
                tsslEstado.Text = "Primero elige un módulo.";
            }
        }

        private static string TituloPara(string modulo)
        {
            switch (modulo)
            {
                case "Categoria":
                    return "CATEGORÍA";
                case "Prestamo":
                    return "PRÉSTAMO";
                case "Sancion":
                    return "SANCIÓN";
                default:
                    return modulo == null ? "" : modulo.ToUpper();
            }
        }

        private static string NombrePara(string modulo)
        {
            switch (modulo)
            {
                case "Categoria":
                    return "Categoría";
                case "Prestamo":
                    return "Préstamo";
                case "Sancion":
                    return "Sanción";
                default:
                    return modulo ?? "";
            }
        }

        private void cmbModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AbrirModulo(cmbModulo.SelectedItem?.ToString());
        }

        private Type TipoPara(string modulo)
        {
            switch (modulo)
            {
                case "Administrador":
                    return typeof(FrmAdministrador);
                case "Autor":
                    return typeof(FrmAutor);
                case "Categoria":
                    return typeof(FrmCategoria);
                case "Editorial":
                    return typeof(FrmEditorialcs);
                case "Ejemplar":
                    return typeof(FrmEjemplar);
                case "Intercambio":
                    return typeof(FrmIntercambio);
                case "Libro":
                    return typeof(FrmLibro);
                case "Pago":
                    return typeof(FrmPago);
                case "Prestamo":
                    return typeof(FrmPrestamo);
                case "Sancion":
                    return typeof(FrmSancion);
                case "Usuario":
                    return typeof(FrmUsuario);
                default:
                    return null;
            }
        }

        private Form CrearVista(string modulo)
        {
            switch (modulo)
            {
                case "Administrador":
                    return new FrmAdministrador();
                case "Autor":
                    return new FrmAutor();
                case "Categoria":
                    return new FrmCategoria();
                case "Editorial":
                    return new FrmEditorialcs();
                case "Ejemplar":
                    return new FrmEjemplar();
                case "Intercambio":
                    return new FrmIntercambio();
                case "Libro":
                    return new FrmLibro();
                case "Pago":
                    return new FrmPago();
                case "Prestamo":
                    return new FrmPrestamo();
                case "Sancion":
                    return new FrmSancion();
                case "Usuario":
                    return new FrmUsuario();
                default:
                    return null;
            }
        }

        private void AbrirModulo(string modulo)
        {
            Type tipo = TipoPara(modulo);
            if (tipo == null)
                return;

            foreach (Form abierto in this.MdiChildren)
            {
                if (abierto.GetType() == tipo)
                {
                    if (abierto.WindowState == FormWindowState.Minimized)
                    {
                        abierto.WindowState = FormWindowState.Normal;
                    }
                    abierto.Activate();
                    return;
                }
            }

            Form vista = CrearVista(modulo);
            if (vista == null)
                return;

            MostrarVista(vista, modulo);
        }

        private void DeseleccionarMenu(string modulo)
        {
            if (IsDisposed || Disposing || menuModulos.IsDisposed)
                return;

            int i = menuModulos.SelectedIndex;
            if (i < 0 || i >= modulos.Length || modulos[i] != modulo)
                return;

            menuModulos.SelectedIndex = -1;
            cmbModulo.SelectedIndex = -1;
        }

        private void MostrarVista(Form vista, string modulo)
        {
            BarraAccionesPersona3.Agregar(vista, errorProvider1, statusStrip1);

            TemaPersona3.Aplicar(vista);
            FondoModulo.Aplicar(vista, TituloPara(modulo));
            AplicarPermisosEnBotones(vista);

            vista.Text = NombrePara(modulo);

            vista.AutoValidate = AutoValidate.EnableAllowFocusChange;

            EscaladorModulo.FijarAnclas(vista);

            if (areaMdi != null)
            {
                int desfase = (this.MdiChildren.Length % 4) * 25;
                int ancho = areaMdi.ClientSize.Width * 75 / 100;
                int alto = areaMdi.ClientSize.Height * 75 / 100;

                vista.StartPosition = FormStartPosition.Manual;
                vista.Size = new Size(ancho, alto);
                vista.Location = new Point(10 + desfase, 10 + desfase);
            }

            vista.FormClosed += (s, e) => DeseleccionarMenu(modulo);

            vista.MdiParent = this;
            vista.Show();

            vista.AutoValidate = AutoValidate.EnableAllowFocusChange;

            if (areaMdi != null)
            {
                int desfase = ((this.MdiChildren.Length - 1) % 4) * 25;
                vista.Location = new Point(10 + desfase, 10 + desfase);
            }

            EscaladorModulo.Aplicar(vista);

            vistaActiva = vista as IPanelCRUD;

            errorProvider1.Clear();
            tsslEstado.Text = "Módulo activo: " + modulo;
            guia.Text = "¿Qué deseas hacer en " + NombrePara(modulo) + "?";
        }

        private void btnMasterGuardar_Click(object sender, EventArgs e)
        {
            if (!HayVistaActiva()) return;
            vistaActiva.EjecutarGuardar();
        }

        private void btnMasterBuscar_Click(object sender, EventArgs e)
        {
            if (!HayVistaActiva()) return;
            vistaActiva.EjecutarBuscar(txtIdBusqueda.Text, errorProvider1);
        }

        private void btnMasterActualizar_Click(object sender, EventArgs e)
        {
            if (!HayVistaActiva()) return;
            vistaActiva.EjecutarActualizar();
        }

        private void btnMasterEliminar_Click(object sender, EventArgs e)
        {
            if (!HayVistaActiva()) return;
            vistaActiva.EjecutarEliminar(statusStrip1);
        }

        private bool HayVistaActiva()
        {
            if (vistaActiva == null)
            {
                tsslEstado.Text = "Primero elige un módulo.";
                return false;
            }
            return true;
        }

        private void txtIdBusqueda_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}