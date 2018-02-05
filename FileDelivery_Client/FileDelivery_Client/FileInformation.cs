using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileDelivery2_Client
{
    public class FileInformation
    {

        public int progrssState;
        public bool isServer;

        public string receiverName;
        public string keyOfreceivNode;      //전송받을 측의 노드key(실제경로)

        public byte[] BYTE_keyOfrecvNode;

        public string parentNodeKey;      //전송될 노드의 부모노드의 키
        public string nodePath;            //전송될 노드의 키
        public string filePath;             //전송될 파일의 전체경로
        public string fileName;

        public byte[] BYTE_filePath;
        public byte[] BYTE_keyOfparentNode;
        public byte[] BYTE_keyOfFile;
        public byte[] BYTE_fileName;

        public FileInformation()
        {
            progrssState = 0;
        }

        public void InitReceiver(string recvName, string recvNodeKey, bool isS){
            receiverName = recvName;
            BYTE_keyOfrecvNode = Encoding.UTF8.GetBytes(recvNodeKey);
            keyOfreceivNode = recvNodeKey;
            isServer = isS;
            
        }

        public void InitSender(string senderParnetNodeKey, string nodekey, string name, string fullpath)
        {
            BYTE_keyOfparentNode = Encoding.UTF8.GetBytes(senderParnetNodeKey);
            parentNodeKey = senderParnetNodeKey;

            BYTE_keyOfFile = Encoding.UTF8.GetBytes(nodekey);
            nodePath = nodekey;

            BYTE_fileName = Encoding.UTF8.GetBytes(name);
            fileName = name;

            BYTE_filePath = Encoding.UTF8.GetBytes(fullpath);
            filePath = fullpath;
        }

        public byte[] KeyOfreceivNode
        {
            get { return BYTE_keyOfrecvNode; }
        }

        public byte[] KeyOfparentNode
        {
            get { return BYTE_keyOfparentNode; }
        }

        public byte[] KeyOfFile
        {
            get { return BYTE_keyOfFile; }
        }
        public byte[] FileName
        {
            get { return BYTE_fileName; }
        }


    }
}
