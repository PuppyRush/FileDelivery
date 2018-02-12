using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Threading;

namespace FileDelivery2_Client
{
    public partial class MainForm : Form
    {
        
        public string clinetLog;


        public List<String[]> rootFolderPath;
        public Dictionary<string, ShareFolder> serverFolerMap;
        public Dictionary<string, ShareFolder> shareFolderMap;     //P2P를 위한 공유폴더들의 정보를 담는다.
   
        private List<string> file_list = null;
        public Client client;

        public string destKey;
        private int searchNumber;

        private delegate void ChangeBtnName(Button btn, string name);
        private delegate void AddLog(string msg);

        public FindNodeDialog findDialog;
        public nodeDialog nodeDialog;
        public DirectoryManager directoryManager;
        public NoticeDialog noticeDialog;
        public JoinDialog joinDialog;
        public RegistryManager registryManager;         //이 변수의 사용은 로그인후 사용되어야함.
        public TransferManager transferManager;
        public ShareDialog shareDialog;
        public TransferProgressDialog progressDialog;
        public MainForm()
        {
            InitializeComponent();
           
            transferManager = null;
            searchNumber = 0;
            nodeDialog = new nodeDialog(this);
            directoryManager = new DirectoryManager(this);
            shareDialog = new ShareDialog(this);
            noticeDialog = new NoticeDialog("");
            joinDialog = new JoinDialog(this);
            progressDialog = new TransferProgressDialog(this);
            registryManager = new RegistryManager();
            serverFolerMap = new Dictionary<string, ShareFolder>();
            shareFolderMap = new Dictionary<string, ShareFolder>();
            findDialog = new FindNodeDialog(this,ref nodeDialog.keys_list,ref nodeDialog.find_keys_list );
            DoChangeBtnName(btnConnect, CONST.BTN_NAME_LOGIN);   
            
            file_list = new List<string>();

            txtID.Text = "cks1023";
            txtPassword.Text = "1234";
            txtServerip.Text = "127.0.0.1";
            txtServerport.Text = "35000";

            client = new Client(this);
            rootFolderPath = new List<string[]>();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

            if (ClientRectangle.Width <= 300 || ClientRectangle.Height <= 300 )
            {
                return;
            }

            int tree_w = ClientSize.Width/2 - CONST.TREE_SPACE - CONST.WINDOW_MARGIN*2;
            int tree_h = ClientSize.Height/2;
            clientTree.SetBounds(CONST.CLIENT_TREE_LOCATION.X, CONST.CLIENT_TREE_LOCATION.Y,
                tree_w, tree_h);


            serverTree.SetBounds(clientTree.Bounds.Right + CONST.TREE_SPACE, CONST.CLIENT_TREE_LOCATION.Y,
                tree_w+25, tree_h);

            lstFile.SetBounds(CONST.CLIENT_TREE_LOCATION.X, serverTree.Bounds.Bottom + CONST.TREE_SPACE,
                ClientSize.Width - CONST.WINDOW_MARGIN * 2, tree_h / 2);

            lstLog.SetBounds(CONST.CLIENT_TREE_LOCATION.X, lstFile.Bounds.Bottom + CONST.TREE_SPACE,
                ClientSize.Width - CONST.WINDOW_MARGIN * 2, ClientSize.Height-CONST.TREE_SPACE*2-serverTree.Bounds.Height-
                lstFile.Bounds.Height-CONST.CLIENT_TREE_LOCATION.Y-10);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form1_Resize(sender, e);

          
        }

        public void DoAddText(string msg)
        {
            if (lstLog.InvokeRequired)
            {
                AddLog del = new AddLog(AddText);
                lstLog.Invoke(del, msg);
            }
            else
            {
                clinetLog += msg;
                lstLog.Items.Add(msg);
            }
        }

        private void AddText(string msg)
        {
            lstLog.Items.Add(msg);
            clinetLog += "\r\n" + msg;
        }

        public void DoChangeBtnName(Button btn, string name)
        {
            if (btn.InvokeRequired)
            {
                ChangeBtnName change = new ChangeBtnName(Changebtnname);
                change.Invoke(btn, name);
            }
            else
            {
                btn.Text = name;
            }
        }

        private void Changebtnname(Button btn, string name)
        {
            btn.Text = name;
        }

