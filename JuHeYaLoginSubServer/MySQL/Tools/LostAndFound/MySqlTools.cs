using YSF;

namespace SubServer
{
    public partial  class MySqlTools
    {
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
        public static IListData<LostData> GetLost(long account,int lastID)
        {
            IMySqlCommand command = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.lost_list)
                .Where
                .Compare("account", CompareType.Equal, account.ToString())
                .And
                .Compare("id", CompareType.Big, lastID.ToString())
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
        public static bool AddLost(string name ,string pos,long startTime,long endTime,long account,string images) 
        {
            MySQL insert = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            dict.Add("name",name);
            dict.Add("pos", pos);
            dict.Add("start_time", startTime.ToString());
            dict.Add("end_time", endTime.ToString());
            dict.Add("account", account.ToString());
            dict.Add("images", images);
            insert.SetData(MySQLTableData.lost_list,dict);
            bool res = mManager.Add(insert);
            insert.Recycle();
            return res;
        }
    }
}
