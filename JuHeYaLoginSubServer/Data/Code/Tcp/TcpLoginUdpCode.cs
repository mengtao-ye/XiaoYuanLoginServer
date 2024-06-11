namespace SubServer
{
    public enum TcpLoginUdpCode : short
    {
        //目前到了  52
        //空 19
        //SubServer
        LoginSubServerRegister = TcpLoginRequestCode.SubServer +1,//登录部分分布式服务器注册
        LoginSubServerHeartBeat = TcpLoginRequestCode.SubServer +2,//登录部分分布式服务器心跳包
        //Login
        HeartBeat = TcpLoginRequestCode.Login + 1,//心跳包    
        LoginAccount = TcpLoginRequestCode.Login + 2,//账号登录
        RegisterAccount = TcpLoginRequestCode.Login + 3,//注册账号
        GetUserData = TcpLoginRequestCode.Login + 4,//获取用户数据
        GetMySchool = TcpLoginRequestCode.Login + 5,//获取我的学校
        GetSchoolData = TcpLoginRequestCode.Login + 6,//获取学校数据
        SearchSchool = TcpLoginRequestCode.Login + 7,//查找学校
        JoinSchool = TcpLoginRequestCode.Login + 8,//加入学校
        ModifyName = TcpLoginRequestCode.Login + 63,//修改名称
        ModifySex = TcpLoginRequestCode.Login + 64,//修改性别
        ModifyBrithday = TcpLoginRequestCode.Login + 65,//修改生日
        ExitSchool = TcpLoginRequestCode.Login + 66,//退出学校


        //Chat
        GetNewChatMsg = TcpLoginRequestCode.Login + 9,//获取最新聊天信息
        SendChatMsg = TcpLoginRequestCode.Login + 10,//发送聊天消息
        GetFriendList = TcpLoginRequestCode.Login + 11,//获取好友列表
        SearchFriendData = TcpLoginRequestCode.Login + 12,//查找好友信息
        SendAddFriendRequest = TcpLoginRequestCode.Login + 13,//发送添加好友请求
        GetAddFriendRequest = TcpLoginRequestCode.Login + 14,//获取添加好友请求列表
        RefuseFriend = TcpLoginRequestCode.Login + 15,//拒绝好友申请
        ConfineFriend = TcpLoginRequestCode.Login + 16,//同意好友申请
        ChangeNotes = TcpLoginRequestCode.Login + 53,//修改备注
        DeleteFriend = TcpLoginRequestCode.Login + 61,//删除好友
        IsFriend = TcpLoginRequestCode.Login + 62,//是否是好友

        //CampusCircle
        PublishCampusCircle = TcpLoginRequestCode.Login + 17,//发表校友圈
        GetCampusCircle = TcpLoginRequestCode.Login + 18,//获取朋友圈
        LikeCampusCircleItem = TcpLoginRequestCode.Login + 20,//朋友圈点赞 
        HasLikeCampusCircleItem = TcpLoginRequestCode.Login + 21,//是否朋友圈点赞
        GetCommit = TcpLoginRequestCode.Login + 22,//获取评论信息
        GetFriendCampusCircle = TcpLoginRequestCode.Login + 54,//获取好友的校友圈
        GetCampusCircleLikeCount = TcpLoginRequestCode.Login + 55,//获取朋友圈点赞个数
        GetCampusCircleCommitCount = TcpLoginRequestCode.Login + 56,//获取朋友圈评论个数
        SendCampCircleCommit = TcpLoginRequestCode.Login + 19,//发送朋友圈评论
        SendCampCircleReplayCommit = TcpLoginRequestCode.Login + 57,//发送朋友圈回复评论
        GetReplayCommit = TcpLoginRequestCode.Login + 58,//获取回复评论信息
        DeleteCommit = TcpLoginRequestCode.Login + 59,//删除评论
        DeleteReplayCommit = TcpLoginRequestCode.Login + 60,//删除回复评论

        //Lost
        PublishLostData = TcpLoginRequestCode.Login + 23,//发表失物招领
        GetMyLostList = TcpLoginRequestCode.Login + 24,//获取我的失物招领
        GetLostList = TcpLoginRequestCode.Login + 34,//获取我的失物招领
        SearchLostList = TcpLoginRequestCode.Login + 35,//查找失物列表
        DeleteLost = TcpLoginRequestCode.Login + 36,//删除失物招领

        //Found
        PublishFoundData = TcpLoginRequestCode.Login + 37,//发表失物招领
        GetFoundList = TcpLoginRequestCode.Login + 38,//获取我的寻物招领
        GetMyFoundList = TcpLoginRequestCode.Login + 39,//获取我的寻物
        DeleteFound = TcpLoginRequestCode.Login + 40,//删除寻物
        SearchFoundList = TcpLoginRequestCode.Login + 41,//查找寻物

        //PartTimeJob
        ReleasePartTimeJob = TcpLoginRequestCode.Login + 25,//发布兼职
        GetMyReleasePartTimeJob = TcpLoginRequestCode.Login + 26,//获取我发布的兼职
        GetPartTimeJobList = TcpLoginRequestCode.Login + 27,//获取兼职列表
        ApplicationPartTimeJob = TcpLoginRequestCode.Login + 28,//报名兼职
        GetApplicationPartTimeJob = TcpLoginRequestCode.Login + 29,//获取报名兼职列表
        GetMyApplicationPartTimeJobList = TcpLoginRequestCode.Login + 42,//获取我的兼职报名列表
        CancelApplicationPartTimeJob = TcpLoginRequestCode.Login + 43,//取消报名
        CollectionPartTimeJob = TcpLoginRequestCode.Login + 44,//收藏兼职
        IsCollectionPartTimeJob = TcpLoginRequestCode.Login + 45,//是否收藏兼职
        GetMyCollectionPartTimeJobList = TcpLoginRequestCode.Login + 46,//获取我收藏的兼职报名列表
        SearchPartTimeJobList = TcpLoginRequestCode.Login + 52,//查找兼职列表

        //Unuse
        ReleaseUnuse = TcpLoginRequestCode.Login + 30,//发布兼职
        GetUnuseList = TcpLoginRequestCode.Login + 31,//获取闲置列表
        GetMyReleaseUnuseList = TcpLoginRequestCode.Login + 47,//获取我的闲置列表
        IsCollectionUnuse = TcpLoginRequestCode.Login + 48,//是否收藏闲置
        CollectionUnuse = TcpLoginRequestCode.Login + 49,//操作收藏闲置
        GetMyCollectionUnuseList = TcpLoginRequestCode.Login + 50,//获取我收藏的闲置
        SearchUnuseList = TcpLoginRequestCode.Login + 51,//查找闲置列表

        //MetaSchool
        GetMyMetaSchoolData = TcpLoginRequestCode.Login + 32,//获取我的校园数据
        SetMyMetaSchoolData = TcpLoginRequestCode.Login + 33,//选择我的校园数据

    }
}