        public void ReadShareFile(string id)
        {

            //Program files에 로그파일 읽기

            string fullpath = RegistryManager.UserDirectory(id) + "\\" + RegistryManager.LogfileName;


            FileStream file = new FileStream(fullpath, FileMode.OpenOrCreate, FileAccess.Read);

            StreamReader sr = new StreamReader(fullpath);

            string temp = "";
            if ((temp = sr.ReadLine()) == null)
            {
                return;
                sr.Close();
                file.Close();
            }
            int foldernum = Int32.Parse(temp);

            //로그를 읽어서 공유폴더, 유저, 설정을 세팅한다.

            for (int i = 0; i < foldernum; i++)
            {
                string path = sr.ReadLine();
                string nick = sr.ReadLine();
                string fname = sr.ReadLine();

                if (shareFolderMap.ContainsKey(path))
                {
                    DoAddText("로그파일에 이상이 있습니다.");
                    break;
                }
                rootFolderPath.Add(new string[2] { path, fname });
                shareFolderMap.Add(path, new ShareFolder(path, nick, fname));
                shareFolderMap[path].shareUserMap = new Dictionary<string, ShareFolder.ShareUser>();

                int usernum = Int32.Parse(sr.ReadLine());
                for (int l = 0; l < usernum; l++)
                {
                    string line = sr.ReadLine();
                    string[] part = line.Split(' ');
                    ShareFolder.ShareUser su = new ShareFolder.ShareUser(part[0], int.Parse(part[1]),
                        int.Parse(part[2]), int.Parse(part[3]), int.Parse(part[4]));

                    shareFolderMap[path].shareUserMap.Add(part[0], su);
                }
                sr.ReadLine();
            }

            sr.Close();
            file.Close();
            sr.Dispose();
            file.Dispose();
        }

        private void menuShowShareDialog_Click(object sender, EventArgs e)
        {
            if (client.isLogin)
                shareDialog.ShowDialog();
            else
                DoAddText("로그인 후 설정 가능합니다");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client.isLogin)
            {
                WriteShareFile(client.clientName);
                client.DoLogout();
            } 
        }

        public void WriteShareFile(string id)
        {

            try
            {

                string filename = RegistryManager.UserDirectory(id) + "\\" + RegistryManager.LogfileName;

                //각 공유폴더들의 유저마다 설정값을 저장한다.
                //저장 순서
                //공유폴더갯수, 공유폴더별명, 공유폴더이름, 공유폴더의 공유유저 수, 각 유저의 설정값


                StreamWriter sw = File.CreateText(filename);
                Dictionary<string, ShareFolder> sf = shareFolderMap;
                sw.Write(sf.Count + " \r\n");
                for (int i = 0; i < sf.Count; i++)
                {
                    Dictionary<string, ShareFolder.ShareUser> su = sf.ElementAt(i).Value.shareUserMap;
                    sw.Write(sf.ElementAt(i).Value.pathname + "\r\n" + sf.ElementAt(i).Value.nickname + "\r\n" +
                        sf.ElementAt(i).Value.folderName + "\r\n" + su.Count + "\r\n");

                    for (int l = 0; l < sf.ElementAt(i).Value.shareUserMap.Count; l++)
                    {
                        sw.Write(su.ElementAt(l).Value.ToString() + "\r\n");
                    }
                    sw.Write("\r\n");
                }

                sw.Flush();
                sw.Close();




            }
            catch (FileLoadException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {

   

            if (btnConnect.Text.Equals(CONST.BTN_NAME_DISCONNECT))
            {
                client.DoLogout();
                return;
            }

            if (txtID.TextLength == 0)
            {
                DoAddText("아이디가 입력되지 않았습니다");
                return;
            }
            else if (txtPassword.TextLength == 0)
            {
                DoAddText("패스워드가 입력되지 않았습니다");
                return;
            }
            else if (txtServerip.TextLength == 0)
            {
                DoAddText("아이피를 입력하세요");
                return;
            }
            else if (txtServerport.TextLength == 0)
            {
                DoAddText("포트번호를 입력하세요");
                return;
            }
            else if (int.Parse(txtServerport.Text) <= 0 || int.Parse(txtServerport.Text) > CONST.PORT_MAX)
            {
                DoAddText("포트번호는 0부터 " + CONST.PORT_MAX.ToString() + "까지입니다");
                return;
            }

            //접속시도
            else
            {
                client.LoadClient(txtID.Text, txtPassword.Text, txtServerip.Text, txtServerport.Text);               
            }


          
        }

        private void clientTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Point pt = PointToScreen(new Point(e.X + 15, e.Y + 60));
                menuClientTree.Show(pt);
                
            }
           
            lstFile.SetList(e.Node);
        }

