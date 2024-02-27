using System;
using System.Threading;

namespace SubServer
{
    public abstract class BaseThread : IThread
    {
        private Thread mThread;
        public bool isPop { get; set; }
        public void Init(ThreadStart action)
        {
            mThread = new Thread(action);
        }
        /// <summary>
        /// 执行线程
        /// </summary>
        public void Start()
        {
            if (mThread == null) return;
            mThread.Start();
        }
        public virtual void PopPool() { }
        public virtual void PushPool(){ }
        public abstract void Recycle();
    }
}
