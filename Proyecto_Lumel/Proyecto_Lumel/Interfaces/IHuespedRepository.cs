using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Lumel.Models;

namespace Proyecto_Lumel.Interfaces
{
    public interface IHuespedRepository
    {
        void Add(Huesped huesped);
        void Edit(Huesped huesped);
        void Delete(int id);
        Huesped GetById(int id);
        IEnumerable<Huesped> GetAll();
        IEnumerable<Huesped> GetByFilter(string filter);
    }
} 