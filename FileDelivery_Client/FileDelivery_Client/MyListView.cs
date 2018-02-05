using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FileDelivery2_Client
{
    class MyListView : ListView

    {

        public MyListView()
        {
            View = View.Details;

            Columns.Add("파일명", 300, HorizontalAlignment.Left);
           
            Columns.Add("크기(kb)", 70, HorizontalAlignment.Left);
            Columns.Add("수정날짜", 150, HorizontalAlignment.Left);

        }

        public void SetList(TreeNode node)
        {

            //리스트뷰에 출력
            string[] path_source = node.Name.Split('\\'); ;       //트리의경로
            string path_dest = "";


            path_dest = node.Name;

            Clear();
            BeginUpdate();
            string maxstring = "";
            DirectoryInfo di = new DirectoryInfo(path_dest);
            if (!di.Exists)
            {
                path_dest = path_dest.Replace(path_source[path_source.Length - 1], "");
                di = new DirectoryInfo(path_dest);
            }

            DirectoryInfo[] dis = di.GetDirectories();
            FileInfo[] fis = di.GetFiles();
            foreach (DirectoryInfo d in dis)
            {
                ListViewItem item = new ListViewItem(d.Name);
                item.SubItems.Add("");
                item.SubItems.Add(d.LastWriteTime.ToShortDateString() + " " + d.LastWriteTime.ToShortTimeString());
                Items.Add(item);
                if (maxstring.Length < d.Name.Length)
                    maxstring = d.Name;
            }

            foreach (FileInfo f in fis)
            {
                ListViewItem item = new ListViewItem(f.Name);
                item.SubItems.Add((f.Length / 1024.0).ToString());
                item.SubItems.Add(f.LastWriteTime.ToShortDateString() + " " + f.LastWriteTime.ToShortTimeString());
                Items.Add(item);
                if (maxstring.Length < f.Name.Length)
                    maxstring = f.Name;

            }

            Columns.Add("파일명", 300, HorizontalAlignment.Left);

            Columns.Add("크기(kb)", 70, HorizontalAlignment.Left);
            Columns.Add("수정날짜", 150, HorizontalAlignment.Left);

            EndUpdate();
        }


    }
}
