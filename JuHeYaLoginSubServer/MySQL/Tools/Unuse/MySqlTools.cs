using YSF;

namespace SubServer
{
    public partial class MySqlTools
    {
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

        public static bool AddUnuse(string content, string images, byte type, int price, long time, long account)
        {
            MySQL add = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            dict.Add("account", account.ToString());
            dict.Add("time", time.ToString());
            dict.Add("content", content);
            dict.Add("images", images);
            dict.Add("type", type.ToString());
            dict.Add("price", price.ToString());
            add.SetData(MySQLTableData.unuse_list, dict);
            bool res = mManager.Add(add);
            add.Recycle();
            return res;
        }
    }
}
