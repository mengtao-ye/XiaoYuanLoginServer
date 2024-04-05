using System;
using System.Net;
using YSF;

namespace SubServer
{
    public class UdpLoginHandler : BaseUdpRequestHandle
    {
        public override short requestCode => (short)LoginRequestCode.Login;
        public UdpLoginHandler(IUdpServer server) : base(server)
        {

        }
        protected override void ComfigActionCode()
        {
            Add((short)LoginUdpCode.LoginAccount, LoginAccount);
            Add((short)LoginUdpCode.HeartBeat, HeartBeat);
            Add((short)LoginUdpCode.RegisterAccount, RegisterAccount);
            Add((short)LoginUdpCode.GetUserData, GetUserData);
            Add((short)LoginUdpCode.GetMySchool, GetMySchool);
            Add((short)LoginUdpCode.GetSchoolData, GetSchoolData);
            Add((short)LoginUdpCode.SearchSchool, SearchSchool);
            Add((short)LoginUdpCode.JoinSchool, JoinSchool);
            Add((short)LoginUdpCode.GetNewChatMsg, GetNewChatMsg);
            Add((short)LoginUdpCode.GetFriendList, GetFriendList);
            Add((short)LoginUdpCode.SearchFriendData, SearchFriendData);
            Add((short)LoginUdpCode.SendAddFriendRequest, SendAddFriendRequest);
            Add((short)LoginUdpCode.GetAddFriendRequest, GetAddFriendRequest);
            Add((short)LoginUdpCode.RefuseFriend, RefuseFriend);
            Add((short)LoginUdpCode.ConfineFriend, ConfineFriend);
            Add((short)LoginUdpCode.SendChatMsg, SendChatMsg);
            Add((short)LoginUdpCode.PublishCampusCircle, PublishCampusCircle);
            Add((short)LoginUdpCode.GetCampusCircle, GetCampusCircle);
            Add((short)LoginUdpCode.GetCampusCircleItemDetail, GetCampusCircleItemDetail);
            Add((short)LoginUdpCode.LikeCampusCircleItem, LikeCampusCircleItem);
            Add((short)LoginUdpCode.HasLikeCampusCircleItem, HasLikeCampusCircleItem);
            Add((short)LoginUdpCode.GetCommit, GetCommit);
        }
        /// <summary>
        /// 获取评论信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetCommit(byte[] data, EndPoint endPoint)
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
        /// 朋友圈点赞
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] HasLikeCampusCircleItem(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            long campusCircleID = data.ToLong(8);
            bool res =  MySqlTools.HasLikeCampusCircleItem(account, campusCircleID);
            return ByteTools.Concat(campusCircleID.ToBytes(), res.ToBytes());
        }

