using FiltroSeguridad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiltroSeguridad.Infraestructure
{
    public interface IServicesCursos
    {
        List<Cursos> Get();
    }
}