using YSF;

namespace SubServer
{
    public partial  class MySqlTools
    {
        /// <summary>
        /// 删除聊天信息
        /// </summary>
        /// <param name="sendAccount"></param>
        /// <param name="receiveAccount"></param>
        /// <returns></returns>
        public static bool DeleteChatMsg(long sendAccount,long receiveAccount,byte msgType) 
        {
            MySQL delete = ClassPool<MySQL>.Pop();
            delete.SetData(MySQLTableData.chat_msg_list,"send_userid",sendAccount.ToString(),"receive_userid",receiveAccount.ToString(),"msg_type",msgType.ToString());
            bool res = mManager.Delete(delete);
            delete.Recycle();
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
