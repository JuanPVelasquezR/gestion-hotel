using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Lumel.Interfaces
{
    public interface IHuespedView
    {
        // Propiedades - Model Binding
        string IdHuesped { get; set; }
        string Nombre { get; set; }
        string Apellido { get; set; }
        string TipoDocumento { get; set; }
        string NumeroDocumento { get; set; }
        string Telefono { get; set; }
        string Correo { get; set; }
        string Direccion { get; set; }
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
        void SetHuespedListBindingSource(BindingSource huespedList);
        void Show();
    }
} 