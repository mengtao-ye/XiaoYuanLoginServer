using YSF;

namespace SubServer
{
    public partial  class MySqlTools
    {
        /// <summary>
        /// 添加好友请求
        /// </summary>
        /// <param name="account"></param>
        /// <param name="friendID"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte AddFriendRequest(long account, long friendID,string content)
        {
            byte returnCode = 0;//0 为失败 1为发送成功 2为好友添加成功 3为已发送过
            MySQL find = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> findDict = ClassPool<DictionaryData<string, string>>.Pop();
            findDict.Add("send_account", friendID.ToString());
            findDict.Add("receive_account", account.ToString());
            find.SetData(MySQLTableData.add_friend_request_list,findDict);
            bool isExist = mManager.IsExist(find);
            find.Recycle();
            if (isExist)
            {
                //如果好友已经向我发起了好友申请的话
                bool res1 = AddFriendPair(account, friendID);
                bool res2 = AddFriendPair(friendID, account);
                bool res3 = DeleteAddFriendRequest(friendID,account);
                bool res = res1 && res2 && res3;
                if (res) 
                {
                    returnCode = 2;
                }
            }
            else
            {
                MySQL find2 = ClassPool<MySQL>.Pop();
                IDictionaryData<string, string> findDict2 = ClassPool<DictionaryData<string, string>>.Pop();
                findDict2.Add("send_account", account.ToString());
                findDict2.Add("receive_account", friendID.ToString());
                find.SetData(MySQLTableData.add_friend_request_list, findDict);
                bool isExist2 = mManager.IsExist(find2);
                find2.Recycle();
                if (isExist2)
                {
                    //已经发过一次申请了
                    returnCode = 3;
                }
                else
                {
                    MySQL mysql = ClassPool<MySQL>.Pop();
                    IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
                    dict.Add("send_account", account.ToString());
                    dict.Add("receive_account", friendID.ToString());
                    dict.Add("add_content", content);
                    mysql.SetData(MySQLTableData.add_friend_request_list, dict);
                    bool res = mManager.Add(mysql);
                    mysql.Recycle();
                    if (res) {
                        returnCode = 1;
                    }
                }
            }
            return returnCode;
        }

        public static bool DeleteAddFriendRequest(long account,long  friendAccount) 
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            dict.Add("send_account", account.ToString());
            dict.Add("receive_account", friendAccount.ToString());
            mysql.SetData(MySQLTableData.add_friend_request_list, dict);
            bool res = mManager.Delete(mysql);
            mysql.Recycle();
            return res;
        }

