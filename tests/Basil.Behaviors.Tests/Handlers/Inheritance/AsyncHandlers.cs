using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.Handlers;
using Basil.Behaviors.Events.Handlers.Conditional;
using Basil.Behaviors.Events.HandlersAsync;
using Xunit;

namespace Basil.Behaviors.Tests.Handlers.Inheritance
{
    public class AsyncHandlers
    {
        [Fact]
        public void ParallelHandlerExecutorTest()
        {
            var handler = new ParallelHandlerExecutor();
            
            Assert.True(handler is ICompositeParallelHandler);
            Assert.True(handler is IAsyncRisible);
            
            Assert.False(handler is IRisible);
            Assert.False(handler is IMethodExecutable);
            Assert.False(handler is IParameterContainer);
            Assert.False(handler is ISkipReturnable);
            Assert.False(handler is IGenericRisible);
            Assert.False(handler is ICommandExecutable);
            Assert.False(handler is IAsyncGenericRisible);
            Assert.False(handler is ICompositeHandler);
        }
        
        [Fact]
        public void SequenceHandlerExecutorTest()
        {
            var handler = new SequenceHandlerExecutor();
            
            Assert.True(handler is ICompositeHandler);
            Assert.True(handler is IAsyncRisible);
            
            Assert.False(handler is IRisible);
            Assert.False(handler is IMethodExecutable);
            Assert.False(handler is IParameterContainer);
            Assert.False(handler is ISkipReturnable);
            Assert.False(handler is IGenericRisible);
            Assert.False(handler is ICommandExecutable);
            Assert.False(handler is IAsyncGenericRisible);
            Assert.False(handler is ICompositeParallelHandler);
        }
        
        [Fact]
        public void ConditionalCompositeHandlerAsyncTest()
        {
            var handler = new ConditionalCompositeHandlerAsync();
            
            Assert.True(handler is ICompositeHandler);
            Assert.True(handler is IAsyncRisible);
            
            Assert.False(handler is IRisible);
            Assert.False(handler is IMethodExecutable);
            Assert.False(handler is IParameterContainer);
            Assert.False(handler is ISkipReturnable);
            Assert.False(handler is IGenericRisible);
            Assert.False(handler is ICommandExecutable);
            Assert.False(handler is IAsyncGenericRisible);
            Assert.False(handler is ICompositeParallelHandler);
        }
        
        [Fact]
        public void ConditionalCompositeHandlerAsyncGenericTest()
        {
            var handler = new ConditionalCompositeHandlerAsync<object>();
            
            Assert.True(handler is ICompositeHandler);
            Assert.True(handler is IAsyncGenericRisible);
            
            Assert.False(handler is IRisible);
            Assert.False(handler is IAsyncRisible);
            Assert.False(handler is IMethodExecutable);
            Assert.False(handler is IParameterContainer);
            Assert.False(handler is ISkipReturnable);
            Assert.False(handler is ICommandExecutable);
            Assert.False(handler is IGenericRisible);
            Assert.False(handler is ICompositeParallelHandler);
        }
        
        [Fact]
        public void DelayedCompositeEventHandlerTest()
        {
            var handler = new DelayedCompositeEventHandler();
            
            Assert.True(handler is ICompositeHandler);
            Assert.True(handler is IAsyncRisible);
            
            Assert.False(handler is IAsyncGenericRisible);
            Assert.False(handler is IRisible);
            Assert.False(handler is IMethodExecutable);
            Assert.False(handler is IParameterContainer);
            Assert.False(handler is ISkipReturnable);
            Assert.False(handler is ICommandExecutable);
            Assert.False(handler is IGenericRisible);
            Assert.False(handler is ICompositeParallelHandler);
        }
        
        [Fact]
        public void DelayedCompositeEventHandlerGenericTest()
        {
            var handler = new DelayedCompositeEventHandler<object>();
            
            Assert.True(handler is ICompositeHandler);
            Assert.True(handler is IAsyncGenericRisible);
            
            Assert.False(handler is IRisible);
            Assert.False(handler is IAsyncRisible);
            Assert.False(handler is IMethodExecutable);
            Assert.False(handler is IParameterContainer);
            Assert.False(handler is ISkipReturnable);
            Assert.False(handler is ICommandExecutable);
            Assert.False(handler is IGenericRisible);
            Assert.False(handler is ICompositeParallelHandler);
        }
        
        [Fact]
        public void DelayEventHandlerTest()
        {
            var handler = new DelayEventHandler();
            
            Assert.True(handler is IAsyncRisible);
            Assert.True(handler is ISkipReturnable);
            
            Assert.False(handler is IRisible);
            Assert.False(handler is ICompositeHandler);
            Assert.False(handler is IAsyncGenericRisible);
            Assert.False(handler is IMethodExecutable);
            Assert.False(handler is IParameterContainer);
            Assert.False(handler is ICommandExecutable);
            Assert.False(handler is IGenericRisible);
            Assert.False(handler is ICompositeParallelHandler);
        }
        
        [Fact]
        public void EventToAsyncMethodHandlerTest()
        {
            var handler = new EventToAsyncMethodHandler();
            
            Assert.True(handler is IAsyncRisible);
            Assert.True(handler is IMethodExecutable);
            Assert.True(handler is IParameterContainer);
            
            Assert.False(handler is ISkipReturnable);
            Assert.False(handler is IRisible);
            Assert.False(handler is ICompositeHandler);
            Assert.False(handler is IAsyncGenericRisible);
            Assert.False(handler is ICommandExecutable);
            Assert.False(handler is IGenericRisible);
            Assert.False(handler is ICompositeParallelHandler);
        }
        
        [Fact]
        public void EventToAsyncMethodHandlerGenericTest()
        {
            var handler = new EventToAsyncMethodHandler<object>();
            
            Assert.True(handler is IMethodExecutable);
            Assert.True(handler is IParameterContainer);
            Assert.True(handler is IAsyncGenericRisible);
            
            Assert.False(handler is ISkipReturnable);
            Assert.False(handler is IRisible);
            Assert.False(handler is ICompositeHandler);
            Assert.False(handler is IAsyncRisible);
            Assert.False(handler is ICommandExecutable);
            Assert.False(handler is IGenericRisible);
            Assert.False(handler is ICompositeParallelHandler);
        }
    }
}