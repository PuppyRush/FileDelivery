namespace FileDelivery2_Client
{
    partial class NoticeDialog
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
            this.txtNotice = new System.Windows.Forms.TextBox();
            this.chkNotice = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtNotice
            // 
            this.txtNotice.Location = new System.Drawing.Point(11, 13);
            this.txtNotice.Multiline = true;
            this.txtNotice.Name = "txtNotice";
            this.txtNotice.Size = new System.Drawing.Size(283, 236);
            this.txtNotice.TabIndex = 0;
            // 
            // chkNotice
            // 
            this.chkNotice.AutoSize = true;
            this.chkNotice.Location = new System.Drawing.Point(69, 254);
            this.chkNotice.Name = "chkNotice";
            this.chkNotice.Size = new System.Drawing.Size(224, 16);
            this.chkNotice.TabIndex = 1;
            this.chkNotice.Text = "오늘은 더 공지사항을 보지 않습니다.";
            this.chkNotice.UseVisualStyleBackColor = true;
            this.chkNotice.CheckedChanged += new System.EventHandler(this.chkNotice_CheckedChanged);
            // 
            // NoticeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 282);
            this.Controls.Add(this.chkNotice);
            this.Controls.Add(this.txtNotice);
            this.MaximumSize = new System.Drawing.Size(321, 320);
            this.MinimumSize = new System.Drawing.Size(321, 320);
            this.Name = "NoticeDialog";
            this.Text = "공지사항";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NoticeDialog_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NoticeDialog_FormClosed);
            this.Load += new System.EventHandler(this.NoticeDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtNotice;
        private System.Windows.Forms.CheckBox chkNotice;

    }
}