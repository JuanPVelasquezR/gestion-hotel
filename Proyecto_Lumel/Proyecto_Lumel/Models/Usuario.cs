using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Lumel.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cargo { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }

        public Usuario()
        {
        }

        public Usuario(int idUsuario, string nombre, string apellido, string cargo, 
            string telefono, string correo, string contraseña)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Apellido = apellido;
            Cargo = cargo;
            Telefono = telefono;
            Correo = correo;
            Contraseña = contraseña;
        }

        public string NombreCompleto
        {
            get { return $"{Nombre} {Apellido}"; }
        }
    }
}
