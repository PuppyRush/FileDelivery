using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace FileDelivery2_Client
{
    public partial class TransferProgressDialog : Form
    {
        private Queue<FileInformation> fileQ;
        private MainForm form;
        public TransferProgressDialog(MainForm form)
        {
            
            InitializeComponent();
            fileQ = new Queue<FileInformation>();
            this.form = form;
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Step = 1;
        }

        public void PutFileInfo(ref FileInformation f){
            fileQ.Enqueue(f);
        }

        private void TransferProgressDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Hide();
            e.Cancel = true;
        }

        public void StartProgress(){
            
            Thread _th = new Thread(run);
            _th.IsBackground = true;
            _th.Start();
        }

        public void run()
        {
            
            bool exit = true;
            while (exit)
            {

                if (fileQ.Count <= 0)
                    break;

                FileInformation f = fileQ.Dequeue();          

                 this.Invoke(new Action(delegate()
                {
                    Visible = true;
                    lstFileList.Items.Add(f.receiverName + "로" + f.fileName + "을 전송중입니다...");
                    
                    while (f.progrssState < 100)
                    {
                        progressBar.Value = f.progrssState;
                    }

                    lstFileList.Items.Add(f.fileName + "의 전송을 마쳤습니다.");
                    lstFileList.Items.Add("======================================");
                    progressBar.Value = 0;
                }));
               

            }

        }

    
    }
}
