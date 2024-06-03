using YSF;

namespace SubServer
{
    public class UdpHandlerMapper : BaseMap<short, IUdpRequestHandle>
    {
        protected override void Config()
        {
           
        }
        public void Init() 
        {
            Add((short)LoginRequestCode.SubServer, new UdpSubServerHandler(MainCenter.Instance.udpServer));
            //Add((short)LoginRequestCode.Login, new UdpLoginHandler(MainCenter.Instance.udpServer));
        }
    }
}
