using System.Collections.Generic;
using System.Net;
using System.Threading;
using YSF;

namespace SubServer
{
    public class MainCenter 
    {
        public static MainCenter Instance { get; private set; }
        private ServerCenter mCenterServer;
        public ServerCenter centerServer { get { return mCenterServer; } }
        public UdpServer udpServer { get { return mCenterServer.udpServer; } }
        public TcpServer tcpServer { get { return mCenterServer.tcpServer; } }
        private IControllerManager mController;
        public IControllerManager controllerManager { get { return mController; } }
        private Thread mLifeThread;//生命周期线程
        private IList<ILife> mLifes;
        private IPEndPoint mCenterPoint;
        private MySQLManager mMySQLManager;
        public MySQLManager mySQLManager { get { return mMySQLManager; } }
        public MainCenter()
        {
            Instance = this;
            Init();
        }
        private void Init()
        {
            mLifes = new List<ILife>();
            mLifeThread = new Thread(Update);
            mLifeThread.Start();
            mCenterServer = new ServerCenter();
            mCenterServer.InitHelper(new XiaoYuanDataHelper());
           
        }

      

        private void Update()
        {
            while (true)
            {
                Thread.Sleep(Time.DeltaTimeMill);
                for (int i = 0; i < mLifes.Count; i++)
                {
                    mLifes[i].Update();
                }
            }
        }
        /// <summary>
        /// 启动MySQL管理器
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="databaseName"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        public MySQLManager LauncherMySQLManager(string ipAddress, string databaseName, string account, string password) {
            mMySQLManager = new MySQLManager();
            mMySQLManager.Launcher(ipAddress, databaseName, account, password);
            return mMySQLManager;
        }
        /// <summary>
        /// 启动控制器
        /// </summary>
        public void LauncherController()
        {
            mController = new ControllerManager(this);
            AddLife(mController);
        }
        /// <summary>
        /// 添加生命周期函数
        /// </summary>
        /// <param name="life"></param>
        public void AddLife(ILife life)
        {
            if (life == null)
            {
                Debug.LogError("add life is null");
                return;
            }
            if (mLifes.Contains(life))
            {
                Debug.LogError("add life is contains : " + life.ToString());
                return;
            }
            mLifes.Add(life);
            life.Awake();
            life.Start();
        }
        /// <summary>
        /// 启动Udp服务器服务
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public void LauncherUdpServer(string ipAddress, int port)
        {
            UdpHandlerMapper udpHandlerMapper = new UdpHandlerMapper();
            mCenterServer.LauncherUDPServer(ipAddress, port, udpHandlerMapper);
            udpHandlerMapper.Init();
        }
        /// <summary>
        /// 启动Tcp服务器服务
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public void LauncherTcpServer(string ipAddress, int port)
        {
            TcpHandlerMapper tcpHandlerMapper = new TcpHandlerMapper();
            mCenterServer.LauncherTCPServer(ipAddress, port, tcpHandlerMapper);
            tcpHandlerMapper.Init();
            mCenterServer.tcpServer.clientLostCallBack+=ClientLostCallback;
        }

        private void ClientLostCallback(Client client)
        {
            TokenManager.Instance.RemoveToken(client.Token);
        }
        /// <summary>
        /// 初始化中心服务器point
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="port"></param>
        public void InitCenterPoint(string ipAddress,int port) 
        {
            mCenterPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        }
        /// <summary>
        /// 向中心服务器发送消息
        /// </summary>
        /// <param name="udpCode"></param>
        /// <param name="data"></param>
        public void UdpSendCenterServer(short udpCode,byte[] data)
        {
            UdpSend(mCenterPoint,udpCode,data);
        }


        /// <summary>
        /// 向服务器发送消息
        /// </summary>
        /// <param name="udpCode"></param>
        /// <param name="data"></param>
        public void UdpSend(EndPoint point,short udpCode, byte[] data)
        {
            if (data.IsNullOrEmpty()) return;
            if (mCenterPoint == null) return;
            udpServer.UdpSend(point, udpCode, data);
        }
    }
}
