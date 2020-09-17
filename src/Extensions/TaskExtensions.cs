using System.Threading.Tasks;

namespace Basil.Behaviors.Extensions
{
    public static class TaskExtensions
    {
        public static bool IsNotComplited(this Task task)
            => !task.IsCompleted && !task.IsCanceled && !task.IsFaulted;
    }
}