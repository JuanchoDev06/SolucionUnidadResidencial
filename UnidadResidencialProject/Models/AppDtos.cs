namespace UnidadResidencialProject.Models
{
    // ── Shared ────────────────────────────────────────────
    public class RolDto
    {
        public int RolId { get; set; }
        public string Nombre { get; set; } = "";
    }

    public class ConjuntoDto
    {
        public int ConjuntoId { get; set; }
        public string Nombre { get; set; } = "";
        public string Direccion { get; set; } = "";
        public string Ciudad { get; set; } = "";
        public string Nit { get; set; } = "";
        public string Telefono { get; set; } = "";
    }

    public class TorreDto
    {
        public int TorreId { get; set; }
        public int ConjuntoId { get; set; }
        public string Nombre { get; set; } = "";
        public ConjuntoDto? Conjunto { get; set; }
    }

    public class ApartamentoDto
    {
        public int UnidadId { get; set; }
        public int TorreId { get; set; }
        public string Numero { get; set; } = "";
        public string Tipo { get; set; } = "";
        public double Area { get; set; }
        public TorreDto? Torre { get; set; }
    }

    // ── Usuario / Residente ───────────────────────────────
    public class UsuarioDto
    {
        public int UsuarioId { get; set; }
        public int RolId { get; set; }
        public string Nombre { get; set; } = "";
        public string Documento { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telefono { get; set; } = "";
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string PasswordHash { get; set; } = "";
        public RolDto? Rol { get; set; }
        public List<ResidenteUnidadDto>? ResidentesUnidad { get; set; }
    }

    public class ResidenteUnidadDto
    {
        public int ResidenteUnidadId { get; set; }
        public int UsuarioId { get; set; }
        public int UnidadId { get; set; }
        public bool EsPropietario { get; set; }
        public ApartamentoDto? Unidad { get; set; }
    }

    public class UsuarioCreateDto
    {
        public string Nombre { get; set; } = "";
        public string Documento { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telefono { get; set; } = "";
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public int RolId { get; set; }
        public string PasswordHash { get; set; } = "";
    }

    // ── Ingreso ───────────────────────────────────────────
    public class IngresoDto
    {
        public int IngresoId { get; set; }
        public string Tipo { get; set; } = "";
        public string NombrePersona { get; set; } = "";
        public string Documento { get; set; } = "";
        public DateTime FechaHoraIngreso { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
        public int UsuarioId { get; set; }
        public int? UnidadId { get; set; }
        public UsuarioDto? Vigilante { get; set; }
        public ApartamentoDto? Unidad { get; set; }
    }

    public class IngresoCreateDto
    {
        public string Tipo { get; set; } = "";
        public string NombrePersona { get; set; } = "";
        public string Documento { get; set; } = "";
        public DateTime FechaHoraIngreso { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
        public int UsuarioId { get; set; }
        public int? UnidadId { get; set; }
    }

    // ── Parqueadero ───────────────────────────────────────
    public class ParqueaderoDto
    {
        public int ParqueaderoId { get; set; }
        public string Tipo { get; set; } = "";
        public string Numero { get; set; } = ""; // ← cambiar int por string
        public int? UnidadId { get; set; }
        public ApartamentoDto? Unidad { get; set; }
    }

    public class ParqueaderoVisitanteDto
    {
        public int ParqueaderoVisitanteId { get; set; }
        public int ParqueaderoId { get; set; }
        public string Placa { get; set; } = "";
        public DateTime FechaHoraIngreso { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
        public int? IngresoId { get; set; }
        public ParqueaderoDto? Parqueadero { get; set; }
    }

    public class ParqueaderoVisitanteCreateDto
    {
        public int ParqueaderoId { get; set; }
        public string Placa { get; set; } = "";
        public DateTime FechaHoraIngreso { get; set; } = DateTime.Now;
        public DateTime? FechaHoraSalida { get; set; }
        public int? IngresoId { get; set; }
    }
}