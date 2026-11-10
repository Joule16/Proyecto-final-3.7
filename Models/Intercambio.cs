//Suñiga Maciel Joule Alexander
//Villa Olivares Ariel
//Nuñez Martinez Marco Antonio
//Equipo 2
//=============================
using System;
using SistemaBiblioteca1.Datos;
using System.Collections.Generic;
using System.Linq;
using SistemaBiblioteca1.Models;

namespace SistemaBiblioteca1.Models
{
    public enum EstatusIntercambio
    {
        Pendiente = 0,
        Aceptado = 1,
        Concluido = 2,
        Rechazado = 3,
        Cancelado = 4
    }

    public class Intercambio : EntidadBase, IAlmacenamientoCRUD
    {
        private static List<Intercambio> listaIntercambios = new List<Intercambio>();

        private int idUsuarioSolicitante;
        private int idLibroSolicitado;
        private int? idLibroOfrecido;
        private DateTime fechaSolicitud;
        private DateTime? fechaConclusion;
        private EstatusIntercambio estatus;
        private string puntoEncuentro;

        public int IdIntercambio
        {
            get { return Id; }
            set { Id = value; }
        }

        public int IdUsuarioSolicitante
        {
            get { return idUsuarioSolicitante; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Debe asignarse un usuario solicitante válido.");
                idUsuarioSolicitante = value;
            }
        }

        public int IdLibroSolicitado
        {
            get { return idLibroSolicitado; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Debe asignarse un libro solicitado válido.");
                idLibroSolicitado = value;
            }
        }

        public int? IdLibroOfrecido
        {
            get { return idLibroOfrecido; }
            set
            {
                if (value.HasValue && value.Value <= 0)
                    throw new ArgumentException("El libro ofrecido debe ser un id válido, o quedar vacío si es una donación.");
                idLibroOfrecido = value;
            }
        }

        public bool EsDonacion
        {
            get { return !idLibroOfrecido.HasValue; }
        }

        public DateTime FechaSolicitud
        {
            get { return fechaSolicitud; }
            set
            {
                if (value.Date > DateTime.Today)
                    throw new ArgumentException("La fecha de solicitud no puede ser futura.");
                fechaSolicitud = value.Date;
            }
        }

        public DateTime? FechaConclusion
        {
            get { return fechaConclusion; }
            set
            {
                if (value.HasValue && value.Value.Date > DateTime.Today)
                    throw new ArgumentException("La fecha de conclusión no puede ser futura.");
                fechaConclusion = value.HasValue ? value.Value.Date : (DateTime?)null;
            }
        }

        public EstatusIntercambio Estatus
        {
            get { return estatus; }
            set
            {
                if (!Enum.IsDefined(typeof(EstatusIntercambio), value))
                    throw new ArgumentException("El estatus del intercambio no es válido.");
                estatus = value;
            }
        }

        public string PuntoEncuentro
        {
            get { return puntoEncuentro; }
            set
            {
                string texto = string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim();
                if (texto.Length > 200)
                    throw new ArgumentException("El punto de encuentro o las notas no pueden pasar de 200 caracteres.");
                puntoEncuentro = texto;
            }
        }

        public Intercambio() : base()
        {
            idUsuarioSolicitante = 0;
            idLibroSolicitado = 0;
            idLibroOfrecido = null;
            fechaSolicitud = DateTime.Today;
            fechaConclusion = null;
            estatus = EstatusIntercambio.Pendiente;
            puntoEncuentro = string.Empty;
            EsActivo = true;
        }

        public Intercambio(int idIntercambio, int idUsuarioSolicitante, int idLibroSolicitado,
                           int? idLibroOfrecido, DateTime fechaSolicitud, EstatusIntercambio estatus,
                           DateTime? fechaConclusion, string puntoEncuentro) : base(idIntercambio)
        {
            IdUsuarioSolicitante = idUsuarioSolicitante;
            IdLibroSolicitado = idLibroSolicitado;
            IdLibroOfrecido = idLibroOfrecido;
            FechaSolicitud = fechaSolicitud;
            Estatus = estatus;
            FechaConclusion = fechaConclusion;
            PuntoEncuentro = puntoEncuentro;
            EsActivo = true;

            ValidarCoherencia();
        }

