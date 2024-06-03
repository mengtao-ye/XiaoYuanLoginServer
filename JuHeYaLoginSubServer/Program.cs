using System;
using YSF;

namespace SubServer
{
    class Program
    {
        static void Main(string[] args)
        {
            MainCenter center = new MainCenter();
            int tcpPoint = GetPoint(50101,"Tcp");
            int udpPoint = GetPoint(50100,"Udp");
            ServerData.TcpServerPort = tcpPoint;
            ServerData.UdpServerPort = udpPoint;
            center.InitCenterPoint(ServerData.IPAddress, 50000);
            center.LauncherUdpServer(ServerData.IPAddress, ServerData.UdpServerPort);
            center.LauncherTcpServer(ServerData.IPAddress, ServerData.TcpServerPort);
            center.LauncherController();
            MySQLManager mySQLManager = center.LauncherMySQLManager("127.0.0.1", "xiaoyuan", "root", "528099tt...");
            if (mySQLManager != null)
            {
                MySqlTools.InitMySQLManager(mySQLManager);
            }
            while (true)
            {
                Console.ReadLine();
            }
        }
        /// <summary>
        /// 获取Point数据
        /// </summary>
        /// <param name="defaultPoint"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static int GetPoint(int defaultPoint,string name)
        {
            int Point = defaultPoint;
            if (ServerData.serverEnv == ServerEnvType.Test)
            {
                Point = defaultPoint;
            }
            else
            {
                Debug.Log("请输入绑定的"+ name + "端口号码");
                string point = Console.ReadLine();
                Point = LoginServerTools.ParsePoint(point);
                if (Point == -1)
                {
                    Debug.LogError(name+"端口号错误");
                    return Point;
                }
            }
            return Point;
        }
     
    }
}
