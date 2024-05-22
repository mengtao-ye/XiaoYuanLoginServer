using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    /// <summary>
    /// 失物招领信息
    /// </summary>
    public class LostData : BaseMySqlReflection
    {
        public int id;
        public string name;
        public string pos;
        public long startTime;
        public long endTime;
        public long account;
        public byte[] images;
        public long schoolCode;
        public long updateTime;
        public string detail;
        public byte contactType;
        public string contact;

        public override void Recycle()
        {
            ClassPool<LostData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            id = reader.GetInt32(0);
            name = reader.GetString(1);
            pos = reader.GetString(2);
            startTime = reader.GetInt64(3);
            endTime = reader.GetInt64(4);
            account = reader.GetInt64(5);
            images = SelectImageDataTools.GetBytes(reader.GetString(6));
            schoolCode = reader.GetInt64(7);
            updateTime = reader.GetInt64(8);
            detail = reader.GetString(9);
            contactType = reader.GetByte(10);
            contact = reader.GetString(11);
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> bytes = ClassPool<ListData<byte[]>>.Pop();
            bytes.Add(id.ToBytes());
            bytes.Add(name.ToBytes());
            bytes.Add(pos.ToBytes());
            bytes.Add(startTime.ToBytes());
            bytes.Add(endTime.ToBytes());
            bytes.Add(account.ToBytes());
            bytes.Add(images);
            bytes.Add(schoolCode.ToBytes());
            bytes.Add(updateTime.ToBytes());
            bytes.Add(detail.ToBytes());
            bytes.Add(contactType.ToBytes());
            bytes.Add(contact.ToBytes());
            byte[] mDatas = bytes.list.ToBytes();
            bytes.Recycle();
            return mDatas;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> bytes = data.ToListBytes();
            id = bytes[0].ToInt();
            name = bytes[1].ToStr();
            pos = bytes[2].ToStr();
            startTime = bytes[3].ToLong();
            endTime = bytes[4].ToLong();
            account = bytes[5].ToLong();
            images = bytes[6];
            schoolCode = bytes[7].ToLong();
            updateTime = bytes[8].ToLong();
            detail = bytes[9].ToStr();
            contactType = bytes[10].ToByte();
            contact = bytes[11].ToStr();
            bytes.Recycle();
        }
    }
}
