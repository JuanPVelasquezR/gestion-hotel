using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_Lumel.Forms;

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
            
            // Iniciar la aplicación con el menú principal
            // Esto permitirá que la aplicación funcione mientras se resuelven los problemas de referencia
            Application.Run(new FormMenu(null));
        }
    }
}
