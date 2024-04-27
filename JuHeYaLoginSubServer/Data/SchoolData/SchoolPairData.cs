using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    public class SchoolPairData : BaseMySqlReflection
    {
        public long account;
        public long schoolCode;

        public override void Recycle()
        {
            ClassPool<SchoolPairData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            account = reader.GetInt64(0);
            schoolCode = reader.GetInt64(1);
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> list = ClassPool<ListData<byte[]>>.Pop();
            list.Add(account.ToBytes());
            list.Add(schoolCode.ToBytes());
            byte[] bytes = list.list.ToBytes();
            list.Recycle();
            return bytes;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> list = data.ToListBytes();
            account = list[0].ToLong();
            schoolCode = list[1].ToLong();
            list.Recycle();
        }
    }
}
