using System;
using System.Collections.Generic;

namespace webmotos.Models;

public partial class Accesorio
{
    public int IdAccesorio { get; set; }

    public int? IdCategoria { get; set; }

    public string NombreAccesorio { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public bool? Disponible { get; set; }

    public DateTime CreadoEn { get; set; }

    public virtual Categoriasaccesorio? IdCategoriaNavigation { get; set; }

    public virtual ICollection<Moto> IdMotos { get; set; } = new List<Moto>();
}
