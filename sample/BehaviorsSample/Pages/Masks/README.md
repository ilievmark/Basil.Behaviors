# Basil.Behaviors Masks

Mask behaviours created to format text in Editor or Entry
Actually, you can attach this behaviour on any view element you want to

## Behaviors
There are two mask behaviors you can use

1. MaskBehavior
2. MaskWithRulesBehavior

### MaskBehavior
On MaskPage you can see "Demo 1" entry with MaskBehavior, that configured like

```
<masks:MaskBehavior Pattern="(XXX) - XXX - XX - XX" TargetCharacter="X" PropertyName="Text"/>
```

That means, that any text in this entry wil be masked with pattern (XXX) - XXX - XX - XX, where 'X' is character that user inputs
and 'Text' - name of property MaskBehavior will mask

IMAGE

### MaskWithRulesBehavior
There are second section marked as 'Demo 2'. There is Entry with MaskWithRulesBehavior

```
<masks:MaskWithRulesBehavior Pattern="(XXc) -- E cccX X XE ccX" PropertyName="Text">
    <maskRules:CharMaskRule Rule="X" AllowedChars="QWERTYUIOPASDFGHJKLZXCVBNM"/>
    <maskRules:CharMaskRule Rule="E" AllowedChars="asDF1236"/>
    <maskRules:CharMaskRule Rule="c" AllowedChars="1234567890"/>
</masks:MaskWithRulesBehavior>
```
Here is more complex pattern for mask. We can use any symbols to target in pattern. This symbols will be marked, and when user inputs
some string, all characters we target will be replaced according rules.

Here, in '(XXc) -- E cccX X XE ccX' we will target on three symbols: 'X', 'E', 'c'. Using rules CharMaskRule for this symbols,
we must manage allowed characters that user can input in right place. Here for character 'c' we allow only numbers. 'E' symbol represent
any symbols from "asDF1236" and so on.

IMAGE

You can used for input complicated values. For example car registration number

```
<masks:MaskWithRulesBehavior Pattern="AAA BBBB" PropertyName="Text">
    <maskRules:CharMaskRule Rule="A" AllowedChars="QWERTYUIOPASDFGHJKLZXCVBNM"/>
    <maskRules:CharMaskRule Rule="B" AllowedChars="1234567890"/>
</masks:MaskWithRulesBehavior>
```

IMAGE

### Rules for MaskWithRulesBehavior
There are different rules for this kind of mask

1. CharMaskRule
2. AnyCharMaskRule
3. IgnoreCaseCharMaskRule

All rules derived from MaskRuleBase. You can create your own rule

#### CharMaskRule
That rule used like this

```
<maskRules:CharMaskRule Rule="A" AllowedChars="1234567890"/>
```

and represent single symbol marked as 'A' (for that instance), and allow you input there only one of symbol, described in AllowedChars.
(!!!WARNING!!!) Also its case sensitive

#### IgnoreCaseCharMaskRule
That rule used like this

```
<maskRules:IgnoreCaseCharMaskRule Rule="B" AllowedChars="1234567890"/>
```

and represent single symbol marked as 'B' (for that instance), and allow you input there only one of symbol,
described in AllowedChars ignoring case

#### AnyCharMaskRule
That rule used like this

```
<maskRules:AnyCharMaskRule Rule="C"/>
```
and represent single symbol marked as 'C' (for that instance), and allow you input any you want to instead this symbol

### Demo
To see how behaviors works, compile sample app and go MaskPage page
