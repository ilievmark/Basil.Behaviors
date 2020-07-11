# Basil.Behaviors Validations

Validation behavior allows you to check incoming string on property changed
After checking 'Validated' event will be raised and 'Command' executed
Actually, you can attach this behaviour on any view element you want to

## Validations
There are three validation behaviors you can use

1. RegexValidationBehavior
2. RegexValidationWithRulesBehavior
3. EmailValidationBehavior

### RegexValidationBehavior
On ValidationPage you can see "Demo 1" entry with RegexValidationBehavior, that configured like

```
<validations:RegexValidationBehavior Pattern="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$" PropertyName="Text" Command="{Binding ValidatedCommand}"/>
```

That means, that any text in this entry wil be validated with Regex pattern '^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$'
(rule is - "Minimum eight characters, at least one letter and one number"), and propery 'Text' changes, in will execute ValidatedCommand,
placed in ValidationPageViewModel.

Validation behaviors only check value in property that changes, do not change it like masks do.

Event that rizes follow thid delegate

```
public delegate void Result<TPropery>(bool success, TPropery value);
```

Command can receive arguments with result model of class 'CommandParams.ValidationResultArgs'

```
public class ValidationResultArgs<TProperty>
{
    public ValidationResultArgs(TProperty value, bool valid)
    {
        Value = value;
        Valid = valid;
    }

    public TProperty Value { get; }
    public bool Valid { get; }
}
```

### EmailValidationBehavior
Works like 'RegexValidationBehavior', but do not has 'Pattern' property, and can be used like (Demo 2)

```
<validations:EmailValidationBehavior PropertyName="Text" Command="{Binding ValidatedCommand}"/>
```

It has default email regex pattern
```
private const string EmailPattern = 
            "^(([^<>()\\[\\]\\.,;:\\s@\"]+(\\.[^<>()\\[\\]\\.,;:\\s@\"]+)*)|(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{1,}))$";
```

### RegexValidationWithRulesBehavior
There are next section marked as 'Demo 3'. There is Entry with RegexValidationWithRulesBehavior

```
<validations:RegexValidationWithRulesBehavior PropertyName="Text" Command="{Binding ValidatedCommand}">
    <validation:RegexValidationRule Rule="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{3,}$" Index="0" Length="5"/>
    <validation:RegexValidationRule Rule="\d" Index="5" Length="1"/>
    <validation:RegexValidationRule Rule="dog$" Index="6" Length="8"/>
</validations:RegexValidationWithRulesBehavior>
```

This behavior checks user input text by this rules

1. Minimum three characters, at least one letter and one number
2. Contains number
3. Contains word that ends with 'dog'

For example this string can be 'Gd22j1watchdog'

You can perform text validation with different rules.
You can create own validation rule deriving from BaseRule class
Also you can create own validation behavior, just deriving from ValidationBehaviorBase

### Rules for RegexValidationWithRulesBehavior
There are different rules for this validation behavior

1. RegexValidationRule
(to be continued)

#### RegexValidationRule
That rule used like this

```
<validation:RegexValidationRule Rule="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{3,}$" Index="0" Length="5"/>
```

that will receive full text, and check it by regex expression '^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{3,}$' starting from index 0 and next 5 chars

### Demo
To see how behaviors works, compile sample app and go MaskPage page
