using System;
using System.Collections.Generic;

namespace webmotos.Models;

public partial class Marca
{
    public int IdMarca { get; set; }

    public string NombreMarca { get; set; } = null!;

    public string? PaisOrigen { get; set; }

    public DateTime CreadoEn { get; set; }

    public virtual ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();
}
