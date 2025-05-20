using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Lumel.Interfaces
{
    public interface IHabitacionView
    {
        // Propiedades - Model Binding
        string IdHabitacion { get; set; }
        string Numero { get; set; }
        string Tipo { get; set; }
        string Descripcion { get; set; }
        string Capacidad { get; set; }
        string PrecioNoche { get; set; }
        string Estado { get; set; }
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
        event EventHandler LoadAllEvent;

        // Métodos
        void SetHabitacionListBindingSource(BindingSource habitacionList);
        void Show();
    }
}
