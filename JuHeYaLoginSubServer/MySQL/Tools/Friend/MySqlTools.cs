using System;
using YSF;

namespace SubServer
{
    public partial class MySqlTools
    {

        /// <summary>
        /// 添加好友请求
        /// </summary>
        /// <param name="account"></param>
        /// <param name="friendID"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static byte AddFriendRequest(long account, long friendID, string content)
        {
            byte returnCode = 0;//0 为失败 1为发送成功 2为好友添加成功 3为已发送过
            MySQL find = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> findDict = ClassPool<DictionaryData<string, string>>.Pop();
            findDict.Add("send_account", friendID.ToString());
            findDict.Add("receive_account", account.ToString());
            find.SetData(MySQLTableData.add_friend_request_list, findDict);
            bool isExist = mManager.IsExist(find);
            find.Recycle();
            if (isExist)
            {
                //如果好友已经向我发起了好友申请的话
                if (!MySqlTools.IsFriend(account, friendID)) //如果两个人没加好友
                {
                    bool res1 = AddFriendPair(account, friendID);
                    bool res2 = AddFriendPair(friendID, account);
                    DeleteAddFriendRequest(friendID, account);
                    bool res = res1 && res2;
                    if (res)
                    {
                        //发送打招呼信息
                        MySqlTools.AddChatMsg(friendID, account, (byte)ChatMsgType.Text, StrConstData.AddNewFriendTip, DateTimeOffset.Now.ToUnixTimeSeconds());
                        MySqlTools.AddChatMsg(account, friendID, (byte)ChatMsgType.Text, StrConstData.AddNewFriendTip, DateTimeOffset.Now.ToUnixTimeSeconds());
                        //删除添加好友发送的信息
                        MySqlTools.DeleteChatMsg(UserAccountConstData.NEW_FRIEND_ACCOUNT, friendID, (byte)ChatMsgType.NewFriend);
                        MySqlTools.DeleteChatMsg(UserAccountConstData.NEW_FRIEND_ACCOUNT, account, (byte)ChatMsgType.NewFriend);
                        returnCode = 2;
                    }
                }
                else
                {
                    //两个人已经是好友了
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
                    if (res)
                    {
                        returnCode = 1;
                    }
                }
            }
            return returnCode;
        }

        public static bool DeleteAddFriendRequest(long account, long friendAccount)
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

        public static bool AddFriendPair(long account, long friendAccount)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            dict.Add("user_account", account.ToString());
            dict.Add("friend_account", friendAccount.ToString());
            UserData userData = MySqlTools.GetUserDataByAccount(friendAccount);
            if (userData == null)
            {
                Debug.LogError("friendAccount:" + friendAccount + "未找到该用户");
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
        public static IListData<FriendPairData> GetFriendPair(long account, int lastFriendID)
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
            bool b1 = IsInFriendPair(account, friendAccount);
            bool b2 = IsInFriendPair(friendAccount, account);
            return b1 && b2;
        }
        /// <summary>
        /// 判断这两个账号是否存在再好友列表中
        /// </summary>
        /// <param name="account1"></param>
        /// <param name="account2"></param>
        /// <returns></returns>
        private static bool IsInFriendPair(long account1, long account2)
        {
            IMySqlCommand isExist = MySQLCommand.Select()
               .From
               .Datebase(MySQLTableData.friend_pair)
               .Where
               .Compare("user_account", CompareType.Equal, account1.ToString())
               .And
               .Compare("friend_account", CompareType.Equal, account2.ToString())
               .End
               ;
            bool b = mManager.IsExist(isExist.mySqlStr);
            isExist.Recycle();
            return b;
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

    }
}
