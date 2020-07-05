using System;
using System.Collections.Generic;
using System.Linq;
using Basil.Behaviors.Extensions.Internal;
using Basil.Behaviors.Rules;
using Basil.Behaviors.Rules.Mask;
using Xamarin.Forms;

namespace Basil.Behaviors.Masks
{
    public class MaskWithRulesBehavior : PropertyRulesBehaviorBase<string, MaskRuleBase>
    {
        private List<RuleSpec> _ruledPositions;

        #region Properties
        
        #region Pattern property
        
        public static readonly BindableProperty PatternProperty =
            BindableProperty.Create(
                propertyName: nameof(Pattern),
                returnType: typeof(string),
                declaringType: typeof(MaskWithRulesBehavior),
                defaultValue: string.Empty);

        public string Pattern
        {
            get => (string)GetValue(PatternProperty);
            set => SetValue(PatternProperty, value);
        }
        
        #endregion
        
        #endregion
        
        protected override bool ProcessProperty(string newValue, Func<string> getValueDelegate, Func<string, string> setValueDelegate)
        {
            if (string.IsNullOrWhiteSpace(newValue) || GetRuledPositions() == null)
                return false;

            if (IsLessThanPattern(newValue))
            {
                setValueDelegate(newValue.Remove(newValue.Length - 1));
                return false;
            }

            var rules = GetRuledPositions();
            for (int position = 0; position < rules.Count; position++)
            {
                if (newValue.Length >= position + 1)
                {
                    var rule = rules[position];
                    var targetCharacter = rule.Value;
                    var currentCharacter = newValue.Substring(position, 1);
                    if (!rule.IsSpecified && currentCharacter != targetCharacter)
                        newValue = newValue.Insert(position, targetCharacter);
                    else if (rule.IsSpecified && !IsAllowed(currentCharacter, rule))
                        newValue = newValue.Remove(newValue.Length - 1);
                }
                else break;
            }
            
            if (getValueDelegate() != newValue)
                setValueDelegate(newValue);

            return true;
        }
        
        private bool IsAllowed(string str, RuleSpec spec) => spec.Rule.Verify(str);

        private bool IsLessThanPattern(string val) => val.Length > Pattern.Length;

        private List<RuleSpec> GetRuledPositions()
        {
            if (string.IsNullOrEmpty(Pattern))
            {
                _ruledPositions = null;
            }
            else if (_ruledPositions == null)
            {
                var list = new List<RuleSpec>();
                for (var i = 0; i < Pattern.Length; i++)
                {
                    var rule = Rules.FirstOrDefault(r => r.Rule[0] == Pattern[i]);
                    if (rule != null)
                        list.Add(CreateSpecifiedRulePosition(rule));
                    else
                        list.Add(CreateUnspecifiedRulePosition(Pattern[i]));
                }
                
                _ruledPositions = list;
            }

            return _ruledPositions;
        }

        private RuleSpec CreateSpecifiedRulePosition(MaskRuleBase rule)
        {
            return new RuleSpec(true)
            {
                Value = rule.Rule,
                Rule = rule
            };
        }

        private RuleSpec CreateUnspecifiedRulePosition(char patternChar)
        {
            return new RuleSpec(false)
            {
                Value = patternChar.ToString()
            };
        }

        private class RuleSpec
        {
            public RuleSpec(bool isSpecified) => IsSpecified = isSpecified;
            
            public bool IsSpecified { get; }
            public string Value { get; set; }
            public MaskRuleBase Rule { get; set; }
        }
    }
}