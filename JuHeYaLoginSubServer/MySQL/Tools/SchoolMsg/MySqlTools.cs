using YSF;

namespace SubServer
{
    public partial  class MySqlTools
    {
        /// <summary>
        /// 加入学校
        /// </summary>
        /// <param name="account"></param>
        /// <param name="schoolID"></param>
        /// <returns></returns>
        public static byte JoinSchool(long account,int schoolID) 
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
            pair.Add("school_id",schoolID.ToString());
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
            SchoolPairData tempSchoolPair = DictionaryModule<long, SchoolPairData>.Get(account);
            if (tempSchoolPair != null)
            {
                return tempSchoolPair;
            }
            MySQL findMySQL = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            findMySQL.SetData(MySQLTableData.school_pair, "account", account.ToString());
            SchoolPairData schoolPairData = mManager.FindPool<SchoolPairData>(findMySQL);
            findMySQL.Recycle();
            if (schoolPairData.IsNull()) return null;
            DictionaryModule<long, SchoolPairData>.Add(account, schoolPairData);
            return schoolPairData;
        }
        /// <summary>
        /// 获取玩家信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static SchoolData GetSchoolData(int schoolID)
        {
            SchoolData tempSchool = DictionaryModule<int, SchoolData>.Get(schoolID);
            if (tempSchool != null)
            {
                return tempSchool;
            }
            MySQL findMySQL = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            findMySQL.SetData(MySQLTableData.schools, "id", schoolID.ToString());
            SchoolData schoolData = mManager.FindPool<SchoolData>(findMySQL);
            findMySQL.Recycle();
            if (schoolData.IsNull()) return null;
            DictionaryModule<int, SchoolData>.Add(schoolID, schoolData);
            return schoolData;
        }

        /// <summary>
        /// 查找学校信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static IListData<SchoolData> SearchSchoolsData(string name)
        {
            IListData<SchoolData> schoolList = ClassPool<ListData<SchoolData>>.Pop();
            if (!DictionaryModule<int, SchoolData>.data.IsNullOrEmpty())
            {
                DictionaryModule<int, SchoolData>.data.Foreach(SchoolDictForeach, schoolList, name);
            }
            if (schoolList.Count >= 10) return schoolList;
            IListData<CompareItem> compares = null;
            if (schoolList.Count != 0) 
            {
                compares = ClassPool<ListPoolData<CompareItem>>.Pop();
                for (int i = 0; i < schoolList.Count; i++)
                {
                    CompareItem compareItem = ClassPool<CompareItem>.Pop();
                    compareItem.field = "id";
                    compareItem.value = schoolList[i].schoolID.ToString();
                    compareItem.compareType = CompareType.NotEqual;
                    compareItem.operatorType =  MySQLOperatorType.And;
                    compares.Add(compareItem);
                }
            }
            MySQL findMySQL = ClassPool<MySQL>.Pop();
            IMySqlCommand command = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.schools)
                .Where
                .Compares(compares == null ? null : compares.ToArray())
                .Like("name", name)
                .Limit(10 - schoolList.Count)
                .End
                ;
            compares?.Recycle();
            IListData<SchoolData> schoolData = mManager.FindAllByListData<SchoolData>(command.mySqlStr);
            if (!schoolData.IsNull())
            {
                for (int i = 0; i < schoolData.list.Count; i++)
                {
                    bool isContains = false;
                    for (int j = 0; j < schoolList.Count; j++)
                    {
                        if (schoolList[j].schoolID == schoolData.list[i].schoolID)
                        {
                            isContains = true;
                        }
                    }
                    if (!isContains)
                    {
                        schoolList.Add(schoolData.list[i]);
                    }
                    DictionaryModule<int, SchoolData>.Add(schoolData.list[i].schoolID, schoolData.list[i]);
                }
            }
            schoolData?.Recycle();
            findMySQL?.Recycle();
            command?.Recycle();
            return schoolList;
        }
        /// <summary>
        /// 遍历学校列表
        /// </summary>
        /// <param name="schoolID"></param>
        /// <param name="data"></param>
        /// <param name="listData"></param>
        /// <param name="name"></param>
        private static bool SchoolDictForeach(int schoolID, SchoolData data, IListData<SchoolData> listData, string name)
        {
            if (listData.Count >= 10)
            {
                return true;
            }
            if (data.name.Contains(name))
            {
                listData.Add(data);
            }
            return false;
        }

    }
}
