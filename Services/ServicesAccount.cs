using FiltroSeguridad.Infraestructure;
using FiltroSeguridad.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiltroSeguridad.Services
{
    public class ServicesAccount : IServiceAccount
    {
        public Usuario Login(Usuario usuario)
        {
            Usuario user = null;
            try
            {
                using (var contexto = new FiltrosSeguridadEntities())
                {
                    user = contexto.Usuario.Include(p => p.Role).FirstOrDefault(p => p.NombreUsuario == usuario.NombreUsuario && p.Password == usuario.Password);
                }
            }
            catch (Exception ex)
            {
                String mensajeErr = ex.Message;
            }

            return user;
        }
    }
}