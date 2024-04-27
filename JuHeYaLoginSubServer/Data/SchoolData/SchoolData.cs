using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    public class SchoolData : BaseMySqlReflection
    {
        public int schoolID;
        public string name;
        public long code;//学校编码
        public string assetBundleName;//校园场景名称

        public override void Recycle()
        {
            ClassPool<SchoolData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            schoolID = reader.GetInt32(0);
            name = reader.GetString(1);
            code = reader.GetInt64(2);
            assetBundleName = reader.GetString(3);
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> list = ClassPool<ListData<byte[]>>.Pop();
            list.Add(schoolID.ToBytes());
            list.Add(name.ToBytes());
            list.Add(code.ToBytes());
            list.Add(assetBundleName.ToBytes());
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
            assetBundleName = list[3].ToStr();
            list.Recycle();
        }
    }
}
