﻿using MySql.Data.MySqlClient;
using YSF;

namespace SubServer
{
    public class UserData : BaseMySqlReflection
    {
        public int ID;
        public long Account;
        public string Username;
        public string Password;
        public bool IsSetHead;
        public string IDCard;

        public override void Recycle()
        {
            ClassPool<UserData>.Push(this);
        }

        public override void ReflectionMySQLData(MySqlDataReader reader)
        {
            ID = reader.GetValue(0).ToInt();
            Account = reader.GetValue(1).ToLong();
            Username = reader.GetValue(2).ToString();
            Password = reader.GetValue(3).ToString();
            IsSetHead = reader.GetBoolean(4);
            IDCard = reader.GetValue(5).ToString();
        }

        public override byte[] ToBytes()
        {
            IListData<byte[]> bytes = ClassPool<ListData<byte[]>>.Pop();
            bytes.Add(ID.ToBytes());
            bytes.Add(Account.ToBytes());
            bytes.Add(Username.ToBytes());
            bytes.Add(Password.ToBytes());
            bytes.Add(IsSetHead.ToBytes());
            bytes.Add(IDCard.ToBytes());
            byte[] returnBytes = bytes.list.ToBytes();
            bytes.Recycle();
            return returnBytes;
        }

        public override void ToValue(byte[] data)
        {
            IListData<byte[]> bytes = data.ToListBytes();
            ID = bytes[0].ToInt();
            Account = bytes[1].ToLong();
            Username = bytes[2].ToStr();
            Password = bytes[3].ToStr();
            IsSetHead = bytes[4].ToBool();
            IDCard = bytes[5].ToString();
            bytes.Recycle();
        }
    }
}
