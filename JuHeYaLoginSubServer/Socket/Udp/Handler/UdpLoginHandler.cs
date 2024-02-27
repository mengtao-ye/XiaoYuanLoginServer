using System.Net;
using YSF;

namespace SubServer
{
    public class UdpLoginHandler : BaseUdpRequestHandle
    {
        public override short requestCode => (short)LoginRequestCode.Login;
        public UdpLoginHandler(IUdpServer server) : base(server)
        {

        }
        protected override void ComfigActionCode()
        {
            Add((short)LoginUdpCode.LoginAccount, LoginAccount);
            Add((short)LoginUdpCode.HeartBeat, HeartBeat);
            Add((short)LoginUdpCode.RegisterAccount, RegisterAccount);
            Add((short)LoginUdpCode.GetUserData, GetUserData);
        }
        private byte[] GetUserData(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            int account = data.ToInt();
            UserData userData = MySqlTools.GetUserDataByAccount(account);
            if (userData == null) return null;
            return userData.ToBytes();
        }
        private byte[] HeartBeat(byte[] data, EndPoint endPoint)
        {
            return BytesConst.TRUE_BYTES;
        }

        private byte[] RegisterAccount(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            IListData< byte[]> item = data.ToListBytes();
            long account = item[0].ToLong();
            string userName = item[1].ToStr();
            string password = item[2].ToStr();
            byte code = MySqlTools.RegisterAccount(account, userName, password);
            item.Recycle();
            return code.ToBytes();
        }

        private byte[] LoginAccount(byte[] data, EndPoint endPoint)
        {
            if (data.IsNullOrEmpty()) return null;
            IListData<byte[]> item = data.ToListBytes();
            long account = item[0].ToLong();
            string password = item[1].ToStr();
            item.Recycle();
            IDictionaryData<byte, byte[]> tempReturnDict = ClassPool<DictionaryData<byte, byte[]>>.Pop();
            byte loginRes = 0;// 0 为失败 1为成功  2为账号或密码错误  
            bool isExist = MySqlTools.ConfineAccount(account.ToString(), password);
            if (isExist)
            {
                loginRes = 1;
                UserData userData = MySqlTools.GetUserDataByAccount(account);
                tempReturnDict.Add(1, userData.ToBytes());
                userData.Recycle();
            }
            else
            {
                loginRes = 2;
            }
            tempReturnDict.Add(0, loginRes.ToBytes());
            byte[] returnBytes = tempReturnDict.data.ToBytes();
            tempReturnDict.Recycle();
            return returnBytes;
        }
    }
}
