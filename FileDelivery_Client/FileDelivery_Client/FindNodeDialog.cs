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
    public partial class FindNodeDialog : Form
    {
        public TreeView tree;

        private int searchNumber;
        private bool isOpen;
        public Dictionary<String, String> keys_list;
        public List<String[]> find_keys_list;
        MainForm f;
        public FindNodeDialog(MainForm form, ref  Dictionary<String, String> k,ref List<String[]> ff )
        {
            InitializeComponent();
            f = form;
            isOpen = false;

            this.AcceptButton = btnOk;
            keys_list = k;
            find_keys_list = ff;

        }


        public void find_Nodes()
        {
      
            for (int i = 0; i < f.ClientTree.Nodes.Count; i++)
            {
                List<String> temp = new List<string>();
                f.ClientTree.FindNodes(f.ClientTree.Nodes[i], txtName.Text, ref find_keys_list);
                
            }

         

       
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            f.ClientTree.CollapseAll();
            keys_list.Clear();
            find_Nodes();
            btnNext_Click(sender, e);
        }

        private void FindNodeDialog_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (isOpen)
            {
                Hide();
                e.Cancel = true;
                isOpen = false;
            }

        }

        private void FindNodeDialog_Load(object sender, EventArgs e)
        {
            isOpen = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (find_keys_list.Count == 0)
                return;
            else if (find_keys_list.Count == searchNumber)
                searchNumber = 0;

            int j = 0;
            j = (searchNumber % find_keys_list.Count);

            TreeNode[] t = f.ClientTree.Nodes.Find(find_keys_list[j][0], true);
            f.ClientTree.SelectedNode = t[0];

            if (t.Length > 0)
            {
                f.ClientTree.ExpendParent(t[0]);
            }
            searchNumber++; 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
