namespace FileDelivery2_Client
{
    partial class FolderPrivilageDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDirection = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNickname = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstUser = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkRemove = new System.Windows.Forms.CheckBox();
            this.chkUpload = new System.Windows.Forms.CheckBox();
            this.chkDownload = new System.Windows.Forms.CheckBox();
            this.chkRead = new System.Windows.Forms.CheckBox();
            this.RemoveUserMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.제거하기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddUserMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_username = new System.Windows.Forms.ToolStripTextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.RemoveUserMenu.SuspendLayout();
            this.AddUserMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "경로";
            // 
            // txtDirection
            // 
            this.txtDirection.Location = new System.Drawing.Point(45, 12);
            this.txtDirection.Name = "txtDirection";
            this.txtDirection.ReadOnly = true;
            this.txtDirection.Size = new System.Drawing.Size(304, 21);
            this.txtDirection.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "별칭";
            // 
            // txtNickname
            // 
            this.txtNickname.Location = new System.Drawing.Point(45, 39);
            this.txtNickname.Name = "txtNickname";
            this.txtNickname.Size = new System.Drawing.Size(199, 21);
            this.txtNickname.TabIndex = 9;
            this.txtNickname.Leave += new System.EventHandler(this.txtNickname_Leave_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstUser);
            this.groupBox2.Location = new System.Drawing.Point(7, 155);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(338, 219);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "공유사용자";
            // 
            // lstUser
            // 
            this.lstUser.FormattingEnabled = true;
            this.lstUser.ItemHeight = 12;
            this.lstUser.Location = new System.Drawing.Point(5, 13);
            this.lstUser.Name = "lstUser";
            this.lstUser.Size = new System.Drawing.Size(328, 184);
            this.lstUser.TabIndex = 0;
            this.lstUser.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstUser_MouseUp_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkRemove);
            this.groupBox1.Controls.Add(this.chkUpload);
            this.groupBox1.Controls.Add(this.chkDownload);
            this.groupBox1.Controls.Add(this.chkRead);
            this.groupBox1.Location = new System.Drawing.Point(7, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(174, 70);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "권한";
            // 
            // chkRemove
            // 
            this.chkRemove.AutoSize = true;
            this.chkRemove.Location = new System.Drawing.Point(82, 42);
            this.chkRemove.Name = "chkRemove";
            this.chkRemove.Size = new System.Drawing.Size(48, 16);
            this.chkRemove.TabIndex = 5;
            this.chkRemove.Text = "삭제";
            this.chkRemove.UseVisualStyleBackColor = true;
            this.chkRemove.CheckedChanged += new System.EventHandler(this.chkRemove_CheckedChanged_1);
            // 
            // chkUpload
            // 
            this.chkUpload.AutoSize = true;
            this.chkUpload.Location = new System.Drawing.Point(82, 20);
            this.chkUpload.Name = "chkUpload";
            this.chkUpload.Size = new System.Drawing.Size(60, 16);
            this.chkUpload.TabIndex = 4;
            this.chkUpload.Text = "업로드";
            this.chkUpload.UseVisualStyleBackColor = true;
            this.chkUpload.CheckedChanged += new System.EventHandler(this.chkUpload_CheckedChanged_1);
            // 
            // chkDownload
            // 
            this.chkDownload.AutoSize = true;
            this.chkDownload.Location = new System.Drawing.Point(6, 42);
            this.chkDownload.Name = "chkDownload";
            this.chkDownload.Size = new System.Drawing.Size(72, 16);
            this.chkDownload.TabIndex = 3;
            this.chkDownload.Text = "다운로드";
            this.chkDownload.UseVisualStyleBackColor = true;
            this.chkDownload.CheckedChanged += new System.EventHandler(this.chkDownload_CheckedChanged_1);
            // 
            // chkRead
            // 
            this.chkRead.AutoSize = true;
            this.chkRead.Location = new System.Drawing.Point(6, 20);
            this.chkRead.Name = "chkRead";
            this.chkRead.Size = new System.Drawing.Size(48, 16);
            this.chkRead.TabIndex = 2;
            this.chkRead.Text = "읽기";
            this.chkRead.UseVisualStyleBackColor = true;
            this.chkRead.CheckedChanged += new System.EventHandler(this.chkRead_CheckedChanged_1);
            // 
            // RemoveUserMenu
            // 
            this.RemoveUserMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.제거하기ToolStripMenuItem});
            this.RemoveUserMenu.Name = "RemoveUserMenu";
            this.RemoveUserMenu.Size = new System.Drawing.Size(123, 26);
            // 
            // 제거하기ToolStripMenuItem
            // 
            this.제거하기ToolStripMenuItem.Name = "제거하기ToolStripMenuItem";
            this.제거하기ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.제거하기ToolStripMenuItem.Text = "제거하기";
            // 
            // AddUserMenu
            // 
            this.AddUserMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.AddUserMenu.Name = "MenuOption";
            this.AddUserMenu.Size = new System.Drawing.Size(123, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txt_username});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.toolStripMenuItem1.Text = "추가하기";
            // 
            // txt_username
            // 
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(100, 23);
            this.txt_username.Leave += new System.EventHandler(this.txt_username_Leave);
            // 
            // FolderPrivilageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 393);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDirection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNickname);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FolderPrivilageDialog";
            this.Text = "FolderPrivilageDialog";
            this.Load += new System.EventHandler(this.FolderPrivilageDialog_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.RemoveUserMenu.ResumeLayout(false);
            this.AddUserMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDirection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNickname;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstUser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkRemove;
        private System.Windows.Forms.CheckBox chkUpload;
        private System.Windows.Forms.CheckBox chkDownload;
        private System.Windows.Forms.CheckBox chkRead;
        private System.Windows.Forms.ContextMenuStrip RemoveUserMenu;
        private System.Windows.Forms.ToolStripMenuItem 제거하기ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip AddUserMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox txt_username;
    }
}