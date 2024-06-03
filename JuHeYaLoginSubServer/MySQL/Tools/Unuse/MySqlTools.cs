using YSF;

namespace SubServer
{
    public partial class MySqlTools
    {

        /// <summary>
        /// 查找闲置物品
        /// </summary>
        /// <param name="schoolCode"></param>
        /// <param name="lastID"></param>
        /// <returns></returns>
        public static IListData<UnuseData> SearchUnuseList(long schoolCode, int lastID, string searchKey)
        {
            IMySqlCommand command = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.unuse_list)
                .Where
                .Compare("school_code", CompareType.Equal, schoolCode.ToString())
                .And
                .Compare("id", CompareType.Small, lastID.ToString())
                .And
                .Str(MySQLStr.GetLikeStr(searchKey, "content"))
                .Order("id", MySQLCoding.GBK, MySQLSort.DESC)
                .Limit(10)
                .End
                ;
            IListData<UnuseData> listData = mManager.FindAllByListPoolData<UnuseData>(command.mySqlStr);
            command?.Recycle();
            return listData;
        }

        public static IListData<UnuseData> GetMyCollectionUnuseList(long account, int lastID)
        {
            IMySqlCommand command = MySQLCommand.Select("unuse_id")
                .From
                .Datebase(MySQLTableData.collection_unuse)
                .Where
                .Compare("account", CompareType.Equal, account.ToString())
                .And
                .Compare("id", CompareType.Small, lastID.ToString())
                .Order("id", MySQLCoding.GBK, MySQLSort.DESC)
                .Limit(10)
                .End;

            IListData<int> myApplications = mManager.FindAllByListData(command.mySqlStr, 0);
            command.Recycle();
            if (myApplications.IsNullOrEmpty())
            {
                return null;
            }
            IListData<UnuseData> list = ClassPool<ListData<UnuseData>>.Pop();
            for (int i = 0; i < myApplications.Count; i++)
            {
                list.Add(FindUnuse(myApplications[i]));
            }
            myApplications.Recycle();
            return list;
        }



        public static UnuseData FindUnuse(int unuseID)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            mysql.SetData(MySQLTableData.unuse_list, "id", unuseID.ToString());
            UnuseData unuseData = mManager.FindPool<UnuseData>(mysql);
            mysql.Recycle();
            return unuseData;
        }

        /// <summary>
        /// 取消收藏兼职
        /// </summary>
        /// <param name="partTimeJobID"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool CancelCollectionUnuse(int unuseID, long account)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            mysql.SetData(MySQLTableData.collection_unuse, "account", account.ToString(), "unuse_id", unuseID.ToString());
            bool res = mManager.Delete(mysql);
            mysql?.Recycle();
            return res;
        }
        /// <summary>
        /// 收藏兼职
        /// </summary>
        /// <param name="partTimeJobID"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool CollectionUnuse(int unuseID, long account)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            mysql.SetData(MySQLTableData.collection_unuse, "account", account.ToString(), "unuse_id", unuseID.ToString());
            bool res = mManager.Add(mysql);
            mysql?.Recycle();
            return res;
        }
        /// <summary>
        /// 是否收藏兼职
        /// </summary>
        /// <param name="partTimeJobID"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool IsCollectionUnuse(int unuseID, long account)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            mysql.SetData(MySQLTableData.collection_unuse, "account", account.ToString(), "unuse_id", unuseID.ToString());
            bool res = mManager.IsExist(mysql);
            mysql?.Recycle();
            return res;
        }

        public static IListData<UnuseData> GetMyReleaseUnuseList(long account, int lastID)
        {
            IMySqlCommand command = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.unuse_list)
                .Where
                .Compare("account", CompareType.Equal, account.ToString())
                .And
                .Compare("id", CompareType.Small, lastID.ToString())
                .Order("id", MySQLCoding.GBK, MySQLSort.DESC)
                .Limit(10)
                .End;
            IListData<UnuseData> myApplications = mManager.FindAllByListPoolData<UnuseData>(command.mySqlStr);
            command.Recycle();
            if (myApplications.IsNullOrEmpty())
            {
                return null;
            }
            return myApplications;
        }

        public static UnuseData GetUnuseList(long account, int lastID, byte type)
        {
            IMySqlCommand mySqlCommand = null;
            if (type != 0)
            {
                mySqlCommand = MySQLCommand.Select()
               .From
               .Datebase(MySQLTableData.unuse_list)
               .Where
               .Compare("type", CompareType.Equal, type.ToString())
               .And
               .Compare("id", CompareType.Small, lastID.ToString())
               .Order("id", MySQLCoding.GBK, MySQLSort.DESC)
               .Limit(1)
               .End
               ;
            }
            else
            {
                mySqlCommand = MySQLCommand.Select()
             .From
             .Datebase(MySQLTableData.unuse_list)
             .Where
             .Compare("id", CompareType.Small, lastID.ToString())
             .Order("id", MySQLCoding.GBK, MySQLSort.DESC)
             .Limit(1)
             .End
             ;
            }

            UnuseData unuseData = mManager.FindPool<UnuseData>(mySqlCommand.mySqlStr);
            mySqlCommand.Recycle();
            return unuseData;
        }

        public static bool AddUnuse(string content, string images, byte type, int price, long time, long account,byte contactType,string contact,long schoolCode)
        {
            MySQL add = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            dict.Add("account", account.ToString());
            dict.Add("time", time.ToString());
            dict.Add("content", content);
            dict.Add("images", images);
            dict.Add("type", type.ToString());
            dict.Add("price", price.ToString());
            dict.Add("contact_type", contactType.ToString());
            dict.Add("contact", contact.ToString());
            dict.Add("school_code", schoolCode.ToString());
            add.SetData(MySQLTableData.unuse_list, dict);
            bool res = mManager.Add(add);
            add.Recycle();
            return res;
        }
    }
}
