﻿using YSF;

namespace SubServer
{
    public partial  class MySqlTools
    {
        /// <summary>
        /// 修改生日
        /// </summary>
        /// <param name="account"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ModifyBirthday(long account, int brithday)
        {
            WhereMySQL mysql = ClassPool<WhereMySQL>.Pop();
            mysql.SetData(MySQLTableData.users, "birthday", brithday.ToString(), "account", account.ToString());
            bool res = mManager.Update(mysql);
            mysql.Recycle();
            if (res)
            {
                UserData tempUserData = DictionaryModule<long, UserData>.Get(account);
                if (tempUserData != null)
                {
                    tempUserData.birthday= brithday;
                }
            }
            return res;
        }
        /// <summary>
        /// 修改性别
        /// </summary>
        /// <param name="account"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ModifySex(long account, byte  sex)
        {
            WhereMySQL mysql = ClassPool<WhereMySQL>.Pop();
            mysql.SetData(MySQLTableData.users, "sex", sex.ToString(), "account", account.ToString());
            bool res = mManager.Update(mysql);
            mysql.Recycle();
            if (res)
            {
                UserData tempUserData = DictionaryModule<long, UserData>.Get(account);
                if (tempUserData != null)
                {
                    tempUserData.sex = sex;
                }
            }
            return res;
        }
        /// <summary>
        /// 修改名称
        /// </summary>
        /// <param name="account"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ModifyName(long account,string name)
        {
            WhereMySQL mysql = ClassPool<WhereMySQL>.Pop();
            mysql.SetData(MySQLTableData.users,"username",name,"account",account.ToString());
            bool res = mManager.Update(mysql);
            mysql.Recycle();
            if (res) 
            {
                UserData tempUserData = DictionaryModule<long, UserData>.Get(account);
                if (tempUserData != null)
                {
                    tempUserData.Username = name;
                }
            }
            return res;
        }
        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="account"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
            UserData tempUserData = DictionaryModule<long, UserData>.Get(account);
            if (tempUserData != null)
            {
                return tempUserData;
            }
            MySQL findMySQL = ClassPool<MySQL>.Pop();
            findMySQL.SetData(MySQLTableData.users, "account", account.ToString());
            UserData userData = mManager.FindPool<UserData>(findMySQL);
            findMySQL.Recycle();
            if (userData.IsNull()) return null;
            DictionaryModule<long, UserData>.Add(account, userData);
            return userData;
        }
        /// <summary>
        /// 获取玩家信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static UserData GetUserDataByUserID(long id)
        {
            UserData userDataTemp = null;
            DictionaryModule<long, UserData>.data.Foreach(UserDataForeach,id, userDataTemp);
            if (userDataTemp != null)
            {
                return userDataTemp;
            }
            MySQL findMySQL = ClassPool<MySQL>.Pop();
            findMySQL.SetData(MySQLTableData.users, "id", id.ToString());
            UserData userData = mManager.FindPool<UserData>(findMySQL);
            findMySQL.Recycle();
            if (userData.IsNull()) return null;
            DictionaryModule<long, UserData>.Add(userData.Account, userData);
            return userData;
        }
        private static bool UserDataForeach(long account,UserData userData,long id,UserData outData)
        {
            if (userData != null && userData.ID == id) 
            {
                outData = userData;
                return true;
            }
            return false;
        }
    }
}
