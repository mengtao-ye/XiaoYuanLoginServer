using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    public class MyMetaSchoolData : BaseMySqlReflection
    {
        public long Account;
        public byte RoleID;
        public override void Recycle()
        {
            ClassPool<MyMetaSchoolData>.Push(this);
        }
        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            Account = reader.GetInt64(0);
            RoleID = reader.GetByte(1);
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> bytes = ClassPool<ListData<byte[]>>.Pop();
            bytes.Add(Account.ToBytes());
            bytes.Add(RoleID.ToBytes());
            byte[] returnBytes = bytes.list.ToBytes();
            bytes.Recycle();
            return returnBytes;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> bytes = data.ToListBytes();
            Account = bytes[0].ToLong();
            RoleID = bytes[1].ToByte();
            bytes.Recycle();
        }
    }
}
