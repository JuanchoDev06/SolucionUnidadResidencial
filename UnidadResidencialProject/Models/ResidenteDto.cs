namespace UnidadResidencialProject.Models
{
    // ── Modelo de Usuario/Residente ──
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
    }

    // ── DTO simplificado para crear usuario (POST) ──
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

    // ── Modelo de Apartamento/Unidad ──
    public class ApartamentoDto
    {
        public int UnidadId { get; set; }
        public int TorreId { get; set; }
        public string Numero { get; set; } = "";
        public string Tipo { get; set; } = "";
        public double Area { get; set; }
        public TorreDto? Torre { get; set; }
    }
}
