namespace SubServer
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
        GetMySchool = LoginRequestCode.Login + 5,//获取我的学校
        GetSchoolData = LoginRequestCode.Login + 6,//获取学校数据
        SearchSchool = LoginRequestCode.Login + 7,//查找学校
        JoinSchool = LoginRequestCode.Login + 8,//加入学校
        //Chat
        GetNewChatMsg = LoginRequestCode.Login + 9,//获取最新聊天信息
        SendChatMsg = LoginRequestCode.Login + 10,//发送聊天消息
        GetFriendList = LoginRequestCode.Login + 11,//获取好友列表
        SearchFriendData = LoginRequestCode.Login + 12,//查找好友信息
        SendAddFriendRequest = LoginRequestCode.Login + 13,//发送添加好友请求
        GetAddFriendRequest = LoginRequestCode.Login + 14,//获取添加好友请求列表
        RefuseFriend = LoginRequestCode.Login + 15,//拒绝好友申请
        ConfineFriend = LoginRequestCode.Login + 16,//同意好友申请

        //CampusCircle
        PublishCampusCircle = LoginRequestCode.Login + 17,//发表校友圈
        GetCampusCircle = LoginRequestCode.Login + 18,//获取朋友圈
        GetCampusCircleItemDetail = LoginRequestCode.Login + 19,//获取朋友圈对象详情 
        LikeCampusCircleItem = LoginRequestCode.Login + 20,//朋友圈点赞 
        HasLikeCampusCircleItem = LoginRequestCode.Login + 21,//是否朋友圈点赞
        GetCommit = LoginRequestCode.Login + 22,//获取评论信息
       
        //LostAndFound
        PublishLostData = LoginRequestCode.Login + 23,//发表失物招领
        GetMyLostData = LoginRequestCode.Login + 24,//获取我的失物招领

        //PartTimeJob
        ReleasePartTimeJob = LoginRequestCode.Login + 25,//发布兼职
        GetMyReleasePartTimeJob = LoginRequestCode.Login + 26,//获取我发布的兼职
        GetPartTimeJobList = LoginRequestCode.Login + 27,//获取兼职列表
        ApplicationPartTimeJob = LoginRequestCode.Login + 28,//报名兼职
        GetApplicationPartTimeJob = LoginRequestCode.Login + 29,//获取报名兼职列表
        
        //Unuse
        ReleaseUnuse = LoginRequestCode.Login + 30,//发布兼职
        GetUnuseList = LoginRequestCode.Login + 31,//获取闲置列表

        //MetaSchool
        GetMyMetaSchoolData = LoginRequestCode.Login + 32,//获取我的校园数据
        SetMyMetaSchoolData = LoginRequestCode.Login + 33,//选择我的校园数据

    }
}
