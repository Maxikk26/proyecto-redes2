using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPruebaUI.Classes.ControlUsuarios
{
    class Usuario
    {
        public string nombre;
        public string apellido;
        public string login;
        public string clave;
        public Usuario(string login, string clave, string nombre, string apellido)
        {
            this.nombre =nombre;
            this.apellido = apellido;
            this.login = login;
            this.clave = clave;
        }

        public string toString()
        {
            return login + " " + clave + " " + nombre + " " + apellido;
        }
    }
}
