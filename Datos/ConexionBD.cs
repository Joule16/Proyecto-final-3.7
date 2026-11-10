//   Equipo #2
//   NUÑEZ Martinez Marco Antonio
//   SUÑIGA Maciel Joule Alexander
//   VILLA Olivarez Ariel
// ===============================
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SistemaBiblioteca1.Datos
{
    public static class ConexionBD
    {
        private const string ARCHIVO_CONFIGURACION = "appsettings.json";

        private static string cadenaConexion = string.Empty;

        public static bool Habilitada { get; private set; }

        public static string Destino { get; private set; } = string.Empty;

        public static bool Inicializar(out string mensaje)
        {
            Habilitada = false;

            try
            {
                cadenaConexion = ConstruirCadenaConexion();

                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand("SELECT 1", conexion))
                    {
                        comando.ExecuteScalar();
                    }
                }

                Habilitada = true;
                mensaje = "Conexión establecida con " + Destino + ".";
                return true;
            }
            catch (MySqlException ex)
            {
                mensaje = "No se pudo conectar con MariaDB (" + Destino + "):\n" + TraducirError(ex);
                return false;
            }
            catch (Exception ex)
            {
                mensaje = "No se pudo preparar la conexión con MariaDB:\n" + ex.Message;
                return false;
            }
        }

        public static void Deshabilitar()
        {
            Habilitada = false;
        }

        private static string ConstruirCadenaConexion()
        {
            string servidor = "localhost";
            uint puerto = 3306;
            string baseDatos = "BibliotecaDB";
            string usuario = "root";
            string contrasena = string.Empty;

            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ARCHIVO_CONFIGURACION);

            if (File.Exists(ruta))
            {
                using (JsonDocument documento = JsonDocument.Parse(File.ReadAllText(ruta)))
                {
                    if (documento.RootElement.TryGetProperty("BaseDatos", out JsonElement config))
                    {
                        servidor = LeerTexto(config, "Servidor", servidor);
                        baseDatos = LeerTexto(config, "NombreBD", baseDatos);
                        usuario = LeerTexto(config, "Usuario", usuario);
                        contrasena = LeerTexto(config, "Contrasena", contrasena);

                        string puertoTexto = LeerTexto(config, "Puerto", puerto.ToString());
                        if (!uint.TryParse(puertoTexto, out puerto))
                            throw new InvalidOperationException("El puerto de appsettings.json no es un número válido.");
                    }
                }
            }

            Destino = servidor + ":" + puerto + " / " + baseDatos;

            MySqlConnectionStringBuilder constructor = new MySqlConnectionStringBuilder();
            constructor.Server = servidor;
            constructor.Port = puerto;
            constructor.Database = baseDatos;
            constructor.UserID = usuario;
            constructor.Password = contrasena;
            constructor.ConnectionTimeout = 5;
            constructor.DefaultCommandTimeout = 15;

            return constructor.ConnectionString;
        }

        private static string LeerTexto(JsonElement config, string nombre, string valorPorDefecto)
        {
            if (!config.TryGetProperty(nombre, out JsonElement valor))
                return valorPorDefecto;

            if (valor.ValueKind == JsonValueKind.String)
                return valor.GetString() ?? valorPorDefecto;

            if (valor.ValueKind == JsonValueKind.Number)
                return valor.GetRawText();

            return valorPorDefecto;
        }

        public static MySqlConnection CrearConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }

        public static MySqlParameter P(string nombre, object? valor)
        {
            return new MySqlParameter(nombre, valor ?? DBNull.Value);
        }

        public static int Ejecutar(string sql, params MySqlParameter[] parametros)
        {
            if (!Habilitada)
                return 0;

            try
            {
                using (MySqlConnection conexion = CrearConexion())
                {
                    conexion.Open();
                    return EjecutarComando(conexion, null, sql, parametros);
                }
            }
            catch (MySqlException ex)
            {
                throw new InvalidOperationException(TraducirError(ex), ex);
            }
        }

        public static void EjecutarEnTransaccion(Action<MySqlConnection, MySqlTransaction> operaciones)
        {
            if (!Habilitada)
                return;

            try
            {
                using (MySqlConnection conexion = CrearConexion())
                {
                    conexion.Open();
                    using (MySqlTransaction transaccion = conexion.BeginTransaction())
                    {
                        try
                        {
                            operaciones(conexion, transaccion);
                            transaccion.Commit();
                        }
                        catch
                        {
                            transaccion.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new InvalidOperationException(TraducirError(ex), ex);
            }
        }

        public static int EjecutarComando(MySqlConnection conexion, MySqlTransaction? transaccion,
                                          string sql, params MySqlParameter[] parametros)
        {
            using (MySqlCommand comando = new MySqlCommand(sql, conexion, transaccion))
            {
                comando.Parameters.AddRange(parametros);
                return comando.ExecuteNonQuery();
            }
        }

        public static List<T> Leer<T>(string sql, Func<MySqlDataReader, T> convertir, params MySqlParameter[] parametros)
        {
            List<T> resultado = new List<T>();

            if (!Habilitada)
                return resultado;

            try
            {
                using (MySqlConnection conexion = CrearConexion())
                {
                    conexion.Open();
                    using (MySqlCommand comando = new MySqlCommand(sql, conexion))
                    {
                        comando.Parameters.AddRange(parametros);
                        using (MySqlDataReader lector = comando.ExecuteReader())
                        {
                            while (lector.Read())
                                resultado.Add(convertir(lector));
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new InvalidOperationException(TraducirError(ex), ex);
            }

            return resultado;
        }


        public static string Texto(MySqlDataReader r, int i)
        {
            return r.IsDBNull(i) ? string.Empty : (Convert.ToString(r.GetValue(i)) ?? string.Empty);
        }

        public static int Entero(MySqlDataReader r, int i)
        {
            return Convert.ToInt32(r.GetValue(i));
        }

        public static int? EnteroNulo(MySqlDataReader r, int i)
        {
            return r.IsDBNull(i) ? (int?)null : Convert.ToInt32(r.GetValue(i));
        }

        public static bool Booleano(MySqlDataReader r, int i)
        {
            return Convert.ToBoolean(r.GetValue(i));
        }

        public static double Real(MySqlDataReader r, int i)
        {
            return Convert.ToDouble(r.GetValue(i));
        }

        public static DateTime Fecha(MySqlDataReader r, int i)
        {
            return r.GetDateTime(i);
        }

        public static DateTime? FechaNula(MySqlDataReader r, int i)
        {
            return r.IsDBNull(i) ? (DateTime?)null : r.GetDateTime(i);
        }


        public static string TraducirError(MySqlException ex)
        {
            switch (ex.Number)
            {
                case 1062:
                    return "Ya existe un registro con " + DescribirValorUnico(ex.Message) + ".";
                case 1452:
                    return "El registro hace referencia a un dato que no existe (revisa los ids relacionados).";
                case 1451:
                    return "No se puede eliminar porque otros registros dependen de este.";
                case 3819:
                case 4025:
                    return "Un dato no cumple las restricciones de la base de datos (revisa formatos, longitudes y rangos).";
                case 1406:
                    return "Un dato es más largo de lo que permite la base de datos.";
                case 1045:
                    return "Usuario o contraseña de MariaDB incorrectos. Revisa appsettings.json.";
                case 1049:
                    return "La base de datos no existe. Ejecuta el script BibliotecaDB_MariaDB.sql.";
                case 1146:
                    return "Falta una tabla en la base de datos. Ejecuta el script BibliotecaDB_MariaDB.sql.";
                case 1042:
                    return "No se pudo contactar con el servidor. Verifica que MariaDB esté iniciado y que el servidor y el puerto de appsettings.json sean correctos.";
                default:
                    return "Error de la base de datos (" + ex.Number + "): " + ex.Message;
            }
        }

        private static string DescribirValorUnico(string mensaje)
        {
            if (mensaje.Contains("UQ_Usuario_Email")) return "ese correo electrónico";
            if (mensaje.Contains("UQ_Usuario_IdUniv")) return "ese id universitario";
            if (mensaje.Contains("UQ_Libro_Isbn")) return "ese ISBN";
            if (mensaje.Contains("UQ_Ejemplar_Codigo")) return "ese código de inventario";
            if (mensaje.Contains("UQ_Administrador_Acceso")) return "ese usuario de acceso";
            if (mensaje.Contains("PRIMARY")) return "ese id";
            return "ese valor (debe ser único)";
        }
    }
}
