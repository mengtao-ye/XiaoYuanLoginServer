namespace SubServer
{
    /// <summary>
    /// 服务器常量数据
    /// </summary>
    public static class ServerData
    {
        /// <summary>
        /// udp服务端口号
        /// </summary>
        public static int UdpServerPort = -1;
        /// <summary>
        /// tcp服务端口号
        /// </summary>
        public const int TcpServerPort = -1;
        /// <summary>
        /// 服务器环境
        /// </summary>
        public const ServerEnvType serverEnv = ServerEnvType.Test;
        /// <summary>
        /// 服务器地址类型
        /// </summary>
        public const ServerNetType serverNet = ServerNetType.Local;
        /// <summary>
        /// ip地址
        /// </summary>
        public static string IPAddress
        {
            get {
                switch (ServerData.serverNet)
                {
                    case ServerNetType.Local:
                        return "127.0.0.1";
                        break;
                    case ServerNetType.AliYun:
                        return "121.40.245.164";
                        break;
                    case ServerNetType.TenXunYun:
                        return "127.0.0.1";
                        break;
                }
            }
        }

    }

    /// <summary>
    /// 生产环境
    /// </summary>
    public enum ServerEnvType
    {
        Test,//测试环境
        Pre,//预生产环境
        Pubilsh,//正式环境
    }
    /// <summary>
    /// 服务器环境
    /// </summary>
    public enum ServerNetType
    {
        Local,//本地
        AliYun,//阿里云
        TenXunYun,//腾讯云
    }

}
