using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    /// <summary>
    /// 添加好友申请列表
    /// </summary>
    public class AddFriendRequestData : BaseMySqlReflection
    {
        public int id;
        public long friendAccount;
        public string addContent;

        public override void Recycle()
        {
            ClassPool<AddFriendRequestData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            id = reader.GetValue(0).ToInt();
            friendAccount = reader.GetValue(1).ToLong();
            addContent = reader.GetValue(3).ToString();
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> bytes = ClassPool<ListData<byte[]>>.Pop();
            bytes.Add(id.ToBytes());
            bytes.Add(friendAccount.ToBytes());
            bytes.Add(addContent.ToBytes());
            byte[] mDatas = bytes.list.ToBytes();
            bytes.Recycle();
            return mDatas;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> bytes = data.ToListBytes();
            id = bytes[0].ToInt();
            friendAccount = bytes[1].ToLong();
            addContent = bytes[2].ToStr();
            bytes.Recycle();
        }
    }
}
