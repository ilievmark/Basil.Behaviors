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

![Image of usage MaskBehavior for demo 1](https://github.com/ilievmark/Basil.Behaviors/blob/doc_and_guides/inf/mask_demo_1.png)

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

![Image of usage MaskWithRulesBehavior for demo 2](https://github.com/ilievmark/Basil.Behaviors/blob/doc_and_guides/inf/mask_demo_2.png)

You can used for input complicated values. For example (Demo 3) car registration number

```
<masks:MaskWithRulesBehavior Pattern="AAA BBBB" PropertyName="Text">
    <maskRules:CharMaskRule Rule="A" AllowedChars="QWERTYUIOPASDFGHJKLZXCVBNM"/>
    <maskRules:CharMaskRule Rule="B" AllowedChars="1234567890"/>
</masks:MaskWithRulesBehavior>
```

![Image of usage MaskWithRulesBehavior for demo 3](https://github.com/ilievmark/Basil.Behaviors/blob/doc_and_guides/inf/mask_demo_3.png)

You can perform text masking with duferent rules.

### Rules for MaskWithRulesBehavior

There are different rules for this kind of mask

1. CharMaskRule
2. AnyCharMaskRule
3. IgnoreCaseCharMaskRule

All rules derived from MaskRuleBase. You can create your own rule

#### CharMaskRule

Represents rule for direct symbol in pattern with only allowed symbols (Demo 2, 3)
CASE SENSITIVE
That rule used like this

```
<maskRules:CharMaskRule Rule="A" AllowedChars="1234567890"/>
```

and represent single symbol marked as 'A' (for that instance), and allow you input there only one of symbol, described in AllowedChars.
(!!!WARNING!!!) Also its case sensitive.

#### AnyCharMaskRule

Represents rule for direct symbol in pattern for any (Demo 4)
That rule used like this

```
<maskRules:CharMaskRule Rule="A" AllowedChars="1234567890"/>
```

#### IgnoreCaseCharMaskRule

Represents rule for direct symbol in pattern with only allowed symbols (Demo 4, 5)
NOT CASE SENSITIVE
That rule used like this

```
<maskRules:IgnoreCaseCharMaskRule Rule="A" AllowedChars="QWERTYUIOPASDFGHJKLZXCVBNM"/>
```

### Demo
To see how behaviors works, compile sample app and go MaskPage page
