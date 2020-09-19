using Basil.Behaviors.Events;
using Basil.Behaviors.Events.Handlers;
using Basil.Behaviors.Events.HandlersAsync;
using Basil.Behaviors.Events.Parameters;
using Basil.Behaviors.Extensions;
using Basil.Behaviors.Tests.Environment.MVVM.View.Handlers;
using Basil.Behaviors.Tests.Environment.MVVM.ViewModel.Handlers;
using FluentAssertions;
using Xunit;

namespace Basil.Behaviors.Tests.Handlers.Sequential
{
    public class Return
    {
        [Fact]
        public void ReturnParameterSimpleTest()
        {
            var view = new ViewWithButton {BindingContext = new ViewModelSimleHandling()};
            var behavior = new EventToMultipleHandlersBehavior { EventName = "Clicked" };
            var handlerWithReturn = new EventToMethodHandler<int> { MethodName = "MethodWithReturnInt"};
            var handlerWithReturnParameter = new EventToMethodHandler { MethodName = "MethodWithParameterInt"};
            var returnParameter = new ReturnParameter<int>();
            handlerWithReturnParameter.Parameters.Add(returnParameter);
            behavior.Handlers.Add(handlerWithReturn);
            behavior.Handlers.Add(handlerWithReturnParameter);
            view.Button.Behaviors.Add(behavior);
            
            view.Button.SendClicked();

            returnParameter.GetValue().Should().Be(9);
            view.BindingContext.GetFieldValue("_parameterValue").Should().Be(9);
        }
        
        [Fact]
        public void ReturnParameterSkipableTest()
        {
            var view = new ViewWithButton {BindingContext = new ViewModelSimleHandling()};
            var behavior = new EventToMultipleHandlersBehavior() { EventName = "Clicked" };
            var handlerWithReturn = new EventToMethodHandler<int> { MethodName = "MethodWithReturnInt"};
            var delayHandler = new DelayEventHandler();
            var handlerWithReturnParameter = new EventToMethodHandler { MethodName = "MethodWithParameterInt"};
            var returnParameter = new ReturnParameter<int>();
            handlerWithReturnParameter.Parameters.Add(returnParameter);
            behavior.Handlers.Add(handlerWithReturn);
            behavior.Handlers.Add(delayHandler);
            behavior.Handlers.Add(handlerWithReturnParameter);
            view.Button.Behaviors.Add(behavior);
            
            view.Button.SendClicked();

            returnParameter.GetValue().Should().Be(9);
            view.BindingContext.GetFieldValue("_parameterValue").Should().Be(9);
        }
        
        [Fact]
        public void ReturnParameterMultipleSkipableTest()
        {
            var view = new ViewWithButton {BindingContext = new ViewModelSimleHandling()};
            var behavior = new EventToMultipleHandlersBehavior { EventName = "Clicked" };
            var handlerWithReturn = new EventToMethodHandler<int> { MethodName = "MethodWithReturnInt"};
            var handlerWithReturnParameter = new EventToMethodHandler { MethodName = "MethodWithParameterInt"};
            var returnParameter = new ReturnParameter<int>();
            handlerWithReturnParameter.Parameters.Add(returnParameter);
            behavior.Handlers.Add(handlerWithReturn);
            behavior.Handlers.Add(new DelayEventHandler());
            behavior.Handlers.Add(new DelayEventHandler());
            behavior.Handlers.Add(new DelayEventHandler());
            behavior.Handlers.Add(handlerWithReturnParameter);
            view.Button.Behaviors.Add(behavior);
            
            view.Button.SendClicked();

            returnParameter.GetValue().Should().Be(9);
            view.BindingContext.GetFieldValue("_parameterValue").Should().Be(9);
        }
        
        [Fact]
        public void ReturnParameterNotLostWithoutParamTest()
        {
            var view = new ViewWithButton {BindingContext = new ViewModelSimleHandling()};
            var behavior = new EventToMultipleHandlersBehavior { EventName = "Clicked" };
            var handlerWithReturn = new EventToMethodHandler<int> { MethodName = "MethodWithReturnInt"};
            var handlerWithMethod = new EventToMethodHandler { MethodName = "Method"};
            var handlerWithReturnParameter = new EventToMethodHandler { MethodName = "MethodWithParameterInt"};
            var returnParameter = new ReturnParameter<int>();
            handlerWithReturnParameter.Parameters.Add(returnParameter);
            behavior.Handlers.Add(handlerWithReturn);
            behavior.Handlers.Add(handlerWithMethod);
            behavior.Handlers.Add(handlerWithReturnParameter);
            view.Button.Behaviors.Add(behavior);
            
            view.Button.SendClicked();

            returnParameter.GetValue().Should().Be(9);
            view.BindingContext.GetFieldValue("_parameterValue").Should().Be(9);
        }

