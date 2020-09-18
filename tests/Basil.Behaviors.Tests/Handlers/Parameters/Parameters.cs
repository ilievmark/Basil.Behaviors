using System.Threading;
using System.Threading.Tasks;
using Basil.Behaviors.Events.Parameters;
using Basil.Behaviors.Events.Parameters.Abstract;
using Basil.Behaviors.Extensions;
using Basil.Behaviors.Tests.Environment.Sample;
using FluentAssertions;
using Xamarin.Forms;
using Xunit;
using Binding = Xamarin.Forms.Binding;

namespace Basil.Behaviors.Tests.Handlers.Parameters
{
    public class Parameters
    {
        [Fact]
        public void DefaultParameterTest()
        {
            var intParameter = new DefaultParameter<int>();
            var stringParameter = new DefaultParameter<string>();
            var dynamicParameter = new DefaultParameter<dynamic>();
            var objectParameter = new DefaultParameter<object>();

            intParameter.GetParamType().Should().BeAssignableTo<int>();
            stringParameter.GetParamType().Should().BeAssignableTo<string>();
            dynamicParameter.GetParamType().Should().BeAssignableTo<object>();
            objectParameter.GetParamType().Should().BeAssignableTo<object>();
            
            intParameter.GetValue().Should().BeAssignableTo<int>();
            
            intParameter.GetValue().Should().Be(0);
            stringParameter.GetValue().Should().Be(null);
            dynamicParameter.GetValue().Should().Be(null);
            objectParameter.GetValue().Should().Be(null);
        }
        
        [Fact]
        public void GenericParameterTest()
        {
            var intParameter = new GenericParameter<int> { Value = 23 };
            var stringParameter = new GenericParameter<string> { Value = "qwerty1234" };
            var dynamicParameter = new GenericParameter<Task> { Value = Task.CompletedTask };
            var objectParameter = new GenericParameter<object> { Value = CancellationToken.None };

            intParameter.GetParamType().Should().BeAssignableTo<int>();
            stringParameter.GetParamType().Should().BeAssignableTo<string>();
            dynamicParameter.GetParamType().Should().BeAssignableTo<Task>();
            objectParameter.GetParamType().Should().BeAssignableTo<object>();
            
            intParameter.GetValue().Should().BeAssignableTo<int>();
            stringParameter.GetValue().Should().BeAssignableTo<string>();
            dynamicParameter.GetValue().Should().BeAssignableTo<Task>();
            objectParameter.GetValue().Should().BeAssignableTo<CancellationToken>();
            
            intParameter.GetValue().Should().Be(23);
            stringParameter.GetValue().Should().Be("qwerty1234");
            dynamicParameter.GetValue().Should().Be(Task.CompletedTask);
            objectParameter.GetValue().Should().Be(CancellationToken.None);
        }
        
        [Fact]
        public void NamedParameterTest()
        {
            var intParameter = new NamedParameter<int> { Value = 23, Name = "name1" };
            var stringParameter = new NamedParameter<string> { Value = "qwerty1234", Name = "name2" };
            var dynamicParameter = new NamedParameter<Task> { Value = Task.CompletedTask, Name = "name3" };
            var objectParameter = new NamedParameter<object> { Value = CancellationToken.None, Name = "name4" };

            intParameter.GetParamType().Should().BeAssignableTo<int>();
            stringParameter.GetParamType().Should().BeAssignableTo<string>();
            dynamicParameter.GetParamType().Should().BeAssignableTo<Task>();
            objectParameter.GetParamType().Should().BeAssignableTo<object>();
            
            intParameter.GetValue().Should().BeAssignableTo<int>();
            stringParameter.GetValue().Should().BeAssignableTo<string>();
            dynamicParameter.GetValue().Should().BeAssignableTo<Task>();
            objectParameter.GetValue().Should().BeAssignableTo<object>();
            
            intParameter.GetValue().Should().Be(23);
            stringParameter.GetValue().Should().Be("qwerty1234");
            dynamicParameter.GetValue().Should().Be(Task.CompletedTask);
            objectParameter.GetValue().Should().Be(CancellationToken.None);
            
            intParameter.Name.Should().Be("name1");
            stringParameter.Name.Should().Be("name2");
            dynamicParameter.Name.Should().Be("name3");
            objectParameter.Name.Should().Be("name4");
        }
        
