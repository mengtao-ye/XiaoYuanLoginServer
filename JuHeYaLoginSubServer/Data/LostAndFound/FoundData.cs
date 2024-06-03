using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    /// <summary>
    /// 失物招领信息
    /// </summary>
    public class FoundData : BaseMySqlReflection
    {
        public int id;
        public long account;
        public string name;
        public string pos;
        public long time;
        public byte[] images;
        public long schoolCode;
        public long updateTime;
        public string detail;
        public byte contactType;
        public string contact;
        public string quest;
        public string result;

        public override void Recycle()
        {
            ClassPool<FoundData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            id = reader.GetInt32(0);
            account = reader.GetInt64(1);
            name = reader.GetString(2);
            pos = reader.GetString(3);
            time = reader.GetInt64(4);
            images = SelectImageDataTools.GetBytes(reader.GetString(5));
            schoolCode = reader.GetInt64(6);
            updateTime = reader.GetInt64(7);
            detail = reader.GetString(8);
            contactType = reader.GetByte(9);
            contact = reader.GetString(10);
            quest = reader.GetString(11);
            result = reader.GetString(12);
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> bytes = ClassPool<ListData<byte[]>>.Pop();
            bytes.Add(id.ToBytes());
            bytes.Add(account.ToBytes());
            bytes.Add(name.ToBytes());
            bytes.Add(pos.ToBytes());
            bytes.Add(time.ToBytes());
            bytes.Add(images);
            bytes.Add(schoolCode.ToBytes());
            bytes.Add(updateTime.ToBytes());
            bytes.Add(detail.ToBytes());
            bytes.Add(contactType.ToBytes());
            bytes.Add(contact.ToBytes());
            bytes.Add(quest.ToBytes());
            bytes.Add(result.ToBytes());
            byte[] mDatas = bytes.list.ToBytes();
            bytes.Recycle();
            return mDatas;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> bytes = data.ToListBytes();
            id = bytes[0].ToInt();
            account = bytes[1].ToLong();
            name = bytes[2].ToStr();
            pos = bytes[3].ToStr();
            time = bytes[4].ToLong();
            images = bytes[5];
            schoolCode = bytes[6].ToLong();
            updateTime = bytes[7].ToLong();
            detail = bytes[8].ToStr();
            contactType = bytes[9].ToByte();
            contact = bytes[10].ToStr();
            quest = bytes[11].ToStr();
            result = bytes[12].ToStr();
            bytes.Recycle();
        }
    }
}
