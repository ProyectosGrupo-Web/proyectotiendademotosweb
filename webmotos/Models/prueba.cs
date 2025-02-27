//using webmotos.Models;

//public interface IUsuarioRepositorio
//{
//    Usuario ObtenerUsuarioPorId(int id);
//}


//public class UsuarioRepositorio : IUsuarioRepositorio
//{
//    public Usuario ObtenerUsuarioPorId(int id)
//    {
//        // Aquí iría la consulta real a la base de datos (Ej: Entity Framework)
//        return new Usuario { Id = id, Nombre = "Jhonny" };
//    }
//}


//public class UsuarioService
//{
//    private readonly IUsuarioRepositorio _repositorio;

//    public UsuarioService(IUsuarioRepositorio repositorio)
//    {
//        _repositorio = repositorio;
//    }

//    public string ObtenerNombreUsuario(int id)
//    {
//        var usuario = _repositorio.ObtenerUsuarioPorId(id);
//        return usuario?.Nombre ?? "Desconocido";
//    }
//}
