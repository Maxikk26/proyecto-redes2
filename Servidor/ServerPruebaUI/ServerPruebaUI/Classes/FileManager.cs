using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ServerUI;

namespace Classes
{
    class FileManager
    {
        private string path = @"C:\server";
        private Server target;

        public FileManager(Server f1)
        {
            target = f1;
            rootDirectory();
        }

        public void rootDirectory()
        {
            try
            {
                if (Directory.Exists(path))
                {
                    target.putText(path + " exists!");
                }
                else
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                    target.putText(path + " created!");
                }
            } 
            catch(Exception e)
            {
                target.showError(e.Message,"Exception");
            }
            
        }

        

    }
}
