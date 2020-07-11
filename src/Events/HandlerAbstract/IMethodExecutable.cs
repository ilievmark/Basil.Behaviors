namespace Basil.Behaviors.Events.HandlerAbstract
{
    public interface IMethodExecutable
    {
        object GetTargetMethodRiseObject();
        
        string MethodName { get; }
    }
}