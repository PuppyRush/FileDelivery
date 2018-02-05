using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Security.AccessControl;

namespace FileDelivery2_Client
{
    public class TransferManager
    {

        private Object lockKey;
        public Socket sock;
        private Queue<FileInformation> sendQ;
        private MainForm f;

        public int QCount
        {
            get { return sendQ.Count; }
        }

        public TransferManager(MainForm form, Socket s)
        {
            sendQ = new Queue<FileInformation>();
            f = form;
            sock = s;
            lockKey = new object();
            
        }

        public void run()
        {
           
            bool exit = true;
            while (exit)
            {
                if (sendQ.Count == 0)
                    break;

                FileInformation fi = sendQ.Dequeue();
              
                if (fi.isServer)
                {
                    System.Threading.Thread t1 = new System.Threading.Thread(delegate()
                    {
                        exit = ThreadUploadFile(fi);
                    });
                    t1.Start();
                    t1.Join();
                    Thread.Sleep(500);
                    
                }
                else
                {

                }   
            }

        

        }

        public void DownLoadFile()
        {
            List<string[]> tempList = new List<string[]>();
            List<string> pathList = new List<string>();
            for (int i = 0; i < f.ServerTree.SelectedNodes.Count; i++)
            {
                TreeNode node = f.ServerTree.SelectedNodes[i];
                f.ServerTree.GetChildNodes(node, ref tempList);
            }
            for (int i = 0; i < tempList.Count; i++)
                pathList.Add(tempList[i][3]);

            string [] pathAry = GetDistinctValues<string>(pathList.ToArray());


            string clientNodeKey = f.ClientTree.GetParentPath(f.ClientTree.SelectedNode);
            SendRequstListToServer(pathAry, clientNodeKey);
        }



        public void SendFileToServer(){
            
            //fileList의 배열이 4개.
            //1은 부모노드키 2는 노드상경로 3은 이름 4는 실제경로(키)
            List<string[]> fileList = new List<string[]>();

            for (int i = 0; i < f.ClientTree.SelectedNodes.Count; i++)
            {
                TreeNode node = f.ClientTree.SelectedNodes[i];
                f.ClientTree.GetChildNodes(node, ref fileList);
            }
           
            string serverNodekey = f.ServerTree.GetParentPath( f.ServerTree.SelectedNode);

            for (int i = 0; i < fileList.Count; i++)
            {
                //폴더가 리스트에 들어있다면 무시한다.
                if (fileList[i][3].IndexOf(".") == -1)
                    continue;

                FileInformation fi = new FileInformation();
                fi.InitReceiver(f.client.serverName+"(서버)", serverNodekey, true);
                fi.InitSender(fileList[i][0], fileList[i][1], fileList[i][2], fileList[i][3]);
                sendQ.Enqueue(fi);

                f.progressDialog.PutFileInfo(ref fi);
            }

            PreviousUploadFile(fileList);
            Thread.Sleep(1000);
            //전송을 시작하기 위한 쓰레드
            Thread th = new Thread(run);
            th.IsBackground = true;
            th.Start();

            f.progressDialog.StartProgress();
            //f.progressDialog.ShowDialog();
        }

        private void PreviousUploadFile(List<string[]> fileList)
        {
            MemoryStream out_stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(out_stream);
            NetworkStream ns = new NetworkStream(sock);
            byte[] temp = new byte[4];

            //전송할 파일의 갯수보내기
            writer.Write(IPAddress.HostToNetworkOrder((int)CLIENT_MESSAGE.PREV_SEND_UPLOADFILE));
            writer.Write(IPAddress.HostToNetworkOrder(fileList.Count));
            writer.Flush();
            sock.Send(out_stream.ToArray());

            //파일의 제목들 보내기
            for (int i = 0; i < fileList.Count; i++)
            {
                temp = BitConverter.GetBytes( Encoding.UTF8.GetByteCount(fileList[i][2]));
                ns.Write(temp, 0, temp.Length);
                temp = Encoding.UTF8.GetBytes(fileList[i][2]);
                ns.Write(temp, 0, temp.Length);
            }

            ns.Flush();
            ns.Close();
            ns.Dispose();
            writer.Close();
            writer.Dispose();
            out_stream.Close();
            out_stream.Dispose();

        }

