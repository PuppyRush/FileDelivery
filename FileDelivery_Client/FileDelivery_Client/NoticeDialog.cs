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
    public partial class NoticeDialog : Form
    {
        private bool isOpen;
        public string notice = "";
        public NoticeDialog(string notice)
        {
            isOpen = false;
            InitializeComponent();
            txtNotice.Text = notice;
        }

        private void NoticeDialog_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void NoticeDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isOpen)
            {
                Hide();
                e.Cancel = true;
                isOpen = false;
            }
        }

        private void NoticeDialog_Load(object sender, EventArgs e)
        {
            txtNotice.Text = notice;
            chkNotice.Checked = false;
            isOpen = true;
        }

        private void chkNotice_CheckedChanged(object sender, EventArgs e)
        {
            RegistryManager.Notice = DateTime.Now.ToShortDateString();
            chkNotice.Checked = true;  
            Hide();
            isOpen = false;
        }

        

    }
}
