using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    public class SchoolData : BaseMySqlReflection
    {
        public int schoolID;
        public string name;
        public string icon;
        public string bg;

        public override void Recycle()
        {
            ClassPool<SchoolData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            schoolID = reader.GetValue(0).ToInt();
            name = reader.GetValue(1).ToString();
            icon = reader.GetValue(2).ToString();
            bg = reader.GetValue(3).ToString();
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> list = ClassPool<ListData<byte[]>>.Pop();
            list.Add(schoolID.ToBytes());
            list.Add(name.ToBytes());
            list.Add(icon.ToBytes());
            list.Add(bg.ToBytes());
            byte[] bytes = list.list.ToBytes();
            list.Recycle();
            return bytes;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> list = data.ToListBytes();
            schoolID = list[0].ToInt();
            name = list[1].ToString();
            icon = list[2].ToString();
            bg = list[3].ToString();
            list.Recycle();
        }
    }
}
