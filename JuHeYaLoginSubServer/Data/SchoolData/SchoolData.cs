using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    public class SchoolData : BaseMySqlReflection
    {
        public int schoolID;
        public string name;
        public long code;//学校编码

        public override void Recycle()
        {
            ClassPool<SchoolData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            schoolID = reader.GetValue(0).ToInt();
            name = reader.GetValue(1).ToString();
            code = reader.GetValue(2).ToLong();
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> list = ClassPool<ListData<byte[]>>.Pop();
            list.Add(schoolID.ToBytes());
            list.Add(name.ToBytes());
            list.Add(code.ToBytes());
            byte[] bytes = list.list.ToBytes();
            list.Recycle();
            return bytes;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> list = data.ToListBytes();
            schoolID = list[0].ToInt();
            name = list[1].ToString();
            code = list[2].ToLong();
            list.Recycle();
        }
    }
}
