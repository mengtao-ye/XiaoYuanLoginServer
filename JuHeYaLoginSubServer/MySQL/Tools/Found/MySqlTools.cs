using System;
using YSF;

namespace SubServer
{
    public partial  class MySqlTools
    {

        /// <summary>
        /// 获取寻物列表
        /// </summary>
        /// <param name="schoolCode"></param>
        /// <param name="lastID"></param>
        /// <returns></returns>
        public static IListData<FoundData> SearchFoundList(long schoolCode, long updateTime, string searchKey)
        {
            IMySqlCommand command = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.found_list)
                .Where
                .Compare("school_code", CompareType.Equal, schoolCode.ToString())
                .And
                .Compare("update_time", CompareType.Small, updateTime.ToString())
                .And
                .Str(MySQLStr.GetLikeStr(searchKey, "name"))
                .Order("update_time", MySQLCoding.GBK, MySQLSort.DESC)
                .Limit(3)
                .End
                ;
            IListData<FoundData> listData = mManager.FindAllByListPoolData<FoundData>(command.mySqlStr);
            command?.Recycle();
            return listData;
        }


        /// <summary>
        /// 删除失物招领
        /// </summary>
        /// <param name="lostID"></param>
        public static bool DeleteFound(int lostID)
        {
            MySQL delete = ClassPool<MySQL>.Pop();
            delete.SetData(MySQLTableData.found_list, "id", lostID.ToString());
            bool res = mManager.Delete(delete);
            delete.Recycle();
            return res;
        }
        /// <summary>
        /// 获取寻物
        /// </summary>
        /// <param name="account"></param>
        /// <param name="updateTime"></param>
        /// <returns></returns>
        public static IListData<FoundData> GetMyFound(long account, long updateTime)
        {
            IMySqlCommand command = MySQLCommand.Select()
               .From
               .Datebase(MySQLTableData.found_list)
               .Where
               .Compare("account", CompareType.Equal, account.ToString())
               .And
               .Compare("update_time", CompareType.Small, updateTime.ToString())
               .Order("update_time", MySQLCoding.GBK, MySQLSort.DESC)
               .Limit(3)
               .End
               ;
            IListData<FoundData> listData = mManager.FindAllByListPoolData<FoundData>(command.mySqlStr);
            command?.Recycle();
            return listData;
        }
        /// <summary>
        /// 获取失物招领列表
        /// </summary>
        /// <param name="schoolCode"></param>
        /// <param name="lastID"></param>
        /// <returns></returns>
        public static IListData<FoundData> GetFoundList(long schoolCode, long updateTime)
        {
            IMySqlCommand command = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.found_list)
                .Where
                .Compare("school_code", CompareType.Equal, schoolCode.ToString())
                .And
                .Compare("update_time", CompareType.Small, updateTime.ToString())
                .Order("update_time", MySQLCoding.GBK, MySQLSort.DESC)
                .Limit(3)
                .End
                ;
            IListData<FoundData> listData = mManager.FindAllByListPoolData<FoundData>(command.mySqlStr);
            command?.Recycle();
            return listData;
        }

        /// <summary>
        ///  添加失物数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pos"></param>
        /// <param name="time"></param>
        /// <param name="account"></param>
        /// <param name="images"></param>
        /// <param name="schoolCode"></param>
        /// <param name="detail"></param>
        /// <param name="contactType"></param>
        /// <param name="contact"></param>
        /// <param name="quest"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool AddFound(string name, string pos, long time,long account, string images, long schoolCode, string detail, byte contactType, string contact,string quest,string result)
        {
            MySQL insert = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            dict.Add("name", name);
            dict.Add("account", account.ToString());
            dict.Add("pos", pos);
            dict.Add("time", time.ToString());
            dict.Add("images", images);
            dict.Add("school_code", schoolCode.ToString());
            dict.Add("update_time", DateTimeTools.GetValueByDateTime(DateTime.Now).ToString());
            dict.Add("detail", detail);
            dict.Add("contact_type", contactType.ToString());
            dict.Add("contact", contact.ToString());
            dict.Add("quest", quest);
            dict.Add("result", result);
            insert.SetData(MySQLTableData.found_list, dict);
            bool res = mManager.Add(insert);
            insert.Recycle();
            return res;
        }
    }
}