        [Fact]
        public void ReturnParameterForParallelTest()
        {
            var view = new ViewWithButton {BindingContext = new ViewModelSimleHandling()};
            var behavior = new EventToMultipleHandlersBehavior { EventName = "Clicked" };
            var handlerWithReturn = new EventToMethodHandler<int> { MethodName = "MethodWithReturnInt"};
            var parallelExecutor = new ParallelHandlerExecutor();
            var innerParallelExecutor = new ParallelHandlerExecutor();
            var handlerWithMethod = new EventToMethodHandler { MethodName = "Method"};
            
            var handlerWithReturnParameter1 = new EventToMethodHandler { MethodName = "MethodWithParameterInt"};
            var returnParameter1 = new ReturnParameter<int>();
            handlerWithReturnParameter1.Parameters.Add(returnParameter1);
            var handlerWithReturnParameter2 = new EventToMethodHandler { MethodName = "MethodWithParameterInt"};
            var returnParameter2 = new ReturnParameter<int>();
            handlerWithReturnParameter2.Parameters.Add(returnParameter2);
            var handlerWithReturnParameter3 = new EventToMethodHandler { MethodName = "MethodWithParameterInt"};
            var returnParameter3 = new ReturnParameter<int>();
            handlerWithReturnParameter3.Parameters.Add(returnParameter3);
            
            parallelExecutor.Handlers.Add(handlerWithReturnParameter1);
            parallelExecutor.Handlers.Add(handlerWithMethod);
            parallelExecutor.Handlers.Add(innerParallelExecutor);
            parallelExecutor.Handlers.Add(handlerWithReturnParameter2);
            
            innerParallelExecutor.Handlers.Add(handlerWithReturnParameter3);
            innerParallelExecutor.Handlers.Add(handlerWithMethod);
                
            behavior.Handlers.Add(handlerWithReturn);
            behavior.Handlers.Add(handlerWithMethod);
            behavior.Handlers.Add(new DelayEventHandler());
            behavior.Handlers.Add(parallelExecutor);
            view.Button.Behaviors.Add(behavior);
            
            view.Button.SendClicked();

            returnParameter1.GetValue().Should().Be(9);
            returnParameter2.GetValue().Should().Be(9);
            returnParameter3.GetValue().Should().Be(9);
        }
        
        [Fact]
        public void ReturnParameterForSequenceTest()
        {
            var view = new ViewWithButton {BindingContext = new ViewModelSimleHandling()};
            var behavior = new EventToMultipleHandlersBehavior { EventName = "Clicked" };
            var handlerWithReturn = new EventToMethodHandler<int> { MethodName = "MethodWithReturnInt"};
            var sequenceExecutor = new SequenceHandlerExecutor { WaitResult = true };
            var innerSequenceExecutor = new SequenceHandlerExecutor { WaitResult = true };
            var handlerWithMethod = new EventToMethodHandler { MethodName = "Method"};
            
            var handlerWithReturnParameter1 = new EventToMethodHandler { MethodName = "MethodWithParameterInt"};
            var returnParameter1 = new ReturnParameter<int>();
            handlerWithReturnParameter1.Parameters.Add(returnParameter1);
            var handlerWithReturnParameter2 = new EventToMethodHandler { MethodName = "MethodWithParameterInt"};
            var returnParameter2 = new ReturnParameter<int>();
            handlerWithReturnParameter2.Parameters.Add(returnParameter2);
            var handlerWithReturnParameter3 = new EventToMethodHandler { MethodName = "MethodWithParameterInt"};
            var returnParameter3 = new ReturnParameter<int>();
            handlerWithReturnParameter3.Parameters.Add(returnParameter3);
            var handlerWithReturnParameter4 = new EventToMethodHandler { MethodName = "MethodWithParameterInt"};
            var returnParameter4 = new ReturnParameter<int>();
            handlerWithReturnParameter4.Parameters.Add(returnParameter4);
                
            behavior.Handlers.Add(handlerWithReturn);
            behavior.Handlers.Add(handlerWithMethod);
            behavior.Handlers.Add(new DelayEventHandler());
            behavior.Handlers.Add(sequenceExecutor);
            
            sequenceExecutor.Handlers.Add(handlerWithReturnParameter1);
            sequenceExecutor.Handlers.Add(handlerWithMethod);
            sequenceExecutor.Handlers.Add(innerSequenceExecutor);
            
            innerSequenceExecutor.Handlers.Add(handlerWithReturnParameter3);
            innerSequenceExecutor.Handlers.Add(handlerWithReturn);
            innerSequenceExecutor.Handlers.Add(handlerWithMethod);
            innerSequenceExecutor.Handlers.Add(handlerWithReturnParameter4);
            
            sequenceExecutor.Handlers.Add(handlerWithReturnParameter2);
            
            view.Button.Behaviors.Add(behavior);
            
            view.Button.SendClicked();

            returnParameter1.GetValue().Should().Be(9);
            returnParameter2.GetValue().Should().Be(null);
            returnParameter3.GetValue().Should().Be(null);
            returnParameter4.GetValue().Should().Be(9);
        }
        
