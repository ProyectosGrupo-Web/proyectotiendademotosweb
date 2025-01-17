using System;
using System.Collections.Generic;

namespace webmotos.Models;

public partial class Foto
{
    public int IdFoto { get; set; }

    public int? IdMoto { get; set; }

    public string UrlFoto { get; set; } = null!;

    public DateTime CreadoEn { get; set; }

    public virtual Moto? IdMotoNavigation { get; set; }
}
