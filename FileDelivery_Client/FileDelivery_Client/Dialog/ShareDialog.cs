using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;


namespace FileDelivery2_Client
{


    public partial class ShareDialog : Form
    {
        private Point pt;
        public string PROGRAM_NAME;
        public string login_id;

        MainForm mainForm = null;
        private Dictionary<string, ShareFolder> shareFolderMap;
        //파일의 전체경로가 key
        FolderPrivilageDialog privilagedlg = null;


        public ListBox.ObjectCollection ShareListString
        {
            get { return lstShare.Items; }
        }



        public Dictionary<string, ShareFolder> Sharefolder
        {
            set
            {
                shareFolderMap = value;

            }
            get { return shareFolderMap; }
        }

        public ShareDialog(MainForm form)
        {
            InitializeComponent();
            mainForm = form;
            PROGRAM_NAME = CONST.PROGRAM_NAME;

            privilagedlg = new FolderPrivilageDialog();

        }
        public void ResetShareDialog()
        {
            shareFolderMap.Clear();
            lstShare.Items.Clear();
        }

        private void lstShare_DragDrop(object sender, DragEventArgs e)
        {
            try
            {

                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] path = ((string[])e.Data.GetData(DataFormats.FileDrop));

                    for (int l = 0; l < path.Length; l++)
                    {
                        FileAttributes attr = File.GetAttributes(path[l]);
                        bool isFolder = (attr & FileAttributes.Directory) == FileAttributes.Directory;

                        if (isFolder && Directory.Exists(path[l]))
                        {
                            //리스트에 동일한 경로가 없을 경우에만 추가한다.
                            int i = 0;
                            for (i = 0; i < lstShare.Items.Count; i++)
                            {
                                if (path[l].IndexOf((String)lstShare.Items[i]) != -1)
                                    break;
                            }
                            if (i == lstShare.Items.Count)
                            {

                                int idx = path[l].LastIndexOf("\\") != -1 ? path[l].LastIndexOf("\\") + 1 : 0;
                                string nick = path[l].Substring(idx);

                                lstShare.Items.Add(nick);
                                ShareFolder sf = new ShareFolder(path[l], nick, nick);
                                sf.shareUserMap = new Dictionary<string, ShareFolder.ShareUser>();
                                sf.shareUserMap.Add("guest", new ShareFolder.ShareUser("guest",
                                    (int)PRVIL_NUMBER.READ, (int)PRVIL_NUMBER.DOWNLOAD,
                                     (int)PRVIL_NUMBER.UPLOAD, (int)PRVIL_NUMBER.REMOVE));
                                shareFolderMap.Add(path[l], sf);

                                mainForm.ClientTree.AddNewNode(nick);
                            }


                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void lstShare_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;

        }

        private void menu_showpridlg_Click(object sender, EventArgs e)
        {

        }

        private void serverPrivilageList_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void menuShowDialog_Click(object sender, EventArgs e)
        {
            string str = (string)lstShare.Items[lstShare.SelectedIndex];
            for (int i = 0; i < shareFolderMap.Count; i++)
                if (shareFolderMap.ElementAt(i).Key.Contains(str))
                {
                    ShareFolder sf = shareFolderMap.ElementAt(i).Value;
                    privilagedlg.ShareFolder = sf;
                    privilagedlg.ShowDialog();
                    shareFolderMap[shareFolderMap.ElementAt(i).Key] = privilagedlg.ShareFolder;
                    break;

                }
        }

        private void menuRemove_Click(object sender, EventArgs e)
        {
           
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection col = lstShare.SelectedIndices;
            List<string> key = new List<string>();
            int selectednum = col.Count;
            for (int i = selectednum - 1; i >= 0; i--)
            {
                string str = (string)lstShare.Items[col[i]];
                lstShare.Items.RemoveAt(col[i]);
                for (int l = 0; l < shareFolderMap.Count; l++)
                {
                    if (shareFolderMap.ElementAt(l).Value.folderName.Equals(str))
                    {
                        key.Add(shareFolderMap.ElementAt(l).Key);
                        break;
                    }
                }
            }

            mainForm.ClientTree.RemoveNode(key);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = true;
            dlg.ShowDialog();

            int i = 0;
            for (i = 0; i < lstShare.Items.Count; i++)
            {
                if (dlg.SelectedPath == (string)lstShare.Items[i])
                {
                    MessageBox.Show("동일한 경로의 공유폴더가 이미 있습니다");
                    break;
                }
            }

            if (i == lstShare.Items.Count && dlg.SelectedPath.Length > 0)
            {

                int idx = dlg.SelectedPath.LastIndexOf("\\") != -1 ? dlg.SelectedPath.LastIndexOf("\\") + 1 : 0;
                string nick = dlg.SelectedPath.Substring(idx);
                lstShare.Items.Add(nick);

                shareFolderMap.Add(dlg.SelectedPath, new ShareFolder(dlg.SelectedPath, nick, nick));
                shareFolderMap[dlg.SelectedPath].shareUserMap = new Dictionary<string, ShareFolder.ShareUser>();
                shareFolderMap[dlg.SelectedPath].shareUserMap.Add(
                "guest", new ShareFolder.ShareUser("guest", (int)PRVIL_NUMBER.READ, (int)PRVIL_NUMBER.DOWNLOAD,
                 (int)PRVIL_NUMBER.UPLOAD, (int)PRVIL_NUMBER.REMOVE));

                mainForm.ClientTree.AddNewNode(nick);

            }
        }

        private void lstShare_MouseUp(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                if (lstShare.Items.Count == 0)
                    return;



                lstShare.ClearSelected();

                int height = lstShare.GetItemHeight(0);
                int idx = e.Y / height;
                if (idx >= lstShare.Items.Count)
                    return;

                lstShare.SelectedIndex = idx;
                Point pt = PointToScreen(new Point(e.X + 15, e.Y + 15));
                menuOption.Show(pt);
            }
        }

        private void ShareDialog_Load(object sender, EventArgs e)
        {
            lstShare.Items.Clear();
            for (int i = 0; i < shareFolderMap.Count; i++)
                lstShare.Items.Add(shareFolderMap.ElementAt(i).Value.nickname);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

 

    }//class


}
