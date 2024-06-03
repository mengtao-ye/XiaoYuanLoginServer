using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    public class CampusCircleCommitData : BaseMySqlReflection
    {
        public long ID;
        public long Account;
        public long CampusCircleID;
        public string Content;
        public int ReplayCount;//回复数量
        public override void Recycle()
        {
            ClassPool<CampusCircleCommitData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            ID = reader.GetInt64(0);
            Account = reader.GetInt64(1);
            CampusCircleID = reader.GetInt64(2);
            Content = reader.GetString(3);
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> bytes = ClassPool<ListData<byte[]>>.Pop();
            bytes.Add(ID.ToBytes());
            bytes.Add(Account.ToBytes());
            bytes.Add(CampusCircleID.ToBytes());
            bytes.Add(Content.ToBytes());
            bytes.Add(ReplayCount.ToBytes());
            byte[] returnBytes = bytes.list.ToBytes();
            bytes.Recycle();
            return returnBytes;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> bytes = data.ToListBytes();
            ID = bytes[0].ToLong();
            Account = bytes[1].ToLong();
            CampusCircleID = bytes[2].ToLong();
            Content = bytes[3].ToStr();
            ReplayCount = bytes[4].ToInt();
            bytes.Recycle();
        }
    }
}
