namespace Basil.Behaviors.Events.HandlerAbstract
{
    public interface IGenericRisible
    {
        T Rise<T>(object sender, object eventArgs);
    }
}