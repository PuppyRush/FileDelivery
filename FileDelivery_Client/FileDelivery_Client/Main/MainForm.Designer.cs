namespace FileDelivery2_Client
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtID = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtPassword = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.txtServerip = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.txtServerport = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnConnect = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuShowShareDialog = new System.Windows.Forms.ToolStripMenuItem();
            this.서버ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuJoin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuShowProgressDialog = new System.Windows.Forms.ToolStripMenuItem();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.menuClientTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuClientTreeServer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUploadServer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuClientTreeClient = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUploadClient = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.menuServerTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.lstFile = new FileDelivery2_Client.MyListView();
            this.serverTree = new FileDelivery2_Client.MyTreeView();
            this.clientTree = new FileDelivery2_Client.MyTreeView();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.menuClientTree.SuspendLayout();
            this.menuServerTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.txtID,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.txtPassword,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.txtServerip,
            this.toolStripSeparator3,
            this.toolStripLabel4,
            this.txtServerport,
            this.toolStripSeparator4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(808, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(43, 22);
            this.toolStripLabel1.Text = "아이디";
            // 
            // txtID
            // 
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(55, 22);
            this.toolStripLabel2.Text = "비밀번호";
            // 
            // txtPassword
            // 
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(71, 22);
            this.toolStripLabel3.Text = "서버 아이피";
            // 
            // txtServerip
            // 
            this.txtServerip.Name = "txtServerip";
            this.txtServerip.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(55, 22);
            this.toolStripLabel4.Text = "서버포트";
            // 
            // txtServerport
            // 
            this.txtServerport.Name = "txtServerport";
            this.txtServerport.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(670, 24);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(81, 25);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "서버접속";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.서버ToolStripMenuItem,
            this.menuShowProgressDialog});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(808, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuShowShareDialog});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(43, 20);
            this.menuFile.Text = "파일";
            // 
            // menuShowShareDialog
            // 
            this.menuShowShareDialog.Name = "menuShowShareDialog";
            this.menuShowShareDialog.Size = new System.Drawing.Size(146, 22);
            this.menuShowShareDialog.Text = "공유폴더설정";
            this.menuShowShareDialog.Click += new System.EventHandler(this.menuShowShareDialog_Click);
            // 
            // 서버ToolStripMenuItem
            // 
            this.서버ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuJoin});
            this.서버ToolStripMenuItem.Name = "서버ToolStripMenuItem";
            this.서버ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.서버ToolStripMenuItem.Text = "서버";
            // 
            // menuJoin
            // 
            this.menuJoin.Name = "menuJoin";
            this.menuJoin.Size = new System.Drawing.Size(122, 22);
            this.menuJoin.Text = "가입하기";
            this.menuJoin.Click += new System.EventHandler(this.menuJoin_Click);
            // 
            // menuShowProgressDialog
            // 
            this.menuShowProgressDialog.Name = "menuShowProgressDialog";
            this.menuShowProgressDialog.Size = new System.Drawing.Size(83, 20);
            this.menuShowProgressDialog.Text = "전송상태 창";
            this.menuShowProgressDialog.Click += new System.EventHandler(this.menuShowProgressDialog_Click);
            // 
            // lstLog
            // 
            this.lstLog.ItemHeight = 12;
            this.lstLog.Location = new System.Drawing.Point(12, 617);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(809, 100);
            this.lstLog.TabIndex = 6;
            // 
            // menuClientTree
            // 
            this.menuClientTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuClientTreeServer,
            this.toolStripSeparator6,
            this.menuClientTreeClient,
            this.toolStripSeparator8,
            this.menuOpen,
            this.menuCreate,
            this.menuRemove,
            this.menuRename,
            this.toolStripSeparator5,
            this.menuSearch});
            this.menuClientTree.Name = "menuClientTree";
            this.menuClientTree.Size = new System.Drawing.Size(180, 176);
            this.menuClientTree.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuClientTree_ItemClicked);
            // 
            // menuClientTreeServer
            // 
            this.menuClientTreeServer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUploadServer});
            this.menuClientTreeServer.Name = "menuClientTreeServer";
            this.menuClientTreeServer.Size = new System.Drawing.Size(179, 22);
            this.menuClientTreeServer.Text = "서버";
            this.menuClientTreeServer.Click += new System.EventHandler(this.menuClientTreeServer_Click);
            // 
            // menuUploadServer
            // 
            this.menuUploadServer.Name = "menuUploadServer";
            this.menuUploadServer.Size = new System.Drawing.Size(134, 22);
            this.menuUploadServer.Text = "업로드하기";
            this.menuUploadServer.Click += new System.EventHandler(this.menuUploadServer_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(176, 6);
            // 
            // menuClientTreeClient
            // 
            this.menuClientTreeClient.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUploadClient});
            this.menuClientTreeClient.Name = "menuClientTreeClient";
            this.menuClientTreeClient.Size = new System.Drawing.Size(179, 22);
            this.menuClientTreeClient.Text = "클라이언트";
            this.menuClientTreeClient.Click += new System.EventHandler(this.menuClientTreeClient_Click);
            // 
            // menuUploadClient
            // 
            this.menuUploadClient.Name = "menuUploadClient";
            this.menuUploadClient.Size = new System.Drawing.Size(134, 22);
            this.menuUploadClient.Text = "업로드하기";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(176, 6);
            // 
            // menuOpen
            // 
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.Size = new System.Drawing.Size(179, 22);
            this.menuOpen.Text = "폴더열기";
            // 
            // menuCreate
            // 
            this.menuCreate.Name = "menuCreate";
            this.menuCreate.Size = new System.Drawing.Size(179, 22);
            this.menuCreate.Text = "폴더만들기";
            this.menuCreate.Click += new System.EventHandler(this.menuCreate_Click);
            // 
            // menuRemove
            // 
            this.menuRemove.Name = "menuRemove";
            this.menuRemove.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.menuRemove.Size = new System.Drawing.Size(179, 22);
            this.menuRemove.Text = "삭제하기";
            this.menuRemove.Click += new System.EventHandler(this.menuRemove_Click);
            // 
            // menuRename
            // 
            this.menuRename.Name = "menuRename";
            this.menuRename.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.menuRename.Size = new System.Drawing.Size(179, 22);
            this.menuRename.Text = "이름바꾸기";
            this.menuRename.Click += new System.EventHandler(this.menuRename_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(176, 6);
            // 
            // menuSearch
            // 
            this.menuSearch.Name = "menuSearch";
            this.menuSearch.Size = new System.Drawing.Size(179, 22);
            this.menuSearch.Text = "검색하기";
            this.menuSearch.Click += new System.EventHandler(this.menuSearch_Click);
            // 
            // menuServerTree
            // 
            this.menuServerTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDownload});
            this.menuServerTree.Name = "menuClientTree";
            this.menuServerTree.Size = new System.Drawing.Size(123, 26);
            // 
            // menuDownload
            // 
            this.menuDownload.Name = "menuDownload";
            this.menuDownload.Size = new System.Drawing.Size(122, 22);
            this.menuDownload.Text = "다운로드";
            this.menuDownload.Click += new System.EventHandler(this.menuDownload_Click);
            // 
            // lstFile
            // 
            this.lstFile.Location = new System.Drawing.Point(12, 416);
            this.lstFile.Name = "lstFile";
            this.lstFile.Size = new System.Drawing.Size(809, 195);
            this.lstFile.TabIndex = 5;
            this.lstFile.UseCompatibleStateImageBehavior = false;
            this.lstFile.View = System.Windows.Forms.View.Details;
            // 
            // serverTree
            // 
            this.serverTree.Location = new System.Drawing.Point(420, 52);
            this.serverTree.Name = "serverTree";
            this.serverTree.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("serverTree.SelectedNodes")));
            this.serverTree.Size = new System.Drawing.Size(401, 358);
            this.serverTree.TabIndex = 3;
            this.serverTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.serverTree_NodeMouseClick);
            // 
            // clientTree
            // 
            this.clientTree.AllowDrop = true;
            this.clientTree.Location = new System.Drawing.Point(12, 52);
            this.clientTree.Name = "clientTree";
            this.clientTree.SelectedNodes = ((System.Collections.Generic.List<System.Windows.Forms.TreeNode>)(resources.GetObject("clientTree.SelectedNodes")));
            this.clientTree.Size = new System.Drawing.Size(402, 358);
            this.clientTree.TabIndex = 2;
            
            this.clientTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.clientTree_NodeMouseClick);
            
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 746);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.lstFile);
            this.Controls.Add(this.serverTree);
            this.Controls.Add(this.clientTree);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "MainForm";
            this.Text = "FileDeliery2_Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.menuClientTree.ResumeLayout(false);
            this.menuServerTree.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtID;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox txtPassword;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button btnConnect;
        private MyTreeView clientTree;
        private MyTreeView serverTree;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox txtServerip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox txtServerport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem 서버ToolStripMenuItem;
        private MyListView lstFile;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.ToolStripMenuItem menuShowShareDialog;
        private System.Windows.Forms.ContextMenuStrip menuClientTree;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripMenuItem menuClientTreeServer;
        private System.Windows.Forms.ToolStripMenuItem menuUploadServer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem menuClientTreeClient;
        private System.Windows.Forms.ToolStripMenuItem menuUploadClient;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem menuShowProgressDialog;
        private System.Windows.Forms.ContextMenuStrip menuServerTree;
        private System.Windows.Forms.ToolStripMenuItem menuDownload;
        private System.Windows.Forms.ToolStripMenuItem menuJoin;
        private System.Windows.Forms.ToolStripMenuItem menuCreate;
        private System.Windows.Forms.ToolStripMenuItem menuRemove;
        private System.Windows.Forms.ToolStripMenuItem menuRename;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem menuSearch;

        public MyTreeView ClientTree{
            get { return clientTree;}
        }
        public MyTreeView ServerTree
        {
            get { return serverTree; }
        }

        public System.Windows.Forms.Button BtnConnect
        {
            get { return btnConnect; }
        }
        
    }
}

