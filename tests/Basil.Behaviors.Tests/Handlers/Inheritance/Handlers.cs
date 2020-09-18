using Basil.Behaviors.Events.HandlerAbstract;
using Basil.Behaviors.Events.Handlers;
using Basil.Behaviors.Events.Handlers.Conditional;
using Xunit;

namespace Basil.Behaviors.Tests.Handlers.Inheritance
{
    public class Handlers
    {
        [Fact]
        public void EventToCommandHandlerTest()
        {
            var handler = new EventToCommandHandler();
            
            Assert.True(handler is IRisible);
            Assert.True(handler is ICommandExecutable);
            Assert.True(handler is ISkipReturnable);
            
            Assert.False(handler is IGenericRisible);
            Assert.False(handler is IAsyncRisible);
            Assert.False(handler is IAsyncGenericRisible);
            Assert.False(handler is IMethodExecutable);
            Assert.False(handler is ICompositeHandler);
            Assert.False(handler is ICompositeParallelHandler);
            Assert.False(handler is IParameterContainer);
        }
        
        [Fact]
        public void EventToMethodHandlerTest()
        {
            var handler = new EventToMethodHandler();
            
            Assert.True(handler is IRisible);
            Assert.True(handler is IMethodExecutable);
            Assert.True(handler is IParameterContainer);
            
            Assert.False(handler is ISkipReturnable);
            Assert.False(handler is ICommandExecutable);
            Assert.False(handler is IGenericRisible);
            Assert.False(handler is IAsyncRisible);
            Assert.False(handler is IAsyncGenericRisible);
            Assert.False(handler is ICompositeHandler);
            Assert.False(handler is ICompositeParallelHandler);
        }
        
        [Fact]
        public void EventToMethodGenericHandlerTest()
        {
            var handler = new EventToMethodHandler<object>();
            
            Assert.True(handler is IGenericRisible);
            Assert.True(handler is IMethodExecutable);
            Assert.True(handler is IParameterContainer);
            
            Assert.False(handler is ISkipReturnable);
            Assert.False(handler is ICommandExecutable);
            Assert.False(handler is IRisible);
            Assert.False(handler is IAsyncRisible);
            Assert.False(handler is IAsyncGenericRisible);
            Assert.False(handler is ICompositeHandler);
            Assert.False(handler is ICompositeParallelHandler);
        }
        
        [Fact]
        public void EventToSetFieldHandlerTest()
        {
            var handler = new EventToSetFieldHandler<object>();
            
            Assert.True(handler is IRisible);
            
            Assert.False(handler is IMethodExecutable);
            Assert.False(handler is IParameterContainer);
            Assert.False(handler is ISkipReturnable);
            Assert.False(handler is IGenericRisible);
            Assert.False(handler is ICommandExecutable);
            Assert.False(handler is IAsyncRisible);
            Assert.False(handler is IAsyncGenericRisible);
            Assert.False(handler is ICompositeHandler);
            Assert.False(handler is ICompositeParallelHandler);
        }
        
        [Fact]
        public void EventToSetPropertyHandlerTest()
        {
            var handler = new EventToSetPropertyHandler<object>();
            
            Assert.True(handler is IRisible);
            
            Assert.False(handler is IMethodExecutable);
            Assert.False(handler is IParameterContainer);
            Assert.False(handler is ISkipReturnable);
            Assert.False(handler is IGenericRisible);
            Assert.False(handler is ICommandExecutable);
            Assert.False(handler is IAsyncRisible);
            Assert.False(handler is IAsyncGenericRisible);
            Assert.False(handler is ICompositeHandler);
            Assert.False(handler is ICompositeParallelHandler);
        }
        
        [Fact]
        public void ConditionalCompositeHandlerTest()
        {
            var handler = new ConditionalCompositeHandler();
            
            Assert.True(handler is ICompositeHandler);
            Assert.True(handler is IRisible);
            
            Assert.False(handler is IAsyncRisible);
            Assert.False(handler is IMethodExecutable);
            Assert.False(handler is IParameterContainer);
            Assert.False(handler is ISkipReturnable);
            Assert.False(handler is IGenericRisible);
            Assert.False(handler is ICommandExecutable);
            Assert.False(handler is IAsyncGenericRisible);
            Assert.False(handler is ICompositeParallelHandler);
        }
        
        [Fact]
        public void ConditionalCompositeHandlerGenericTest()
        {
            var handler = new ConditionalCompositeHandler<object>();
            
            Assert.True(handler is ICompositeHandler);
            Assert.True(handler is IGenericRisible);
            
            Assert.False(handler is IRisible);
            Assert.False(handler is IAsyncRisible);
            Assert.False(handler is IMethodExecutable);
            Assert.False(handler is IParameterContainer);
            Assert.False(handler is ISkipReturnable);
            Assert.False(handler is ICommandExecutable);
            Assert.False(handler is IAsyncGenericRisible);
            Assert.False(handler is ICompositeParallelHandler);
        }
    }
}