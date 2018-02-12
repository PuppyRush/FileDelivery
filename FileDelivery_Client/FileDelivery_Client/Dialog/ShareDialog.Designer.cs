namespace FileDelivery2_Client
{
    partial class ShareDialog
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
            this.lstShare = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.menuOption = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuShowDialog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.제거하기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.menuOption.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstShare
            // 
            this.lstShare.AllowDrop = true;
            this.lstShare.FormattingEnabled = true;
            this.lstShare.HorizontalScrollbar = true;
            this.lstShare.ItemHeight = 12;
            this.lstShare.Location = new System.Drawing.Point(8, 20);
            this.lstShare.Name = "lstShare";
            this.lstShare.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstShare.Size = new System.Drawing.Size(170, 184);
            this.lstShare.TabIndex = 0;
            this.lstShare.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstShare_DragDrop);
            this.lstShare.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstShare_DragEnter);
            this.lstShare.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstShare_MouseUp);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(184, 20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(106, 37);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "추가하기";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(184, 63);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(106, 37);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "제거하기";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // menuOption
            // 
            this.menuOption.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuShowDialog,
            this.menuRemove});
            this.menuOption.Name = "menu_option";
            this.menuOption.Size = new System.Drawing.Size(123, 48);
            this.menuOption.Text = "설정";
            // 
            // menuShowDialog
            // 
            this.menuShowDialog.Name = "menuShowDialog";
            this.menuShowDialog.Size = new System.Drawing.Size(122, 22);
            this.menuShowDialog.Text = "공유설정";
            this.menuShowDialog.Click += new System.EventHandler(this.menuShowDialog_Click);
            // 
            // menuRemove
            // 
            this.menuRemove.Name = "menuRemove";
            this.menuRemove.Size = new System.Drawing.Size(122, 22);
            this.menuRemove.Text = "제거하기";
            this.menuRemove.Click += new System.EventHandler(this.menuRemove_Click);
            // 
            // 제거하기ToolStripMenuItem
            // 
            this.제거하기ToolStripMenuItem.Name = "제거하기ToolStripMenuItem";
            this.제거하기ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.제거하기ToolStripMenuItem.Text = "제거하기";
            // 
            // listBox1
            // 
            this.listBox1.AllowDrop = true;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(6, 20);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox1.Size = new System.Drawing.Size(163, 172);
            this.listBox1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(175, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 37);
            this.button1.TabIndex = 6;
            this.button1.Text = "제거하기";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(175, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 37);
            this.button2.TabIndex = 5;
            this.button2.Text = "추가하기";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.lstShare);
            this.groupBox1.Location = new System.Drawing.Point(12, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 218);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "내 공유폴더 설정";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Location = new System.Drawing.Point(340, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 212);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "서버 공유폴더 설정";
            // 
            // ShareDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 280);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ShareDialog";
            this.Text = "ShareDialog";
            this.Load += new System.EventHandler(this.ShareDialog_Load);
            this.menuOption.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstShare;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ContextMenuStrip menuOption;
        private System.Windows.Forms.ToolStripMenuItem menuShowDialog;
        private System.Windows.Forms.ToolStripMenuItem menuRemove;
        private System.Windows.Forms.ToolStripMenuItem 제거하기ToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}