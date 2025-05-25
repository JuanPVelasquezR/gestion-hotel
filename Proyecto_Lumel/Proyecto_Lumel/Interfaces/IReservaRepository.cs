using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Lumel.Models;

namespace Proyecto_Lumel.Interfaces
{
    public interface IReservaRepository
    {
        void Add(Reserva reserva);
        void Edit(Reserva reserva);
        void Delete(int id);
        IEnumerable<Reserva> GetAll();
        Reserva GetById(int id);
        IEnumerable<Reserva> GetByValue(string value);
        IEnumerable<Reserva> GetByFechas(DateTime fechaInicio, DateTime fechaFin);
        IEnumerable<Reserva> GetByHuesped(int idHuesped);
        IEnumerable<Reserva> GetByHabitacion(int idHabitacion);
        IEnumerable<Reserva> GetByEstado(string estado);
    }
}
