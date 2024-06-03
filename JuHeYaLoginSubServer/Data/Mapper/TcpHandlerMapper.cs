using YSF;

namespace SubServer
{
    public class TcpHandlerMapper : BaseMap<short, ITCPRequestHandle>
    {
        protected override void Config()
        {
           
        }
        public void Init() 
        {
            Add((short)TcpLoginRequestCode.SubServer, new TcpSubServerHandler(MainCenter.Instance.tcpServer));
            Add((short)TcpLoginRequestCode.Login, new TcpLoginHandler(MainCenter.Instance.tcpServer));
        }
    }
}
