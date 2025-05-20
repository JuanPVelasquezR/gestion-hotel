using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Lumel.Models
{
    public class Habitacion
    {
        public int IdHabitacion { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public int Capacidad { get; set; }
        public decimal PrecioNoche { get; set; }
        public string Estado { get; set; }
    }
}
