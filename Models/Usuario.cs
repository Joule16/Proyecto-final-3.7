//Suñiga Maciel Joule Alexander 
//Villa Olivares Ariel 
//Nuñes Martinez Marco Antonio 
//Equipo 2 
//============================= 
using System;
using SistemaBiblioteca1.Datos;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using SistemaBiblioteca1.Models;

namespace SistemaBiblioteca1.Models
{
    public enum RolUsuario
    {
        Usuario = 0,
        Administrador = 1
    }

    public class Usuario : EntidadBase, IAlmacenamientoCRUD
    {
        private static List<Usuario> listaUsuarios = new List<Usuario>();

        private string nombre;
        private string apellido;
        private string email;
        private string telefono;
        private string idUniversitario;
        private int contadorSanciones;
        private bool estaBaneado;
        private string motivoBaneo;
        private RolUsuario rol;
        private string contrasenaHash;

        private const int LIMITE_SANCIONES_BANEO = 5;
        private const int LONGITUD_MAXIMA_MOTIVO = 200;

        private const int ITERACIONES_HASH = 100000;
        private const int LONGITUD_SAL = 16;
        private const int LONGITUD_HASH = 32;

        public int IdUsuario
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

        public string Email
        {
            get { return email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@") || !value.Contains("."))
                    throw new ArgumentException("El correo electrónico no tiene un formato válido.");
                email = value.Trim();
            }
        }

        public string Telefono
        {
            get { return telefono; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Trim().Length != 10)
                    throw new ArgumentException("El teléfono debe contener 10 dígitos.");
                foreach (char c in value)
                {
                    if (!char.IsDigit(c))
                        throw new ArgumentException("El teléfono solo debe contener números.");
                }
                telefono = value.Trim();
            }
        }

        public string IdUniversitario
        {
            get { return idUniversitario; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El id universitario no puede estar vacío.");
                idUniversitario = value.Trim();
            }
        }

        public int ContadorSanciones
        {
            get { return contadorSanciones; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("El contador de sanciones no puede ser negativo.");
                contadorSanciones = value;
            }
        }

        public bool EstaBaneado
        {
            get { return estaBaneado; }
            set { estaBaneado = value; }
        }

        public string MotivoBaneo
        {
            get { return motivoBaneo; }
            set { motivoBaneo = value == null ? string.Empty : value.Trim(); }
        }

        public bool Estado
        {
            get { return EsActivo; }
            set { EsActivo = value; }
        }

        public string ContrasenaHash
        {
            get { return contrasenaHash; }
            set { contrasenaHash = value ?? string.Empty; }
        }

        public bool TieneContrasena
        {
            get { return contrasenaHash.Length > 0; }
        }

        public RolUsuario Rol
        {
            get { return rol; }
            set
            {
                if (!Enum.IsDefined(typeof(RolUsuario), value))
                    throw new ArgumentException("El rol del usuario no es válido.");
                rol = value;
            }
        }

        public Usuario() : base()
        {
            nombre = string.Empty;
            apellido = string.Empty;
            email = string.Empty;
            telefono = string.Empty;
            idUniversitario = string.Empty;
            contadorSanciones = 0;
            estaBaneado = false;
            motivoBaneo = string.Empty;
            rol = RolUsuario.Usuario;
            contrasenaHash = string.Empty;
            EsActivo = false;
        }

        public Usuario(int idUsuario, string nombre, string apellido, string email,
                        string telefono, string idUniversitario, bool estado,
                        RolUsuario rol = RolUsuario.Usuario) : base(idUsuario)
        {
            contrasenaHash = string.Empty;
            motivoBaneo = string.Empty;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Telefono = telefono;
            IdUniversitario = idUniversitario;
            ContadorSanciones = 0;
            EstaBaneado = false;
            Rol = rol;
            EsActivo = estado;
        }

