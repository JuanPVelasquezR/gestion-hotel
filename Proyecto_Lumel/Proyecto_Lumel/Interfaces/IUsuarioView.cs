using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Lumel.Interfaces
{
    public interface IUsuarioView
    {
        // Propiedades - Model Binding
        string IdUsuario { get; set; }
        string Nombre { get; set; }
        string Apellido { get; set; }
        string Cargo { get; set; }
        string Telefono { get; set; }
        string Correo { get; set; }
        string Contraseña { get; set; }
        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        // Eventos
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        // Métodos
        void SetUsuarioListBindingSource(BindingSource usuarioList);
        void Show();
    }
}
