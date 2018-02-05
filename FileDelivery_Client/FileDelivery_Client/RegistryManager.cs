using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace FileDelivery2_Client
{
    public class RegistryManager
    {
        private static RegistryKey key;
        public RegistryManager()
        {


        }

        public static void InitUserRegistry(string id)
        {
            Registry.CurrentUser.CreateSubKey("SOFTWARE").CreateSubKey(CONST.PROGRAM_NAME).CreateSubKey("Client").CreateSubKey(id);
            key = Registry.CurrentUser.OpenSubKey("SOFTWARE", true).OpenSubKey(CONST.PROGRAM_NAME, true).OpenSubKey("Client", true).OpenSubKey(id, true);

            if (null == key.GetValue("userlog_filename"))
            {
                key.SetValue("userlog_filename", "ShareFolderLog.txt", RegistryValueKind.String);
            }
        }

        public static string UserDirectory(string id)
        {
            key.SetValue("user_dir", @"C:\Program Files (x86)\" + CONST.PROGRAM_NAME + @"\Client\" + id, RegistryValueKind.String);
            return (string)key.GetValue("user_dir", "");
           
        }

        public static string ProgramDirectory
        {
            set { key.SetValue("program_dir", value); }
            get { return (string)key.GetValue("program_dir", @"C:\Program Files (x86)\" + CONST.PROGRAM_NAME); }
        }

        public static string LogfileName
        {
            get { return (string)key.GetValue("userlog_filename", "ShareFolderLog.txt"); }
        }

        public static string Notice
        {
            set { key.SetValue("notice", value, RegistryValueKind.String); }
            get { return (string)key.GetValue("notice", ""); }
        }

    }
}
