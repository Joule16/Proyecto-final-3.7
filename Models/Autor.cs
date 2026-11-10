// Suñiga Maciel Joule Alexander
// Villa Olivares Ariel
// Nuñes Martinez Marco Antonio
// Equipo 2
// =============================
using System;
using SistemaBiblioteca1.Datos;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SistemaBiblioteca1.Models
{
    public class Autor : EntidadBase, IAlmacenamientoCRUD
    {
        private static List<Autor> listaAutores = new List<Autor>();

        private string nombre;
        private string apellido;
        private string nacionalidad;
        private DateTime fechaNacimiento;
        private string rutaImagen;

        public int IdAutor
        {
            get { return Id; }
            set { Id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre no puede estar vacío.");
                nombre = value.Trim();
            }
        }

        public string Apellido
        {
            get { return apellido; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El apellido no puede estar vacío.");
                apellido = value.Trim();
            }
        }

        public string Nacionalidad
        {
            get { return nacionalidad; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("La nacionalidad no puede estar vacía.");
                nacionalidad = value.Trim();
            }
        }

        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("La fecha de nacimiento no puede ser una fecha futura.");
                fechaNacimiento = value;
            }
        }

        public string RutaImagen
        {
            get { return rutaImagen; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && Path.IsPathRooted(value))
                    throw new ArgumentException("La ruta de la imagen debe ser relativa, no una ruta absoluta.");
                rutaImagen = string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
            }
        }

        public bool Estado
        {
            get { return EsActivo; }
            set { EsActivo = value; }
        }

        public Autor() : base()
        {
            nombre = string.Empty;
            apellido = string.Empty;
            nacionalidad = string.Empty;
            rutaImagen = string.Empty;
            fechaNacimiento = DateTime.Now;
            EsActivo = false;
        }

        public Autor(int idAutor, string nombre, string apellido, string nacionalidad,
                     DateTime fechaNacimiento, bool estado) : base(idAutor)
        {
            rutaImagen = string.Empty;
            Nombre = nombre;
            Apellido = apellido;
            Nacionalidad = nacionalidad;
            FechaNacimiento = fechaNacimiento;
            EsActivo = estado;
        }

        public int CalcularEdad()
        {
            return CalcularEdad(DateTime.Now);
        }

        public int CalcularEdad(DateTime fechaReferencia)
        {
            int edad = fechaReferencia.Year - fechaNacimiento.Year;
            if (fechaReferencia < fechaNacimiento.AddYears(edad))
                edad--;
            return edad;
        }

        internal static void CargarDesdeBD(IEnumerable<Autor> registros)
        {
            listaAutores.Clear();
            listaAutores.AddRange(registros);
        }

        public void InsertarRegistro(object objeto)
        {
            if (objeto == null)
                throw new ArgumentNullException(nameof(objeto), "El objeto a insertar no puede ser nulo.");

            if (!(objeto is Autor autor))
                throw new ArgumentException("El objeto a insertar no es de tipo Autor.");

            if (listaAutores.Any(a => a.Id == autor.Id))
                throw new InvalidOperationException($"Ya existe un autor con el id {autor.Id}.");

            RepositorioBD.Insertar(autor);

            listaAutores.Add(autor);
        }

        public object ConsultarRegistro(string id)
        {
            int idBuscado = ConvertirId(id);
            return listaAutores.FirstOrDefault(a => a.Id == idBuscado);
        }

        public void ActualizarRegistro(object objeto)
        {
            if (objeto == null)
                throw new ArgumentNullException(nameof(objeto), "El objeto a actualizar no puede ser nulo.");

            if (!(objeto is Autor autor))
                throw new ArgumentException("El objeto a actualizar no es de tipo Autor.");

            int indice = listaAutores.FindIndex(a => a.Id == autor.Id);
            if (indice == -1)
                throw new InvalidOperationException($"No existe un autor con el id {autor.Id}.");

            RepositorioBD.Actualizar(autor);

            listaAutores[indice] = autor;
        }

        public void EliminarRegistro(string id)
        {
            int idBuscado = ConvertirId(id);

            int indice = listaAutores.FindIndex(a => a.Id == idBuscado);
            if (indice == -1)
                throw new InvalidOperationException($"No existe un autor con el id {idBuscado}.");

            RepositorioBD.Eliminar("Autor", "IdAutor", idBuscado);

            listaAutores.RemoveAt(indice);
        }

        private static int ConvertirId(string id)
        {
            if (!int.TryParse(id, out int resultado) || resultado < 0)
                throw new ArgumentException("El id debe ser un número entero no negativo.");
            return resultado;
        }

        public override string ToString()
        {
            return $"Autor #{Id}: {nombre} {apellido} | Nacionalidad: {nacionalidad} | " +
                   $"Edad: {CalcularEdad()} años | Estado: {(EsActivo ? "Activo" : "Inactivo")}";
        }
    }
}