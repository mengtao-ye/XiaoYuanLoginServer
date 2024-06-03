﻿using YSF;

namespace SubServer
{
    /// <summary>
    /// 登录分布式服务器心跳包
    /// </summary>
    public class TcpLoginSubServerHeartBeatController : BaseController
    {
        private float mTimer;
        private float mTime;
        public override ControllerType controllerType => ControllerType.TcpLoginSubServerHeartBeat;
        public TcpLoginSubServerHeartBeatController(IControllerManager controllerManager) : base(controllerManager)
        {

        }
        public override void Awake()
        {
            mTime = 1;
        }
        public override void Update()
        {
            mTimer += Time.DeltaTime;
            if (mTimer > mTime)
            {
                mTimer = 0;
                mControllerManager.center.UdpSendCenterServer((short)LoginUdpCode.TcpLoginSubServerHeartBeat, SystemData.SubServerIDBytes);
            }
        }
    }
}
