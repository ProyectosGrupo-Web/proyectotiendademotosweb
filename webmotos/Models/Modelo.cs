using System;
using System.Collections.Generic;

namespace webmotos.Models;

public partial class Modelo
{
    public int IdModelo { get; set; }

    public int? IdMarca { get; set; }

    public string NombreModelo { get; set; } = null!;

    public int? Anio { get; set; }

    public int IdTipo { get; set; }

    public DateTime CreadoEn { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }

    public virtual Tipo IdTipoNavigation { get; set; } = null!;

    public virtual ICollection<Moto> Motos { get; set; } = new List<Moto>();

    // Metodos

    public List<Modelo> listarModelos()
    {
        var query = new List<Modelo>();
        try
        {
            using (var db = new WebmotosContext())
            {
                query = db.Modelos.ToList();
            }
        }
        catch (Exception) { throw; }
        return query;
    }
}
