using System;
using System.Net;
using YSF;

namespace SubServer
{
    public class TcpLoginHandler : BaseTcpRequestHandle
    {
        protected override short mRequestCode => (short)TcpLoginRequestCode.Login;
        public TcpLoginHandler(ITcpServer server) : base(server)
        {

        }
        protected override void ComfigActionCode()
        {
            //Base
            Add((short)TcpLoginUdpCode.LoginAccount, LoginAccount);
            Add((short)TcpLoginUdpCode.HeartBeat, HeartBeat);
            Add((short)TcpLoginUdpCode.RegisterAccount, RegisterAccount);
            Add((short)TcpLoginUdpCode.GetUserData, GetUserData);
            Add((short)TcpLoginUdpCode.GetMySchool, GetMySchool);
            Add((short)TcpLoginUdpCode.GetSchoolData, GetSchoolData);
            Add((short)TcpLoginUdpCode.SearchSchool, SearchSchool);
            Add((short)TcpLoginUdpCode.JoinSchool, JoinSchool);
            Add((short)TcpLoginUdpCode.ModifyName, ModifyName);
            Add((short)TcpLoginUdpCode.ModifySex, ModifySex);
            Add((short)TcpLoginUdpCode.ModifyBrithday, ModifyBrithday);
            Add((short)TcpLoginUdpCode.ExitSchool, ExitSchool);
            //Chat
            Add((short)TcpLoginUdpCode.GetNewChatMsg, GetNewChatMsg);
            Add((short)TcpLoginUdpCode.GetFriendList, GetFriendList);
            Add((short)TcpLoginUdpCode.SearchFriendData, SearchFriendData);
            Add((short)TcpLoginUdpCode.SendAddFriendRequest, SendAddFriendRequest);
            Add((short)TcpLoginUdpCode.GetAddFriendRequest, GetAddFriendRequest);
            Add((short)TcpLoginUdpCode.RefuseFriend, RefuseFriend);
            Add((short)TcpLoginUdpCode.ConfineFriend, ConfineFriend);
            Add((short)TcpLoginUdpCode.SendChatMsg, SendChatMsg);
            Add((short)TcpLoginUdpCode.ChangeNotes, ChangeNotes);
            Add((short)TcpLoginUdpCode.DeleteFriend, DeleteFriend);
            Add((short)TcpLoginUdpCode.IsFriend, IsFriend);
            //CampusCircle
            Add((short)TcpLoginUdpCode.PublishCampusCircle, PublishCampusCircle);
            Add((short)TcpLoginUdpCode.GetCampusCircle, GetCampusCircle);
            Add((short)TcpLoginUdpCode.LikeCampusCircleItem, LikeCampusCircleItem);
            Add((short)TcpLoginUdpCode.HasLikeCampusCircleItem, HasLikeCampusCircleItem);
            Add((short)TcpLoginUdpCode.GetCommit, GetCommit);
            Add((short)TcpLoginUdpCode.GetFriendCampusCircle, GetFriendCampusCircle);
            Add((short)TcpLoginUdpCode.GetCampusCircleLikeCount, GetCampusCircleLikeCount);
            Add((short)TcpLoginUdpCode.GetCampusCircleCommitCount, GetCampusCircleCommitCount);
            Add((short)TcpLoginUdpCode.SendCampCircleCommit, SendCampCircleCommit);
            Add((short)TcpLoginUdpCode.SendCampCircleReplayCommit, SendCampCircleReplayCommit);
            Add((short)TcpLoginUdpCode.GetReplayCommit, GetReplayCommit);
            Add((short)TcpLoginUdpCode.DeleteCommit, DeleteCommit);
            Add((short)TcpLoginUdpCode.DeleteReplayCommit, DeleteReplayCommit);
            //Lost
            Add((short)TcpLoginUdpCode.PublishLostData, PublishLostData);
            Add((short)TcpLoginUdpCode.GetMyLostList, GetMyLostList);
            Add((short)TcpLoginUdpCode.GetLostList, GetLostList);
            Add((short)TcpLoginUdpCode.SearchLostList, SearchLostList);
            Add((short)TcpLoginUdpCode.DeleteLost, DeleteLost);
            //兼职
            Add((short)TcpLoginUdpCode.ReleasePartTimeJob, ReleasePartTimeJob);
            Add((short)TcpLoginUdpCode.GetMyReleasePartTimeJob, GetMyReleasePartTimeJob);
            Add((short)TcpLoginUdpCode.GetPartTimeJobList, GetPartTimeJobList);
            Add((short)TcpLoginUdpCode.ApplicationPartTimeJob, ApplicationPartTimeJob);
            Add((short)TcpLoginUdpCode.GetApplicationPartTimeJob, GetApplicationPartTimeJob);
            Add((short)TcpLoginUdpCode.GetMyApplicationPartTimeJobList, GetMyApplicationPartTimeJobList);
            Add((short)TcpLoginUdpCode.CancelApplicationPartTimeJob, CancelApplicationPartTimeJob);
            Add((short)TcpLoginUdpCode.CollectionPartTimeJob, CollectionPartTimeJob);
            Add((short)TcpLoginUdpCode.IsCollectionPartTimeJob, IsCollectionPartTimeJob);
            Add((short)TcpLoginUdpCode.GetMyCollectionPartTimeJobList, GetMyCollectionPartTimeJobList);
            Add((short)TcpLoginUdpCode.SearchPartTimeJobList, SearchPartTimeJobList);

            //闲置
            Add((short)TcpLoginUdpCode.ReleaseUnuse, ReleaseUnuse);
            Add((short)TcpLoginUdpCode.GetUnuseList, GetUnuseList);
            Add((short)TcpLoginUdpCode.GetMyReleaseUnuseList, GetMyReleaseUnuseList);
            Add((short)TcpLoginUdpCode.IsCollectionUnuse, IsCollectionUnuse);
            Add((short)TcpLoginUdpCode.CollectionUnuse, CollectionUnuse);
            Add((short)TcpLoginUdpCode.GetMyCollectionUnuseList, GetMyCollectionUnuseList);
            Add((short)TcpLoginUdpCode.SearchUnuseList, SearchUnuseList);

            //MetaSchool
            Add((short)TcpLoginUdpCode.GetMyMetaSchoolData, GetMyMetaSchoolData);
            Add((short)TcpLoginUdpCode.SetMyMetaSchoolData, SetMyMetaSchoolData);
            //Found
            Add((short)TcpLoginUdpCode.PublishFoundData, PublishFoundData);
            Add((short)TcpLoginUdpCode.GetFoundList, GetFoundList);
            Add((short)TcpLoginUdpCode.GetMyFoundList, GetMyFoundList);
            Add((short)TcpLoginUdpCode.DeleteFound, DeleteFound);
            Add((short)TcpLoginUdpCode.SearchFoundList, SearchFoundList);
        }

