using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;




namespace FileDelivery2_Client
{
    public partial class JoinDialog : Form
    {
        MainForm f = null;

        ArrayList btn_ary = new ArrayList();

        public string ID
        {
            get { return txtId.Text; }
        }

        public string PASSWORD
        {
            get { return txtPassword.Text; }
        }

        public string PASSWORDCHECK
        {
            get { return txtPasswordRewrite.Text; }
        }

        public string EMAIL
        {
            get { return txtEmail.Text; }
        }

        public JoinDialog(MainForm form)
        {
            InitializeComponent();

            this.f = form;

            btn_ary.Add(txtId);
            btn_ary.Add(txtIp);
            btn_ary.Add(txtEmail);
            btn_ary.Add(txtPort);
            btn_ary.Add(txtPassword);
            btn_ary.Add(txtPasswordRewrite);

            txtId.Text = "cks1023";
            txtIp.Text = "127.0.0.1";
            txtEmail.Text = "Asdf";
            txtPort.Text = "35000";
            txtPassword.Text = "1234";
            txtPasswordRewrite.Text = "1234";


        }

        private void btn_join_Click(object sender, EventArgs e)
        {
            try
            {
                //버튼들에 공백이 있을경우...
                for (int i = 0; i < btn_ary.Count; i++)
                    if (((TextBox)btn_ary[i]).Text.Length == 0)
                    {
                        f.DoAddText("비어 있는 칸이 있습니다");
                        MessageBox.Show("비어 있는 칸이 있습니다");
                        ((TextBox)btn_ary[i]).Focus();
                        return;
                    }

                //비밀번호 무결성 검사
                if (txtPassword.Text != txtPasswordRewrite.Text)
                {
                    string msg = "비밀번호가 다릅니다";
                    MessageBox.Show(msg);
                    f.DoAddText(msg);
                    txtPassword.Focus();
                    return;
                }

                if (txtEmail.Text.IndexOf("@") == -1)
                    if (txtEmail.Text.IndexOf(".", txtEmail.Text.IndexOf("@") + 1) == -1)
                    {
                        string msg = "메일의 형식이 잘못됐습니다";
                        MessageBox.Show(msg);
                        f.DoAddText(msg);
                        txtEmail.Focus();
                        return;

                    }

                f.client.DoJoin(txtId.Text, txtPassword.Text, txtEmail.Text, txtIp.Text, txtPort.Text);
            }

            catch (SocketException ex)
            {
                f.DoAddText(ex.Message);
                MessageBox.Show(ex.Message);
            }
            Close();
        }

        private void btn_rewrite_Click(object sender, EventArgs e)
        {
            txtEmail.Text = "";
            txtId.Text = "";
            txtIp.Text = "";
            txtPassword.Text = "";
            txtPasswordRewrite.Text = "";
            txtPort.Text = "";
        }

  

   

    }
}
