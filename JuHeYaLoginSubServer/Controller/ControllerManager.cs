using System.Collections.Generic;
using YSF;

namespace SubServer
{
    public class ControllerManager : IControllerManager
    {
        private IList<IController> mControllerList;
        public MainCenter center { get; private set; }
        public ControllerManager(MainCenter center)
        {
            this.center = center;
            mControllerList = new List<IController>();
        }
        public void Awake()
        {
            //配置数据
            Add(new RegisterLoginSubServerController(this));
        }

        public void Start()
        {
        }

        public void Update()
        {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                mControllerList[i].Update();
            }
        }

        public void OnDestory()
        {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                mControllerList[i].OnDestory();
            }
        }

        /// <summary>
        /// 添加控制器
        /// </summary>
        /// <param name="controller"></param>
        public void Add(IController controller) 
        {
            if (controller == null) {
                Debug.LogError("add controller is null");
                return;
            }
            if (mControllerList.Contains(controller))
            {
                Debug.LogError("add controller is contains:"+controller.ToString());
                return;
            }
            mControllerList.Add(controller);
            controller.Awake();
            controller.Start();
        }
        /// <summary>
        /// 移除控制器
        /// </summary>
        /// <param name="controller"></param>
        public void Remove(IController controller)
        {
            if (controller == null)
            {
                Debug.LogError("Remove controller is null");
                return;
            }
            if (!mControllerList.Contains(controller))
            {
                Debug.LogError("Remove controller is  not contains:" + controller.ToString());
                return;
            }
            mControllerList.Remove(controller);
        }
        /// <summary>
        /// 获取控制器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetController<T>() where T :class, IController
         {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                if (mControllerList[i] is T) {
                    return mControllerList[i] as T;
                }
            }
            return default(T);
        }

        public void Remove(ControllerType controllerEnum)
        {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                if (mControllerList[i].controllerType == controllerEnum)
                {
                    mControllerList.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
