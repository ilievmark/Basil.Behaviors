using Basil.Behaviors.Events.Parameters;

namespace Basil.Behaviors.Extensions
{
    public static class ParameterExtensions
    {
        public static bool IsOptional(this Parameter parameter)
            => parameter is NamedParameter;
    }
}