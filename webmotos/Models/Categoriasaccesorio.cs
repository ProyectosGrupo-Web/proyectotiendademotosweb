using System;
using System.Collections.Generic;

namespace webmotos.Models;

public partial class Categoriasaccesorio
{
    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Accesorio> Accesorios { get; set; } = new List<Accesorio>();
}
