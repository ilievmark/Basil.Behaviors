using Basil.Behaviors.Extensions;
using System.Windows.Input;
using Basil.Behaviors.Events.HandlerAbstract;
using Xamarin.Forms;

namespace Basil.Behaviors.Events
{
    public class EventToCommandBehavior : EventBehaviorBase, ICommandExecutable
    {
        #region Properties

        #region Command property

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                propertyName: nameof(Command),
                returnType: typeof(ICommand),
                declaringType: typeof(EventToCommandBehavior),
                defaultValue: default(ICommand));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        #endregion

        #region ConverterParameter property

        public static readonly BindableProperty ConverterParameterProperty =
            BindableProperty.Create(
                propertyName: nameof(ConverterParameter),
                returnType: typeof(object),
                declaringType: typeof(EventToCommandBehavior),
                defaultValue: default);

        public object ConverterParameter
        {
            get => GetValue(ConverterParameterProperty);
            set => SetValue(ConverterParameterProperty, value);
        }

        #endregion

        #region Converter property

        public static readonly BindableProperty ConverterProperty =
            BindableProperty.Create(
                propertyName: nameof(Converter),
                returnType: typeof(IValueConverter),
                declaringType: typeof(EventToCommandBehavior),
                defaultValue: default(IValueConverter));

        public IValueConverter Converter
        {
            get => (IValueConverter)GetValue(ConverterProperty);
            set => SetValue(ConverterProperty, value);
        }

        #endregion

        #region CommandParameter property

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(
                propertyName: nameof(CommandParameter),
                returnType: typeof(object),
                declaringType: typeof(EventToCommandBehavior),
                defaultValue: default(object));


        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        #endregion

        #endregion

        #region Overrides

        protected override void HandleEvent(object sender, object eventArgs)
            => this.ExecuteCommand(eventArgs);

        #endregion
    }
}
