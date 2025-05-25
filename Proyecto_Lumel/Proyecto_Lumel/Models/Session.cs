using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Lumel.Models
{
    /// <summary>
    /// Clase estática para mantener la información de la sesión actual
    /// </summary>
    public static class Session
    {
        /// <summary>
        /// Usuario actualmente autenticado en el sistema
        /// </summary>
        public static Usuario CurrentUser { get; set; }

        /// <summary>
        /// Verifica si el usuario actual es administrador
        /// </summary>
        /// <returns>True si es administrador, False en caso contrario</returns>
        public static bool IsAdmin()
        {
            if (CurrentUser == null)
                return false;
            
            return CurrentUser.Cargo.ToLower() == "administrador";
        }

        /// <summary>
        /// Verifica si el usuario actual es empleado
        /// </summary>
        /// <returns>True si es empleado, False en caso contrario</returns>
        public static bool IsEmployee()
        {
            if (CurrentUser == null)
                return false;
            
            return CurrentUser.Cargo.ToLower() == "empleado";
        }

        /// <summary>
        /// Cierra la sesión actual
        /// </summary>
        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
