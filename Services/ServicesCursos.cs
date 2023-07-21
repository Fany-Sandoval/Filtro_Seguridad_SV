using FiltroSeguridad.Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FiltroSeguridad.Models;

namespace FiltroSeguridad.Services
{
    public class ServicesCursos : IServicesCursos
    {
        public List<Cursos> Get()
        {
            List<Cursos> cursos = new List<Cursos>();
            try
            {
                using (var conexion = new FiltrosSeguridadEntities())
                {
                    cursos = conexion.Cursos.Include(c => c.Categorias).Include(c => c.Status).Include(c => c.AutorId).Where(c => c.StatusId == 1).ToList();
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }

            return cursos;
        }

    }
}