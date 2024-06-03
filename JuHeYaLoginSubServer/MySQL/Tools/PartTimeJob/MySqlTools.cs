using System;
using YSF;

namespace SubServer
{
    public partial  class MySqlTools
    {
        /// <summary>
        /// 查找兼职
        /// </summary>
        /// <param name="schoolCode"></param>
        /// <param name="lastID"></param>
        /// <returns></returns>
        public static IListData<PartTimeJobData> SearchPartTimeJobList( int lastID, string searchKey)
        {
            IMySqlCommand command = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.part_time_job)
                .Where
                .Compare("id", CompareType.Small, lastID.ToString())
                .And
                .Str(MySQLStr.GetLikeStr(searchKey, "title"))
                .Order("id", MySQLCoding.GBK, MySQLSort.DESC)
                .Limit(10)
                .End
                ;
            IListData<PartTimeJobData> listData = mManager.FindAllByListPoolData<PartTimeJobData>(command.mySqlStr);
            command?.Recycle();
            return listData;
        }

        public static IListData<PartTimeJobData> GetMyCollectionPartTimeJobList(long account, int lastID)
        {
            IMySqlCommand command = MySQLCommand.Select("parttimejob_id")
                .From
                .Datebase(MySQLTableData.collection_parttimejob)
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
            IListData<PartTimeJobData> list = ClassPool<ListData<PartTimeJobData>>.Pop();
            for (int i = 0; i < myApplications.Count; i++)
            {
                list.Add(FindPartTimeJob(myApplications[i]));
            }
            myApplications.Recycle();
            return list;
        }

        /// <summary>
        /// 是否收藏兼职
        /// </summary>
        /// <param name="partTimeJobID"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool IsCollectionPartTimeJob(int partTimeJobID, long account)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            mysql.SetData(MySQLTableData.collection_parttimejob, "account", account.ToString(), "parttimejob_id", partTimeJobID.ToString());
            bool res = mManager.IsExist(mysql);
            mysql?.Recycle();
            return res;
        }
        /// <summary>
        /// 取消收藏兼职
        /// </summary>
        /// <param name="partTimeJobID"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool CancelCollectionPartTimeJob(int partTimeJobID, long account)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            mysql.SetData(MySQLTableData.collection_parttimejob, "account", account.ToString(), "parttimejob_id", partTimeJobID.ToString());
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
        public static bool CollectionPartTimeJob(int partTimeJobID, long account)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            mysql.SetData(MySQLTableData.collection_parttimejob, "account", account.ToString(), "parttimejob_id", partTimeJobID.ToString());
            bool res = mManager.Add(mysql);
            mysql?.Recycle();
            return res;
        }
        /// <summary>
        /// 取消兼职报名
        /// </summary>
        /// <param name="partTimeJobID"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool CancelPartTimeJobApplication(int partTimeJobID, long account)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            mysql.SetData(MySQLTableData.application_parttimejob_list, "account", account.ToString(), "parttimejob_id", partTimeJobID.ToString());
            bool res = mManager.Delete(mysql);
            mysql?.Recycle();
            return res;
        }

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

        public static byte ApplicationPartTimeJob(long account,int partTimeJobID,string name,bool isMan,int age,string call) 
        {
            byte resCode = 0;//0 失败 1成功 2已存在
            MySQL exist = ClassPool<MySQL>.Pop();
            exist.SetData(MySQLTableData.application_parttimejob_list,"account",account.ToString(),"parttimejob_id", partTimeJobID.ToString());
            bool isExist = mManager.IsExist(exist);
            exist.Recycle();
            if (isExist)
            {
                resCode = 2;
            }
            else 
            {
                MySQL add = ClassPool<MySQL>.Pop();
                IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
                dict.Add("account", account.ToString());
                dict.Add("parttimejob_id", partTimeJobID.ToString());
                dict.Add("name", name);
                dict.Add("is_man", isMan ? "1" : "0");
                dict.Add("age", age.ToString());
                dict.Add("call", call);
                add.SetData(MySQLTableData.application_parttimejob_list, dict);
                bool res = mManager.Add(add);
                add.Recycle();
                if (res) 
                {
                    resCode = 1;
                }
            }
            return resCode;
        }
        public static  IListData<PartTimeJobData> GetMyApplicationPartTimeJobList(long account, int lastID)
        {
            IMySqlCommand command = MySQLCommand.Select("parttimejob_id")
                .From
                .Datebase(MySQLTableData.application_parttimejob_list)
                .Where
                .Compare("account", CompareType.Equal,account.ToString())
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
            IListData<PartTimeJobData> list = ClassPool<ListData<PartTimeJobData>>.Pop();
            for (int i = 0; i < myApplications.Count; i++)
            {
                list.Add(FindPartTimeJob(myApplications[i]));
            }
            myApplications.Recycle();
            return list;
        }

        public static PartTimeJobData FindPartTimeJob(int partTimeJobID)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            mysql.SetData(MySQLTableData.part_time_job,"id", partTimeJobID.ToString());
            PartTimeJobData myReleasePartTimeJobData = mManager.FindPool<PartTimeJobData>(mysql);
            mysql.Recycle();
            return myReleasePartTimeJobData;
        }

        public static PartTimeJobData GetPartTimeJob( int lastID)
        {
            IMySqlCommand command = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.part_time_job)
                .Where
                .Compare("id", CompareType.Small, lastID.ToString())
                .Order("id", MySQLCoding.GBK, MySQLSort.DESC)
                .Limit(1)
                .End;
            PartTimeJobData myReleasePartTimeJobData = mManager.FindPool<PartTimeJobData>(command.mySqlStr);
            command.Recycle();
            return myReleasePartTimeJobData;
        }
        public static PartTimeJobData GetMyReleasePartTimeJob(long account,int lastID)
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
            PartTimeJobData myReleasePartTimeJobData = mManager.FindPool<PartTimeJobData>(command.mySqlStr);
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
