using YSF;

namespace SubServer
{
    public partial class MySqlTools
    {

        /// <summary>
        /// 获取评论信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IListData<CampusCircleCommitData> GetCommit(long id,long lastID)
        {
            IMySqlCommand command = MySQLCommand.Select()
              .From
              .Datebase(MySQLTableData.campus_circle_commit)
              .Where
              .Compare("id", CompareType.Small, lastID.ToString())
              .And
              .Compare("campus_circle_id", CompareType.Equal,id.ToString())
              .Order("id", MySQLSort.DESC)
              .Limit(5)
              .End
              ;
            IListData<CampusCircleCommitData> listData = mManager.FindAllByListPoolData<CampusCircleCommitData>(command.mySqlStr);
            command.Recycle();
            return listData;
        }

        /// <summary>
        /// 是否点赞好友圈
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool HasLikeCampusCircleItem(long account, long id)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            mysql.SetData(MySQLTableData.campus_circle_like, "account", account.ToString(), "campus_circle_id", id.ToString());
            bool isExist = mManager.IsExist(mysql);
            mysql.Recycle();
            return isExist;
        }

        /// <summary>
        /// 点赞好友圈
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static void LikeCampusCircleItem(long account, long id, bool isLike)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            mysql.SetData(MySQLTableData.campus_circle_like, "account", account.ToString(), "campus_circle_id", id.ToString());
            bool isExist = mManager.IsExist(mysql);
            if (isLike)
            {
                if (isExist)
                {
                    mManager.Delete(mysql);
                    string mySQL = $"UPDATE {MySQLTableData.campus_circle} SET like_count = like_count - 1 where id={id} ;";
                    mManager.Exe(mySQL);
                    if (DictionaryModule<long, CampusCircleData>.IsContains(id))
                    {
                        DictionaryModule<long, CampusCircleData>.Get(id).LikeCount--;
                    }
                }
            }
            else
            {
                if (!isExist)
                {
                    mManager.Add(mysql);
                    string mySQL = $"UPDATE {MySQLTableData.campus_circle} SET like_count = like_count + 1 where id={id} ;";
                    mManager.Exe(mySQL);
                    if (DictionaryModule<long, CampusCircleData>.IsContains(id))
                    {
                        DictionaryModule<long, CampusCircleData>.Get(id).LikeCount++;
                    }
                }
            }
            mysql.Recycle();
        }

        /// <summary>
        /// 获取校友圈
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CampusCircleData GetCampusCircleItemDetail(long id)
        {
            if (DictionaryModule<long, CampusCircleData>.IsContains(id))
            {
                return DictionaryModule<long, CampusCircleData>.Get(id);
            }
            MySQL find = ClassPool<MySQL>.Pop();
            find.SetData(MySQLTableData.campus_circle, "id", id.ToString());
            CampusCircleData campusCircleData = mManager.FindPool<CampusCircleData>(find);
            if (campusCircleData != null)
            {
                DictionaryModule<long, CampusCircleData>.Add(id, campusCircleData);
            }
            return campusCircleData;
        }
        /// <summary>
        /// 获取校友圈
        /// </summary>
        /// <param name="account"></param>
        /// <param name="lastID"></param>
        /// <param name="schoolcode"></param>
        /// <returns></returns>
        public static IListData<long> GetCampusCircle(long account, long lastID, long schoolcode)
        {
            IMySqlCommand command = MySQLCommand.Select("id")
                .From
                .Datebase(MySQLTableData.campus_circle)
                .Where
                .Compare("id", CompareType.Small, lastID.ToString())
                .And
                .Compare("school_code", CompareType.Equal, schoolcode.ToString())
                .Order("id", MySQLSort.DESC)
                .Limit(20)
                .End
                ;
            IListData<long> listData = mManager.FindAll(command.mySqlStr, "id");
            command.Recycle();
            return listData;
        }

        /// <summary>
        /// 发表校友圈
        /// </summary>
        /// <param name="account"></param>
        /// <param name="content"></param>
        /// <param name="images"></param>
        /// <param name="isSchool"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static bool PublishCampusCircle(long account, string content, string images, long schoolCode, long time,bool isAnomymous)
        {
            MySQL add = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            dict.Add("account", account.ToString());
            dict.Add("content", content);
            dict.Add("images", images);
            dict.Add("school_code", schoolCode.ToString());
            dict.Add("time", time.ToString());
            dict.Add("is_anonymous", isAnomymous ? "1":"0");
            add.SetData(MySQLTableData.campus_circle, dict);
            bool res = mManager.Add(add);
            add.Recycle();
            return res;
        }
    }
}
