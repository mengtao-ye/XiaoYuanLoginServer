namespace SubServer
{
    public enum LoginUdpCode : short
    {
        //SubServer
        LoginSubServerRegister = LoginRequestCode.SubServer +1,//登录部分分布式服务器注册
        LoginSubServerHeartBeat = LoginRequestCode.SubServer +2,//登录部分分布式服务器心跳包
        TcpLoginSubServerRegister = LoginRequestCode.SubServer + 5,//Tcp登录部分分布式服务器注册
        TcpLoginSubServerHeartBeat = LoginRequestCode.SubServer + 6,//Tcp登录部分分布式服务器心跳包
    }
}
