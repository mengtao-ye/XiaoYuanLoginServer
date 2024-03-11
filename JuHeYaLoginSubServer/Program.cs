using System;
using System.IO;
using YSF;

namespace SubServer
{
    class Program
    {
        static void Main(string[] args)
        {
            MainCenter center = new MainCenter();
            int udpPoint = 0;
            if (ServerData.serverEnv == ServerEnvType.Test)
            {
                udpPoint = 50002;
            }
            else
            {
                Debug.Log("请输入绑定的udp端口号码");
                string point = Console.ReadLine();
                udpPoint = LoginServerTools.ParsePoint(point);
                if (udpPoint == -1)
                {
                    Debug.LogError("UDP端口号错误");
                    return;
                }
            }
            ServerData.UdpServerPort = udpPoint;
            center.InitCenterPoint("127.0.0.1", 50000);
            center.LauncherServer(ServerData.IPAddress, ServerData.UdpServerPort);
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
     
    }
}
