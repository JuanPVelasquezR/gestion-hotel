using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto_Lumel.Models;

namespace Proyecto_Lumel.Interfaces
{
    public interface IReservaView
    {
        // Propiedades - Campos de texto
        string IdReserva { get; set; }
        string IdHuesped { get; set; }
        string NombreHuesped { get; set; }
        string IdHabitacion { get; set; }
        string NumeroHabitacion { get; set; }
        string TipoHabitacion { get; set; }
        DateTime FechaEntrada { get; set; }
        DateTime FechaSalida { get; set; }
        string PrecioNoche { get; set; }
        string PrecioTotal { get; set; }
        string Estado { get; set; }
        string Observaciones { get; set; }
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
        event EventHandler SeleccionarHuespedEvent;
        event EventHandler SeleccionarHabitacionEvent;
        event EventHandler CalcularPrecioEvent;
        event EventHandler FiltrarPorFechasEvent;
        event EventHandler FiltrarPorEstadoEvent;
        event EventHandler FiltrarPorHuespedEvent;
        event EventHandler FiltrarPorHabitacionEvent;
        event EventHandler LimpiarFiltrosEvent;

        // Métodos
        void SetReservaListBindingSource(BindingSource reservaList);
        void Show();
    }
}