        [Fact]
        public void ReturnParameterForSequenceAndParallelTest()
        {
            var view = new ViewWithButton {BindingContext = new ViewModelSimleHandling()};
            var behavior = new EventToMultipleHandlersBehavior { EventName = "Clicked" };
            var handlerWithReturn = new EventToMethodHandler<int> { MethodName = "MethodWithReturnInt"};
            var sequenceExecutor = new SequenceHandlerExecutor { WaitResult = true };
            var innerParallelExecutor = new ParallelHandlerExecutor { WaitResult = true };
            var handlerWithMethod = new EventToMethodHandler { MethodName = "Method"};
            
            var handlerWithReturnParameter1 = new EventToMethodHandler { MethodName = "MethodWithParameterInt"};
            var returnParameter1 = new ReturnParameter<int>();
            handlerWithReturnParameter1.Parameters.Add(returnParameter1);
            var handlerWithReturnParameter2 = new EventToMethodHandler { MethodName = "MethodWithParameterInt"};
            var returnParameter2 = new ReturnParameter<int>();
            handlerWithReturnParameter2.Parameters.Add(returnParameter2);
            var handlerWithReturnParameter3 = new EventToMethodHandler { MethodName = "MethodWithParameterInt"};
            var returnParameter3 = new ReturnParameter<int>();
            handlerWithReturnParameter3.Parameters.Add(returnParameter3);
            var handlerWithReturnParameter4 = new EventToMethodHandler { MethodName = "MethodWithParameterInt"};
            var returnParameter4 = new ReturnParameter<int>();
            handlerWithReturnParameter4.Parameters.Add(returnParameter4);
                
            behavior.Handlers.Add(handlerWithReturn);
            behavior.Handlers.Add(handlerWithMethod);
            behavior.Handlers.Add(new DelayEventHandler());
            behavior.Handlers.Add(sequenceExecutor);
            
            sequenceExecutor.Handlers.Add(handlerWithReturnParameter1);
            sequenceExecutor.Handlers.Add(handlerWithReturn);
            sequenceExecutor.Handlers.Add(handlerWithMethod);
            sequenceExecutor.Handlers.Add(innerParallelExecutor);
            
            innerParallelExecutor.Handlers.Add(handlerWithReturnParameter3);
            innerParallelExecutor.Handlers.Add(handlerWithReturn);
            innerParallelExecutor.Handlers.Add(handlerWithMethod);
            innerParallelExecutor.Handlers.Add(handlerWithReturnParameter4);
            
            sequenceExecutor.Handlers.Add(handlerWithReturnParameter2);
            
            view.Button.Behaviors.Add(behavior);
            
            view.Button.SendClicked();

            returnParameter1.GetValue().Should().Be(9);
            returnParameter2.GetValue().Should().Be(9);
            returnParameter3.GetValue().Should().Be(9);
            returnParameter4.GetValue().Should().Be(9);
        }
    }
}