        /// <summary>
        /// 朋友圈点赞
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] LikeCampusCircleItem(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = data.ToLong();
            long campusCircleID = data.ToLong(8);
            bool isLike = data.ToBool(16);
            MySqlTools.LikeCampusCircleItem(account, campusCircleID,isLike);
            return ByteTools.Concat(campusCircleID.ToBytes(),(!isLike).ToBytes()) ;
        }
        /// <summary>
        /// 获取校友圈对象详情
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetCampusCircleItemDetail(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long id = data.ToLong();
            CampusCircleData target = MySqlTools.GetCampusCircleItemDetail(id);
            if (target == null) return BytesConst.FALSE_BYTES;
            return target.ToBytes();
        }

        /// <summary>
        /// 获取校友圈信息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetCampusCircle(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IListData<byte[]> listData = data.ToListBytes();
            if (listData.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = listData[0].ToLong();
            long lastID = listData[1].ToLong();
            long schoolCode = listData[2].ToLong();
            listData.Recycle();
            IListData<long> list = MySqlTools.GetCampusCircle(account, lastID, schoolCode);
            if(list .IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
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
        private byte[] PublishCampusCircle(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            IListData<byte[]> listData = data.ToListBytes();
            if (listData.IsNullOrEmpty()) return BytesConst.FALSE_BYTES;
            long account = listData[0].ToLong();
            string content = listData[1].ToStr();
            string images  = listData[2].ToStr();
            bool isSchool = listData[3].ToBool();
            long time = DateTimeOffset.Now.ToUnixTimeSeconds();
            listData.Recycle();
            bool res = MySqlTools.PublishCampusCircle(account, content, images, isSchool, time);
            return res.ToBytes();
        }

        /// <summary>
        /// 同意好友申请
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] ConfineFriend(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            long account = data.ToLong();
            long friendAccount = data.ToLong(8);
            MySqlTools.DeleteAddFriendRequest(friendAccount, account);
            MySqlTools.AddFriendPair(account, friendAccount);
            MySqlTools.AddFriendPair(friendAccount, account);
            return friendAccount.ToBytes();
        }
        /// <summary>
        /// 拒绝好友申请
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] RefuseFriend(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            long account = data.ToLong();
            long friendAccount = data.ToLong(8);
            MySqlTools.DeleteAddFriendRequest(friendAccount, account);
            return friendAccount.ToBytes();
        }
        /// <summary>
        /// 获取好友申请列表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetAddFriendRequest(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            long account = data.ToLong();
            int lastAddFriendID = data.ToInt(8);
            IListData<AddFriendRequestData> listData = MySqlTools.GetAddFriendRequestList(account, lastAddFriendID);
            if (listData .IsNullOrEmpty()) return null;
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
        private byte[] SendAddFriendRequest(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            IListData<byte[]> lists = data.ToListBytes();
            long myAccount = lists[0].ToLong();
            long friendAccount = lists[1].ToLong();
            string addContent = lists[2].ToStr();
            lists.Recycle();
            byte returnCode = MySqlTools.AddFriendRequest(myAccount, friendAccount, addContent);
            MySqlTools.AddChatMsg(UserAccountConstData.NEW_FRIEND_ACCOUNT, friendAccount, (byte)ChatMsgType.NewFriend, "有新的朋友，请点击查看", DateTimeOffset.Now.ToUnixTimeSeconds());
            return returnCode.ToBytes();
        }

        /// <summary>
        /// 加入学校
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] SearchFriendData(byte[] data, EndPoint endPoint)
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
            return ByteTools.Concat(isFriend.ToBytes(),userData.Account.ToBytes());
        }
        /// <summary>
        /// 加入学校
        /// </summary>
        /// <param name="data"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private byte[] GetFriendList(byte[] data, EndPoint endPoint)
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
        private byte[] SendChatMsg(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            IListData<byte[]> list = data.ToListBytes();
            long sendAccount = list[0].ToLong();
            long receiveAccount = list[1].ToLong();
            byte msgType = list[2].ToByte();
            string content = list[3].ToStr();
            long time = list[4].ToLong();
            long id = 0;
            bool res  = MySqlTools.AddChatMsg(sendAccount, receiveAccount, msgType, content, time,out id);
            byte[] returnBytes = null;
            if (res)
            {
                list.Add(id.ToBytes());
                returnBytes = ByteTools.Concat(res.ToBytes(), list.list.ToBytes());
            }
            else 
            {
                returnBytes = res.ToBytes();
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
        private byte[] GetNewChatMsg(byte[] data, EndPoint endPoint)
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
        private byte[] JoinSchool(byte[] data, EndPoint endPoint)
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
        private byte[] SearchSchool(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            string name = data.ToStr();
            IListData<SchoolData> schoolDatas = MySqlTools.SearchSchoolsData(name);
            if (schoolDatas .IsNullOrEmpty()) return null;
            byte[] returnBytes = schoolDatas.list.ToBytes();
            schoolDatas.Recycle();
            return returnBytes;
        }
        private byte[] GetSchoolData(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            long schoolCode= data.ToLong();
            SchoolData schoolData = MySqlTools.GetSchoolData(schoolCode);
            if (schoolData == null) return null;
            return schoolData.ToBytes();
        }
        private byte[] GetMySchool(byte[] data, EndPoint endPoint)
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

        private byte[] GetUserData(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            long account = data.ToLong();
            UserData userData = MySqlTools.GetUserDataByAccount(account);
            if (userData == null) return null;
            return userData.ToBytes();
        }
        private byte[] HeartBeat(byte[] data, EndPoint endPoint)
        {
            return BytesConst.TRUE_BYTES;
        }

        private byte[] RegisterAccount(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            IListData< byte[]> item = data.ToListBytes();
            long account = item[0].ToLong();
            string userName = item[1].ToStr();
            string password = item[2].ToStr();
            byte code = MySqlTools.RegisterAccount(account, userName, password);
            item.Recycle();
            return code.ToBytes();
        }

        private byte[] LoginAccount(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            IListData<byte[]> item = data.ToListBytes();
            long account = item[0].ToLong();
            string password = item[1].ToStr();
            item.Recycle();
            IDictionaryData<byte, byte[]> tempReturnDict = ClassPool<DictionaryData<byte, byte[]>>.Pop();
            byte loginRes = 0;// 0 为失败 1为成功  2为账号或密码错误  
            bool isExist = MySqlTools.ConfineAccount(account.ToString(), password);
            if (isExist)
            {
                loginRes = 1;
                UserData userData = MySqlTools.GetUserDataByAccount(account);
                tempReturnDict.Add(1, userData.ToBytes());
               
            }
            else
            {
                loginRes = 2;
            }
            tempReturnDict.Add(0, loginRes.ToBytes());
            byte[] returnBytes = tempReturnDict.data.ToBytes();
            tempReturnDict.Recycle();
            return returnBytes;
        }
    }
}
