using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    public class PartTimeJobData : BaseMySqlReflection
    {
        public int ID;
        public long Account;
        public string Title;
        public int Price;
        public byte PriceType;
        public string JobTime;
        public string Position;
        public string Detail;

        public override void Recycle()
        {
            ClassPool<PartTimeJobData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            ID = reader.GetInt32(0);
            Account = reader.GetInt64(1);
            Title = reader.GetString(2);
            Price = reader.GetInt32(3);
            PriceType = reader.GetByte(4);
            JobTime = reader.GetString(5);
            Position = reader.GetString(6);
            Detail = reader.GetString(7);
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> bytes = ClassPool<ListData<byte[]>>.Pop();
            bytes.Add(ID.ToBytes());
            bytes.Add(Account.ToBytes());
            bytes.Add(Title.ToBytes());
            bytes.Add(Price.ToBytes());
            bytes.Add(PriceType.ToBytes());
            bytes.Add(JobTime.ToBytes());
            bytes.Add(Position.ToBytes());
            bytes.Add(Detail.ToBytes());
            byte[] returnBytes = bytes.list.ToBytes();
            bytes.Recycle();
            return returnBytes;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> bytes = data.ToListBytes();
            ID = bytes[0].ToInt();
            Account = bytes[1].ToLong();
            Title = bytes[2].ToStr();
            Price = bytes[3].ToInt();
            PriceType = bytes[4].ToByte();
            JobTime = bytes[5].ToStr();
            Position = bytes[6].ToStr();
            Detail = bytes[7].ToStr();
            bytes.Recycle();
        }
    }
}