        private bool ThreadUploadFile(FileInformation I)
        {
            MemoryStream out_stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(out_stream);
            NetworkStream ns = new NetworkStream(sock);
            byte[] temp = new byte[4];

            writer.Write(IPAddress.HostToNetworkOrder((int)CLIENT_MESSAGE.SEND_UPLOADFILE));
            writer.Flush();
            sock.Send(out_stream.ToArray());

            //서버에 업로드될 노드의 키

            temp = BitConverter.GetBytes(I.BYTE_keyOfrecvNode.Length);
            ns.Write(temp, 0, temp.Length);
            ns.Write(I.BYTE_keyOfrecvNode, 0, I.BYTE_keyOfrecvNode.Length);

           

            //파일의 부모노드를 보낸다

            temp = BitConverter.GetBytes(I.BYTE_keyOfparentNode.Length);
            ns.Write(temp, 0, temp.Length);
            ns.Write(I.BYTE_keyOfparentNode, 0, I.BYTE_keyOfparentNode.Length);

            //노드의 키
            temp = BitConverter.GetBytes(I.BYTE_keyOfFile.Length);
            ns.Write(temp, 0, temp.Length);
            ns.Write(I.BYTE_keyOfFile, 0, I.BYTE_keyOfFile.Length);

            //노드의 이름
            temp = BitConverter.GetBytes(I.BYTE_fileName.Length);
            ns.Write(temp, 0, temp.Length);
            ns.Write(I.BYTE_fileName, 0, I.BYTE_fileName.Length);


             FileStream fs = File.OpenRead(I.filePath);
            long fileLength = fs.Length;

            //파일크기보내기//
            temp = BitConverter.GetBytes(fileLength);
            ns.Write(temp, 0, temp.Length);

            //버퍼보다 크기가 작은 파일은 한번에 보낸다.
            if (fileLength < CONST.BUFFER_SIZE)
            {
                //파일크기가 버퍼보다 작으면 한번에 끝
                temp = new byte[fileLength];
                fs.Read(temp, 0, temp.Length);
                ns.Write(temp, 0, temp.Length);
                I.progrssState = 100;
            }
            //버퍼보다 크기가 큰 파일은 버퍼만큼 나눈 횟수에 여분의 크기를 보낸다.
            else
            {
                //파일보내기
                
                long fileCount = fileLength / CONST.BUFFER_SIZE;
                int degree = 0;
                int remain = (int)(fileLength - fileCount * (long)CONST.BUFFER_SIZE);
                int l = 0;
                while (l < fileCount)
                {
                    temp = new byte[CONST.BUFFER_SIZE];
                    fs.Read(temp, 0, temp.Length);
                    ns.Write(temp, 0, temp.Length);

                    l++;
                    degree++;
                    if (degree == (int)fileCount / 100)
                    {
                        I.progrssState++;
                        degree = 0;
                    }
                }
                I.progrssState = 100;
                temp = new byte[remain];
                fs.Read(temp, 0, temp.Length);
                ns.Write(temp, 0, temp.Length);

                //ms.Close();
                //ms.Dispose();
            }

            
           
            ns.Flush();
            ns.Close();
            ns.Dispose();
            writer.Close();
            writer.Dispose();
            out_stream.Close();
            out_stream.Dispose();
            return true;
        }


