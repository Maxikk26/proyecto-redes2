using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPruebaUI.Classes.ControlUsuarios
{
    class AdaptadorTXT
    {
        public string path;

        public AdaptadorTXT(string path)
        {
            this.path = path;
            if (!File.Exists(path))
            {
                File.CreateText(path);
            }
        }

        public void salvarUsuarios(List<Usuario> usuarios)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                foreach (Usuario usuario in usuarios)
                {
                    sw.WriteLine(usuario.toString());
                }
            }
        }

        public List<Usuario> cargarUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    String str = new String(s.ToCharArray());

                    String[] spearator = { " " };
                    Int32 count = 4;

                    // using the method 
                    String[] strlist = str.Split(spearator, count,
                           StringSplitOptions.RemoveEmptyEntries);

                    usuarios.Add(new Usuario(strlist[0], strlist[1], strlist[2], strlist[3]));
                }
            }

            return usuarios;
        }
    }
}
