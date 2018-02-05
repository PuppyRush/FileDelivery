using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace FileDelivery2_Client
{
    public class DirectoryManager
    {
        private RegistryKey key;
        private MainForm f;

        public DirectoryManager(MainForm form)
        {
            f = form;
        }

        public static bool CreateDir(string path, string name)
        {

            path += ("\\" + name);

            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists == false)
            {
                di.Create();
            }

            return true;
        }

        public static void CreateRootDir(string fullpath)
        {
            string[] partial = fullpath.Split('\\');
            string path = "";
            for (int i = 0; i < partial.Length; i++)
            {
                for (int l = 0; l <= i; l++)
                {
                    path += partial[l] + "\\";
                }
                if (path == "")
                    continue;
                path = path.Trim();
                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                    di.Create();
                path = "";
            }
        }


        public static void FolderNameChange(string path, string changepath)
        {
            try
            {
                DirectoryInfo Dir = new DirectoryInfo(path);
                DirectoryInfo CDir = new DirectoryInfo(changepath);

                FileInfo File = new FileInfo(path);
                FileInfo CFile = new FileInfo(changepath);

                if (File.Exists)
                {
                    System.IO.File.Move(path, changepath);
                }

                if (CDir.Exists)
                {
                    DirectoryInfo dir = new DirectoryInfo(changepath);

                    if (dir.Exists)
                    {
                        System.IO.FileInfo[] files = dir.GetFiles("*.*", SearchOption.AllDirectories);

                        foreach (System.IO.FileInfo file in files)
                        {
                            file.Attributes = FileAttributes.Normal;
                        }
                        dir.Delete(true);
                    }
                }
                if (Dir.Exists)
                {
                    Dir.MoveTo(changepath);
                    Dir = new DirectoryInfo(changepath);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("에러 발생 : " + e.Message);
            }
        }


    }
}
