namespace FileDelivery2_Client
{
    partial class JoinDialog
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
            this.btn_rewrite = new System.Windows.Forms.Button();
            this.btn_join = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPasswordRewrite = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_rewrite
            // 
            this.btn_rewrite.Location = new System.Drawing.Point(114, 157);
            this.btn_rewrite.Name = "btn_rewrite";
            this.btn_rewrite.Size = new System.Drawing.Size(95, 22);
            this.btn_rewrite.TabIndex = 41;
            this.btn_rewrite.Text = "다시쓰기";
            this.btn_rewrite.UseVisualStyleBackColor = true;
            this.btn_rewrite.Click += new System.EventHandler(this.btn_rewrite_Click);
            // 
            // btn_join
            // 
            this.btn_join.Location = new System.Drawing.Point(8, 157);
            this.btn_join.Name = "btn_join";
            this.btn_join.Size = new System.Drawing.Size(95, 22);
            this.btn_join.TabIndex = 40;
            this.btn_join.Text = "가입하기";
            this.btn_join.UseVisualStyleBackColor = true;
            this.btn_join.Click += new System.EventHandler(this.btn_join_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(114, 124);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(77, 21);
            this.txtPort.TabIndex = 39;
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(114, 103);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(77, 21);
            this.txtIp.TabIndex = 38;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(114, 81);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(77, 21);
            this.txtEmail.TabIndex = 37;
            // 
            // txtPasswordRewrite
            // 
            this.txtPasswordRewrite.Location = new System.Drawing.Point(114, 58);
            this.txtPasswordRewrite.Name = "txtPasswordRewrite";
            this.txtPasswordRewrite.Size = new System.Drawing.Size(77, 21);
            this.txtPasswordRewrite.TabIndex = 36;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(114, 35);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(77, 21);
            this.txtPassword.TabIndex = 35;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(114, 12);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(77, 21);
            this.txtId.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "포트번호";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 32;
            this.label5.Text = "아이피";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 31;
            this.label4.Text = "메일";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "비밀번호 확인";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 29;
            this.label2.Text = "비밀번호";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 28;
            this.label1.Text = "아이디";
            // 
            // JoinDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 188);
            this.Controls.Add(this.btn_rewrite);
            this.Controls.Add(this.btn_join);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPasswordRewrite);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(239, 226);
            this.MinimumSize = new System.Drawing.Size(239, 226);
            this.Name = "JoinDialog";
            this.Text = "JoinDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_rewrite;
        private System.Windows.Forms.Button btn_join;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPasswordRewrite;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}