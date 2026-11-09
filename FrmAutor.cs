//Suñiga Maciel Joule Alexander
//Villa Olivares Ariel 
//Martinez Marco Antonio
//Equipo 2
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SistemaBiblioteca1.Models;

namespace SistemaBiblioteca1
{
    public partial class FrmAutor : FrmBase, IPanelCRUD
    {
        private const string CarpetaImagenes = "ImagenesDatos";

        private string rutaOrigenSeleccionada = string.Empty;

        public FrmAutor()
        {
            InitializeComponent();

            ConfigurarCampoLetras(txtNombreAutor, "Nombre");
            ConfigurarCampoLetras(txtApellidoAutor, "Apellido");
            ConfigurarCampoLetras(txtNacionalidadAutor, "Nacionalidad");
        }

        public void EjecutarGuardar()
        {
            if (!ValidarFormulario())
                return;

            try
            {
                Autor autor = CrearAutorDesdeControles();

                if (autor.ConsultarRegistro(txtId.Text) != null)
                    throw new InvalidOperationException("Ya existe un autor con el id " + autor.Id + ".");

                if (!GuardarImagenFisica(autor.Id, string.Empty, out string rutaRelativa))
                    return;

                autor.RutaImagen = rutaRelativa;
                autor.InsertarRegistro(autor);

                MessageBox.Show("Autor guardado correctamente.", "Guardar",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarControles();
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException || ex is FormatException)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void EjecutarBuscar(string id, ErrorProvider alerta)
        {
            alerta.SetError(txtId, string.Empty);

            try
            {
                Autor autor = new Autor().ConsultarRegistro(id) as Autor;

                if (autor == null)
                {
                    alerta.SetError(txtId, "No existe un autor con ese id.");
                    return;
                }

                txtId.Text = autor.Id.ToString();
                txtNombreAutor.Text = autor.Nombre;
                txtApellidoAutor.Text = autor.Apellido;
                txtNacionalidadAutor.Text = autor.Nacionalidad;
                dtpFechaNacimientoAutor.Value = autor.FechaNacimiento;
                chkEstadoAutor.Checked = autor.EsActivo;

                rutaOrigenSeleccionada = string.Empty;
                MostrarImagenGuardada(autor.RutaImagen);
            }
            catch (ArgumentException ex)
            {
                alerta.SetError(txtId, ex.Message);
            }
        }

        public void EjecutarActualizar()
        {
            if (!ValidarFormulario())
                return;

            try
            {
                Autor autor = CrearAutorDesdeControles();

                Autor existente = new Autor().ConsultarRegistro(txtId.Text) as Autor;
                if (existente == null)
                    throw new InvalidOperationException("No existe un autor con el id " + autor.Id + ".");

                autor.FechaRegistro = existente.FechaRegistro;

                if (!GuardarImagenFisica(autor.Id, existente.RutaImagen, out string rutaRelativa))
                    return;

                autor.RutaImagen = rutaRelativa;
                autor.ActualizarRegistro(autor);
                rutaOrigenSeleccionada = string.Empty;

                MessageBox.Show("Autor actualizado correctamente.", "Actualizar",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException || ex is FormatException)
            {
                MessageBox.Show(ex.Message, "No se pudo actualizar",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void EjecutarEliminar(StatusStrip barraEstado)
        {
            if (!ValidarIdObligatorio())
            {
                MostrarEstado(barraEstado, "Escribe o busca el ID del registro que deseas eliminar.");
                return;
            }

            try
            {
                Autor existente = new Autor().ConsultarRegistro(txtId.Text) as Autor;
                string rutaImagen = existente != null ? existente.RutaImagen : string.Empty;

                new Autor().EliminarRegistro(txtId.Text);
                EliminarImagenFisica(rutaImagen);
                LimpiarControles();
                MostrarEstado(barraEstado, "Autor eliminado correctamente.");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                MostrarEstado(barraEstado, ex.Message);
            }
        }

        private Autor CrearAutorDesdeControles()
        {
            if (!int.TryParse(txtId.Text, out int id))
                throw new FormatException("El id debe ser un número entero.");

            return new Autor(id, txtNombreAutor.Text, txtApellidoAutor.Text, txtNacionalidadAutor.Text,
                             dtpFechaNacimientoAutor.Value, chkEstadoAutor.Checked);
        }

        private void LimpiarControles()
        {
            LimpiarErrores();
            txtId.Clear();
            txtNombreAutor.Clear();
            txtApellidoAutor.Clear();
            txtNacionalidadAutor.Clear();
            dtpFechaNacimientoAutor.Value = DateTime.Now;
            chkEstadoAutor.Checked = false;

            rutaOrigenSeleccionada = string.Empty;
            MostrarImagen(null);
        }

        private void btnCargarImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogoAbrir = new OpenFileDialog())
            {
                dialogoAbrir.Title = "Seleccionar fotografía del autor";
                dialogoAbrir.Filter = "Archivos de imagen (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                dialogoAbrir.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                if (dialogoAbrir.ShowDialog() != DialogResult.OK)
                    return;

                string rutaOrigen = dialogoAbrir.FileName;

                string extension = Path.GetExtension(rutaOrigen).ToLowerInvariant();
                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".bmp")
                {
                    MessageBox.Show("Solo se admiten archivos de imagen (.jpg, .jpeg, .png, .bmp).", "Cargar imagen",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    MostrarImagen(CargarImagenSinBloqueo(rutaOrigen));
                    rutaOrigenSeleccionada = rutaOrigen;
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("El archivo seleccionado ya no existe.", "Cargar imagen",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("No tienes permisos para leer ese archivo.", "Cargar imagen",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("No se pudo leer el archivo (puede estar en uso): " + ex.Message, "Cargar imagen",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex) when (ex is OutOfMemoryException || ex is System.Runtime.InteropServices.ExternalException || ex is ArgumentException)
                {
                    MessageBox.Show("El archivo está dañado o no es una imagen válida.", "Cargar imagen",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Image CargarImagenSinBloqueo(string rutaOrigen)
        {
         using (Image original = Image.FromFile(rutaOrigen))
            {
                return new Bitmap(original);
            }
        }

        private void MostrarImagen(Image? nueva)
        {
            Image? anterior = pbxFotografia.Image;
            pbxFotografia.Image = nueva;
            pbxFotografia.SizeMode = PictureBoxSizeMode.StretchImage;

            if (anterior != null)
                anterior.Dispose();
        }

        private void MostrarImagenGuardada(string rutaRelativa)
        {
            if (string.IsNullOrWhiteSpace(rutaRelativa))
            {
                MostrarImagen(null);
                return;
            }

            string rutaCompleta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rutaRelativa);

            try
            {
                if (!File.Exists(rutaCompleta))
                {
                    MostrarImagen(null);
                    MessageBox.Show("No se encontró el archivo de la fotografía guardada:\n" + rutaRelativa, "Fotografía",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MostrarImagen(CargarImagenSinBloqueo(rutaCompleta));
            }
            catch (UnauthorizedAccessException)
            {
                MostrarImagen(null);
                MessageBox.Show("No tienes permisos para leer la fotografía guardada.", "Fotografía",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MostrarImagen(null);
                MessageBox.Show("No se pudo leer la fotografía guardada: " + ex.Message, "Fotografía",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) when (ex is OutOfMemoryException || ex is System.Runtime.InteropServices.ExternalException || ex is ArgumentException)
            {
                MostrarImagen(null);
                MessageBox.Show("El archivo de la fotografía guardada está dañado.", "Fotografía",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ObtenerCarpetaImagenes()
        {
            string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CarpetaImagenes);

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            return carpeta;
        }

        private bool GuardarImagenFisica(int id, string rutaAnterior, out string rutaRelativa)
        {
            rutaRelativa = rutaAnterior;

            if (string.IsNullOrEmpty(rutaOrigenSeleccionada))
                return true;

            try
            {
                string carpetaDestino = ObtenerCarpetaImagenes();

                string extension = Path.GetExtension(rutaOrigenSeleccionada).ToLowerInvariant();
                string nombreUnico = "Autor_" + id + extension;
                string rutaDestino = Path.Combine(carpetaDestino, nombreUnico);

                File.Copy(rutaOrigenSeleccionada, rutaDestino, true);

                string nuevaRuta = Path.Combine(CarpetaImagenes, nombreUnico);

                if (!string.IsNullOrEmpty(rutaAnterior) &&
                    !string.Equals(rutaAnterior, nuevaRuta, StringComparison.OrdinalIgnoreCase))
                {
                    EliminarImagenFisica(rutaAnterior);
                }

                rutaRelativa = nuevaRuta;
                return true;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("La imagen seleccionada ya no existe en su ubicación original.", "Guardar imagen",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("No hay permisos para escribir en la carpeta de imágenes del sistema.", "Guardar imagen",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show("No se pudo copiar la imagen (disco lleno o archivo bloqueado): " + ex.Message,
                                "Guardar imagen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private void EliminarImagenFisica(string rutaRelativa)
        {
            if (string.IsNullOrWhiteSpace(rutaRelativa))
                return;

            try
            {
                string rutaCompleta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rutaRelativa);

                if (File.Exists(rutaCompleta))
                    File.Delete(rutaCompleta);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("No hay permisos para borrar el archivo de imagen anterior.", "Imagen",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (IOException ex)
            {
                MessageBox.Show("No se pudo borrar el archivo de imagen anterior (puede estar bloqueado): " + ex.Message,
                                "Imagen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MostrarEstado(StatusStrip barraEstado, string mensaje)
        {
            if (barraEstado.Items.Count > 0)
                barraEstado.Items[0].Text = mensaje;
        }
    }
}