        [Fact]
        public void ReflectionParameterTest()
        {
            var intPropertyClassInstance = EnvironmentManager.CreateInstanceWithProperty(4);
            var intPrivatePropertyClassInstance = EnvironmentManager.CreateInstanceWithPrivateProperty(8);
            var intFieldClassInstance = EnvironmentManager.CreateInstanceWithField(6);
            var intPrivateFieldClassInstance = EnvironmentManager.CreateInstanceWithPrivateField(7);
            
            var stringPropertyClassInstance = EnvironmentManager.CreateInstanceWithProperty("qwerty1");
            var stringPrivatePropertyClassInstance = EnvironmentManager.CreateInstanceWithPrivateProperty("qwerty2");
            var stringFieldClassInstance = EnvironmentManager.CreateInstanceWithField("qwerty3");
            var stringPrivateFieldClassInstance = EnvironmentManager.CreateInstanceWithPrivateField("qwerty4");
            
            var intParameter = new ReflectionParameter<int>();
            var stringParameter = new ReflectionParameter<string>();

            intParameter.Target = intPropertyClassInstance;
            intParameter.SourceName = nameof(intPropertyClassInstance.Property);
            intParameter.GetParamType().Should().BeAssignableTo<int>();
            intParameter.GetValue().Should().Be(4);

            intParameter.Target = intPrivatePropertyClassInstance;
            intParameter.SourceName = nameof(intPropertyClassInstance.Property);
            intParameter.GetParamType().Should().BeAssignableTo<int>();
            intParameter.GetValue().Should().Be(null);

            intParameter.Target = intFieldClassInstance;
            intParameter.SourceName = nameof(intFieldClassInstance.Field);
            intParameter.GetParamType().Should().BeAssignableTo<int>();
            intParameter.GetValue().Should().Be(6);

            intParameter.Target = intPrivateFieldClassInstance;
            intParameter.SourceName = nameof(intFieldClassInstance.Field);
            intParameter.GetParamType().Should().BeAssignableTo<int>();
            intParameter.GetValue().Should().Be(7);

            stringParameter.Target = stringPropertyClassInstance;
            stringParameter.SourceName = nameof(stringPropertyClassInstance.Property);
            stringParameter.GetParamType().Should().BeAssignableTo<string>();
            stringParameter.GetValue().Should().Be("qwerty1");

            stringParameter.Target = stringPrivatePropertyClassInstance;
            stringParameter.SourceName = nameof(stringPropertyClassInstance.Property);
            stringParameter.GetParamType().Should().BeAssignableTo<string>();
            stringParameter.GetValue().Should().Be(null);

            stringParameter.Target = stringFieldClassInstance;
            stringParameter.SourceName = nameof(stringFieldClassInstance.Field);
            stringParameter.GetParamType().Should().BeAssignableTo<string>();
            stringParameter.GetValue().Should().Be("qwerty3");

            stringParameter.Target = stringPrivateFieldClassInstance;
            stringParameter.SourceName = nameof(stringFieldClassInstance.Field);
            stringParameter.GetParamType().Should().BeAssignableTo<string>();
            stringParameter.GetValue().Should().Be("qwerty4");
        }

        [Fact]
        public void ReturnParameterTest()
        {
            var intParameter = new ReturnParameter<int>();
            var stringParameter = new ReturnParameter<string>();
            
            intParameter.SetReturnValue(3);
            stringParameter.SetReturnValue("qwerty1234");
            
            intParameter.GetParamType().Should().BeAssignableTo<int>();
            intParameter.GetValue().Should().Be(3);
            intParameter.Should().BeAssignableTo<IReturnable>();
            
            stringParameter.GetParamType().Should().BeAssignableTo<string>();
            stringParameter.GetValue().Should().Be("qwerty1234");
            stringParameter.Should().BeAssignableTo<IReturnable>();
        }

        [Fact]
        public void CancellationParameterTest()
        {
            var cts = new CancellationTokenSource();
            var parameter = new CancellationParameter();
            
            parameter.SetCancellationToken(cts.Token);
            
            parameter.GetParamType().Should().BeAssignableTo<CancellationToken>();
            parameter.Should().BeAssignableTo<ICancellation>();
            parameter.GetValue().Should().Be(cts.Token);

            ((CancellationToken) parameter.GetValue()).IsCancellationRequested.Should().BeFalse();
            cts.Cancel();
            ((CancellationToken) parameter.GetValue()).IsCancellationRequested.Should().BeTrue();
        }

        [Fact]
        public void BindableParameterTest()
        {
            var classWithIntProperty = EnvironmentManager.CreateInstanceWithProperty(4);
            var classWithStringProperty = EnvironmentManager.CreateInstanceWithProperty("qwerty1234");
            
            var intParameter = new BindableParameter<int>();
            intParameter.SetBinding(BindableParameter<int>.ValueProperty, new Binding("Property"));
            var stringParameter = new BindableParameter<string>();
            stringParameter.SetBinding(BindableParameter<string>.ValueProperty, new Binding("Property"));

            intParameter.AttachBindableObject(new ContentView {BindingContext = classWithIntProperty});
            stringParameter.AttachBindableObject(new ContentView {BindingContext = classWithStringProperty});
            
            intParameter.GetParamType().Should().BeAssignableTo<int>();
            intParameter.GetValue().Should().Be(4);
            
            stringParameter.GetParamType().Should().BeAssignableTo<string>();
            stringParameter.GetValue().Should().Be("qwerty1234");
        }

        [Fact]
        public void GenericTaskReturnParameterTest()
        {
            var gtIntReturnParameter = new GenericTaskReturnParameter<int>();
            var gtStringReturnParameter = new GenericTaskReturnParameter<string>();
            
            gtIntReturnParameter.SetReturnValue(3);
            gtStringReturnParameter.SetReturnValue("qwerty1234");
            
            gtIntReturnParameter.GetParamType().Should().BeAssignableTo<Task<int>>();
            gtIntReturnParameter.GetValue().Should().Be(3);
            gtIntReturnParameter.Should().BeAssignableTo<IReturnable>();
            
            gtStringReturnParameter.GetParamType().Should().BeAssignableTo<Task<string>>();
            gtStringReturnParameter.GetValue().Should().Be("qwerty1234");
            gtStringReturnParameter.Should().BeAssignableTo<IReturnable>();
        }
    }
}