        private void serverTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Point pt = PointToScreen(new Point(clientTree.Bounds.Width+ e.X + 20, e.Y + 65));
                menuServerTree.Show(pt);

            }
        }

        private void menuClientTree_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            

            if (e.ClickedItem.Text.Equals("폴더열기")  && clientTree.SelectedNodes.Count == 1)
            {
                //파일인경우
                if (clientTree.SelectedNode.Text.IndexOf(".") != -1)
                    System.Diagnostics.Process.Start(clientTree.SelectedNode.Parent.Name);
                else
                {
                    if (clientTree.SelectedNode.Level == 0)
                        System.Diagnostics.Process.Start(clientTree.SelectedNode.Name);
                    else
                        System.Diagnostics.Process.Start(clientTree.SelectedNode.Parent.Name);
                }
            }
        }

        private void menuClientTreeServer_Click(object sender, EventArgs e)
        {
       
        }

        private void menuClientTreeClient_Click(object sender, EventArgs e)
        {
           
        }

        private void menuUploadServer_Click(object sender, EventArgs e)
        {
            if (serverTree.SelectedNodes.Count == 0)
            {
                DoAddText("오른쪽 탐색기에 업로드될 폴더를 선택하세요");
            }
     
            //else if (serverTree.SelectedNode.Level ==0 && serverTree.SelectedNode.Text.Equals( client.clientName ) == false)
            //{
            //    DoAddText("자신의 폴더에만 업로드할 수 있습니다.");
            //}
            //else if (ServerTree.SelectedNode.Level > 0 && serverTree.SelectedNode..Text.Equals(client.clientName) == false)
            //{
            //    DoAddText("자신의 폴더에만 업로드할 수 있습니다.");
            //}
            else if (client.isLogin)
            {

                transferManager.SendFileToServer();
               
            }
            else
            {

                DoAddText("서버에 접속되지 않았습니다");
            }


        }

        private void menuShowProgressDialog_Click(object sender, EventArgs e)
        {
          
            progressDialog.Owner = this;

            if (progressDialog.Visible)
                progressDialog.Visible = false;
            else
                progressDialog.Visible = true;
            

        }

        private void menuJoin_Click(object sender, EventArgs e)
        {

            if (client.isLogin)
                DoAddText("로그인한 상태에서는 가입을 할 수 없습니다");
            else
                joinDialog.ShowDialog();

        }

        public void ShowNotice(string notice)
        {
            noticeDialog.Owner = this;
            noticeDialog.notice = notice;
            if (noticeDialog.Visible)
                noticeDialog.Visible = false;
            else
                noticeDialog.Visible = true;
        }

        private void menuDownload_Click(object sender, EventArgs e)
        {
            if (clientTree.SelectedNodes.Count == 0)
            {
                DoAddText("왼쪽 탐색기에 다운로드될 폴더를 선택하세요");
            }
            else if (client.isLogin)
            {

                transferManager.DownLoadFile();

            }
            else
            {

                DoAddText("서버에 접속되지 않았습니다");
            }

        }

        private void menuCreate_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo(ClientTree.SelectedNode.Name);
            if (fi.Exists)
            {
                MessageBox.Show("파일에 노드를 추가할 수 없습니다.");
                return;
            }


            nodeDialog.Activate();
            nodeDialog.txtName.Focus();
            nodeDialog.txtName.Text = clientTree.SelectedNode.FullPath + "\\";
            nodeDialog.txtName.SelectionStart = clientTree.SelectedNode.FullPath.Length + 1;
            nodeDialog.txtName.SelectionLength = nodeDialog.txtName.Text.Length;

            
            DirectoryInfo dir = new DirectoryInfo(nodeDialog.txtName.Text);
            if (dir.Exists == false)
            {
                dir.Create();
            }
            nodeDialog.ShowDialog(this);
        }

        private void menuRemove_Click(object sender, EventArgs e)
        {
         

            foreach (TreeNode temp in clientTree.SelectedNode.Nodes)
            {
                destKey = temp.FullPath;
                if (nodeDialog.keys_list.ContainsKey(destKey))
                {
                    nodeDialog.keys_list.Remove(destKey);
                }
            }


            destKey = clientTree.SelectedNode.FullPath;

            if (nodeDialog.keys_list.ContainsKey(destKey))
            {
                clientTree.Nodes.Remove(clientTree.SelectedNode);
                nodeDialog.keys_list.Remove(destKey);
            }

            DirectoryInfo dir = new DirectoryInfo(destKey);
            FileInfo file = new FileInfo(destKey);


            if (dir.Exists)
            {
                System.IO.FileInfo[] files = dir.GetFiles("*.*", SearchOption.AllDirectories);

                foreach (System.IO.FileInfo File in files)
                {
                    File.Attributes = FileAttributes.Normal;
                }
                clientTree.Nodes.Remove(clientTree.SelectedNode);
                dir.Delete(true);
            }


            if (file.Exists)
            {
                clientTree.Nodes.Remove(clientTree.SelectedNode);
                file.Delete();
            }
        }

        private void menuRename_Click(object sender, EventArgs e)
        {
            if (clientTree.SelectedNode.Level == 0)
            {
                DoAddText("최단노드의 이름을 바꿀 수 없습니다");
                return;
            }

            if (clientTree.SelectedNode != null && clientTree.SelectedNode.Parent != null && clientTree.SelectedNode.Level!=0)
            {
                clientTree.LabelEdit = true;

                if (!clientTree.SelectedNode.IsEditing)
                {
                    clientTree.SelectedNode.BeginEdit();
                }
            }
            else
            {
                MessageBox.Show("노드가 선택되지 않았거나 선택된 노드가 루트 노드입니다.");
            }
          

        }

        private void menuSearch_Click(object sender, EventArgs e){
            findDialog.find_keys_list.Clear();
            findDialog.Owner = this;
            if (findDialog.Visible)
                findDialog.Visible = false;
            else
                findDialog.Visible = true;
   
         
        }

     




    }
}
