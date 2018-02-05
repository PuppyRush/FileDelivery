using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileDelivery2_Client
{
    public partial class FolderPrivilageDialog : Form
    {

        private ShareFolder sf;

        public ShareFolder ShareFolder
        {
            get { return sf; }
            set { sf = value; }
        }

        public FolderPrivilageDialog()
        {
            InitializeComponent();
        }

     

        public void SetDialog(ShareFolder sf)
        {
            this.sf = sf;

            txtDirection.Text = sf.pathname;

            txtNickname.Text = sf.nickname;



        }

    

        private void FolderPrivilageDialog_Load(object sender, EventArgs e)
        {
            lstUser.Items.Clear();
            txtDirection.Text = sf.pathname;
            txtNickname.Text = sf.nickname;
            for (int i = 0; i < sf.shareUserMap.Count; i++)
                lstUser.Items.Add(sf.shareUserMap.ElementAt(i).Value.name);

            if (lstUser.Items.Count > 0)
                lstUser.SelectedIndex = 0;
        }

        private void txt_username_Leave(object sender, EventArgs e)
        {

        }

        private void txtNickname_Leave_1(object sender, EventArgs e)
        {
            sf.nickname = txtNickname.Text;
        }

        private void lstUser_MouseUp_1(object sender, MouseEventArgs e)
        {
            Point pt = PointToScreen(new Point(e.X + 20, e.Y + 170));
            lstUser.ClearSelected();
            string user_name = "";
            int height = lstUser.GetItemHeight(0);
            int idx = e.Y / height;


            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {      //선택된 항목이 없는 경우
                if (idx >= lstUser.Items.Count)
                {

                    return;
                }

                lstUser.SelectedIndex = idx;

                user_name = (string)lstUser.Items[idx];

            }

            //리스트창의 빈칸을 눌렀을 때 추가하기 메뉴가 등장
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

                //선택된 항목이 없는 경우
                if (idx >= lstUser.Items.Count)
                {
                    AddUserMenu.Show(pt);
                    return;
                }

                //선택된게 있으면 제거메뉴가 추가됨
                lstUser.SelectedIndex = idx;
                RemoveUserMenu.Show(pt);
                user_name = (string)lstUser.Items[idx];


            }

            chkRead.Checked = (sf.shareUserMap[user_name].isread == (int)PRVIL_NUMBER.READ) ? true : false;
            chkDownload.Checked = (sf.shareUserMap[user_name].isdownload == (int)PRVIL_NUMBER.DOWNLOAD) ? true : false;
            chkUpload.Checked = (sf.shareUserMap[user_name].isupload == (int)PRVIL_NUMBER.UPLOAD) ? true : false;
            chkRemove.Checked = (sf.shareUserMap[user_name].isremove == (int)PRVIL_NUMBER.REMOVE) ? true : false;

            txtDirection.Text = sf.pathname;
            txtNickname.Text = sf.nickname;
        }

        private void chkRead_CheckedChanged_1(object sender, EventArgs e)
        {
            if (lstUser.SelectedIndex == -1)
                return;

            string user_name = (string)lstUser.Items[lstUser.SelectedIndex];
            sf.shareUserMap[user_name].isread = chkRead.Checked ? (int)PRVIL_NUMBER.READ : (int)PRVIL_NUMBER.NONE;
            if (sf.shareUserMap[user_name].isread == (int)PRVIL_NUMBER.UPLOAD)
                sf.shareUserMap[user_name].isread = (int)PRVIL_NUMBER.READ;
            else
            {
                sf.shareUserMap[user_name].isread = (int)PRVIL_NUMBER.NONE;
                sf.shareUserMap[user_name].isdownload = (int)PRVIL_NUMBER.DOWNLOAD;
                sf.shareUserMap[user_name].isupload = (int)PRVIL_NUMBER.UPLOAD;
                sf.shareUserMap[user_name].isremove = (int)PRVIL_NUMBER.REMOVE;
            }
            sf.shareUserMap[user_name].SetPrivialge();
        }

        private void chkUpload_CheckedChanged_1(object sender, EventArgs e)
        {
            if (lstUser.SelectedIndex == -1)
                return;

            string user_name = (string)lstUser.Items[lstUser.SelectedIndex];


            sf.shareUserMap[user_name].isupload = chkUpload.Checked ? (int)PRVIL_NUMBER.UPLOAD : (int)PRVIL_NUMBER.NONE;
            if (sf.shareUserMap[user_name].isupload == (int)PRVIL_NUMBER.UPLOAD)
                sf.shareUserMap[user_name].isread = (int)PRVIL_NUMBER.READ;
            else
                sf.shareUserMap[user_name].isread = (int)PRVIL_NUMBER.NONE;

            sf.shareUserMap[user_name].SetPrivialge();
        }

        private void chkDownload_CheckedChanged_1(object sender, EventArgs e)
        {
            if (lstUser.SelectedIndex == -1)
                return;

            string user_name = (string)lstUser.Items[lstUser.SelectedIndex];

            sf.shareUserMap[user_name].isdownload = chkDownload.Checked ? (int)PRVIL_NUMBER.DOWNLOAD : (int)PRVIL_NUMBER.NONE;
            if (sf.shareUserMap[user_name].isdownload == (int)PRVIL_NUMBER.DOWNLOAD)
                sf.shareUserMap[user_name].isread = (int)PRVIL_NUMBER.READ;
            else
                sf.shareUserMap[user_name].isread = (int)PRVIL_NUMBER.NONE;

            sf.shareUserMap[user_name].SetPrivialge();
        }

        private void chkRemove_CheckedChanged_1(object sender, EventArgs e)
        {
            if (lstUser.SelectedIndex == -1)
                return;

            string user_name = (string)lstUser.Items[lstUser.SelectedIndex];
            sf.shareUserMap[user_name].isremove = chkRemove.Checked ? (int)PRVIL_NUMBER.REMOVE : (int)PRVIL_NUMBER.NONE;
            if (sf.shareUserMap[user_name].isremove == (int)PRVIL_NUMBER.REMOVE)
                sf.shareUserMap[user_name].isread = (int)PRVIL_NUMBER.READ;
            else
                sf.shareUserMap[user_name].isread = (int)PRVIL_NUMBER.NONE;

            sf.shareUserMap[user_name].SetPrivialge();
        }

    }
}
