namespace SubServer
{
    public abstract class BaseController : IController
    {
        protected IControllerManager mControllerManager;
        public abstract ControllerType controllerType { get; }

        public BaseController(IControllerManager controllerManager)
        {
            mControllerManager = controllerManager;
        }
        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void OnDestory() { }
    }
}
