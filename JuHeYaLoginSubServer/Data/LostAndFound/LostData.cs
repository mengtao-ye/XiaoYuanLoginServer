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
        public string images;

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
            images = reader.GetString(6);
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> bytes = ClassPool<ListData<byte[]>>.Pop();
            bytes.Add(id.ToBytes());
            bytes.Add(name.ToBytes());
            bytes.Add(pos.ToBytes());
            bytes.Add(startTime.ToBytes());
            bytes.Add(endTime.ToBytes());
            bytes.Add(images.ToBytes());
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
            images = bytes[5].ToStr();
            bytes.Recycle();
        }
    }
}
