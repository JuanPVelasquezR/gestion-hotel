using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Lumel.Models;

namespace Proyecto_Lumel.Interfaces
{
    public interface IHabitacionRepository
    {
        void Add(Habitacion habitacion);
        void Edit(Habitacion habitacion);
        void Delete(int id);
        IEnumerable<Habitacion> GetAll();
        IEnumerable<Habitacion> GetByValue(string value);
        Habitacion GetById(int id);
    }
}
