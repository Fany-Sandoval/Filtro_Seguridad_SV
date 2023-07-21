using FiltroSeguridad.Models;

namespace FiltroSeguridad.Infraestructure
{
    internal interface IServiceAccount
    {
        Usuario Login(Usuario usuario);
    }
}
