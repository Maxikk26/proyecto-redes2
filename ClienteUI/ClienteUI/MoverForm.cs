using ClienteUI.backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClienteUI
{
    public partial class MoverForm : Form
    {
        public FtpClient ftp;
        public string fileName;
        public string listD;

        public int nextX = 0;
        public int nextY = 0;
        public string pathResouces;
        public int itemsXFila = 4;

        public MoverForm(FtpClient ftp, string fileName)
        {
            InitializeComponent();
            this.ftp = ftp;
            this.fileName = fileName;

            pathResouces = Directory.GetCurrentDirectory();
            pathResouces = pathResouces.Replace(@"bin\Debug", @"Resources\");

            listD = ftp.ListDirectories();

            ListFolder(listD);
        }

        public void ListFolder(string list)
        {
            string[] auxlist = list.Split(',');
            string Plist = auxlist[0];

            Plist = Directory.GetParent(Plist).ToString();

            nextX = 0;
            nextY = 0;
            Bitmap img = new Bitmap(pathResouces + "folder.png");

            Console.WriteLine("list: " + list);
            if (!(list == "empty"))
            {
                int count = 0;
                int count2 = 0;
                int count3 = 0;
                int top = 50;
                int left = 200;
                string aux = "";

                

                foreach (char d in Plist)
                {
                    if (d == '\\')
                        count3++;
                }
                if (count3 > 2)
                {
                    string name = Path.GetFileName(Path.GetDirectoryName(Plist));
                    Button button = new Button();

                    button.Image = img;
                    button.AutoSize = true;
                    button.TextAlign = ContentAlignment.BottomCenter;

                    button.Text = name;
                    button.Name = Plist;

                    /* button.Left = left;
                     button.Top = top;*/

                    button.Location = new Point(nextX, nextY);
                    nextX += 100;
                    if (nextX > 100 * itemsXFila)
                    {
                        nextY += 100;
                        nextX = 0;
                    }

                    button.Click += (sender2, e2) => Folder_Click(sender2, e2, button);
                    this.Controls.Add(button);
                }
                foreach (char d in list)
                {
                    if (d == '\\')
                    {
                        count++;
                    }
                    else if (d == ',')
                    {
                        break;
                    }
                }
                foreach (char d in list)
                {
                    if (count2 == count)
                    {
                        if (d == ',')
                        {
                            count2 = 0;
                            Button button = new Button();

                            button.Image = img;
                            button.AutoSize = true;
                            button.TextAlign = ContentAlignment.BottomCenter;

                            button.Text = aux;
                            button.Name = aux;

                            /* button.Left = left;
                             button.Top = top;*/

                            button.Location = new Point(nextX, nextY);
                            nextX += 100;
                            if (nextX > 100 * itemsXFila)
                            {
                                nextY += 100;
                                nextX = 0;
                            }

                            button.Click += (sender2, e2) => Folder_Click(sender2, e2, button);
                            this.Controls.Add(button);
                            //top += button.Height + 2;
                            aux = "";
                            count2 = 0;
                        }
                        else
                        {
                            aux = aux + d;
                        }
                    }
                    else if (d == '\\')
                    {
                        count2++;
                    }
                }
            }

        }

        public void Folder_Click(object sender, EventArgs e, Button button)
        {
            string response = ftp.Move(button.Name, fileName);
            MessageBox.Show(response.Split('!')[0], "Moved", MessageBoxButtons.OK);
        }
    }
}
