//   Equipo #2
//   NUÑEZ Martinez Marco Antonio
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
// ===============================
using MySqlConnector;
using SistemaBiblioteca1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaBiblioteca1.Datos
{
    internal static class RepositorioBD
    {
        private static MySqlParameter P(string nombre, object? valor)
        {
            return ConexionBD.P(nombre, valor);
        }


        public static void Eliminar(string tabla, string columnaId, int id)
        {
            ConexionBD.Ejecutar("DELETE FROM " + tabla + " WHERE " + columnaId + " = @id", P("@id", id));
        }


        public static void Insertar(Administrador a)
        {
            ConexionBD.Ejecutar(
                "INSERT INTO Administrador (IdAdministrador, Nombre, Apellido, UsuarioAcceso, Contrasena, NivelAcceso, FechaRegistro, EsActivo) " +
                "VALUES (@id, @nombre, @apellido, @acceso, @contrasena, @nivel, @registro, @activo)",
                P("@id", a.Id), P("@nombre", a.Nombre), P("@apellido", a.Apellido), P("@acceso", a.UsuarioAcceso),
                P("@contrasena", a.Contrasena), P("@nivel", a.NivelAcceso), P("@registro", a.FechaRegistro), P("@activo", a.EsActivo));
        }

        public static void Actualizar(Administrador a)
        {
            ConexionBD.Ejecutar(
                "UPDATE Administrador SET Nombre = @nombre, Apellido = @apellido, UsuarioAcceso = @acceso, Contrasena = @contrasena, " +
                "NivelAcceso = @nivel, EsActivo = @activo WHERE IdAdministrador = @id",
                P("@id", a.Id), P("@nombre", a.Nombre), P("@apellido", a.Apellido), P("@acceso", a.UsuarioAcceso),
                P("@contrasena", a.Contrasena), P("@nivel", a.NivelAcceso), P("@activo", a.EsActivo));
        }

        private static void CargarAdministradores()
        {
            List<Administrador> lista = ConexionBD.Leer(
                "SELECT IdAdministrador, Nombre, Apellido, UsuarioAcceso, Contrasena, NivelAcceso, FechaRegistro, EsActivo " +
                "FROM Administrador ORDER BY IdAdministrador",
                r =>
                {
                    Administrador a = new Administrador(ConexionBD.Entero(r, 0), ConexionBD.Texto(r, 1), ConexionBD.Texto(r, 2),
                                                        ConexionBD.Texto(r, 3), ConexionBD.Texto(r, 4), ConexionBD.Entero(r, 5),
                                                        ConexionBD.Booleano(r, 7));
                    a.FechaRegistro = ConexionBD.Fecha(r, 6);
                    return a;
                });
            Administrador.CargarDesdeBD(lista);
        }


        public static void Insertar(Autor a)
        {
            ConexionBD.Ejecutar(
                "INSERT INTO Autor (IdAutor, Nombre, Apellido, Nacionalidad, FechaNacimiento, RutaImagen, FechaRegistro, EsActivo) " +
                "VALUES (@id, @nombre, @apellido, @nacionalidad, @nacimiento, @ruta, @registro, @activo)",
                P("@id", a.Id), P("@nombre", a.Nombre), P("@apellido", a.Apellido), P("@nacionalidad", a.Nacionalidad),
                P("@nacimiento", a.FechaNacimiento.Date), P("@ruta", a.RutaImagen), P("@registro", a.FechaRegistro), P("@activo", a.EsActivo));
        }

        public static void Actualizar(Autor a)
        {
            ConexionBD.Ejecutar(
                "UPDATE Autor SET Nombre = @nombre, Apellido = @apellido, Nacionalidad = @nacionalidad, " +
                "FechaNacimiento = @nacimiento, RutaImagen = @ruta, EsActivo = @activo WHERE IdAutor = @id",
                P("@id", a.Id), P("@nombre", a.Nombre), P("@apellido", a.Apellido), P("@nacionalidad", a.Nacionalidad),
                P("@nacimiento", a.FechaNacimiento.Date), P("@ruta", a.RutaImagen), P("@activo", a.EsActivo));
        }

        private static void CargarAutores()
        {
            List<Autor> lista = ConexionBD.Leer(
                "SELECT IdAutor, Nombre, Apellido, Nacionalidad, FechaNacimiento, RutaImagen, FechaRegistro, EsActivo " +
                "FROM Autor ORDER BY IdAutor",
                r =>
                {
                    Autor a = new Autor(ConexionBD.Entero(r, 0), ConexionBD.Texto(r, 1), ConexionBD.Texto(r, 2),
                                        ConexionBD.Texto(r, 3), ConexionBD.Fecha(r, 4), ConexionBD.Booleano(r, 7));
                    a.RutaImagen = ConexionBD.Texto(r, 5);
                    a.FechaRegistro = ConexionBD.Fecha(r, 6);
                    return a;
                });
            Autor.CargarDesdeBD(lista);
        }


        public static void Insertar(Categoria c)
        {
            ConexionBD.Ejecutar(
                "INSERT INTO Categoria (IdCategoria, Nombre, Descripcion, RestringidaMenores, FechaRegistro, EsActivo) " +
                "VALUES (@id, @nombre, @descripcion, @restringida, @registro, @activo)",
                P("@id", c.Id), P("@nombre", c.Nombre), P("@descripcion", c.Descripcion), P("@restringida", c.RestringidaMenores),
                P("@registro", c.FechaRegistro), P("@activo", c.EsActivo));
        }

        public static void Actualizar(Categoria c)
        {
            ConexionBD.Ejecutar(
                "UPDATE Categoria SET Nombre = @nombre, Descripcion = @descripcion, RestringidaMenores = @restringida, " +
                "EsActivo = @activo WHERE IdCategoria = @id",
                P("@id", c.Id), P("@nombre", c.Nombre), P("@descripcion", c.Descripcion),
                P("@restringida", c.RestringidaMenores), P("@activo", c.EsActivo));
        }

        private static void CargarCategorias()
        {
            List<Categoria> lista = ConexionBD.Leer(
                "SELECT IdCategoria, Nombre, Descripcion, RestringidaMenores, FechaRegistro, EsActivo FROM Categoria ORDER BY IdCategoria",
                r =>
                {
                    Categoria c = new Categoria(ConexionBD.Entero(r, 0), ConexionBD.Texto(r, 1), ConexionBD.Texto(r, 2),
                                                ConexionBD.Booleano(r, 3), ConexionBD.Booleano(r, 5));
                    c.FechaRegistro = ConexionBD.Fecha(r, 4);
                    return c;
                });
            Categoria.CargarDesdeBD(lista);
        }


        public static void Insertar(Editorial e)
        {
            ConexionBD.Ejecutar(
                "INSERT INTO Editorial (IdEditorial, Nombre, PaisOrigen, AnioFundacion, FechaRegistro, EsActivo) " +
                "VALUES (@id, @nombre, @pais, @anio, @registro, @activo)",
                P("@id", e.Id), P("@nombre", e.Nombre), P("@pais", e.PaisOrigen), P("@anio", (int)e.AnioFundacion),
                P("@registro", e.FechaRegistro), P("@activo", e.EsActivo));
        }

        public static void Actualizar(Editorial e)
        {
            ConexionBD.Ejecutar(
                "UPDATE Editorial SET Nombre = @nombre, PaisOrigen = @pais, AnioFundacion = @anio, EsActivo = @activo " +
                "WHERE IdEditorial = @id",
                P("@id", e.Id), P("@nombre", e.Nombre), P("@pais", e.PaisOrigen),
                P("@anio", (int)e.AnioFundacion), P("@activo", e.EsActivo));
        }

        private static void CargarEditoriales()
        {
            List<Editorial> lista = ConexionBD.Leer(
                "SELECT IdEditorial, Nombre, PaisOrigen, AnioFundacion, FechaRegistro, EsActivo FROM Editorial ORDER BY IdEditorial",
                r =>
                {
                    Editorial e = new Editorial(ConexionBD.Entero(r, 0), ConexionBD.Texto(r, 1), ConexionBD.Texto(r, 2),
                                                ConexionBD.Entero(r, 3), ConexionBD.Booleano(r, 5));
                    e.FechaRegistro = ConexionBD.Fecha(r, 4);
                    return e;
                });
            Editorial.CargarDesdeBD(lista);
        }


        public static void Insertar(Libro l)
        {
            ConexionBD.Ejecutar(
                "INSERT INTO Libro (IdLibro, Titulo, Isbn, IdAutor, IdCategoria, IdEditorial, AnioPublicacion, PuntoEntrega, FechaRegistro, EsActivo) " +
                "VALUES (@id, @titulo, @isbn, @autor, @categoria, @editorial, @anio, @punto, @registro, @activo)",
                P("@id", l.Id), P("@titulo", l.Titulo), P("@isbn", l.Isbn), P("@autor", l.IdAutor), P("@categoria", l.IdCategoria),
                P("@editorial", l.IdEditorial), P("@anio", l.AnioPublicacion), P("@punto", l.PuntoEntrega),
                P("@registro", l.FechaRegistro), P("@activo", l.EsActivo));
        }

        public static void Actualizar(Libro l)
        {
            ConexionBD.Ejecutar(
                "UPDATE Libro SET Titulo = @titulo, Isbn = @isbn, IdAutor = @autor, IdCategoria = @categoria, IdEditorial = @editorial, " +
                "AnioPublicacion = @anio, PuntoEntrega = @punto, EsActivo = @activo WHERE IdLibro = @id",
                P("@id", l.Id), P("@titulo", l.Titulo), P("@isbn", l.Isbn), P("@autor", l.IdAutor), P("@categoria", l.IdCategoria),
                P("@editorial", l.IdEditorial), P("@anio", l.AnioPublicacion), P("@punto", l.PuntoEntrega), P("@activo", l.EsActivo));
        }

        private static void CargarLibros()
        {
            List<Libro> lista = ConexionBD.Leer(
                "SELECT IdLibro, Titulo, Isbn, IdAutor, IdCategoria, IdEditorial, AnioPublicacion, PuntoEntrega, FechaRegistro, EsActivo " +
                "FROM Libro ORDER BY IdLibro",
                r =>
                {
                    Libro l = new Libro(ConexionBD.Entero(r, 0), ConexionBD.Texto(r, 1), ConexionBD.Texto(r, 2),
                                        ConexionBD.Entero(r, 3), ConexionBD.Entero(r, 4), ConexionBD.Entero(r, 5),
                                        ConexionBD.Entero(r, 6), ConexionBD.Booleano(r, 9), ConexionBD.Texto(r, 7));
                    l.FechaRegistro = ConexionBD.Fecha(r, 8);
                    return l;
                });
            Libro.CargarDesdeBD(lista);
        }


        public static void Insertar(Ejemplar e)
        {
            ConexionBD.Ejecutar(
                "INSERT INTO Ejemplar (IdEjemplar, IdLibro, CodigoInventario, Condicion, Disponible, FechaRegistro, EsActivo) " +
                "VALUES (@id, @libro, @codigo, @condicion, @disponible, @registro, @activo)",
                P("@id", e.Id), P("@libro", e.IdLibro), P("@codigo", e.CodigoInventario), P("@condicion", e.Condicion),
                P("@disponible", e.Disponible), P("@registro", e.FechaRegistro), P("@activo", e.EsActivo));
        }

        public static void Actualizar(Ejemplar e)
        {
            ConexionBD.Ejecutar(
                "UPDATE Ejemplar SET IdLibro = @libro, CodigoInventario = @codigo, Condicion = @condicion, " +
                "Disponible = @disponible, EsActivo = @activo WHERE IdEjemplar = @id",
                P("@id", e.Id), P("@libro", e.IdLibro), P("@codigo", e.CodigoInventario), P("@condicion", e.Condicion),
                P("@disponible", e.Disponible), P("@activo", e.EsActivo));
        }

        private static void CargarEjemplares()
        {
            List<Ejemplar> lista = ConexionBD.Leer(
                "SELECT IdEjemplar, IdLibro, CodigoInventario, Condicion, Disponible, FechaRegistro, EsActivo FROM Ejemplar ORDER BY IdEjemplar",
                r =>
                {
                    Ejemplar e = new Ejemplar(ConexionBD.Entero(r, 0), ConexionBD.Entero(r, 1), ConexionBD.Texto(r, 2),
                                              ConexionBD.Texto(r, 3), ConexionBD.Booleano(r, 4), ConexionBD.Booleano(r, 6));
                    e.FechaRegistro = ConexionBD.Fecha(r, 5);
                    return e;
                });
            Ejemplar.CargarDesdeBD(lista);
        }


        public static void Insertar(Usuario u)
        {
            ConexionBD.Ejecutar(
                "INSERT INTO Usuario (IdUsuario, Nombre, Apellido, Email, Telefono, IdUniversitario, ContadorSanciones, EstaBaneado, " +
                "MotivoBaneo, Rol, ContrasenaHash, FechaRegistro, EsActivo) " +
                "VALUES (@id, @nombre, @apellido, @email, @telefono, @universitario, @contador, @baneado, @motivo, @rol, @hash, @registro, @activo)",
                P("@id", u.Id), P("@nombre", u.Nombre), P("@apellido", u.Apellido), P("@email", u.Email), P("@telefono", u.Telefono),
                P("@universitario", u.IdUniversitario), P("@contador", u.ContadorSanciones), P("@baneado", u.EstaBaneado),
                P("@motivo", u.MotivoBaneo), P("@rol", (int)u.Rol), P("@hash", u.ContrasenaHash),
                P("@registro", u.FechaRegistro), P("@activo", u.EsActivo));
        }

        public static void Actualizar(Usuario u)
        {
            ConexionBD.Ejecutar(
                "UPDATE Usuario SET Nombre = @nombre, Apellido = @apellido, Email = @email, Telefono = @telefono, " +
                "IdUniversitario = @universitario, ContadorSanciones = @contador, EstaBaneado = @baneado, MotivoBaneo = @motivo, " +
                "Rol = @rol, ContrasenaHash = @hash, EsActivo = @activo WHERE IdUsuario = @id",
                P("@id", u.Id), P("@nombre", u.Nombre), P("@apellido", u.Apellido), P("@email", u.Email), P("@telefono", u.Telefono),
                P("@universitario", u.IdUniversitario), P("@contador", u.ContadorSanciones), P("@baneado", u.EstaBaneado),
                P("@motivo", u.MotivoBaneo), P("@rol", (int)u.Rol), P("@hash", u.ContrasenaHash), P("@activo", u.EsActivo));
        }

        private static void CargarUsuarios()
        {
            List<Usuario> lista = ConexionBD.Leer(
                "SELECT IdUsuario, Nombre, Apellido, Email, Telefono, IdUniversitario, ContadorSanciones, EstaBaneado, " +
                "MotivoBaneo, Rol, ContrasenaHash, FechaRegistro, EsActivo FROM Usuario ORDER BY IdUsuario",
                r =>
                {
                    Usuario u = new Usuario(ConexionBD.Entero(r, 0), ConexionBD.Texto(r, 1), ConexionBD.Texto(r, 2),
                                            ConexionBD.Texto(r, 3), ConexionBD.Texto(r, 4), ConexionBD.Texto(r, 5),
                                            ConexionBD.Booleano(r, 12), (RolUsuario)ConexionBD.Entero(r, 9));
                    u.ContadorSanciones = ConexionBD.Entero(r, 6);
                    u.EstaBaneado = ConexionBD.Booleano(r, 7);
                    u.MotivoBaneo = ConexionBD.Texto(r, 8);
                    u.ContrasenaHash = ConexionBD.Texto(r, 10);
                    u.FechaRegistro = ConexionBD.Fecha(r, 11);
                    return u;
                });
            Usuario.CargarDesdeBD(lista);
        }


        public static void Insertar(Prestamo p)
        {
            ConexionBD.EjecutarEnTransaccion((conexion, transaccion) =>
            {
                ConexionBD.EjecutarComando(conexion, transaccion,
                    "INSERT INTO Prestamo (IdPrestamo, IdUsuario, IdEjemplar, FechaPrestamo, FechaLimite, FechaDevolucionReal, Devuelto, FechaRegistro, EsActivo) " +
                    "VALUES (@id, @usuario, @ejemplar, @prestamo, @limite, @devolucion, @devuelto, @registro, @activo)",
                    P("@id", p.Id), P("@usuario", p.IdUsuario), P("@ejemplar", p.IdEjemplar), P("@prestamo", p.FechaPrestamo.Date),
                    P("@limite", p.FechaLimite.Date), P("@devolucion", p.FechaDevolucionReal.HasValue ? p.FechaDevolucionReal.Value.Date : (DateTime?)null),
                    P("@devuelto", p.Devuelto), P("@registro", p.FechaRegistro), P("@activo", p.EsActivo));

                GuardarDetalleEHistorial(conexion, transaccion, p);
            });
        }

        public static void Actualizar(Prestamo p)
        {
            ConexionBD.EjecutarEnTransaccion((conexion, transaccion) =>
            {
                ConexionBD.EjecutarComando(conexion, transaccion,
                    "UPDATE Prestamo SET IdUsuario = @usuario, IdEjemplar = @ejemplar, FechaPrestamo = @prestamo, FechaLimite = @limite, " +
                    "FechaDevolucionReal = @devolucion, Devuelto = @devuelto, EsActivo = @activo WHERE IdPrestamo = @id",
                    P("@id", p.Id), P("@usuario", p.IdUsuario), P("@ejemplar", p.IdEjemplar), P("@prestamo", p.FechaPrestamo.Date),
                    P("@limite", p.FechaLimite.Date), P("@devolucion", p.FechaDevolucionReal.HasValue ? p.FechaDevolucionReal.Value.Date : (DateTime?)null),
                    P("@devuelto", p.Devuelto), P("@activo", p.EsActivo));

                GuardarDetalleEHistorial(conexion, transaccion, p);
            });
        }

        private static void GuardarDetalleEHistorial(MySqlConnection conexion, MySqlTransaction transaccion, Prestamo p)
        {
            ConexionBD.EjecutarComando(conexion, transaccion, "DELETE FROM DetallePrestamo WHERE IdPrestamo = @id", P("@id", p.Id));

            HashSet<int> ejemplaresGuardados = new HashSet<int>();
            foreach (Ejemplar ejemplar in p.ListaEjemplares)
            {
                if (ejemplaresGuardados.Add(ejemplar.Id))
                {
                    ConexionBD.EjecutarComando(conexion, transaccion,
                        "INSERT INTO DetallePrestamo (IdPrestamo, IdEjemplar) VALUES (@prestamo, @ejemplar)",
                        P("@prestamo", p.Id), P("@ejemplar", ejemplar.Id));
                }
            }

            ConexionBD.EjecutarComando(conexion, transaccion, "DELETE FROM HistorialPrestamo WHERE IdPrestamo = @id", P("@id", p.Id));

            string[] acciones = p.HistorialCambios.ToArray();
            Array.Reverse(acciones);
            foreach (string accion in acciones)
            {
                ConexionBD.EjecutarComando(conexion, transaccion,
                    "INSERT INTO HistorialPrestamo (IdPrestamo, Accion) VALUES (@prestamo, @accion)",
                    P("@prestamo", p.Id), P("@accion", accion));
            }
        }

        private static void CargarPrestamos()
        {
            List<Prestamo> lista = ConexionBD.Leer(
                "SELECT IdPrestamo, IdUsuario, IdEjemplar, FechaPrestamo, FechaLimite, FechaDevolucionReal, Devuelto, FechaRegistro, EsActivo " +
                "FROM Prestamo ORDER BY IdPrestamo",
                r =>
                {
                    Prestamo p = new Prestamo(ConexionBD.Entero(r, 0), ConexionBD.Entero(r, 1), ConexionBD.Entero(r, 2),
                                              ConexionBD.Fecha(r, 3), ConexionBD.Fecha(r, 4), ConexionBD.Booleano(r, 8));
                    p.FechaDevolucionReal = ConexionBD.FechaNula(r, 5);
                    p.Devuelto = ConexionBD.Booleano(r, 6);
                    p.FechaRegistro = ConexionBD.Fecha(r, 7);
                    return p;
                });

            Dictionary<int, Prestamo> porId = lista.ToDictionary(p => p.Id);

            Ejemplar buscador = new Ejemplar();
            List<(int, int)> detalle = ConexionBD.Leer(
                "SELECT IdPrestamo, IdEjemplar FROM DetallePrestamo ORDER BY IdPrestamo, IdEjemplar",
                r => (ConexionBD.Entero(r, 0), ConexionBD.Entero(r, 1)));
            foreach ((int idPrestamo, int idEjemplar) in detalle)
            {
                if (porId.TryGetValue(idPrestamo, out Prestamo? prestamo) &&
                    buscador.ConsultarRegistro(idEjemplar.ToString()) is Ejemplar ejemplar)
                {
                    prestamo.RegistrarEjemplar(ejemplar);
                }
            }

            List<(int, string)> historial = ConexionBD.Leer(
                "SELECT IdPrestamo, Accion FROM HistorialPrestamo ORDER BY IdHistorial",
                r => (ConexionBD.Entero(r, 0), ConexionBD.Texto(r, 1)));
            foreach ((int idPrestamo, string accion) in historial)
            {
                if (porId.TryGetValue(idPrestamo, out Prestamo? prestamo))
                    prestamo.ApilarAccion(accion);
            }

            Prestamo.CargarDesdeBD(lista);
        }


        public static void Insertar(Sancion s)
        {
            ConexionBD.Ejecutar(
                "INSERT INTO Sancion (IdSancion, IdUsuario, IdPrestamo, Motivo, DiasRetraso, PorcentajePenalizacionCondicion, " +
                "FechaInicio, FechaFinDesactivacion, Pagada, FechaRegistro, EsActivo) " +
                "VALUES (@id, @usuario, @prestamo, @motivo, @dias, @porcentaje, @inicio, @fin, @pagada, @registro, @activo)",
                P("@id", s.Id), P("@usuario", s.IdUsuario), P("@prestamo", s.IdPrestamo), P("@motivo", s.Motivo),
                P("@dias", s.DiasRetraso), P("@porcentaje", s.PorcentajePenalizacionCondicion), P("@inicio", s.FechaInicio),
                P("@fin", s.FechaFinDesactivacion), P("@pagada", s.Pagada), P("@registro", s.FechaRegistro), P("@activo", s.EsActivo));
        }

        public static void Actualizar(Sancion s)
        {
            ConexionBD.Ejecutar(
                "UPDATE Sancion SET IdUsuario = @usuario, IdPrestamo = @prestamo, Motivo = @motivo, DiasRetraso = @dias, " +
                "PorcentajePenalizacionCondicion = @porcentaje, FechaInicio = @inicio, FechaFinDesactivacion = @fin, " +
                "Pagada = @pagada, EsActivo = @activo WHERE IdSancion = @id",
                P("@id", s.Id), P("@usuario", s.IdUsuario), P("@prestamo", s.IdPrestamo), P("@motivo", s.Motivo),
                P("@dias", s.DiasRetraso), P("@porcentaje", s.PorcentajePenalizacionCondicion), P("@inicio", s.FechaInicio),
                P("@fin", s.FechaFinDesactivacion), P("@pagada", s.Pagada), P("@activo", s.EsActivo));
        }

        private static void CargarSanciones()
        {
            List<Sancion> lista = ConexionBD.Leer(
                "SELECT IdSancion, IdUsuario, IdPrestamo, Motivo, DiasRetraso, PorcentajePenalizacionCondicion, " +
                "FechaInicio, FechaFinDesactivacion, Pagada, FechaRegistro, EsActivo FROM Sancion ORDER BY IdSancion",
                r =>
                {
                    Sancion s = new Sancion(ConexionBD.Entero(r, 0), ConexionBD.Entero(r, 1), ConexionBD.Entero(r, 2),
                                            ConexionBD.Texto(r, 3), ConexionBD.Entero(r, 4), ConexionBD.Real(r, 5),
                                            ConexionBD.Booleano(r, 10));
                    s.FechaInicio = ConexionBD.Fecha(r, 6);
                    s.FechaFinDesactivacion = ConexionBD.Fecha(r, 7);
                    s.Pagada = ConexionBD.Booleano(r, 8);
                    s.FechaRegistro = ConexionBD.Fecha(r, 9);
                    return s;
                });
            Sancion.CargarDesdeBD(lista);
        }


        public static void Insertar(Pago p)
        {
            ConexionBD.Ejecutar(
                "INSERT INTO Pago (IdPago, IdSancion, Monto, FechaPago, MetodoPago, FechaRegistro, EsActivo) " +
                "VALUES (@id, @sancion, @monto, @fecha, @metodo, @registro, @activo)",
                P("@id", p.Id), P("@sancion", p.IdSancion), P("@monto", p.Monto), P("@fecha", p.FechaPago),
                P("@metodo", p.MetodoPago), P("@registro", p.FechaRegistro), P("@activo", p.EsActivo));
        }

        public static void Actualizar(Pago p)
        {
            ConexionBD.Ejecutar(
                "UPDATE Pago SET IdSancion = @sancion, Monto = @monto, FechaPago = @fecha, MetodoPago = @metodo, EsActivo = @activo " +
                "WHERE IdPago = @id",
                P("@id", p.Id), P("@sancion", p.IdSancion), P("@monto", p.Monto), P("@fecha", p.FechaPago),
                P("@metodo", p.MetodoPago), P("@activo", p.EsActivo));
        }

        private static void CargarPagos()
        {
            List<Pago> lista = ConexionBD.Leer(
                "SELECT IdPago, IdSancion, Monto, FechaPago, MetodoPago, FechaRegistro, EsActivo FROM Pago ORDER BY IdPago",
                r =>
                {
                    Pago p = new Pago(ConexionBD.Entero(r, 0), ConexionBD.Entero(r, 1), ConexionBD.Real(r, 2),
                                      ConexionBD.Texto(r, 4), ConexionBD.Booleano(r, 6));
                    p.FechaPago = ConexionBD.Fecha(r, 3);
                    p.FechaRegistro = ConexionBD.Fecha(r, 5);
                    return p;
                });
            Pago.CargarDesdeBD(lista);
        }


        public static void Insertar(Intercambio i)
        {
            ConexionBD.Ejecutar(
                "INSERT INTO Intercambio (IdIntercambio, IdUsuarioSolicitante, IdLibroSolicitado, IdLibroOfrecido, FechaSolicitud, " +
                "FechaConclusion, Estatus, PuntoEncuentro, FechaRegistro, EsActivo) " +
                "VALUES (@id, @solicitante, @solicitado, @ofrecido, @solicitud, @conclusion, @estatus, @punto, @registro, @activo)",
                P("@id", i.Id), P("@solicitante", i.IdUsuarioSolicitante), P("@solicitado", i.IdLibroSolicitado),
                P("@ofrecido", i.IdLibroOfrecido), P("@solicitud", i.FechaSolicitud.Date),
                P("@conclusion", i.FechaConclusion.HasValue ? i.FechaConclusion.Value.Date : (DateTime?)null),
                P("@estatus", (int)i.Estatus), P("@punto", i.PuntoEncuentro), P("@registro", i.FechaRegistro), P("@activo", i.EsActivo));
        }

        public static void Actualizar(Intercambio i)
        {
            ConexionBD.Ejecutar(
                "UPDATE Intercambio SET IdUsuarioSolicitante = @solicitante, IdLibroSolicitado = @solicitado, IdLibroOfrecido = @ofrecido, " +
                "FechaSolicitud = @solicitud, FechaConclusion = @conclusion, Estatus = @estatus, PuntoEncuentro = @punto, " +
                "EsActivo = @activo WHERE IdIntercambio = @id",
                P("@id", i.Id), P("@solicitante", i.IdUsuarioSolicitante), P("@solicitado", i.IdLibroSolicitado),
                P("@ofrecido", i.IdLibroOfrecido), P("@solicitud", i.FechaSolicitud.Date),
                P("@conclusion", i.FechaConclusion.HasValue ? i.FechaConclusion.Value.Date : (DateTime?)null),
                P("@estatus", (int)i.Estatus), P("@punto", i.PuntoEncuentro), P("@activo", i.EsActivo));
        }

        private static void CargarIntercambios()
        {
            List<Intercambio> lista = ConexionBD.Leer(
                "SELECT IdIntercambio, IdUsuarioSolicitante, IdLibroSolicitado, IdLibroOfrecido, FechaSolicitud, FechaConclusion, " +
                "Estatus, PuntoEncuentro, FechaRegistro, EsActivo FROM Intercambio ORDER BY IdIntercambio",
                r =>
                {
                    Intercambio i = new Intercambio(ConexionBD.Entero(r, 0), ConexionBD.Entero(r, 1), ConexionBD.Entero(r, 2),
                                                    ConexionBD.EnteroNulo(r, 3), ConexionBD.Fecha(r, 4),
                                                    (EstatusIntercambio)ConexionBD.Entero(r, 6), ConexionBD.FechaNula(r, 5),
                                                    ConexionBD.Texto(r, 7));
                    i.FechaRegistro = ConexionBD.Fecha(r, 8);
                    i.EsActivo = ConexionBD.Booleano(r, 9);
                    return i;
                });
            Intercambio.CargarDesdeBD(lista);
        }


        public static void CargarTodo()
        {
            if (!ConexionBD.Habilitada)
                return;

            CargarAdministradores();
            CargarAutores();
            CargarCategorias();
            CargarEditoriales();
            CargarLibros();
            CargarEjemplares();
            CargarUsuarios();
            CargarPrestamos();
            CargarSanciones();
            CargarPagos();
            CargarIntercambios();
        }
    }
}