        public void ThreadReceiveFile(Socket sock, int fileCount)
        {

            byte[] temp = new byte[4];
            List<string> nodeList = new List<string>();

            byte[] fileArray = new byte[CONST.BUFFER_SIZE];
            NetworkStream ns = new NetworkStream(sock);
            int strlen = 0;
            string key, name;
            TreeNode[] rootNode = new TreeNode[1];

            long fileLength = 0;
            lock (lockKey)
            {
                //업로드될 노드선택 (노드의키 받기)
                temp = new byte[4];
                ns.Read(temp, 0, temp.Length);
                strlen = BitConverter.ToInt32(temp, 0);
                temp = new byte[strlen];
                ns.Read(temp, 0, temp.Length);
                key = Encoding.UTF8.GetString(temp);
                rootNode = f.ClientTree.Nodes.Find(key, true);


                //파일인 노드 키 받기
                temp = new byte[4];
                ns.Read(temp, 0, temp.Length);
                strlen = BitConverter.ToInt32(temp, 0);
                temp = new byte[strlen];
                ns.Read(temp, 0, temp.Length);
                string originalRoot = Encoding.UTF8.GetString(temp);

                //파일인 노드 키 받기
                temp = new byte[4];
                ns.Read(temp, 0, temp.Length);
                strlen = BitConverter.ToInt32(temp, 0);
                temp = new byte[strlen];
                ns.Read(temp, 0, temp.Length);
                key = Encoding.UTF8.GetString(temp);

                //파일인 노드의 이름 받기
                temp = new byte[4];
                ns.Read(temp, 0, temp.Length);
                strlen = BitConverter.ToInt32(temp, 0);
                temp = new byte[strlen];
                ns.Read(temp, 0, temp.Length);
                name = Encoding.UTF8.GetString(temp);

                string parentNode = rootNode[0].Name + "\\" + originalRoot;//전송받을 파일의 상위폴더
                string fullNamePath = parentNode + "\\" + name;  //전송파일의 전체경로

                //파일크기받기(long)
                temp = new byte[8];
                ns.Read(temp, 0, temp.Length);
                fileLength = BitConverter.ToInt64(temp, 0);


                FileInfo fi = new FileInfo(fullNamePath);

                FileSecurity fSecurity = null;

                FileStream fileStream = null;
                //fSecurity = File.GetAccessControl(rootNode[0].Name);
                //fSecurity.AddAccessRule(new FileSystemAccessRule("NETWORK SERVICE",
                //    FileSystemRights.FullControl, AccessControlType.Allow));

                ////없는 폴더를 받았다면 상위의 폴더들을 만든다.
                DirectoryManager.CreateRootDir(parentNode);
                MyTreeView.MakeParentNode(rootNode[0], originalRoot);

                fileStream = File.Create(fullNamePath, CONST.BUFFER_SIZE);

                //File.SetAccessControl(rootNode[0].Name, fSecurity);

                if (fileLength < CONST.BUFFER_SIZE)
                {
                    fileArray = new byte[fileLength];
                    ns.Read(fileArray, 0, fileArray.Length);
                    fileStream.Write(fileArray, 0, fileArray.Length);
                }
                else
                {
                    int l = 0;
                    int byteCount = (int)(fileLength / CONST.BUFFER_SIZE);
                    int remain = (int)(fileLength - byteCount * CONST.BUFFER_SIZE);
                    //4바이트는 빈공간이 날라오므로 먼저 읽어버린다.
                    //ns.Read(new byte[4], 0, 4);

                    while (l < byteCount)
                    {

                        fileArray = new byte[CONST.BUFFER_SIZE];
                        ns.Read(fileArray, 0, fileArray.Length);
                        fileStream.Write(fileArray, 0, fileArray.Length);

                        l++;

                    }

                    //남은 여분의 크기를 받는다.
                    fileArray = new byte[remain];
                    ns.Read(fileArray, 0, fileArray.Length);
                    fileStream.Write(fileArray, 0, fileArray.Length);

                }

                fileStream.Close();
                fileStream.Dispose();

                f.ClientTree.Invoke(new Action(delegate()
                {
                    //이미 존재하는경우
                    if (f.ClientTree.Nodes.Find(fullNamePath, true).Length > 0)
                        ;

                    else if (f.ClientTree.Nodes.Find(parentNode, true).Length == 0)
                        rootNode[0].Nodes.Add(fullNamePath, name);
                    else
                         f.ClientTree.Nodes.Find(parentNode, true)[0].Nodes.Add(fullNamePath,name);

                }));

            }

            ns.Close();
            ns.Dispose();
        }

        private void SendRequstListToServer(string[] pathAry, string destPath)
        {
            MemoryStream out_stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(out_stream);
            NetworkStream ns = new NetworkStream(sock);
            byte[] temp = new byte[4];

            writer.Write(IPAddress.HostToNetworkOrder((int)CLIENT_MESSAGE.SEND_DOWNLOADFILE_LIST));
            writer.Write(destPath);
            writer.Write(IPAddress.HostToNetworkOrder(pathAry.Length));
            writer.Flush();
            sock.Send(out_stream.ToArray());

            for (int i = 0; i < pathAry.Length; i++)
            {

                //파일경로(key)
                temp = Encoding.UTF8.GetBytes(pathAry[i]);
                temp = BitConverter.GetBytes(temp.Length);
                ns.Write(temp, 0, temp.Length);
                temp = Encoding.UTF8.GetBytes(pathAry[i]);
                ns.Write(temp, 0, temp.Length);

               
            }
        }



        public static T[] GetDistinctValues<T>(T[] array)
        {
            List<T> tmp = new List<T>();

            for (int i = 0; i < array.Length; i++)
            {
                if (tmp.Contains(array[i]))
                    continue;
                tmp.Add(array[i]);
            }

            return tmp.ToArray();
        }


    }

}
