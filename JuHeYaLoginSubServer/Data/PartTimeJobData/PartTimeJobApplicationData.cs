using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    public class PartTimeJobApplicationData : BaseMySqlReflection
    {
        public int id;
        public long account;
        public int partTimeJobID;
        public string name;
        public bool isMan;
        public int age;
        public string call;

        public override void Recycle()
        {
            ClassPool<PartTimeJobApplicationData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            id = reader.GetInt32(0);
            account = reader.GetInt64(1);
            partTimeJobID = reader.GetInt32(2);
            name = reader.GetString(3);
            isMan = reader.GetBoolean(4);
            age = reader.GetInt32(5);
            call = reader.GetString(6);
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> bytes = ClassPool<ListData<byte[]>>.Pop();
            bytes.Add(id.ToBytes());
            bytes.Add(account.ToBytes());
            bytes.Add(partTimeJobID.ToBytes());
            bytes.Add(name.ToBytes());
            bytes.Add(isMan.ToBytes());
            bytes.Add(age.ToBytes());
            bytes.Add(call.ToBytes());
            byte[] returnBytes = bytes.list.ToBytes();
            bytes.Recycle();
            return returnBytes;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> bytes = data.ToListBytes();
            id = bytes[0].ToInt();
            account = bytes[1].ToLong();
            partTimeJobID = bytes[2].ToInt();
            name = bytes[3].ToStr();
            isMan = bytes[4].ToBool();
            age = bytes[5].ToInt();
            call = bytes[6].ToStr();
            bytes.Recycle();
        }
    }
}
