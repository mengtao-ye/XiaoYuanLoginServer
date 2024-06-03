using YSF;

namespace SubServer
{
    public partial class MySqlTools
    {

        /// <summary>
        /// 删除回复评论
        /// </summary>
        /// <param name="campusCircleID"></param>
        /// <returns></returns>
        public static bool DeleteReplayCommit(long replayCommitID)
        {
            MySQL deleteMySQL = ClassPool<MySQL>.Pop();
            deleteMySQL.SetData(MySQLTableData.campus_circle_replay_commit, "id", replayCommitID.ToString());
            bool res = mManager.Delete(deleteMySQL);
            deleteMySQL.Recycle();
            return res;
        }

        private static void DeleteCommitEffect(long commitID)
        {
            MySQL deleteMySQL = ClassPool<MySQL>.Pop();
            deleteMySQL.SetData(MySQLTableData.campus_circle_replay_commit, "replay_commit_id", commitID.ToString());
            bool res = mManager.Delete(deleteMySQL);
            deleteMySQL.Recycle();
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="campusCircleID"></param>
        /// <returns></returns>
        public static bool DeleteCommit(long commitID)
        {
            MySQL deleteMySQL = ClassPool<MySQL>.Pop();
            deleteMySQL.SetData(MySQLTableData.campus_circle_commit, "id", commitID.ToString());
            bool res = mManager.Delete(deleteMySQL);
            if (res) {
                DeleteCommitEffect(commitID);
            }
            deleteMySQL.Recycle();
            return res;
        }

        /// <summary>
        /// 回复评论
        /// </summary>
        /// <param name="campusCircleID"></param>
        /// <returns></returns>
        public static (bool, long) SendCampusCircleReplayCommit(long account, long commitID, string commit,long replayID)
        {
            MySQL addMySQL = ClassPool<MySQL>.Pop();
            addMySQL.SetData(MySQLTableData.campus_circle_replay_commit, "account", account.ToString(), "replay_commit_id", commitID.ToString(), "content", commit,"replay_id",replayID.ToString());
            long lastID = 0;
            bool res = mManager.Add(addMySQL, out lastID);
            addMySQL.Recycle();
            return (res, lastID);
        }

        /// <summary>
        /// 获取朋友圈评论数量
        /// </summary>
        /// <param name="campusCircleID"></param>
        /// <returns></returns>
        public static (bool,long) SendCampusCircleCommit(long account, long campusCircleID, string commit)
        {
            MySQL addMySQL = ClassPool<MySQL>.Pop();
            addMySQL.SetData(MySQLTableData.campus_circle_commit, "account", account.ToString(), "campus_circle_id", campusCircleID.ToString(), "content", commit);
            long lastID = 0;
            bool res = mManager.Add(addMySQL,out lastID);
            addMySQL.Recycle();
            return (res,lastID);
        }
        /// <summary>
        /// 获取朋友圈评论数量
        /// </summary>
        /// <param name="campusCircleID"></param>
        /// <returns></returns>
        public static int GetCampusCircleCommitCount(long campusCircleID)
        {
            MySQL countMySQL = ClassPool<MySQL>.Pop();
            countMySQL.SetData(MySQLTableData.campus_circle_commit, "campus_circle_id", campusCircleID.ToString());
            int count = mManager.Count(countMySQL);
            countMySQL.Recycle();
            return count;
        }
        /// <summary>
        /// 获取朋友圈点赞数量
        /// </summary>
        /// <param name="campusCircleID"></param>
        /// <returns></returns>
        public static int GetCampusCircleLikeCount(long campusCircleID)
        {
            MySQL countMySQL = ClassPool<MySQL>.Pop();
            countMySQL.SetData(MySQLTableData.campus_circle_like, "campus_circle_id", campusCircleID.ToString());
            int count = mManager.Count(countMySQL);
            countMySQL.Recycle();
            return count;
        }
        /// <summary>
        /// 获取评论信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IListData<CampusCircleReplayCommitData> GetReplayCommit(long id, long lastID)
        {
            IMySqlCommand command = MySQLCommand.Select()
              .From
              .Datebase(MySQLTableData.campus_circle_replay_commit)
              .Where
              .Compare("id", CompareType.Small, lastID.ToString())
              .And
              .Compare("replay_commit_id", CompareType.Equal, id.ToString())
              .Order("id", MySQLSort.DESC)
              .Limit(10)
              .End
              ;
            IListData<CampusCircleReplayCommitData> listData = mManager.FindAllByListPoolData<CampusCircleReplayCommitData>(command.mySqlStr);
            if (!listData.IsNullOrEmpty())
            {
                for (int i = 0; i < listData.Count; i++)
                {
                    if (listData[i].ReplayID != 0)
                    {
                        CampusCircleReplayCommitData replay = GetReplayCommitData(listData[i].ReplayID);
                        if (replay != null) 
                        {
                            listData[i].ReplayContent = replay.Content;
                            replay.Recycle();
                        }
                    }
                }
            }
            command.Recycle();
            return listData;
        }

        public static CampusCircleReplayCommitData GetReplayCommitData(long id)
        {
            MySQL mySQL = ClassPool<MySQL>.Pop();
            mySQL.SetData(MySQLTableData.campus_circle_replay_commit,"id",id.ToString());
            CampusCircleReplayCommitData campusCircleReplayCommitData = mManager.FindPool<CampusCircleReplayCommitData>(mySQL);
            mySQL.Recycle();
            return campusCircleReplayCommitData;
        }

        /// <summary>
        /// 获取评论信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IListData<CampusCircleCommitData> GetCommit(long id, long lastID)
        {
            IMySqlCommand command = MySQLCommand.Select()
              .From
              .Datebase(MySQLTableData.campus_circle_commit)
              .Where
              .Compare("id", CompareType.Small, lastID.ToString())
              .And
              .Compare("campus_circle_id", CompareType.Equal, id.ToString())
              .Order("id", MySQLSort.DESC)
              .Limit(10)
              .End
              ;
            IListData<CampusCircleCommitData> listData = mManager.FindAllByListPoolData<CampusCircleCommitData>(command.mySqlStr);
            if (!listData.IsNullOrEmpty()) 
            {
                for (int i = 0; i < listData.Count; i++)
                {
                    listData[i].ReplayCount = GetCampusCircleCommitReplayCount(listData[i].ID);
                }
            }
            command.Recycle();
            return listData;
        }

        /// <summary>
        /// 获取该评论的回复个数
        /// </summary>
        /// <param name="commitID"></param>
        /// <returns></returns>
        private static int GetCampusCircleCommitReplayCount(long commitID) 
        {
            MySQL countMySQL = ClassPool<MySQL>.Pop();
            countMySQL.SetData(MySQLTableData.campus_circle_replay_commit,"replay_commit_id",commitID.ToString());
            int count = mManager.Count(countMySQL);
            countMySQL.Recycle();
            return count;
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
        public static bool LikeCampusCircleItem(long account, long id)
        {
            MySQL mysql = ClassPool<MySQL>.Pop();
            mysql.SetData(MySQLTableData.campus_circle_like, "account", account.ToString(), "campus_circle_id", id.ToString());
            bool isExist = mManager.IsExist(mysql);
            if (isExist)
            {
                mManager.Delete(mysql);
            }
            else
            {
                mManager.Add(mysql);
            }
            mysql.Recycle();
            return !isExist;
        }

        /// <summary>
        /// 获取校友圈
        /// </summary>
        /// <param name="account"></param>
        /// <param name="lastID"></param>
        /// <param name="schoolcode"></param>
        /// <returns></returns>
        public static IListData<CampusCircleData> GetCampusCircle(long account, long lastID, long schoolcode)
        {
            IMySqlCommand command = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.campus_circle)
                .Where
                .Compare("id", CompareType.Small, lastID.ToString())
                .And
                .Compare("school_code", CompareType.Equal, schoolcode.ToString())
                .Order("id", MySQLSort.DESC)
                .Limit(10)
                .End
                ;
            IListData<CampusCircleData> listData = mManager.FindAllByListPoolData<CampusCircleData>(command.mySqlStr);
            command.Recycle();
            return listData;
        }

        /// <summary>
        /// 获取校友圈
        /// </summary>
        /// <param name="account"></param>
        /// <param name="lastID"></param>
        /// <param name="schoolcode"></param>
        /// <returns></returns>
        public static IListData<CampusCircleData> GetFriendCampusCircle(long friendAccount, long lastID)
        {
            IMySqlCommand command = MySQLCommand.Select()
                .From
                .Datebase(MySQLTableData.campus_circle)
                .Where
                .Compare("id", CompareType.Small, lastID.ToString())
                .And
                .Compare("account", CompareType.Equal, friendAccount.ToString())
                .Order("id", MySQLSort.DESC)
                .Limit(10)
                .End
                ;
            IListData<CampusCircleData> listData = mManager.FindAllByListPoolData<CampusCircleData>(command.mySqlStr);
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
        public static bool PublishCampusCircle(long account, string content, string images, long schoolCode, long time, bool isAnomymous)
        {
            MySQL add = ClassPool<MySQL>.Pop();
            IDictionaryData<string, string> dict = ClassPool<DictionaryData<string, string>>.Pop();
            dict.Add("account", account.ToString());
            dict.Add("content", content);
            dict.Add("images", images);
            dict.Add("school_code", schoolCode.ToString());
            dict.Add("time", time.ToString());
            dict.Add("is_anonymous", isAnomymous ? "1" : "0");
            add.SetData(MySQLTableData.campus_circle, dict);
            bool res = mManager.Add(add);
            add.Recycle();
            return res;
        }
    }
}
