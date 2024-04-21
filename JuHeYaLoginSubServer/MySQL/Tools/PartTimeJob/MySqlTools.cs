using YSF;

namespace SubServer
{
    public partial  class MySqlTools
    {
        public static IListData<PartTimeJobApplicationData> GetApplicationPartTimeJobList(int partTimeJobID,int lastID)
        {
            IMySqlCommand command = MySQLCommand.Select()
           .From
           .Datebase(MySQLTableData.application_parttimejob_list)
           .Where
           .Compare("parttimejob_id", CompareType.Equal,partTimeJobID.ToString())
           .And
           .Compare("id", CompareType.Small, lastID.ToString())
           .Order("id", MySQLCoding.GBK, MySQLSort.DESC)
           .Limit(10)
           .End;
            IListData<PartTimeJobApplicationData> listData = mManager.FindAllByListPoolData<PartTimeJobApplicationData>(command.mySqlStr);
            command.Recycle();
            return listData;
        }

        public static bool ApplicationPartTimeJob(long account,int partTimeJobID,string name,bool isMan,int age,string call) 
        {
            MySQL add = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            dict.Add("account",account.ToString());
            dict.Add("parttimejob_id", partTimeJobID.ToString());
            dict.Add("name", name);
            dict.Add("is_man", isMan ?"1":"0");
            dict.Add("age", age.ToString());
            dict.Add("call", call);
            add.SetData(MySQLTableData.application_parttimejob_list, dict);
            bool res =  mManager.Add(add);
            add.Recycle();
            return res;
        }

        public static MyReleasePartTimeJobData GetPartTimeJobList( int lastID)
        {
            IMySqlCommand command = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.part_time_job)
                .Where
                .Compare("id", CompareType.Small, lastID.ToString())
                .Order("id", MySQLCoding.GBK, MySQLSort.DESC)
                .Limit(1)
                .End;
            MyReleasePartTimeJobData myReleasePartTimeJobData = mManager.FindPool<MyReleasePartTimeJobData>(command.mySqlStr);
            command.Recycle();
            return myReleasePartTimeJobData;
        }
        public static MyReleasePartTimeJobData GetMyReleasePartTimeJob(long account,int lastID)
        {
            IMySqlCommand command = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.part_time_job)
                .Where
                .Compare("account", CompareType.Equal, account.ToString())
                .And
                .Compare("id", CompareType.Small, lastID.ToString())
                .Order("id", MySQLCoding.GBK, MySQLSort.DESC)
                .Limit(1)
                .End;
            MyReleasePartTimeJobData myReleasePartTimeJobData = mManager.FindPool<MyReleasePartTimeJobData>(command.mySqlStr);
            command.Recycle();
            return myReleasePartTimeJobData;
        }

        /// <summary>
        /// 添加兼职
        /// </summary>
        /// <param name="title"></param>
        /// <param name="price"></param>
        /// <param name="priceType"></param>
        /// <param name="jobTime"></param>
        /// <param name="position"></param>
        /// <param name="detail"></param>
        /// <param name="account"></param>
        public static bool AddPartTimeJob(string title,int price,int priceType,string jobTime,string position,string detail,long account)
        {
            MySQL add = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            dict.Add("title",title);
            dict.Add("account",account.ToString());
            dict.Add("price",price.ToString());
            dict.Add("price_type",priceType.ToString());
            dict.Add("job_time", jobTime.ToString());
            dict.Add("position", position.ToString());
            dict.Add("detail", detail.ToString());
            add.SetData(MySQLTableData.part_time_job, dict);
            bool res = mManager.Add(add);
            add.Recycle();
            return res;
        }
    }
}
