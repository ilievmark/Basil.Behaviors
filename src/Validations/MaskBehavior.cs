using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Basil.Behaviors.Validations
{
    public class MaskBehavior : ValidationBehaviorBase<string>
    {
        private IDictionary<int, char> _positions;

        #region Properties
        
        #region TargetCharacter
        
        public static readonly BindableProperty TargetCharacterProperty =
            BindableProperty.Create(
                propertyName: nameof(TargetCharacter),
                returnType: typeof(char),
                declaringType: typeof(MaskBehavior),
                defaultValue: 'X');

        public char TargetCharacter
        {
            get => (char)GetValue(TargetCharacterProperty);
            set => SetValue(TargetCharacterProperty, value);
        }
        
        #endregion
        
        #endregion
        
        protected override bool ProcessValidation(string newValue, Func<string> getValueDelegate, Func<string, string> setValueDelegate)
        {
            if (string.IsNullOrWhiteSpace(newValue) || GetPositions() == null)
            {
                return false;
            }

            if (newValue.Length > Pattern.Length)
            {
                setValueDelegate(newValue.Remove(newValue.Length - 1));
                return false;
            }

            foreach (var position in GetPositions())
            {
                if (newValue.Length >= position.Key + 1)
                {
                    var value = position.Value.ToString();
                    if (newValue.Substring(position.Key, 1) != value)
                    {
                        newValue = newValue.Insert(position.Key, value);
                    }
                }
            }

            if (getValueDelegate() != newValue)
            {
                setValueDelegate(newValue);
            }

            return true;
        }
        
        private IDictionary<int, char> GetPositions()
        {
            if (string.IsNullOrEmpty(Pattern))
            {
                _positions = null;
            }
            else if (_positions == null)
            {
                var list = new Dictionary<int, char>();
                for (var i = 0; i < Pattern.Length; i++)
                    if (Pattern[i] != TargetCharacter)
                        list.Add(i, Pattern[i]);

                _positions = list;
            }

            return _positions;
        }
    }
}