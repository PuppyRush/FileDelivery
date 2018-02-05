using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FileDelivery2_Client
{
    public partial class nodeDialog : Form
    {
        public TreeView tree;
        private String temp;
        private int index;
        private String key;
        public Dictionary<String, String> keys_list;
        public List<String[]> find_keys_list;

        private MainForm f;
        public nodeDialog(MainForm form)
        {
            f = form;

            this.AcceptButton = btnOk;
            keys_list = new Dictionary<String, String>();
            find_keys_list = new List<String[]>();


            InitializeComponent();
        }



        private void btnOk_Click(object sender, EventArgs e)
        {
         
            index = txtName.Text.LastIndexOf("\\");
            temp = txtName.Text.Substring(index + 1);
            key = txtName.Text.Remove(0, txtName.Text.IndexOf("\\"));

            //tree.SelectedNode = f.Select_Node;


            //  f.treeView1.ExpandAll();
     
            if (!keys_list.ContainsKey(key))
            {
                string root;
                if(f.ClientTree.SelectedNode.Level==1)
                    root = f.ClientTree.SelectedNode.Parent.Name;
                else
                    root = f.ClientTree.SelectedNode.Parent.Parent.Name;
                f.ClientTree.SelectedNode.Nodes.Add(root+"\\"+ key, temp);
                keys_list.Add(key, temp);
            }
            else
            {
                MessageBox.Show("동일한 경로에 동일한 이름의 폴더를 추가할 수가 없습니다.");
                this.Activate();
                txtName.Focus();
            }

            this.Activate();
            txtName.Focus();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Activate();
            txtName.Focus();
            this.Close();
        }


    }
}
