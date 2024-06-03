using System;
using YSF;

namespace SubServer
{
    public partial  class MySqlTools
    {
        /// <summary>
        /// 删除失物招领
        /// </summary>
        /// <param name="lostID"></param>
        public static bool DeleteLost(int lostID)
        {
            MySQL delete = ClassPool<MySQL>.Pop();
            delete.SetData(MySQLTableData.lost_list,"id",lostID.ToString());
            bool res = mManager.Delete(delete);
            delete.Recycle();
            return res;
        }

        /// <summary>
        /// 获取失物招领列表
        /// </summary>
        /// <param name="schoolCode"></param>
        /// <param name="lastID"></param>
        /// <returns></returns>
        public static IListData<LostData> SearchLostList(long schoolCode, long updateTime,string searchKey)
        {
            IMySqlCommand command = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.lost_list)
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
            IListData<LostData> listData = mManager.FindAllByListPoolData<LostData>(command.mySqlStr);
            command?.Recycle();
            return listData;
        }

        /// <summary>
        /// 获取失物招领列表
        /// </summary>
        /// <param name="schoolCode"></param>
        /// <param name="lastID"></param>
        /// <returns></returns>
        public static IListData<LostData> GetLostList(long schoolCode, long updateTime)
        {
            IMySqlCommand command = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.lost_list)
                .Where
                .Compare("school_code", CompareType.Equal, schoolCode.ToString())
                .And
                .Compare("update_time", CompareType.Small, updateTime.ToString())
                .Order("update_time", MySQLCoding.GBK, MySQLSort.DESC)
                .Limit(3)
                .End
                ;
            IListData<LostData> listData = mManager.FindAllByListPoolData<LostData>(command.mySqlStr);
            command?.Recycle();
            return listData;
        }
      
        /// <summary>
        /// 添加失物招领数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pos"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="account"></param>
        /// <param name="images"></param>
        /// <returns></returns>
        public static IListData<LostData> GetMyLost(long account, long updateTime)
        {
            IMySqlCommand command = MySQLCommand.Select()
               .From
               .Datebase(MySQLTableData.lost_list)
               .Where
               .Compare("account", CompareType.Equal, account.ToString())
               .And
               .Compare("update_time", CompareType.Small, updateTime.ToString())
               .Order("update_time", MySQLCoding.GBK, MySQLSort.DESC)
               .Limit(3)
               .End
               ;
            IListData<LostData> listData = mManager.FindAllByListPoolData<LostData>(command.mySqlStr);
            command?.Recycle();
            return listData;
        }

        /// <summary>
        /// 添加失物招领数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pos"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="account"></param>
        /// <param name="images"></param>
        /// <returns></returns>
        public static bool AddLost(string name ,string pos,long startTime,long endTime,long account,string images,long schoolCode,string detail,byte contactType,string contact) 
        {
            MySQL insert = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            dict.Add("name",name);
            dict.Add("pos", pos);
            dict.Add("start_time", startTime.ToString());
            dict.Add("end_time", endTime.ToString());
            dict.Add("account", account.ToString());
            dict.Add("images", images);
            dict.Add("school_code", schoolCode.ToString());
            dict.Add("update_time", DateTimeTools.GetValueByDateTime(DateTime.Now).ToString());
            dict.Add("detail", detail);
            dict.Add("contact_type", contactType.ToString());
            dict.Add("contact", contact);
            insert.SetData(MySQLTableData.lost_list,dict);
            bool res = mManager.Add(insert);
            insert.Recycle();
            return res;
        }
    }
}
