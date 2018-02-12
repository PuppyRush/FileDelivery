using System;
using System.Drawing;


namespace FileDelivery2_Client
{


    enum SERVER_MESSAGE
    {
        SEND_MESSAGE = 0,
        SEND_ERROR_FULL,
        SEND_ERROR,

        RES_LOGIN,
        RES_JOIN,

        REQ_CLIENT_TREE,
        SEND_SERVER_TREE,

        SEND_DOWNLOAD_FILE,
        
        SEND_NOTICE,

        CLOSE_SERVER
    }

    enum CLIENT_MESSAGE
    {
        REQ_JOIN = 100,
        REQ_LOGIN,

        REQ_SERVER_TREE,
        SEND_CLIENT_TREE,
        RES_SERVER_TREE,

        PREV_SEND_UPLOADFILE,     //파일보내기전 갯수와 제목들 송신
        SEND_UPLOADFILE,

        SEND_DOWNLOADFILE_LIST,

        NOTI_DISCONNECT
    }

    public class CONST
    {
        public const int IS_FOLDER = 1;
        public const int IS_FILE = 0;
        public const int MAX_DEGREE = 100;
        public const int MIN_DEGREE = 0;
        public const int STEP_DEGREE = 1;
        public const int HEIGHT_MARGIN = 25;
        public const int TREE_SPACE = 8;
        public const int WINDOW_MARGIN = 8;
        public static Point CLIENT_TREE_LOCATION = new Point(8, 52);

        public const string PROGRAM_NAME = "FileDelivery";

        public const int ID_MAXLEN = 10;
        public const int PW_MAXLEN = 10;
        public const int PORT_MAX = 65355;
        public const int BUFFER_SIZE = 4096;
        public const string BTN_NAME_DISCONNECT = "접속끊기";
        public const string BTN_NAME_LOGIN = "로그인하기";
    }
}
