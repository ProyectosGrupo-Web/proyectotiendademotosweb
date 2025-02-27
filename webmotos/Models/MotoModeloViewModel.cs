namespace webmotos.Models
{
    public class MotoModeloViewModel
    {
        // Propiedades del Modelo
        public int? IdModelo { get; set; }
        public string? NombreModelo { get; set; }
        public int? Anio { get; set; }
        public int IdTipo { get; set; }
        public int IdMarca { get; set; }

        // Relación de Modelo
        public virtual Marca? Marca { get; set; }
        public virtual Tipo? Tipo { get; set; }

        // Propiedades de la Moto
        public decimal? Precio { get; set; }
        public string? Color { get; set; }
        public int? Cilindrada { get; set; }
        public int? Potencia { get; set; }
        public int VelocidadMax { get; set; }
        public string? Descripcion { get; set; }
        public bool? Disponible { get; set; }

        // Propiedades de la Foto
        public string UrlFoto { get; set; } = null!;

    }

}
