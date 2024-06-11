using YSF;

namespace SubServer
{
    public partial  class MySqlTools
    {
        /// <summary>
        /// 退出学校
        /// </summary>
        /// <returns></returns>
        public static bool ExitSchool(long account,long schoolCode)
        {
            MySQL delete = ClassPool<MySQL>.Pop();
            delete.SetData(MySQLTableData.school_pair,"account",account.ToString(),"school_code",schoolCode.ToString());
            bool res = mManager.Delete(delete);
            delete.Recycle();
            return res;
        }

        /// <summary>
        /// 加入学校
        /// </summary>
        /// <param name="account"></param>
        /// <param name="schoolCode"></param>
        /// <returns></returns>
        public static byte JoinSchool(long account,long schoolCode) 
        {
            //ReturnCode 0失败  1成功 2已加入
            byte returnCode = 0;
            MySQL isExistSql = ClassPool<MySQL>.Pop();
            isExistSql.SetData(MySQLTableData.school_pair, "account", account.ToString());
            bool isExist = mManager.IsExist(isExistSql);
            isExistSql.Recycle();
            if (isExist)
            {
                returnCode = 2;
                return returnCode;
            }
            MySQL addSql = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> pair = ClassPool<DictionaryData<string, string>>.Pop();
            pair.Add("account",account.ToString());
            pair.Add("school_code",schoolCode.ToString());
            addSql.SetData(MySQLTableData.school_pair, pair);
            bool res = mManager.Add(addSql);
            if (res)
            {
                returnCode = 1;
            }
            return returnCode;
        }
        /// <summary>
        /// 获取玩家信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static SchoolPairData GetSchoolPairData(long account)
        {
            MySQL findMySQL = ClassPool<MySQL>.Pop();
            findMySQL.SetData(MySQLTableData.school_pair, "account", account.ToString());
            SchoolPairData schoolPairData = mManager.FindPool<SchoolPairData>(findMySQL);
            findMySQL.Recycle();
            return schoolPairData;
        }
        /// <summary>
        /// 获取玩家信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static SchoolData GetSchoolData(long schoolCode)
        {
            MySQL findMySQL = ClassPool<MySQL>.Pop();
            findMySQL.SetData(MySQLTableData.schools, "code", schoolCode.ToString());
            SchoolData schoolData = mManager.FindPool<SchoolData>(findMySQL);
            findMySQL.Recycle();
            return schoolData;
        }

        /// <summary>
        /// 查找学校信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static IListData<SchoolData> SearchSchoolsData(string name)
        {
            MySQL findMySQL = ClassPool<MySQL>.Pop();
            IMySqlCommand command = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.schools)
                .Where
                .Str(MySQLStr.GetLikeStr(name,"name"))
                .Limit(10)
                .End
                ;
            IListData<SchoolData> schoolData = mManager.FindAllByListData<SchoolData>(command.mySqlStr);
            findMySQL?.Recycle();
            command?.Recycle();
            return schoolData;
        }
    }
}