        public static bool AddFriendPair(long account,long friendAccount)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            dict.Add("user_account", account.ToString());
            dict.Add("friend_account", friendAccount.ToString());
            UserData userData = MySqlTools.GetUserDataByAccount(friendAccount);
            if (userData == null) {
                Debug.LogError("friendAccount:"+ friendAccount+"未找到该用户");
                return false;
            }
            dict.Add("notes", userData.Username.ToString());
            mysql.SetData(MySQLTableData.friend_pair, dict);
            bool res = mManager.Add(mysql);
            mysql.Recycle();
            return res;
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="account"></param>
        /// <param name="lastFriendID"></param>
        /// <returns></returns>
        public static IListData<FriendPairData> GetFriendPair(long account,int lastFriendID)
        {
            IMySqlCommand find = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.friend_pair)
                .Where
                .Compare("id", CompareType.Big, lastFriendID.ToString())
                .And
                .Compare("user_account", CompareType.Equal, account.ToString())
                .Limit(10)
                .End
                ;
            IListData<FriendPairData> listData = mManager.FindAllByListPoolData<FriendPairData>(find.mySqlStr);
            find.Recycle();
            return listData;
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="account"></param>
        /// <param name="lastFriendID"></param>
        /// <returns></returns>
        public static bool IsFriend(long account, long friendAccount)
        {
            IMySqlCommand isExist = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.friend_pair)
                .Where
                .Compare("user_account", CompareType.Equal, account.ToString())
                .And
                .Compare("friend_account", CompareType.Equal,friendAccount.ToString())
                .End
                ;
            bool b = mManager.IsExist(isExist.mySqlStr);
            isExist.Recycle();
            return b;
        }

        /// <summary>
        /// 添加聊天信息
        /// </summary>
        /// <param name="sendAccount"></param>
        /// <param name="receiveAccount"></param>
        /// <param name="msgType"></param>
        /// <param name="chatMsg"></param>
        /// <param name="time"></param>
        public static bool AddChatMsg(long sendAccount, long receiveAccount, byte msgType, string chatMsg, long time,out long lastID)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            dict.Add("send_userid", sendAccount.ToString());
            dict.Add("receive_userid", receiveAccount.ToString());
            dict.Add("msg_type", msgType.ToString());
            dict.Add("chat_msg", chatMsg.ToString());
            dict.Add("time", time.ToString());
            mysql.SetData(MySQLTableData.chat_msg_list, dict);
            bool res = mManager.Add(mysql, out lastID);
            mysql.Recycle();
            return res;
        }
        /// <summary>
        /// 添加聊天信息
        /// </summary>
        /// <param name="sendAccount"></param>
        /// <param name="receiveAccount"></param>
        /// <param name="msgType"></param>
        /// <param name="chatMsg"></param>
        /// <param name="time"></param>
        public static bool AddChatMsg(long sendAccount,long receiveAccount,byte msgType,string chatMsg,long time) {
            MySQL mysql = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            dict.Add("send_userid",sendAccount.ToString());
            dict.Add("receive_userid", receiveAccount.ToString());
            dict.Add("msg_type", msgType.ToString());
            dict.Add("chat_msg", chatMsg.ToString());
            dict.Add("time", time.ToString());
            mysql.SetData(MySQLTableData.chat_msg_list,dict);
            bool res = mManager.Add(mysql);
            mysql.Recycle();
            return res;
        }


        /// <summary>
        /// 获取好友申请列表
        /// </summary>
        /// <param name="account"></param>
        /// <param name="addFriendID"></param>
        public static IListData<AddFriendRequestData> GetAddFriendRequestList(long account, int addFriendID)
        {
            IMySqlCommand find = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.add_friend_request_list)
                .Where
                .Compare("id", CompareType.Big, (addFriendID).ToString())
                .And
                .Compare("receive_account", CompareType.Equal, account.ToString())
                .Limit(5)
                .End
                ;
            IListData<AddFriendRequestData> listData = mManager.FindAllByListPoolData<AddFriendRequestData>(find.mySqlStr);
            find.Recycle();
            return listData;
        }
        /// <summary>
        /// 获取聊天信息
        /// </summary>
        /// <param name="account"></param>
        /// <param name="msgID"></param>
        public static IListData<ChatData> GetChatMsg(long account,long msgID)
        {
            IMySqlCommand find = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.chat_msg_list)
                .Where
                .Compare("id", CompareType.Big, (msgID ) .ToString())
                .And
                .Compare("receive_userid", CompareType.Equal, account.ToString())
                .Limit(5)
                .End
                ;
            IListData<ChatData> listData =  mManager.FindAllByListPoolData<ChatData>(find.mySqlStr);
            find.Recycle();
            if (!listData.IsNullOrEmpty()) 
            {
                //有新的消息时就把旧的消息给删除掉
                IMySqlCommand command = MySQLCommand.Delete
                .From
                .Datebase(MySQLTableData.chat_msg_list)
                .Where
                .Compare("id", CompareType.Small, (msgID+1).ToString())
                .And
                .Compare("receive_userid", CompareType.Equal, account.ToString())
                .End
                ;
                mManager.Delete(command.mySqlStr);
                command.Recycle();
            }
             return listData;
        }
    }
}
