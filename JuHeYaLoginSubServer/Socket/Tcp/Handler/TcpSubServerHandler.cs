using System.Net;
using YSF;

namespace SubServer
{
    public class TcpSubServerHandler : BaseTcpRequestHandle
    {
        protected override short mRequestCode => (short)TcpLoginRequestCode.SubServer;
        public TcpSubServerHandler(ITcpServer server) : base(server)
        {

        }
        protected override void ComfigActionCode()
        {
            Add((short)LoginUdpCode.TcpLoginSubServerRegister, LoginSubServerRegister);
            Add((short)LoginUdpCode.TcpLoginSubServerHeartBeat, LoginSubServerHeartBeat);
        }
        private byte[] LoginSubServerHeartBeat(byte[] data, Client endPoint)
        {
            return null;
        }
        private byte[] LoginSubServerRegister(byte[] data, Client endPoint)
        {
            if (data.IsNullOrEmpty() || data.Length != 4) 
            {
                Debug.LogError("LoginSubServerRegister data error");
                return null;
            }
            int id = data.ToInt();
            if (id == 0) {
                Debug.LogError("LoginSubServerRegister data is zero");
                return null;
            }
            SystemData.SubServerID = id;
            SystemData.SubServerIDBytes = id.ToBytes();
            Debug.Log("分布式服务器注册成功："+id);
            MainCenter.Instance.controllerManager.Remove(ControllerType.RegisterTcpLoginSubServer);
            MainCenter.Instance.controllerManager.Add(new TcpLoginSubServerHeartBeatController(MainCenter.Instance.controllerManager));
            return null;
        }
    }
}
