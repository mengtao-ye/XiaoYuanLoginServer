using YSF;

namespace SubServer
{
    /// <summary>
    ///注册 登录的分布式服务器
    /// </summary>
    public class RegisterLoginSubServerController : BaseController
    {
        public override ControllerType controllerType => ControllerType.RegisterLoginSubServer;
        public RegisterLoginSubServerController(IControllerManager controllerManager) : base(controllerManager)
        {

        }
        public override void Awake()
        {
            mControllerManager.center.UdpSendCenterServer((short)LoginUdpCode.LoginSubServerRegister, new byte[] { (byte)SubServerType.LoginServer });
        }
    }
}
