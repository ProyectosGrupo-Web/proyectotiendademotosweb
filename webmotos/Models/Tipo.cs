using System;
using System.Collections.Generic;

namespace webmotos.Models;

public partial class Tipo
{
    public int IdTipo { get; set; }

    public string Tipo1 { get; set; } = null!;

    public virtual ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();
}
