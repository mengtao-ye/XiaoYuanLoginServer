using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    public class CampusCircleCommitData : BaseMySqlReflection
    {
        public int ID;
        public long Account;
        public long CampusCircleID;
        public long ReplayID;
        public string Content;

        public override void Recycle()
        {
            ClassPool<CampusCircleCommitData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            ID = reader.GetValue(0).ToInt();
            Account = reader.GetValue(1).ToLong();
            CampusCircleID = reader.GetValue(2).ToLong();
            ReplayID = reader.GetValue(3).ToLong();
            Content = reader.GetValue(4).ToString();
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> bytes = ClassPool<ListData<byte[]>>.Pop();
            bytes.Add(ID.ToBytes());
            bytes.Add(Account.ToBytes());
            bytes.Add(CampusCircleID.ToBytes());
            bytes.Add(ReplayID.ToBytes());
            bytes.Add(Content.ToBytes());
            byte[] returnBytes = bytes.list.ToBytes();
            bytes.Recycle();
            return returnBytes;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> bytes = data.ToListBytes();
            ID = bytes[0].ToInt();
            Account = bytes[1].ToLong();
            CampusCircleID = bytes[2].ToLong();
            ReplayID = bytes[3].ToLong();
            Content = bytes[4].ToStr();
            bytes.Recycle();
        }
    }
}
