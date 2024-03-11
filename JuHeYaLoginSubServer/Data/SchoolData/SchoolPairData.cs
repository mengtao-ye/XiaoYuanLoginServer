using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    public class SchoolPairData : BaseMySqlReflection
    {
        public long account;
        public int schoolID;

        public override void Recycle()
        {
            ClassPool<SchoolPairData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            account = reader.GetValue(0).ToLong();
            schoolID = reader.GetValue(1).ToInt();
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> list = ClassPool<ListData<byte[]>>.Pop();
            list.Add(account.ToBytes());
            list.Add(schoolID.ToBytes());
            byte[] bytes = list.list.ToBytes();
            list.Recycle();
            return bytes;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> list = data.ToListBytes();
            account = list[0].ToLong();
            schoolID = list[1].ToInt();
            list.Recycle();
        }
    }
}
