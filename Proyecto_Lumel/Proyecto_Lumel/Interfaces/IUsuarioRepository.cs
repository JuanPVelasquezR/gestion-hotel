using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Lumel.Models;

namespace Proyecto_Lumel.Interfaces
{
    public interface IUsuarioRepository
    {
        void Add(Usuario usuario);
        void Edit(Usuario usuario);
        void Delete(int id);
        Usuario GetById(int id);
        IEnumerable<Usuario> GetAll();
        IEnumerable<Usuario> GetByFilter(string filter);
    }
}
