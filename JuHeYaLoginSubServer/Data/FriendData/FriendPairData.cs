using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    /// <summary>
    /// 好友列表
    /// </summary>
    public class FriendPairData : BaseMySqlReflection
    {
        public int id;
        public long friendAccount;
        public string notes;

        public override void Recycle()
        {
            ClassPool<FriendPairData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            id = reader.GetValue(0).ToInt();
            friendAccount = reader.GetValue(2).ToLong();
            notes = reader.GetValue(3).ToString();
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> bytes = ClassPool<ListData<byte[]>>.Pop();
            bytes.Add(id.ToBytes());
            bytes.Add(friendAccount.ToBytes());
            bytes.Add(notes.ToBytes());
            byte[] mDatas = bytes.list.ToBytes();
            bytes.Recycle();
            return mDatas;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> bytes = data.ToListBytes();
            id = bytes[0].ToInt();
            friendAccount = bytes[1].ToLong();
            notes = bytes[2].ToStr();
            bytes.Recycle();
        }
    }
}
