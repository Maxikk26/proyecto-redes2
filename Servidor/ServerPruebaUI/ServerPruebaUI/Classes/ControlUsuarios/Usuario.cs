

namespace ServerPruebaUI.Classes.ControlUsuarios
{
    public class Usuario
    {
        public string nombre;
        public string apellido;
        public string login;
        public string clave;
        public Usuario(string login, string clave, string nombre, string apellido)
        {
            this.nombre = nombre;
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
