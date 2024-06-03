using YSF;

namespace SubServer
{
    /// <summary>
    ///注册 登录的分布式服务器
    /// </summary>
    public class RegisterTcpLoginSubServerController : BaseController
    {
        public override ControllerType controllerType => ControllerType.RegisterTcpLoginSubServer;
        public RegisterTcpLoginSubServerController(IControllerManager controllerManager) : base(controllerManager)
        {

        }
        public override void Awake()
        {
            byte[] sendBytes = ByteTools.ConcatParam(new byte[] { (byte)SubServerType.LoginServer },EndPointTools.IPAddresToBytes(ServerData.PublicIPAddress),ServerData.TcpServerPort.ToBytes());
            mControllerManager.center.UdpSendCenterServer((short)LoginUdpCode.TcpLoginSubServerRegister, sendBytes);
        }
    }
}
