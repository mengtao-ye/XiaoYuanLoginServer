using YSF;

namespace SubServer
{
    public static class MySqlTools
    {
        private static MySQLManager mManager;
        private static bool IsInit {
            get
            {
                return mManager != null;
            }
        }
        /// <summary>
        /// 初始化MySQl管理器
        /// </summary>
        /// <param name="manager"></param>
        public static void InitMySQLManager(MySQLManager manager) 
        {
            mManager = manager;
        }


        public static byte RegisterAccount(long account, string username, string password)
        {

            byte registerRes = 0;//0为失败 1为成功 2为已存在
            MySQL isExistSql = ClassPool<MySQL>.Pop();
            isExistSql.SetData(MySQLTableData.users, "account", account.ToString());
            bool isExist = mManager.IsExist(isExistSql);
            if (isExist)
            {
                isExistSql.Recycle();
                registerRes = 2;
                return registerRes;
            }
            MySQL mysql = ClassPool<MySQL>.Pop();
            DictionaryData<string, string> data = ClassPool<DictionaryData<string, string>>.Pop();
            data.Add("username", username);
            data.Add("password", password);
            data.Add("account", account.ToString());
            mysql.SetData(MySQLTableData.users, data);
            bool result = mManager.Add(mysql);
            if (result)
            {
                registerRes = 1;

            }
            else
            {
                registerRes = 0;
            }
            isExistSql.Recycle();
            mysql.Recycle();
            return registerRes;
        }

        /// <summary>
        /// 检验账号密码是否正确
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="oldPassword"></param>
        /// <returns></returns>
        public static bool ConfineAccount(string account, string password)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> data = ClassPool<DictionaryData<string, string>>.Pop();
            data.Add("account", account);
            data.Add("password", password);
            mysql.SetData(MySQLTableData.users, data);
            bool isExist = mManager.IsExist(mysql);
            mysql.Recycle();
            return isExist;
        }
        /// <summary>
        /// 获取玩家信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static UserData GetUserDataByAccount(long account)
        {
            UserData  tempUserData =  DictionaryModule<long, UserData>.Get(account);
            if (tempUserData != null) 
            {
                return tempUserData;
            }
            MySQL findMySQL = ClassPool<MySQL>.Pop();
            findMySQL.SetData(MySQLTableData.users, "account", account.ToString());
            UserData userData = mManager.FindPool<UserData>(findMySQL);
            if (userData.IsNull()) return null;
            DictionaryModule<long, UserData>.Add(account, userData);
            return userData;
        }
    }
}
