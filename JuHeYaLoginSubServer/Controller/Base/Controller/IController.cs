namespace SubServer
{
    public interface IController : ILife
    {
        ControllerType controllerType { get; }
    }
}
