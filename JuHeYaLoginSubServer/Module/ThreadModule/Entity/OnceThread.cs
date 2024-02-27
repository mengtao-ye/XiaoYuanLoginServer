using System.Threading;
using YSF;

namespace SubServer
{
    /// <summary>
    /// 只执行一次
    /// </summary>
    public class OnceThread : BaseThread
    {



        public override void Recycle()
        {
            ClassPool<OnceThread>.Push(this);
        }
    }
}
