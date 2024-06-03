using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    public class CampusCircleReplayCommitData : BaseMySqlReflection
    {
        public long ID;
        public long ReplayCommitID;//回复的评论ID
        public long Account;
        public string Content;
        public long ReplayID;//回复 回复 评论的ID
        public string ReplayContent = string.Empty;//回复 回复 评论的内容
        public override void Recycle()
        {
            ReplayContent = string.Empty;
            ReplayID = 0;
            Content = string.Empty;
            Account = 0;
            ReplayCommitID = 0;
            ID = 0;
            ClassPool<CampusCircleReplayCommitData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            ID = reader.GetInt64(0);
            ReplayCommitID = reader.GetInt64(1);
            Account = reader.GetInt64(2);
            Content = reader.GetString(3);
            ReplayID = reader.GetInt64(4);

        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> bytes = ClassPool<ListData<byte[]>>.Pop();
            bytes.Add(ID.ToBytes());
            bytes.Add(ReplayCommitID.ToBytes());
            bytes.Add(Account.ToBytes());
            bytes.Add(Content.ToBytes());
            bytes.Add(ReplayID.ToBytes());
            bytes.Add(ReplayContent.ToBytes());
            byte[] returnBytes = bytes.list.ToBytes();
            bytes.Recycle();
            return returnBytes;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> bytes = data.ToListBytes();
            ID = bytes[0].ToInt();
            ReplayCommitID = bytes[1].ToLong();
            Account = bytes[2].ToLong();
            Content = bytes[3].ToStr();
            ReplayID = bytes[4].ToLong();
            ReplayContent = bytes[5].ToStr();
            bytes.Recycle();
        }
    }
}