        public void RegistrarSancion()
        {
            RegistrarSancion(1, LIMITE_SANCIONES_BANEO);
        }

        public void RegistrarSancion(int cantidadASumar, int limiteBaneo)
        {
            if (cantidadASumar <= 0)
                throw new ArgumentException("La cantidad a sumar debe ser mayor a cero.");

            ContadorSanciones += cantidadASumar;

            if (ContadorSanciones >= limiteBaneo && !EstaBaneado)
            {
                EstaBaneado = true;
                MotivoBaneo = "Acumuló " + ContadorSanciones + " sanciones";
            }
            GuardarCambiosEnBD();
        }

        public static void ValidarFormatoContrasena(string contrasena)
        {
            if (string.IsNullOrEmpty(contrasena) || contrasena.Length < 8)
                throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");

            if (contrasena.Length > 64)
                throw new ArgumentException("La contraseña no puede pasar de 64 caracteres.");

            if (contrasena.Contains(' '))
                throw new ArgumentException("La contraseña no puede contener espacios.");

            if (!contrasena.Any(char.IsLetter) || !contrasena.Any(char.IsDigit))
                throw new ArgumentException("La contraseña debe combinar letras y números.");
        }

        public void EstablecerContrasena(string contrasena)
        {
            ValidarFormatoContrasena(contrasena);

            byte[] sal = RandomNumberGenerator.GetBytes(LONGITUD_SAL);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(contrasena, sal, ITERACIONES_HASH,
                                                    HashAlgorithmName.SHA256, LONGITUD_HASH);

            contrasenaHash = ITERACIONES_HASH + "." + Convert.ToBase64String(sal) + "." + Convert.ToBase64String(hash);
            GuardarCambiosEnBD();
        }

