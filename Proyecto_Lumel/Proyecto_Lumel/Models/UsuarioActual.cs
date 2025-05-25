using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Lumel.Models
{
    /// <summary>
    /// Clase estática para mantener la información del usuario actual en la aplicación
    /// </summary>
    public static class UsuarioActual
    {
        /// <summary>
        /// Usuario actualmente autenticado en el sistema
        /// </summary>
        public static Usuario Usuario { get; set; }

        /// <summary>
        /// Verifica si el usuario actual es administrador
        /// </summary>
        /// <returns>True si es administrador, False en caso contrario</returns>
        public static bool EsAdministrador()
        {
            if (Usuario == null)
                return false;
            
            return Usuario.Cargo.ToLower() == "administrador";
        }

        /// <summary>
        /// Verifica si el usuario actual es empleado
        /// </summary>
        /// <returns>True si es empleado, False en caso contrario</returns>
        public static bool EsEmpleado()
        {
            if (Usuario == null)
                return false;
            
            return Usuario.Cargo.ToLower() == "empleado";
        }

        /// <summary>
        /// Cierra la sesión actual
        /// </summary>
        public static void CerrarSesion()
        {
            Usuario = null;
        }
    }
}
