using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    public class UnuseData : BaseMySqlReflection
    {
        public int id;
        public long account;
        public long time;
        public string content;
        public byte[] images;
        public byte type;
        public int price;

        public override void Recycle()
        {
            ClassPool<UnuseData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            id = reader.GetInt32(0);
            account = reader.GetInt64(1);
            time = reader.GetInt64(2);
            content = reader.GetString(3);
            string images = reader.GetString(4);
            this.images = SelectImageDataTools.GetBytes(images);
            type = reader.GetByte(5);
            price = reader.GetInt32(6);
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> bytes = ClassPool<ListData<byte[]>>.Pop();
            bytes.Add(id.ToBytes());
            bytes.Add(account.ToBytes());
            bytes.Add(time.ToBytes());
            bytes.Add(content.ToBytes());
            bytes.Add(images);
            bytes.Add(type.ToBytes());
            bytes.Add(price.ToBytes());
            byte[] returnBytes = bytes.list.ToBytes();
            bytes.Recycle();
            return returnBytes;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> bytes = data.ToListBytes();
            id = bytes[0].ToInt();
            account = bytes[1].ToLong();
            time = bytes[2].ToLong();
            content = bytes[3].ToStr();
            images = bytes[4];
            type = bytes[5].ToByte();
            price = bytes[6].ToInt();
            bytes.Recycle();
        }
    }
}
