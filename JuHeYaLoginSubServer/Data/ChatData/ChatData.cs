using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    /// <summary>
    /// 聊天信息
    /// </summary>
    public class ChatData : BaseMySqlReflection
    {
        public long id;
        public long send_userid;
        public long receive_userid;
        public int msg_type;
        public string chat_msg;
        public long time;


        public override void Recycle()
        {
            ClassPool<ChatData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            id = reader.GetInt64(0);
            send_userid = reader.GetInt64(1);
            receive_userid = reader.GetInt64(2);
            msg_type = reader.GetInt32(3);
            chat_msg = reader.GetString(4);
            time = reader.GetInt64(5);
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> bytes = ClassPool<ListData<byte[]>>.Pop();
            bytes.Add(id.ToBytes());
            bytes.Add(send_userid.ToBytes());
            bytes.Add(receive_userid.ToBytes());
            bytes.Add(msg_type.ToBytes());
            bytes.Add(chat_msg.ToBytes());
            bytes.Add(time.ToBytes());
            byte[] mDatas = bytes.list.ToBytes();
            bytes.Recycle();
            return mDatas;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> bytes = data.ToListBytes();
            id = bytes[0].ToLong();
            send_userid = bytes[1].ToLong();
            receive_userid = bytes[2].ToLong();
            msg_type = bytes[3].ToByte();
            chat_msg = bytes[4].ToStr();
            time = bytes[5].ToLong();
            bytes.Recycle();
        }
    }
}
