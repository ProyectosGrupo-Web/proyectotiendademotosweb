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

    // CRUD MARCAS

    // METODOS
    public List<Marca> listarMarcas()
    {
        var query = new List<Marca>();
        try
        {
            using (var db = new WebmotosContext())
            {
                // Lista todo
                //query = db.Eventos.ToList();
                //Lista por estado activo
                //query = db.Marcas.Where(d => d.Estado == "1").ToList();
                query = db.Marcas.ToList();
            }
        }
        catch (Exception) { throw; }
        return query;
    }

}
