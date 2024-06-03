namespace SubServer
{
    public enum ControllerType
    {
        RegisterLoginSubServer,//注册Udp登录分布式服务器
        LoginSubServerHeartBeat,//登录Udp分布式服务器心跳包

        RegisterTcpLoginSubServer,//注册Tcp登录分布式服务器
        TcpLoginSubServerHeartBeat,//登录Tcp分布式服务器心跳包
    }
}
