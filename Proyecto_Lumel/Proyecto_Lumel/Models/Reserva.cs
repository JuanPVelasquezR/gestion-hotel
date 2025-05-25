using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Lumel.Models
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public int IdHuesped { get; set; }
        public int IdHabitacion { get; set; }
        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }
        public decimal PrecioTotal { get; set; }
        public string Estado { get; set; } // Confirmada, Cancelada, Completada, etc.
        public string Observaciones { get; set; }
        public DateTime FechaCreacion { get; set; }
        
        // Propiedades de navegación
        public string NombreHuesped { get; set; }
        public string NumeroHabitacion { get; set; }
        public string TipoHabitacion { get; set; }
        public decimal PrecioNoche { get; set; }
        
        // Constructor vacío
        public Reserva()
        {
            FechaCreacion = DateTime.Now;
            Estado = "Pendiente";
        }
        
        // Constructor completo
        public Reserva(int idReserva, int idHuesped, int idHabitacion, DateTime fechaEntrada, 
            DateTime fechaSalida, decimal precioTotal, string estado, string observaciones)
        {
            IdReserva = idReserva;
            IdHuesped = idHuesped;
            IdHabitacion = idHabitacion;
            FechaEntrada = fechaEntrada;
            FechaSalida = fechaSalida;
            PrecioTotal = precioTotal;
            Estado = estado ?? "Pendiente";
            Observaciones = observaciones;
            FechaCreacion = DateTime.Now;
        }
        
        // Método para calcular el precio total basado en las fechas y el precio por noche
        public void CalcularPrecioTotal()
        {
            if (FechaEntrada != null && FechaSalida != null && PrecioNoche > 0)
            {
                int diasEstancia = (int)(FechaSalida - FechaEntrada).TotalDays;
                if (diasEstancia <= 0) diasEstancia = 1; // Mínimo un día
                PrecioTotal = diasEstancia * PrecioNoche;
            }
        }
        
        // Método para obtener el número de días de estancia
        public int DiasEstancia()
        {
            if (FechaEntrada != null && FechaSalida != null)
            {
                int dias = (int)(FechaSalida - FechaEntrada).TotalDays;
                return dias > 0 ? dias : 1;
            }
            return 0;
        }
    }
}
