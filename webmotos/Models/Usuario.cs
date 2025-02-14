using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using webmotos.Models.Security;

namespace webmotos.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreadoEn { get; set; }


    
}
