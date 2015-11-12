namespace MetalForming.Web.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        public int IdRol { get; set; }

        public string NombreCompleto { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int CantidadOV { get; set; }

        public int CantidadOP { get; set; }
    }
}