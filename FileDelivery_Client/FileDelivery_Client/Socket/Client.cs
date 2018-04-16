using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;

namespace FileDelivery2_Client
{
    public class Client
    {

        public int buf_size;
        public byte[] buffer;

        public string clientName;
        public bool isLogin;
        public Socket clientSocket;
        public string serverName;
        
        private TreeNode selectedNode = null;
        private TreeNode serverNode;        //서버단에 추가 될 사용자만의 트리노드
        private TreeNode clientNode;        //클라리언트단에..

        public Dictionary<string, ShareFolder> clientFolderMap;
        public Dictionary<string, ShareFolder> serverFolderMap;

        MainForm f = null;                  //메인폼
 
        public Client( MainForm form)
        {

            isLogin = false;
 
            clientFolderMap = new Dictionary<string, ShareFolder>();
            serverFolderMap = new Dictionary<string, ShareFolder>();

            f = form;

  
            clientNode = new TreeNode();
            clientNode.Name = clientName;
            clientNode.Text = clientName;
            serverNode = new TreeNode();
            serverNode.Name = clientName;
            serverNode.Text = clientName;
        }
        
        public void ConnectToServer(string id, string pw, string ip, string port)
        {
            if (clientSocket == null || clientSocket.Connected == false)
            {
                try
                {
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
                    clientSocket.Connect(ie);
                    f.DoAddText("서버 접속중입니다...");

                    if (clientSocket.Connected == true)
                    {
                        
                        DoConnect(id, pw);

                    }
                    else
                    {
                        f.DoAddText("서버 접속에 실패했습니다");
                    }

                }
                catch (SocketException ex)
                {
                    f.DoAddText(ex.Message);
                }
                catch (ThreadStartException ex)
                {
                    f.DoAddText(ex.Message);
                }
            }//if
        }

        public void DoConnect(string id, string pw)
        {
            MemoryStream out_stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(out_stream);

            //로그인 요구/
            writer.Write(IPAddress.HostToNetworkOrder((int)CLIENT_MESSAGE.REQ_LOGIN));
            writer.Write(id);
            writer.Write(pw);
            out_stream.Flush();
            clientSocket.Send(out_stream.ToArray());

            Thread th = new Thread(Receive);
            th.IsBackground = true;
            th.Start();

            clientName = id;
            f.DoChangeBtnName(f.BtnConnect , CONST.BTN_NAME_DISCONNECT);
            f.transferManager = new TransferManager(f, clientSocket);
            writer.Close();
            writer.Dispose();
            out_stream.Close();
            out_stream.Dispose();


        }

        public void DoLogin(string id)
        {

            isLogin = true;
            RegistryManager.InitUserRegistry(id);
            
            DirectoryInfo di = new DirectoryInfo(RegistryManager.UserDirectory(id));
            if (di.Exists == false)
            {
                di.Create();
            }

            FileInfo fi = new FileInfo(RegistryManager.UserDirectory(id) + "\\" + RegistryManager.LogfileName);
            if (fi.Exists == false)
            {
                fi.CreateText();
                
            }

            f.ReadShareFile(id);

            
            f.ClientTree.InitField(f.rootFolderPath, f.shareFolderMap, f);
            f.ServerTree.InitField(f.rootFolderPath, f.shareFolderMap, f);

            f.shareDialog.Sharefolder = f.shareFolderMap;

            Thread th = new Thread(f.ClientTree.run);
            th.IsBackground = true;
            th.Start();


        }

        public bool DoLogout()
        {
            if (isLogin)
            {
                if (clientSocket != null && clientSocket.Connected)
                {
                    MemoryStream out_stream = new MemoryStream();
                    BinaryWriter writer = new BinaryWriter(out_stream);


                    writer.Write(IPAddress.HostToNetworkOrder((int)CLIENT_MESSAGE.NOTI_DISCONNECT));
                    writer.Flush();
                    clientSocket.Send(out_stream.ToArray());

                    if (clientSocket.Connected)
                    {
                        clientSocket.Disconnect(false);
                    }
                    clientSocket.Close();
                    clientSocket.Dispose();

                    writer.Close();
                    writer.Dispose();
                    out_stream.Close();
                    out_stream.Dispose();

                }

                f.ClientTree.Invoke(new Action(delegate()
                {

                    f.ClientTree.Nodes.Clear();
                }));

                f.ServerTree.Invoke(new Action(delegate()
                {

                    f.ServerTree.Nodes.Clear();
                }));
                
                f.WriteShareFile(clientName);
                f.ServerTree.isInit = false;
                f.ClientTree.isInit = false;

                f.shareDialog.ResetShareDialog();
                f.shareFolderMap.Clear();

                f.ClientTree.shareFolderMap.Clear();
                
                f.DoAddText("서버와 접속이 해제되었습니다");
                f.noticeDialog.Visible = false;
                isLogin = false;
                f.BtnConnect.Invoke(new Action(delegate()
                {
                    f.BtnConnect.Text = CONST.BTN_NAME_LOGIN;
                })); return true;
            }
            
            f.noticeDialog.Visible = false;
            isLogin = false;
            f.BtnConnect.Invoke(new Action(delegate()
            {
                f.BtnConnect.Text = CONST.BTN_NAME_LOGIN;
            }));

            return false;
        }

