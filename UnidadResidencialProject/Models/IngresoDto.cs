namespace UnidadResidencialProject.Models
{
    // ── Modelo principal de Ingreso (coincide con la respuesta de la API) ──
    public class IngresoDto
    {
        public int IngresoId { get; set; }
        public string Tipo { get; set; } = "";
        public string NombrePersona { get; set; } = "";
        public string Documento { get; set; } = "";
        public DateTime FechaHoraIngreso { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
        public int UsuarioId { get; set; }
        public int UnidadId { get; set; }
        public VigilanteDto? Vigilante { get; set; }
        public UnidadDto? Unidad { get; set; }
    }

    // ── DTO simplificado para crear un ingreso (POST) ──
    public class IngresoCreateDto
    {
        public string Tipo { get; set; } = "";
        public string NombrePersona { get; set; } = "";
        public string Documento { get; set; } = "";
        public DateTime FechaHoraIngreso { get; set; }
        public DateTime? FechaHoraSalida { get; set; }
        public int UsuarioId { get; set; }
        public int UnidadId { get; set; }
    }

    // ── Modelos de navegación ──
    public class VigilanteDto
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
    }

    public class UnidadDto
    {
        public int UnidadId { get; set; }
        public int TorreId { get; set; }
        public string Numero { get; set; } = "";
        public string Tipo { get; set; } = "";
        public double Area { get; set; }
        public TorreDto? Torre { get; set; }
    }

    public class TorreDto
    {
        public int TorreId { get; set; }
        public int ConjuntoId { get; set; }
        public string Nombre { get; set; } = "";
        public ConjuntoDto? Conjunto { get; set; }
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

    public class RolDto
    {
        public int RolId { get; set; }
        public string Nombre { get; set; } = "";
    }
}
