using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace webmotos.Models;

public partial class Foto
{
    public int IdFoto { get; set; }

    public int? IdMoto { get; set; }

    public string UrlFoto { get; set; } = null!;

    public DateTime CreadoEn { get; set; }

    public virtual Moto? IdMotoNavigation { get; set; }
    //[ForeignKey("IdMoto")]
    //public virtual Moto Moto { get; set; } // El nombre debe ser exactamente el mismo que en `Moto.Fotos`

}
