namespace LoginCommon
{
    public enum LoginRequestCode : short
    {
        SubServer = CommonConstData.REQUESTCODE_SPAN * 2,//共同数据相关请求
        Login = CommonConstData.REQUESTCODE_SPAN * 66,//登录数据
    }
}
