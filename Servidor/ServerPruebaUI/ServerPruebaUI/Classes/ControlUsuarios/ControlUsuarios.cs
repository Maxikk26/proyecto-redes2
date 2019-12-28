using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPruebaUI.Classes.ControlUsuarios
{
    class ControlUsuarios
    {
        AdaptadorTXT adaptadorTXT;
        public List<Usuario> usuarios;

        public ControlUsuarios(AdaptadorTXT adaptadorTXT)
        {
            this.adaptadorTXT = adaptadorTXT;
            usuarios = adaptadorTXT.cargarUsuarios();
        }

        public void crearUsuario(string login, string clave, string nombre, string apellido)
        {
            if(!confirmarUsuario(login))
                usuarios.Add(new Usuario(login, clave, nombre, apellido));
        }

        public void eliminarUsuario(string login)
        {
            Usuario usuario = buscarUsuario(login);

            if(usuario != null)
                usuarios.Remove(usuario);
        }

        public Usuario buscarUsuario(string login)
        {
            if (usuarios != null)
                foreach (Usuario usuario in usuarios)
                {
                    if (usuario.login == login)
                        return usuario;
                }

            return null;
        }

        public bool confirmarUsuario(string login)
        {
            if (usuarios != null)
                foreach (Usuario usuario in usuarios)
                {
                    if (usuario.login == login)
                        return true;
                }

            return false;
        }

        public bool loginUsuario(string login, string clave)
        {
            foreach(Usuario usuario in usuarios)
            {
                if (usuario.login == login && usuario.clave == clave)
                    return true;
            }
            return false;
        }

        public void motrarUsuarios()
        {
            foreach (Usuario usuario in usuarios)
            {
                Console.WriteLine(usuario.toString());
            }
        }

        public void salvarUsuarios()
        {
            adaptadorTXT.salvarUsuarios(usuarios);
        }
    }
}
