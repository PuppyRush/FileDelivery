using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

enum PRVIL_NUMBER
{

    NONE = 0,
    READ = 1,
    DOWNLOAD = 2,
    UPLOAD = 4,
    REMOVE = 8

}

namespace FileDelivery2_Client
{
    public class ShareFolder
    {
        public string pathname;
        public string nickname;
        public string folderName;
        public Dictionary<string, ShareUser> shareUserMap;
        //공유유저 이름이 key

        public ShareFolder(string pathname, string nickname, string foldername)
        {
            this.pathname = pathname;
            this.nickname = nickname;
            this.folderName = foldername;
        }



        /////////////////////////////////////////
        public class ShareUser
        {
            public string name;
            public int isread;
            public int isdownload;
            public int isupload;
            public int isremove;
            public int privilage;

            public ShareUser(string name, int read, int down, int up, int remove)
            {
                this.name = name;
                isread = read;
                isdownload = down;
                isupload = up;
                isremove = remove;
                SetPrivialge();
            }

            public void SetPrivialge()
            {
                privilage = isread | isdownload | isupload | isremove;
            }

            public override string ToString()
            {

                string str = name + " " + isread.ToString() + " " + isdownload.ToString() + " " + isupload.ToString() + " " + isremove.ToString();
                return str;
            }
        }//shareuser class////////////////////////////////////////  

    }//share folder class /////////////////////////////////////////////
}