        public void ValidarCoherencia()
        {
            if (idLibroOfrecido.HasValue && idLibroOfrecido.Value == idLibroSolicitado)
                throw new ArgumentException("El libro ofrecido no puede ser el mismo que el solicitado.");

            if (estatus == EstatusIntercambio.Concluido && !fechaConclusion.HasValue)
                throw new ArgumentException("Un intercambio concluido necesita fecha de conclusión.");

            if (estatus != EstatusIntercambio.Concluido && fechaConclusion.HasValue)
                throw new ArgumentException("Solo un intercambio concluido puede tener fecha de conclusión.");

            if (fechaConclusion.HasValue && fechaConclusion.Value.Date < fechaSolicitud.Date)
                throw new ArgumentException("La fecha de conclusión no puede ser anterior a la fecha de solicitud.");
        }

        public void Concluir()
        {
            Concluir(DateTime.Today);
        }

        public void Concluir(DateTime fecha)
        {
            if (estatus == EstatusIntercambio.Rechazado || estatus == EstatusIntercambio.Cancelado)
                throw new InvalidOperationException("Un intercambio rechazado o cancelado no puede concluirse.");

            if (fecha.Date < fechaSolicitud.Date)
                throw new ArgumentException("La fecha de conclusión no puede ser anterior a la fecha de solicitud.");

            FechaConclusion = fecha;
            Estatus = EstatusIntercambio.Concluido;
        }

        public int DiasDeTramite()
        {
            return DiasDeTramite(DateTime.Today);
        }

        public int DiasDeTramite(DateTime fechaReferencia)
        {
            DateTime fin = fechaConclusion.HasValue ? fechaConclusion.Value : fechaReferencia.Date;

            if (fin < fechaSolicitud.Date)
                throw new ArgumentException("La fecha de referencia no puede ser anterior a la solicitud.");

            return (fin - fechaSolicitud.Date).Days;
        }

        internal static void CargarDesdeBD(IEnumerable<Intercambio> registros)
        {
            listaIntercambios.Clear();
            listaIntercambios.AddRange(registros);
        }

        public void InsertarRegistro(object objeto)
        {
            if (objeto == null)
                throw new ArgumentNullException(nameof(objeto), "El objeto a insertar no puede ser nulo.");

            if (!(objeto is Intercambio intercambio))
                throw new ArgumentException("El objeto a insertar no es de tipo Intercambio.");

            intercambio.ValidarCoherencia();

            if (listaIntercambios.Any(i => i.Id == intercambio.Id))
                throw new InvalidOperationException($"Ya existe un intercambio con el id {intercambio.Id}.");

            RepositorioBD.Insertar(intercambio);

            listaIntercambios.Add(intercambio);
        }

        public object ConsultarRegistro(string id)
        {
            int idBuscado = ConvertirId(id);
            return listaIntercambios.FirstOrDefault(i => i.Id == idBuscado);
        }

        public void ActualizarRegistro(object objeto)
        {
            if (objeto == null)
                throw new ArgumentNullException(nameof(objeto), "El objeto a actualizar no puede ser nulo.");

            if (!(objeto is Intercambio intercambio))
                throw new ArgumentException("El objeto a actualizar no es de tipo Intercambio.");

            intercambio.ValidarCoherencia();

            int indice = listaIntercambios.FindIndex(i => i.Id == intercambio.Id);
            if (indice == -1)
                throw new InvalidOperationException($"No existe un intercambio con el id {intercambio.Id}.");

            RepositorioBD.Actualizar(intercambio);

            listaIntercambios[indice] = intercambio;
        }

        public void EliminarRegistro(string id)
        {
            int idBuscado = ConvertirId(id);

            int indice = listaIntercambios.FindIndex(i => i.Id == idBuscado);
            if (indice == -1)
                throw new InvalidOperationException($"No existe un intercambio con el id {idBuscado}.");

            RepositorioBD.Eliminar("Intercambio", "IdIntercambio", idBuscado);

            listaIntercambios.RemoveAt(indice);
        }

        private static int ConvertirId(string id)
        {
            if (!int.TryParse(id, out int resultado) || resultado < 0)
                throw new ArgumentException("El id debe ser un número entero no negativo.");
            return resultado;
        }

        public override string ToString()
        {
            string ofrecido = idLibroOfrecido.HasValue ? "libro #" + idLibroOfrecido.Value : "Donación";
            string conclusion = fechaConclusion.HasValue ? fechaConclusion.Value.ToString("dd/MM/yyyy") : "sin concluir";

            return $"Intercambio #{Id}: usuario #{idUsuarioSolicitante} | Solicitado: libro #{idLibroSolicitado} | " +
                   $"Ofrecido: {ofrecido} | Estatus: {estatus} | Solicitud: {fechaSolicitud:dd/MM/yyyy} | " +
                   $"Conclusión: {conclusion}";
        }
    }
}