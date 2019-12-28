using ServerPruebaUI.Classes.ControlUsuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerUI
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AdaptadorTXT adaptadorTXT = new AdaptadorTXT(@"C:\server\usuarios.txt");
            ControlUsuarios controlUsuarios = new ControlUsuarios(adaptadorTXT);
            controlUsuarios.crearUsuario("eze", "123", "ezequiel","montero");
            controlUsuarios.crearUsuario("max", "1234", "maxi", "bogo");
            controlUsuarios.crearUsuario("rob", "1235", "rober", "car");
            controlUsuarios.crearUsuario("rei", "1234", "reinel", "arte");
            controlUsuarios.eliminarUsuario("eze");
            controlUsuarios.motrarUsuarios();//muestra en consola
            controlUsuarios.salvarUsuarios();//importante para salvar en txt

            Control.CheckForIllegalCrossThreadCalls = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Server());
        }

    }
}
