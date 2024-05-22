using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    public class CampusCircleData : BaseMySqlReflection
    {
        public int ID;
        public long Account;
        public string Content;
        public byte[] Images;
        public long SchoolID;
        public long Time;
        public bool IsAnonymous;//是否是匿名
        public int LikeCount;
        public int CommitCount;
        public override void Recycle()
        {
            ClassPool<CampusCircleData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            ID = reader.GetInt32(0);
            Account = reader.GetInt64(1);
            Content = reader.GetString(2);
            string images = reader.GetString(3);
            Images = SelectImageDataTools.GetBytes(images);
            SchoolID = reader.GetInt64(4);
            Time = reader.GetInt64(5);
            IsAnonymous = reader.GetBoolean(6);
            LikeCount = reader.GetInt32(7);
            CommitCount = reader.GetInt32(8);
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> bytes = ClassPool<ListData<byte[]>>.Pop();
            bytes.Add(ID.ToBytes());
            bytes.Add(Account.ToBytes());
            bytes.Add(Content.ToBytes());
            bytes.Add(Images);
            bytes.Add(SchoolID.ToBytes());
            bytes.Add(Time.ToBytes());
            bytes.Add(IsAnonymous.ToBytes());
            bytes.Add(LikeCount.ToBytes());
            bytes.Add(CommitCount.ToBytes());
            byte[] returnBytes = bytes.list.ToBytes();
            bytes.Recycle();
            return returnBytes;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> bytes = data.ToListBytes();
            ID = bytes[0].ToInt();
            Account = bytes[1].ToLong();
            Content = bytes[2].ToStr();
            Images = bytes[3];
            SchoolID = bytes[4].ToLong();
            Time = bytes[5].ToLong();
            IsAnonymous = bytes[6].ToBool();
            LikeCount = bytes[7].ToInt();
            CommitCount = bytes[8].ToInt();
            bytes.Recycle();
        }
    }
}
