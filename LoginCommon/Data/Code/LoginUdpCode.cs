namespace LoginCommon
{
    public enum LoginUdpCode : short
    {
        //SubServer
        LoginSubServerRegister = LoginRequestCode.SubServer +1,//登录部分分布式服务器注册
        LoginSubServerHeartBeat = LoginRequestCode.SubServer +2,//登录部分分布式服务器心跳包
        //Login
        HeartBeat = LoginRequestCode.Login + 1,//心跳包    
        LoginAccount = LoginRequestCode.Login + 2,//账号登录
        RegisterAccount = LoginRequestCode.Login + 3,//注册账号
        GetUserData = LoginRequestCode.Login + 4,//获取用户数据
    }
}
