using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace webmotos.Models;

public partial class Moto
{
    public int IdMoto { get; set; }

    public int? IdModelo { get; set; }

    public decimal? Precio { get; set; }

    public string? Color { get; set; }

    public int? Cilindrada { get; set; }

    public int? Potencia { get; set; }
    public int? velocidadMax { get; set; }

    public string? Descripcion { get; set; }

    public bool? Disponible { get; set; }

    public DateTime CreadoEn { get; set; }

    public virtual ICollection<Foto> Fotos { get; set; } = new List<Foto>();

    public virtual Modelo? IdModeloNavigation { get; set; }

    public virtual ICollection<Accesorio> IdAccesorios { get; set; } = new List<Accesorio>();

    







}