        public void DoJoin(string id, string pw ,string mail, string ip, string port)
        {
                //접속시도
            MemoryStream out_stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(out_stream);

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
            clientSocket.Connect(ie);
            if (clientSocket.Connected)
            {
                System.Threading.Thread t1 = new System.Threading.Thread
                    (delegate()
                    {
                        JoinForReceive(clientSocket);
                    });
                    t1.Start();
                    

                writer.Write(IPAddress.HostToNetworkOrder((int)CLIENT_MESSAGE.REQ_JOIN));
                writer.Write(id);
                writer.Write(pw);
                writer.Write(mail);
                writer.Flush();
                clientSocket.Send(out_stream.ToArray());


                 
            }
            else
            {
                MessageBox.Show("서버에 접속을 실패하였습니다. 서버가 열려있지 않거나 아이피, 포트번호에 이상이 있습니다.");
            }
            writer.Flush();
            writer.Close();
            writer.Dispose();
            out_stream.Close();
            out_stream.Dispose();
         
        }


        //
        public bool JoinCommand(int cmd)
        {
            if (cmd ==(int) SERVER_MESSAGE.RES_JOIN)
            {
                byte[] buf = new byte[1024];
                MemoryStream in_stream = new MemoryStream(buf, 0, buf.Length, true);
                BinaryReader reader = new BinaryReader(in_stream);
                string msg="", name;
                reader.ReadInt32();
                if (reader.ReadBoolean())
                {
                   
                }
                else
                {

                }
                msg = reader.ReadString();
                serverName = reader.ReadString();

                f.DoAddText(msg);

                return false;
            
            }
            return true;
        }

        public void JoinForReceive(Socket socket)
        {
            byte[] buf = new byte[2048];

            bool exit = true;

            try
            {
                while (exit)
                {
                    if (socket == null || socket.Connected == false)
                    {
                        f.DoAddText("서버와 접속에 문제가 발생였습니다.");
                        break;
                    }


                    socket.Receive(buf, 0, buf.Length, SocketFlags.None);
                    int cmd = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buf, 0));
                    exit = JoinCommand(cmd);


                }
            }catch(SocketException ex){
                f.DoAddText(ex.Message);
            }

