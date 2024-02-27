namespace SubServer
{
    public interface IControllerManager : ILife
    {
        MainCenter center { get; }
        void Add(IController controller);
        void Remove(IController controller);
        void Remove(ControllerType controllerEnum);
        T GetController<T>() where T : class, IController;
    }
}