        public bool VerificarContrasena(string contrasena)
        {
            if (string.IsNullOrEmpty(contrasena) || !TieneContrasena)
                return false;

            string[] partes = contrasenaHash.Split('.');
            if (partes.Length != 3 || !int.TryParse(partes[0], out int iteraciones))
                return false;

            try
            {
                byte[] sal = Convert.FromBase64String(partes[1]);
                byte[] hashEsperado = Convert.FromBase64String(partes[2]);

                byte[] hashCalculado = Rfc2898DeriveBytes.Pbkdf2(contrasena, sal, iteraciones,
                                                                 HashAlgorithmName.SHA256, hashEsperado.Length);

                return CryptographicOperations.FixedTimeEquals(hashCalculado, hashEsperado);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static Usuario Autenticar(string email, string contrasena)
        {
            string correo = (email ?? string.Empty).Trim();

            Usuario? usuario = listaUsuarios.FirstOrDefault(
                u => string.Equals(u.Email, correo, StringComparison.OrdinalIgnoreCase));

            if (usuario == null || !usuario.VerificarContrasena(contrasena ?? string.Empty))
                throw new ArgumentException("Correo o contraseña incorrectos.");

            if (usuario.EstaBaneado)
            {
                string motivo = usuario.MotivoBaneo.TrimEnd('.', ' ');
                string detalle = motivo.Length > 0 ? " Motivo: " + motivo + "." : string.Empty;
                throw new InvalidOperationException("Tu cuenta fue baneada." + detalle + " Contacta al administrador.");
            }

            if (!usuario.EsActivo)
                throw new InvalidOperationException("Tu cuenta está desactivada. Contacta al administrador.");

            return usuario;
        }

        public static List<Usuario> ObtenerTodos()
        {
            return listaUsuarios.OrderBy(u => u.Id).ToList();
        }

        public void Activar()
        {
            EsActivo = true;
            GuardarCambiosEnBD();
        }

        public void Desactivar()
        {
            EsActivo = false;
            GuardarCambiosEnBD();
        }

        public void Banear(string motivo)
        {
            if (string.IsNullOrWhiteSpace(motivo))
                throw new ArgumentException("Escribe el motivo del baneo.");

            if (motivo.Trim().Length > LONGITUD_MAXIMA_MOTIVO)
                throw new ArgumentException("El motivo no puede pasar de " + LONGITUD_MAXIMA_MOTIVO + " caracteres.");

            if (EstaBaneado)
                throw new InvalidOperationException("La cuenta ya está baneada.");

            EstaBaneado = true;
            MotivoBaneo = motivo;
            GuardarCambiosEnBD();
        }

        public void QuitarBaneo()
        {
            if (!EstaBaneado)
                throw new InvalidOperationException("La cuenta no está baneada.");

            EstaBaneado = false;
            MotivoBaneo = string.Empty;
            ContadorSanciones = 0;
            GuardarCambiosEnBD();
        }

        public bool EsAdministrador()
        {
            return Rol == RolUsuario.Administrador;
        }

        public bool PuedeSolicitarPrestamo()
        {
            return EsActivo && !EstaBaneado;
        }

        public bool PuedeSolicitarPrestamo(int prestamosActivos, int limitePrestamos)
        {
            return PuedeSolicitarPrestamo() && prestamosActivos < limitePrestamos;
        }

        internal static void CargarDesdeBD(IEnumerable<Usuario> registros)
        {
            listaUsuarios.Clear();
            listaUsuarios.AddRange(registros);
        }

        private void GuardarCambiosEnBD()
        {
            if (ConexionBD.Habilitada && listaUsuarios.Contains(this))
                RepositorioBD.Actualizar(this);
        }

        public void InsertarRegistro(object objeto)
        {
            if (objeto == null)
                throw new ArgumentNullException(nameof(objeto), "El objeto a insertar no puede ser nulo.");

            if (!(objeto is Usuario usuario))
                throw new ArgumentException("El objeto a insertar no es de tipo Usuario.");

            if (listaUsuarios.Any(u => u.Id == usuario.Id))
                throw new InvalidOperationException($"Ya existe un usuario con el id {usuario.Id}.");

            RepositorioBD.Insertar(usuario);

            listaUsuarios.Add(usuario);
        }

        public object ConsultarRegistro(string id)
        {
            int idBuscado = ConvertirId(id);
            return listaUsuarios.FirstOrDefault(u => u.Id == idBuscado);
        }

        public void ActualizarRegistro(object objeto)
        {
            if (objeto == null)
                throw new ArgumentNullException(nameof(objeto), "El objeto a actualizar no puede ser nulo.");

            if (!(objeto is Usuario usuario))
                throw new ArgumentException("El objeto a actualizar no es de tipo Usuario.");

            int indice = listaUsuarios.FindIndex(u => u.Id == usuario.Id);
            if (indice == -1)
                throw new InvalidOperationException($"No existe un usuario con el id {usuario.Id}.");

            RepositorioBD.Actualizar(usuario);

            listaUsuarios[indice] = usuario;
        }

        public void EliminarRegistro(string id)
        {
            int idBuscado = ConvertirId(id);

            int indice = listaUsuarios.FindIndex(u => u.Id == idBuscado);
            if (indice == -1)
                throw new InvalidOperationException($"No existe un usuario con el id {idBuscado}.");

            RepositorioBD.Eliminar("Usuario", "IdUsuario", idBuscado);

            listaUsuarios.RemoveAt(indice);
        }

        private static int ConvertirId(string id)
        {
            if (!int.TryParse(id, out int resultado) || resultado < 0)
                throw new ArgumentException("El id debe ser un número entero no negativo.");
            return resultado;
        }

        public override string ToString()
        {
            return $"Usuario #{Id}: {nombre} {apellido} | ID Universitario: {idUniversitario} | " +
                   $"Email: {email} | Rol: {rol} | Sanciones: {contadorSanciones} | Baneado: {estaBaneado} | " +
                   $"Estado: {(EsActivo ? "Activo" : "Inactivo")}";
        }
    }
} 