using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_Lumel.Forms;
using Proyecto_Lumel.Models;

namespace Proyecto_Lumel
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Iniciar la aplicación con el formulario de login
            var loginForm = new Proyecto_Lumel.Forms.FormLogin();
            Application.Run(loginForm);
        }
    }
}