        /// <summary>
        /// 退出学校
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] ExitSchool(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return SocketTools.GetNullParamError();
            long account = data.ToLong();
            long schoolCode = data.ToLong(8);
            bool res = MySqlTools.ExitSchool(account, schoolCode);
            if (res)
            {
                return SocketTools.ToBytes(SocketResultCode.Success, null, null);
            }
            else
            {
                return SocketTools.ToBytes(SocketResultCode.Error, "学校退出失败", null);
            }
        }

        /// <summary>
        /// 修改生日
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] ModifyBrithday(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return SocketTools.GetNullParamError();
            long account = data.ToLong();
            int brithday = data.ToInt(8);
            bool res = MySqlTools.ModifyBirthday(account, brithday);
            if (res)
            {
                return SocketTools.ToBytes(SocketResultCode.Success, null, brithday.ToBytes());
            }
            else
            {
                return SocketTools.ToBytes(SocketResultCode.Error, "生日修改失败", null);
            }
        }
        /// <summary>
        /// 修改性别
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] ModifySex(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return SocketTools.GetNullParamError();
            long account = data.ToLong();
            byte sex = data.ToByte(8);
            bool res = MySqlTools.ModifySex(account, sex);
            if (res)
            {
                return SocketTools.ToBytes(SocketResultCode.Success, null, sex.ToBytes());
            }
            else
            {
                return SocketTools.ToBytes(SocketResultCode.Error, "性别修改失败", null);
            }
        }
        /// <summary>
        /// 修改名称
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] ModifyName(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return SocketTools.GetNullParamError();
            long account = data.ToLong();
            string modifyName = data.ToStr(8, data.Length - 8);
            bool res = MySqlTools.ModifyName(account, modifyName);
            if (res)
            {
                return SocketTools.ToBytes(SocketResultCode.Success, null, modifyName.ToBytes());
            }
            else
            {
                return SocketTools.ToBytes(SocketResultCode.Error, "名称修改失败", null);
            }
        }
        /// <summary>
        /// 是否是好友
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] IsFriend(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            long friendAccount = data.ToLong(8);
            bool res = MySqlTools.IsFriend(account, friendAccount);
            return ByteTools.Concat(res.ToBytes(), friendAccount.ToBytes());
        }
        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] DeleteFriend(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            long friendAccount = data.ToLong(8);
            MySqlTools.DeleteFriend(account, friendAccount);
            MySqlTools.DeleteFriend(friendAccount, account);
            bool finialRes = true;
            return ByteTools.Concat(finialRes.ToBytes(), friendAccount.ToBytes());
        }
        /// <summary>
        /// 删除回复评论
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] DeleteReplayCommit(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long replayCommitID = data.ToLong();
            bool res = MySqlTools.DeleteReplayCommit(replayCommitID);
            return ByteTools.Concat(res.ToBytes(), replayCommitID.ToBytes());
        }
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] DeleteCommit(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long commitID = data.ToLong();
            bool res = MySqlTools.DeleteCommit(commitID);

            return ByteTools.Concat(res.ToBytes(), commitID.ToBytes());
        }

        /// <summary>
        /// 获取回复评论信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetReplayCommit(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long commitID = data.ToLong();
            long lastID = data.ToLong(8);
            IListData<CampusCircleReplayCommitData> list = MySqlTools.GetReplayCommit(commitID, lastID);
            if (list.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            byte[] returnBytes = list.list.ToBytes();
            list.Recycle();
            return returnBytes;
        }

        /// <summary>
        /// 发送校友圈回复评论
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] SendCampCircleReplayCommit(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IListData<byte[]> list = data.ToListBytes();
            long account = list[0].ToLong();
            long commitID = list[1].ToLong();
            string commit = list[2].ToStr();
            long replayID = list[3].ToLong();
            var res = MySqlTools.SendCampusCircleReplayCommit(account, commitID, commit, replayID);
            list.Add(res.Item1.ToBytes());//是否评论成功了
            list.Add(res.Item2.ToBytes());//评论的ID
            if (replayID != 0)
            {
                CampusCircleReplayCommitData replayData = MySqlTools.GetReplayCommitData(replayID);
                if (replayData != null)
                {
                    list.Add(replayData.Content.ToBytes());//评论的ID
                    replayData.Recycle();
                }
                else
                {
                    list.Add("".ToBytes());//评论的ID
                }
            }
            else
            {
                list.Add("".ToBytes());//评论的ID
            }
            byte[] returnBytes = list.list.ToBytes();
            list?.Recycle();
            return returnBytes;
        }

        /// <summary>
        /// 发送校友圈评论
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] SendCampCircleCommit(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IListData<byte[]> list = data.ToListBytes();
            long account = list[0].ToLong();
            long campusCircleID = list[1].ToLong();
            string commit = list[2].ToStr();
            var res = MySqlTools.SendCampusCircleCommit(account, campusCircleID, commit);
            list.Add(res.Item1.ToBytes());
            list.Add(res.Item2.ToBytes());
            byte[] returnBytes = list.list.ToBytes();
            list?.Recycle();
            return returnBytes;
        }

        /// <summary>
        /// 获取朋友圈评论个数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetCampusCircleCommitCount(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long campusCircleID = data.ToLong();
            byte type = data.ToByte(8);
            int count = MySqlTools.GetCampusCircleCommitCount(campusCircleID);
            return ByteTools.ConcatParam(campusCircleID.ToBytes(), count.ToBytes(), type.ToBytes());
        }
        /// <summary>
        /// 获取朋友圈点赞个数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetCampusCircleLikeCount(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long campusCircleID = data.ToLong();
            byte type = data.ToByte(8);
            int count = MySqlTools.GetCampusCircleLikeCount(campusCircleID);
            return ByteTools.ConcatParam(campusCircleID.ToBytes(), count.ToBytes(), type.ToBytes());
        }
        /// <summary>
        /// 获取好友的校友圈信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetFriendCampusCircle(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            long lastID = data.ToLong(8);
            IListData<CampusCircleData> list = MySqlTools.GetFriendCampusCircle(account, lastID);
            if (list.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            byte[] returnBytes = list.list.ToBytes();
            list.Recycle();
            return returnBytes;
        }

        /// <summary>
        /// 修改备注
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] ChangeNotes(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IListData<byte[]> list = data.ToListBytes();
            long account = list[0].ToLong();
            long friendAccount = list[1].ToLong();
            string notes = list[2].ToStr();
            bool res = MySqlTools.ChangeNotes(account, friendAccount, notes);
            list.Add(res.ToBytes());
            byte[] returnBytes = list.list.ToBytes();
            list?.Recycle();
            return returnBytes;
        }
        /// <summary>
        /// 查找兼职列表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] SearchPartTimeJobList(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IListData<byte[]> list = data.ToListBytes();
            int lastID = list[0].ToInt();
            string searchKey = list[1].ToStr();
            list?.Recycle();
            IListData<PartTimeJobData> listData = MySqlTools.SearchPartTimeJobList(lastID, searchKey);
            if (listData.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            byte[] returnBytes = listData.list.ToBytes();
            listData.Recycle();
            return returnBytes;
        }
        /// <summary>
        /// 查找闲置列表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] SearchUnuseList(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IListData<byte[]> list = data.ToListBytes();
            long schoolCode = list[0].ToLong();
            int lastID = list[1].ToInt();
            string searchKey = list[2].ToStr();
            list?.Recycle();
            IListData<UnuseData> listData = MySqlTools.SearchUnuseList(schoolCode, lastID, searchKey);
            if (listData.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            byte[] returnBytes = listData.list.ToBytes();
            listData.Recycle();
            return returnBytes;
        }
        /// <summary>
        /// 获取我收藏的闲置列表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetMyCollectionUnuseList(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            int lastid = data.ToInt(8);
            IListData<UnuseData> res = MySqlTools.GetMyCollectionUnuseList(account, lastid);
            if (res == null) return BytesConst.FALSE_BYTES;
            byte[] bytes = res.list.ToBytes();
            res.Recycle();
            return bytes;
        }

        /// <summary>
        /// 操作收藏闲置
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] CollectionUnuse(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            int unuseID = data.ToInt(8);
            bool databaseIsCollection = MySqlTools.IsCollectionUnuse(unuseID, account);
            if (databaseIsCollection)
            {
                MySqlTools.CancelCollectionUnuse(unuseID, account);
            }
            else
            {
                MySqlTools.CollectionUnuse(unuseID, account);
            }
            return ByteTools.Concat((!databaseIsCollection).ToBytes(), unuseID.ToBytes());
        }
        /// <summary>
        /// 是否收藏闲置
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] IsCollectionUnuse(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            int unuseID = data.ToInt(8);
            bool res = MySqlTools.IsCollectionUnuse(unuseID, account);
            return ByteTools.Concat(res.ToBytes(), unuseID.ToBytes());
        }


        /// <summary>
        /// 获取我发布的兼职
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetMyReleaseUnuseList(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            int lastid = data.ToInt(8);
            IListData<UnuseData> res = MySqlTools.GetMyReleaseUnuseList(account, lastid);
            if (res == null) return BytesConst.FALSE_BYTES;
            byte[] bytes = res.list.ToBytes();
            res.Recycle();
            return bytes;
        }

        /// <summary>
        /// 获取我收藏的兼职报名列表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetMyCollectionPartTimeJobList(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            int lastid = data.ToInt(8);
            IListData<PartTimeJobData> res = MySqlTools.GetMyCollectionPartTimeJobList(account, lastid);
            if (res == null) return BytesConst.FALSE_BYTES;
            byte[] bytes = res.list.ToBytes();
            res.Recycle();
            return bytes;
        }

        /// <summary>
        /// 是否收藏兼职
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] IsCollectionPartTimeJob(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            int partTimeJobID = data.ToInt(8);
            bool res = MySqlTools.IsCollectionPartTimeJob(partTimeJobID, account);
            return ByteTools.Concat(res.ToBytes(), partTimeJobID.ToBytes());
        }

        /// <summary>
        /// 操作收藏兼职
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] CollectionPartTimeJob(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            int partTimeJobID = data.ToInt(8);
            bool databaseIsCollection = MySqlTools.IsCollectionPartTimeJob(partTimeJobID, account);
            if (databaseIsCollection)
            {
                MySqlTools.CancelCollectionPartTimeJob(partTimeJobID, account);
            }
            else
            {
                MySqlTools.CollectionPartTimeJob(partTimeJobID, account);
            }
            return ByteTools.Concat((!databaseIsCollection).ToBytes(), partTimeJobID.ToBytes());
        }

        /// <summary>
        /// 取消兼职的报名
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] CancelApplicationPartTimeJob(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            int partTimeJobID = data.ToInt(8);
            bool res = MySqlTools.CancelPartTimeJobApplication(partTimeJobID, account);

            return ByteTools.Concat(res.ToBytes(), partTimeJobID.ToBytes());
        }

        /// <summary>
        /// 获取我的兼职报名列表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetMyApplicationPartTimeJobList(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            int lastid = data.ToInt(8);
            IListData<PartTimeJobData> res = MySqlTools.GetMyApplicationPartTimeJobList(account, lastid);
            if (res == null) return BytesConst.FALSE_BYTES;
            byte[] bytes = res.list.ToBytes();
            res.Recycle();
            return bytes;
        }


        /// <summary>
        /// 查找寻物
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] SearchFoundList(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IListData<byte[]> list = data.ToListBytes();
            long schoolCode = list[0].ToLong();
            long last_update_time = list[1].ToLong();
            string searchKey = list[2].ToStr();
            list?.Recycle();
            IListData<FoundData> listData = MySqlTools.SearchFoundList(schoolCode, last_update_time, searchKey);
            if (listData.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            byte[] returnBytes = listData.list.ToBytes();
            listData.Recycle();
            return returnBytes;
        }
        /// <summary>
        /// 设置我的校园数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] DeleteFound(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            int id = data.ToInt();
            bool res = MySqlTools.DeleteFound(id);
            return ByteTools.Concat(res.ToBytes(), data);
        }


        /// <summary>
        /// 获取我的寻物信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetMyFoundList(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            long lastUpdateTime = data.ToLong(8);
            IListData<FoundData> listData = MySqlTools.GetMyFound(account, lastUpdateTime);
            if (listData.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            byte[] returnBytes = listData.list.ToBytes();
            listData.Recycle();
            return returnBytes;
        }

        /// <summary>
        /// 获取寻物列表信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetFoundList(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long schoolCode = data.ToLong();
            long last_update_time = data.ToLong(8);
            IListData<FoundData> listData = MySqlTools.GetFoundList(schoolCode, last_update_time);
            if (listData.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            byte[] returnBytes = listData.list.ToBytes();
            listData.Recycle();
            return returnBytes;
        }

        /// <summary>
        /// 发布寻物启事信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] PublishFoundData(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IDictionaryData<byte, byte[]> dict = data.ToBytesDictionary();
            string name = dict[0].ToStr();
            string pos = dict[1].ToStr();
            long startTime = dict[2].ToLong();
            long account = dict[3].ToLong();
            string images = string.Empty;
            if (dict.ContainsKey(4))
            {
                images = dict[4].ToStr();
            }
            long school_code = dict[5].ToLong();
            string detail = dict[6].ToStr();
            byte contactType = dict[7].ToByte();
            string contact = dict[8].ToStr();
            string quest = string.Empty;
            string result = string.Empty;
            if (dict.ContainsKey(9))
            {
                quest = dict[9].ToStr();
                result = dict[10].ToStr();
            }
            dict.Recycle();
            bool res = MySqlTools.AddFound(name, pos, startTime, account, images, school_code, detail, contactType, contact, quest, result);
            return res.ToBytes();
        }
        /// <summary>
        /// 设置我的校园数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] DeleteLost(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            int id = data.ToInt();
            bool res = MySqlTools.DeleteLost(id);
            return ByteTools.Concat(res.ToBytes(), data);
        }

        /// <summary>
        /// 设置我的校园数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] SetMyMetaSchoolData(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            int roleID = data.ToInt(8);
            bool res = MySqlTools.SetMyMetaSchoolData(account, roleID);
            if (res)
            {
                MyMetaSchoolData myMetaSchoolData = ClassPool<MyMetaSchoolData>.Pop();
                myMetaSchoolData.Account = account;
                myMetaSchoolData.RoleID = roleID;
                byte[] returnBytes = myMetaSchoolData.ToBytes();
                myMetaSchoolData.Recycle();
                return returnBytes;
            }
            return BytesConst.FALSE_BYTES;
        }
        /// <summary>
        /// 获取我的校园数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetMyMetaSchoolData(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return SocketTools.GetNullParamError();
            long account = data.ToLong();
            MyMetaSchoolData myMetaSchoolData = MySqlTools.GetMyMetaSchoolData(account);
            if (myMetaSchoolData == null)
            {
                myMetaSchoolData = ClassPool<MyMetaSchoolData>.Pop();
                myMetaSchoolData.Account = account;
                myMetaSchoolData.RoleID = 0;
                byte[] returnBytes2 = myMetaSchoolData.ToBytes();
                myMetaSchoolData.Recycle();
                return SocketTools.ToBytes(SocketResultCode.Success, "未找到该用户的信息", returnBytes2);
            }
            byte[] returnBytes = myMetaSchoolData.ToBytes();
            myMetaSchoolData.Recycle();
            return SocketTools.ToBytes(SocketResultCode.Success, null, returnBytes);

        }

        /// <summary>
        /// 获取闲置列表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetUnuseList(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            int lastID = data.ToInt(8);
            byte type = data[12];
            UnuseData unuseData = MySqlTools.GetUnuseList(account, lastID, type);
            if (unuseData == null)
            {
                return BytesConst.FALSE_BYTES;
            }
            byte[] returnBytes = unuseData.ToBytes();
            unuseData.Recycle();
            return returnBytes;
        }

        /// <summary>
        /// 发布闲置
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] ReleaseUnuse(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IListData<byte[]> list = data.ToListBytes();
            string content = list[0].ToStr();
            IListData<SelectImageData> selectImageData = SelectImageDataTools.GetData(list[1]);
            string images = SelectImageDataTools.GetStr(selectImageData);
            selectImageData.Recycle();
            byte selectType = list[2].ToByte();
            int price = list[3].ToInt();
            long time = list[4].ToLong();
            long account = list[5].ToLong();
            byte contactType = list[6].ToByte();
            string contact = list[7].ToStr();
            long schoolCode = list[8].ToLong();
            list.Recycle();
            bool res = MySqlTools.AddUnuse(content, images, selectType, price, time, account, contactType, contact, schoolCode);
            return res.ToBytes();
        }
        /// <summary>
        /// 获取报名兼职列表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetApplicationPartTimeJob(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            int partTimeJobID = data.ToInt();
            int lastID = data.ToInt(4);
            IListData<PartTimeJobApplicationData> listData = MySqlTools.GetApplicationPartTimeJobList(partTimeJobID, lastID);
            if (listData.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            byte[] sendBytes = listData.list.ToBytes();
            listData.Recycle();
            return sendBytes;
        }

        /// <summary>
        /// 报名兼职
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] ApplicationPartTimeJob(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IListData<byte[]> listData = data.ToListBytes();
            long account = listData[0].ToLong();
            int partTimeJobID = listData[1].ToInt();
            string name = listData[2].ToStr();
            bool isMan = listData[3].ToBool();
            int age = listData[4].ToInt();
            string call = listData[5].ToStr();
            listData.Recycle();
            byte res = MySqlTools.ApplicationPartTimeJob(account, partTimeJobID, name, isMan, age, call);
            return res.ToBytes();
        }

        /// <summary>
        /// 获取兼职列表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetPartTimeJobList(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            int lastID = data.ToInt();
            PartTimeJobData res = MySqlTools.GetPartTimeJob(lastID);
            if (res == null) return BytesConst.FALSE_BYTES;
            byte[] bytes = res.ToBytes();
            res.Recycle();
            return bytes;
        }

        /// <summary>
        /// 获取我发布的兼职
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetMyReleasePartTimeJob(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            int lastID = data.ToInt(8);
            PartTimeJobData res = MySqlTools.GetMyReleasePartTimeJob(account, lastID);
            if (res == null) return BytesConst.FALSE_BYTES;
            byte[] bytes = res.ToBytes();
            res.Recycle();
            return bytes;
        }

        /// <summary>
        /// 发布兼职
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] ReleasePartTimeJob(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IListData<byte[]> list = data.ToListBytes();
            string title = list[0].ToStr();
            int price = list[1].ToInt();
            int priceType = list[2].ToInt();
            string jobTime = list[3].ToStr();
            string position = list[4].ToStr();
            string detail = list[5].ToStr();
            long account = list[6].ToLong();
            list.Recycle();
            bool res = MySqlTools.AddPartTimeJob(title, price, priceType, jobTime, position, detail, account);
            return res.ToBytes();
        }
        /// <summary>
        /// 获取我的失物招领信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetMyLostList(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            long lastUpdateTime = data.ToLong(8);
            IListData<LostData> listData = MySqlTools.GetMyLost(account, lastUpdateTime);
            if (listData.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            byte[] returnBytes = listData.list.ToBytes();
            listData.Recycle();
            return returnBytes;
        }

        /// <summary>
        /// 获取失物招领列表信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetLostList(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IDictionaryData<byte, byte[]> dictionaryData = data.ToBytesDictionary();
            long schoolCode = dictionaryData[0].ToLong();
            long last_update_time = dictionaryData[1].ToLong();
            dictionaryData?.Recycle();
            IListData<LostData> listData = MySqlTools.GetLostList(schoolCode, last_update_time);
            if (listData.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            byte[] returnBytes = listData.list.ToBytes();
            listData.Recycle();
            return returnBytes;
        }

        /// <summary>
        /// 查找失物列表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] SearchLostList(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IListData<byte[]> list = data.ToListBytes();
            long schoolCode = list[0].ToLong();
            long last_update_time = list[1].ToLong();
            string searchKey = list[2].ToStr();
            list?.Recycle();
            IListData<LostData> listData = MySqlTools.SearchLostList(schoolCode, last_update_time, searchKey);
            if (listData.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            byte[] returnBytes = listData.list.ToBytes();
            listData.Recycle();
            return returnBytes;
        }
        /// <summary>
        /// 发布失物招领信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] PublishLostData(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IDictionaryData<byte, byte[]> dict = data.ToBytesDictionary();
            string name = dict[0].ToStr();
            string pos = string.Empty;
            if (dict.ContainsKey(1))
            {
                pos = dict[1].ToStr();
            }
            long startTime = dict[2].ToLong();
            long endTime = dict[3].ToLong();
            long account = dict[4].ToLong();
            string images = string.Empty;
            if (dict.ContainsKey(5))
            {
                images = dict[5].ToStr();
            }
            long school_code = dict[6].ToLong();
            string detail = dict[7].ToStr();
            byte contactType = dict[8].ToByte();
            string contact = dict[9].ToStr();
            dict.Recycle();
            bool res = MySqlTools.AddLost(name, pos, startTime, endTime, account, images, school_code, detail, contactType, contact);
            return res.ToBytes();
        }
        /// <summary>
        /// 获取评论信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetCommit(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long campusCircleID = data.ToLong();
            long lastID = data.ToLong(8);
            IListData<CampusCircleCommitData> list = MySqlTools.GetCommit(campusCircleID, lastID);
            if (list.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            byte[] returnBytes = list.list.ToBytes();
            list.Recycle();
            return returnBytes;
        }
        /// <summary>
        /// 是否朋友圈点赞
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] HasLikeCampusCircleItem(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            long campusCircleID = data.ToLong(8);
            byte type = data.ToByte(16);
            bool res = MySqlTools.HasLikeCampusCircleItem(account, campusCircleID);
            return ByteTools.ConcatParam(campusCircleID.ToBytes(), res.ToBytes(), type.ToBytes());
        }

        /// <summary>
        /// 朋友圈点赞
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] LikeCampusCircleItem(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            long campusCircleID = data.ToLong(8);
            bool isFriendCampusCircle = data.ToBool(16);
            bool res = MySqlTools.LikeCampusCircleItem(account, campusCircleID);
            return ByteTools.ConcatParam(campusCircleID.ToBytes(), res.ToBytes(), isFriendCampusCircle.ToBytes());
        }


        /// <summary>
        /// 获取校友圈信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetCampusCircle(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IListData<byte[]> listData = data.ToListBytes();
            if (listData.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = listData[0].ToLong();
            long lastID = listData[1].ToLong();
            long schoolCode = listData[2].ToLong();
            listData.Recycle();
            IListData<CampusCircleData> list = MySqlTools.GetCampusCircle(account, lastID, schoolCode);
            if (list.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            byte[] returnBytes = list.list.ToBytes();
            list.Recycle();
            return returnBytes;
        }
        /// <summary>
        /// 发布校友圈
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] PublishCampusCircle(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IListData<byte[]> list = data.ToListBytes();
            string content = list[0].ToStr();
            IListData<SelectImageData> selectImageData = SelectImageDataTools.GetData(list[1]);
            string images = SelectImageDataTools.GetStr(selectImageData);
            selectImageData.Recycle();
            long time = list[2].ToLong();
            long account = list[3].ToLong();
            long schoolCode = list[4].ToLong();
            bool isAnomymous = list[5].ToBool();
            list.Recycle();
            bool res = MySqlTools.PublishCampusCircle(account, content, images, schoolCode, time, isAnomymous);
            return res.ToBytes();
        }

        /// <summary>
        /// 同意好友申请
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] ConfineFriend(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            long account = data.ToLong();
            long friendAccount = data.ToLong(8);
            MySqlTools.DeleteChatMsg(UserAccountConstData.NEW_FRIEND_ACCOUNT, friendAccount, (byte)ChatMsgType.NewFriend);
            MySqlTools.DeleteChatMsg(UserAccountConstData.NEW_FRIEND_ACCOUNT, account, (byte)ChatMsgType.NewFriend);
            bool res = false;
            MySqlTools.DeleteAddFriendRequest(friendAccount, account);
            if (!MySqlTools.IsFriend(account, friendAccount))
            {
                bool b1 = MySqlTools.AddFriendPair(account, friendAccount);
                bool b2 = MySqlTools.AddFriendPair(friendAccount, account);
                res = b1 && b2;
                if (res)
                {
                    //发送打招呼信息
                    MySqlTools.AddChatMsg(friendAccount, account, (byte)ChatMsgType.Text, StrConstData.AddNewFriendTip, DateTimeOffset.Now.ToUnixTimeSeconds());
                    MySqlTools.AddChatMsg(account, friendAccount, (byte)ChatMsgType.Text, StrConstData.AddNewFriendTip, DateTimeOffset.Now.ToUnixTimeSeconds());
                }
            }
            else
            {
                res = true;
            }
            return ByteTools.Concat(res.ToBytes(), friendAccount.ToBytes());
        }
        /// <summary>
        /// 拒绝好友申请
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] RefuseFriend(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            long account = data.ToLong();
            long friendAccount = data.ToLong(8);
            bool res = MySqlTools.DeleteAddFriendRequest(friendAccount, account);
            return ByteTools.Concat(res.ToBytes(), friendAccount.ToBytes());
        }
        /// <summary>
        /// 获取好友申请列表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetAddFriendRequest(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            long account = data.ToLong();
            int lastAddFriendID = data.ToInt(8);
            IListData<AddFriendRequestData> listData = MySqlTools.GetAddFriendRequestList(account, lastAddFriendID);
            if (listData.IsNullOrEmpty()) return null;
            byte[] returnBytes = listData.list.ToBytes();
            listData.Recycle();
            return returnBytes;
        }

        /// <summary>
        /// 添加好友申请
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] SendAddFriendRequest(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            IListData<byte[]> lists = data.ToListBytes();
            long myAccount = lists[0].ToLong();
            long friendAccount = lists[1].ToLong();
            string addContent = lists[2].ToStr();
            lists.Recycle();
            byte returnCode = MySqlTools.AddFriendRequest(myAccount, friendAccount, addContent);
            if (returnCode == 1 || returnCode == 3)
            {
                //发送成功或者已经发送过
                MySqlTools.AddChatMsg(UserAccountConstData.NEW_FRIEND_ACCOUNT, friendAccount, (byte)ChatMsgType.NewFriend, "有新的朋友，请点击查看", DateTimeOffset.Now.ToUnixTimeSeconds());
            }
            return returnCode.ToBytes();
        }

        /// <summary>
        /// 查找好友信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] SearchFriendData(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            long myAccount = data.ToLong(0);
            long friendMsg = data.ToLong(8);
            bool isByUserID = friendMsg.ToString().Length != 11;
            UserData userData = null;
            if (isByUserID)
            {
                userData = MySqlTools.GetUserDataByUserID(friendMsg);
            }
            else
            {
                userData = MySqlTools.GetUserDataByAccount(friendMsg);
            }
            if (userData == null)
            {
                //没有这个人
                return BytesConst.FALSE_BYTES;
            }
            bool isFriend = MySqlTools.IsFriend(myAccount, userData.Account);
            return ByteTools.Concat(isFriend.ToBytes(), userData.Account.ToBytes());
        }
        /// <summary>
        /// 加入学校
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetFriendList(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            long account = data.ToLong(0);
            int friendListID = data.ToInt(8);
            IListData<FriendPairData> listData = MySqlTools.GetFriendPair(account, friendListID);
            if (listData.IsNullOrEmpty())
            {
                return BytesConst.Empty;
            }
            byte[] returnBytes = listData.list.ToBytes();
            listData.Recycle();
            return returnBytes;
        }

        /// <summary>
        /// 加入学校
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] SendChatMsg(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            IListData<byte[]> list = data.ToListBytes();
            long sendAccount = list[0].ToLong();
            long receiveAccount = list[1].ToLong();
            byte msgType = list[2].ToByte();
            string content = list[3].ToStr();
            long time = list[4].ToLong();
            long id = 0;
            bool isFriend = MySqlTools.IsFriend(sendAccount, receiveAccount);
            byte resCode = 0;//0 失败 1 成功 2 不是好友
            byte[] returnBytes = null;
            if (isFriend)
            {
                bool res = MySqlTools.AddChatMsg(sendAccount, receiveAccount, msgType, content, time, out id);
                if (res)
                {
                    list.Add(id.ToBytes());
                    resCode = 1;
                    returnBytes = ByteTools.Concat(new byte[] { resCode }, list.list.ToBytes());
                }
                else
                {
                    resCode = 0;
                    returnBytes = resCode.ToBytes();
                }
            }
            else
            {
                resCode = 2;
                returnBytes = resCode.ToBytes();
            }


            list.Recycle();
            return returnBytes;
        }
        /// <summary>
        /// 获取新消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetNewChatMsg(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            long account = data.ToLong();
            long lastChatID = data.ToLong(8);
            IListData<ChatData> listData = MySqlTools.GetChatMsg(account, lastChatID);
            if (listData == null) return null;
            byte[] returnBytes = listData.list.ToBytes();
            listData.Recycle();
            return returnBytes;
        }

        /// <summary>
        /// 加入学校
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] JoinSchool(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            long account = data.ToLong(0);
            long schoolCode = data.ToLong(8);
            byte res = MySqlTools.JoinSchool(account, schoolCode);
            byte[] returnBytes = null;
            if (res == 1)
            {
                returnBytes = ByteTools.Concat(res, schoolCode.ToBytes());
            }
            else
            {
                returnBytes = res.ToBytes();
            }
            return returnBytes;
        }
        private byte[] SearchSchool(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            string name = data.ToStr();
            IListData<SchoolData> schoolDatas = MySqlTools.SearchSchoolsData(name);
            if (schoolDatas.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            byte[] returnBytes = schoolDatas.list.ToBytes();
            schoolDatas.Recycle();
            return returnBytes;
        }
        private byte[] GetSchoolData(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            long schoolCode = data.ToLong();
            SchoolData schoolData = MySqlTools.GetSchoolData(schoolCode);
            if (schoolData == null) return null;
            return schoolData.ToBytes();
        }
        private byte[] GetMySchool(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            long account = data.ToLong();
            SchoolPairData schoolPairData = MySqlTools.GetSchoolPairData(account);
            long schoolCode = 0;
            if (schoolPairData != null)
            {
                schoolCode = schoolPairData.schoolCode;
            }
            return schoolCode.ToBytes();
        }

        private byte[] GetUserData(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return SocketTools.GetNullParamError();
            data = SocketTools.VarifyClientData(data);
            if (data.IsNullOrEmpty())  return SocketTools.ToBytes( SocketResultCode.Error,"用户Token失效",null);
            long account = data.ToLong();
            UserData userData = MySqlTools.GetUserDataByAccount(account);
            if (userData == null) return SocketTools.ToBytes(SocketResultCode.Error, "未找到对应的账号信息", null);
            return SocketTools.ToBytes(SocketResultCode.Success, null, userData.ToBytes());
        }
        private byte[] HeartBeat(byte[] data, Client endPoint)
        {
            return BytesConst.TRUE_BYTES;
        }

        private byte[] RegisterAccount(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            IListData<byte[]> item = data.ToListBytes();
            long account = item[0].ToLong();
            string userName = item[1].ToStr();
            string password = item[2].ToStr();
            byte code = MySqlTools.RegisterAccount(account, userName, password);
            item.Recycle();
            return code.ToBytes();
        }

        private byte[] LoginAccount(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty()) return SocketTools.GetNullParamError();
            IListData<byte[]> item = data.ToListBytes();
            long account = item[0].ToLong();
            string password = item[1].ToStr();
            item.Recycle();
            bool isExist = MySqlTools.ConfineAccount(account.ToString(), password);
            if (isExist)
            {
                UserData userData = MySqlTools.GetUserDataByAccount(account);
                long token = TokenManager.Instance.CreateToken();
                endPoint.Token = token;
                return SocketTools.ToBytes(SocketResultCode.Success, null, ByteTools.Concat(token.ToBytes(), userData.ToBytes()));
            }
            else
            {
                return SocketTools.ToBytes(101, "账号或密码错误", null);
            }
        }
    }
}