            socket.Disconnect(true);
            socket.Close();
            socket.Dispose();

       

        }

        //

        private void Receive()
        {

            byte[] buf = new byte[2048];

            bool exit = true;

            try
            {
                while (exit)
                {
                    if ( clientSocket==null || clientSocket.Connected == false)
                    {
                        f.DoAddText("서버와 접속이 끊겼습니다");
                        break;
                    }


                    clientSocket.Receive(buf, 0, buf.Length, SocketFlags.None);
                    int cmd = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buf, 0));
                    exit = DoCommand(cmd, buf);
                    

                }
            }
            catch (ThreadAbortException ex)
            {
                f.DoAddText(ex.Message);
            }

            catch (SocketException ex)
            {
                f.DoAddText(ex.Message);
            }

            catch (Exception ex)
            {
                f.DoAddText(ex.Message);
            }
        }

        private bool DoCommand(int cmd, byte[] buf )
        {
            if (clientSocket.Connected == false)
                return false;
            MemoryStream out_stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(out_stream);
            MemoryStream in_stream = new MemoryStream(buf, 0, buf.Length, true);
            BinaryReader reader = new BinaryReader(in_stream);
            NetworkStream ns = new NetworkStream(clientSocket);

            List<string> nodeList = new List<string>();
            byte[] temp = new byte[4];
            int listCount = 0;
            int strlen = 0;
            string key, name;
            TreeNode[] rootNode = new TreeNode[1];
            int len = 0;
            

            string msg = "";
            reader.ReadInt32();

            reader = new BinaryReader(in_stream);
            switch (cmd)
            {
                case (int)SERVER_MESSAGE.CLOSE_SERVER:

                    DoLogout();
                    break;


                case (int)SERVER_MESSAGE.SEND_ERROR:

                    msg = reader.ReadString();
                    f.DoAddText(msg);

                    break;
             
                //로그인 요청에 대한 서버응답
                case (int)SERVER_MESSAGE.RES_LOGIN:

                    //로그인성공
                    isLogin = reader.ReadBoolean();

                    if (isLogin)
                    {

                        DoLogin(clientName);
                        f.DoAddText(reader.ReadString());
                        serverName = reader.ReadString();

                        writer.Write(IPAddress.HostToNetworkOrder((int)CLIENT_MESSAGE.REQ_SERVER_TREE));
                        writer.Flush();
                        clientSocket.Send(out_stream.ToArray());
                    }
                    else
                    {
                        //menu_join.Enabled = true;
                        isLogin = false;
                    }
                    break;
                    
                case (int)SERVER_MESSAGE.SEND_SERVER_TREE:
                    
                    lock (this)
                    {
                                             
                        nodeList = new List<string>();
                        temp = new byte[4];
                        listCount = 0;
                        strlen = 0;
                        rootNode = new TreeNode[1];
                    
                        ns = new NetworkStream(clientSocket);
                        //유저갯수받기
                        temp = new byte[4];
                        ns.Read(temp, 0, temp.Length);
                        int userCount = BitConverter.ToInt32(temp, 0);
                       
                        for (int l = 0; l < userCount; l++)
                        {

                            //루트의 유저이름받기
                            temp = new byte[4];
                            ns.Read(temp, 0, temp.Length);
                            strlen = BitConverter.ToInt32(temp, 0);
                            temp = new byte[strlen];
                            ns.Read(temp, 0, temp.Length);
                            name = Encoding.UTF8.GetString(temp);

                            //루트의 키
                            temp = new byte[4];
                            ns.Read(temp, 0, temp.Length);
                            strlen = BitConverter.ToInt32(temp, 0);
                            temp = new byte[strlen];
                            ns.Read(temp, 0, temp.Length);
                            key = Encoding.UTF8.GetString(temp);

                            f.ServerTree.Invoke(new Action(delegate()
                            {
                                f.ServerTree.Nodes.Add(key, name);
                            }));

                           

                            //리스트갯수받기
                            temp = new byte[4];
                            ns.Read(temp, 0, temp.Length);
                            listCount = BitConverter.ToInt32(temp, 0);

                            //사용자의 빈 폴더를 받은경우
                            if (listCount == 0)
                                continue;


                            //갯수가져오기
                            while (listCount > 0)
                            {
                                temp = new byte[4];
                                ns.Read(temp, 0, 4);
                                len = BitConverter.ToInt32(temp, 0);
                                listCount--;

                                //루트가져오기
                                temp = new byte[4];
                                ns.Read(temp, 0, temp.Length);
                                strlen = BitConverter.ToInt32(temp, 0);
                                temp = new byte[strlen];
                                ns.Read(temp, 0, temp.Length);
                                key = Encoding.UTF8.GetString(temp);
                                listCount--;

                                f.ServerTree.Invoke(new Action(delegate()
                                {
                                    rootNode = f.ServerTree.Nodes.Find(key, true);
                                }));

                               

                                for (int i = 0; i < len; i++)
                                {
                                    temp = new byte[4];
                                    ns.Read(temp, 0, temp.Length);
                                    strlen = BitConverter.ToInt32(temp, 0);
                                    temp = new byte[strlen];
                                    ns.Read(temp, 0, temp.Length);
                                    key = Encoding.UTF8.GetString(temp);

                                    temp = new byte[4];
                                    ns.Read(temp, 0, temp.Length);
                                    strlen = BitConverter.ToInt32(temp, 0);
                                    temp = new byte[strlen];
                                    ns.Read(temp, 0, temp.Length);
                                    name = Encoding.UTF8.GetString(temp);

                                    f.ServerTree.Invoke(new Action(delegate()
                                    {
                                        rootNode[0].Nodes.Add(key, name);
                                    }));

                                    

                                    listCount -= 2;
                                }


                            }
                        }

                        ns.Close();
                    }

                    writer.Write(IPAddress.HostToNetworkOrder((int)CLIENT_MESSAGE.RES_SERVER_TREE));
                    writer.Flush();
                    clientSocket.Send(out_stream.ToArray());
                    
                    break;

                case (int)SERVER_MESSAGE.REQ_CLIENT_TREE:

                    Thread.Sleep(500);

                    //루트폴더와 그 폴더에 걸린 공유사용자와 권한을 보낸다.
                    writer.Write(IPAddress.HostToNetworkOrder((int)CLIENT_MESSAGE.SEND_CLIENT_TREE));
                    writer.Flush();
                    clientSocket.Send(out_stream.ToArray());


                    nodeList = f.ClientTree.nodeList;
                    ns = new NetworkStream(clientSocket);
                    temp = new byte[4];
                    len = 0;
                    int cursor = 0;
                    lock (this)
                    {

                        //이름보내기
                        temp = Encoding.UTF8.GetBytes(clientName);
                        temp = BitConverter.GetBytes(temp.Length);
                        ns.Write(temp, 0, temp.Length);
                        temp = Encoding.UTF8.GetBytes(clientName);
                        ns.Write(temp, 0, temp.Length);

                        //리스트갯수보내기
                        temp = BitConverter.GetBytes(nodeList.Count);
                        ns.Write(temp, 0, temp.Length);

                        //루트갯수보내기
                        len = Int32.Parse(nodeList[cursor++]);
                        temp = BitConverter.GetBytes(len);
                        ns.Write(temp, 0, temp.Length);

                        //루트의 키와 이름 보내기
                        for (int i = 0; i < len * 2; i++)
                        {
                            temp = Encoding.UTF8.GetBytes(nodeList[cursor]);
                            temp = BitConverter.GetBytes(temp.Length);
                            ns.Write(temp, 0, temp.Length);
                            temp = Encoding.UTF8.GetBytes(nodeList[cursor++]);
                            ns.Write(temp, 0, temp.Length);
                        }

                        //보내려는 노드(폴더)의 부모노드의 키를 보내고
                        //보내려는 노드안의 폴더들과 파일들의 갯수, 키, 이름을 보낸다.

                        while (nodeList.Count > cursor)
                        {

                            //폴더갯수
                            len = Int32.Parse(nodeList[cursor++]);
                            temp = BitConverter.GetBytes(len);
                            ns.Write(temp, 0, temp.Length);
                            if (len == 0)
                                continue;

                            //루트
                            temp = Encoding.UTF8.GetBytes(nodeList[cursor]);
                            temp = BitConverter.GetBytes(temp.Length);
                            ns.Write(temp, 0, temp.Length);
                            temp = Encoding.UTF8.GetBytes(nodeList[cursor++]);
                            ns.Write(temp, 0, temp.Length);


                            //폴더들의 키와 이름
                            for (int i = 0; i < len * 2; i++)
                            {
                                temp = Encoding.UTF8.GetBytes(nodeList[cursor]);
                                temp = BitConverter.GetBytes(temp.Length);
                                ns.Write(temp, 0, temp.Length);
                                temp = Encoding.UTF8.GetBytes(nodeList[cursor++]);
                                ns.Write(temp, 0, temp.Length);

                            }
                            ns.Flush();
                            len = 0;
                        }
                    }
                  

                    break;

                case (int)SERVER_MESSAGE.SEND_NOTICE:

                    string notice = reader.ReadString();

                    if (DateTime.Now.ToShortDateString().Equals(RegistryManager.Notice) == false)
                    {
                        f.Invoke(new Action(delegate()
                        {
                            f.ShowNotice(notice);
                        }));
                    }
                   

                    break;


                case (int)SERVER_MESSAGE.SEND_DOWNLOAD_FILE:

                    int fileCount = IPAddress.NetworkToHostOrder(reader.ReadInt32());



                    System.Threading.Thread t1 = new System.Threading.Thread
                     (delegate()
                     {
                         f.transferManager.ThreadReceiveFile(clientSocket, fileCount);
                     });
                    t1.IsBackground = true;
                    t1.Start();
                    t1.Join();

                    break;

            }

            ns.Flush();
            ns.Close();
            ns.Dispose();
            writer.Close();
            reader.Close();
            out_stream.Close();
            in_stream.Close();
            writer.Dispose();
            reader.Dispose();
            out_stream.Dispose();
            in_stream.Dispose();
            return true;

        }
 
    }
}
