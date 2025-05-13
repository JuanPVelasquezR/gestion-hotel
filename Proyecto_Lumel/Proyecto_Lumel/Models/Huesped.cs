using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Lumel.Models
{
    public class Huesped
    {
        public int IdHuesped { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }

        public Huesped()
        {
        }

        public Huesped(int idHuesped, string nombre, string apellido, string tipoDocumento, 
            string numeroDocumento, string telefono, string correo, string direccion)
        {
            IdHuesped = idHuesped;
            Nombre = nombre;
            Apellido = apellido;
            TipoDocumento = tipoDocumento;
            NumeroDocumento = numeroDocumento;
            Telefono = telefono;
            Correo = correo;
            Direccion = direccion;
        }

        public string NombreCompleto
        {
            get { return $"{Nombre} {Apellido}"; }
        }
    }
} 