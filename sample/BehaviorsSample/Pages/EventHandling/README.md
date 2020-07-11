# Basil.Behaviors Event handling

Doc structure:

- Behaviors
    - EventToCommandBehavior
    - EventToMethodBehavior
        - Parameters
            - DefaultParameter
            - GenericParameter
            - BindableParameter
            - NamedParameter
            - ReturnParameter
            - GenericTaskReturnParameter
    - EventMultipleHandlerBehavior
        - Handlers
            - EventToCommandHandler
            - EventToMethodHandler
            - EventToMethodHandler<T>
            - EventToAsyncMethodHandler
            - EventToAsyncMethodHandler<T>
            - DelayEventHandler
            - SequenceHandlerExecutor
            - ParallelHandlerExecutor
- Feature usage

This part of package was created as extension of idea of EventToCommandBehavior
All examples you can find in 'EventPage.xaml' file

## Behaviors

There are three event handling behaviors you can use

1. EventToCommandBehavior
2. EventToMethodBehavior
3. EventMultipleHandlerBehavior

### EventToCommandBehavior

This is well known idea of using behaviours, that you can find in many topics as explanation how xamarin forms
behaviors works.

Its easy to use (from Demo 1)

```
<b:EventToCommandBehavior Command="{Binding Sample1Command}" EventName="Clicked"/>
```

Attach this behavior to any view that has some event, pass that event's name to 'EventName' property and bind
command that must be invoked on this event

By default behavior searches event in attached object. But you can bind target object, using property 'TargetObject',
and if its object not null, behavior will search this event there (same for others event handler behaviors)

### EventToMethodBehavior

Same idea, but with invoking methods. Its based on reflection, so be careful when you use it. Its also can call
private and protected methods.

Usage (from Demo 2):

```
<b:EventToMethodBehavior EventName="Clicked" MethodName="OnSample2Command"/>
```

For the instance, OnSample2Command - method, with return type 'void', and has no parameters

Actually it can be used with path to method. To use path declare 'MethodName' with dotes between propeties and fields (from Demo 3):
(First it will search property with name between dots. If there no property with given name, it search runtime field with given name,
else exception 'ArgumentException' will be thrown)

```
<b:EventToMethodBehavior EventName="Clicked" MethodName="Model.m_model.Model.MethodToRize"/>
```

When you use it, behavior starts listen event from 'EventName' in target object (property 'TargetObject') and call method mentioned in MethodName
property. By default it searches method to call in binding context of attached object. But you can bind target object,
using property 'TargetMethodCallObjectProperty', and if its object not null, behavior will search this event there (same for EventMultipleHandlerBehavior).
To call method from another 

#### Parameters for event to method handlers

As you see, not all methods has return type 'void' and has no parameters. So declaration from previous result will be not
applicable in most cases. But you can pass parameters directly from xaml

By the moment, in package available next parameters:

1. DefaultParameter
2. GenericParameter
3. BindableParameter
4. NamedParameter
5. ReturnParameter
6. GenericTaskReturnParameter

All parameters derives from 'Parameter' abstract class, that provide two methods

```
public abstract class Parameter : AttachableBindableObject
{
    public abstract object GetValue();
    public abstract Type GetParamType();
}
```

Where:
- 'GetValue' returns exact value you want to pass as parameter
- 'GetParamType' returns exact type of parameter

So you can create own parameters for your specific needs

Going ahead, all parameters form list above is generic types. And thats because we need to return right type of parameter. But
problem is you can't pass to 'TypeArguments' generic types. So for example expression will be fine

```
<p:BindableParameter x:TypeArguments="c:ICommand" Value="{Binding Sample1Command}"/>
```

Because type ICommand is not generic. But if you want to pass generic type, you'll mention the problem, that xaml doesn't allow you
to do this. So let's say we have class 'SomeModel' and its generic

```
class SomeModel<T>
{
  ...
}
```

using that expression will produce compile time exception

```
<p:BindableParameter x:TypeArguments="c:SomeModel<s:String>" Value="{Binding Sample1Command}"/>
```

So in cases like that, you can select one of awailable solutions:

* Create deriving class from SomeModel<String> and work with it
* Create own parameter type like that

```
public abstract class CustomParameter<T> : AttachableBindableObject
{
    ...
  
    public abstract object GetValue() => ... return value logic ...
    public abstract Type GetParamType() => typeof(SomeModel<T>);
}
```

#### DefaultParameter

Returns default value of passed type

Usage (Demo 4):

```
<b:EventToMethodBehavior EventName="Clicked" MethodName="JustAMethodNoMore">
    <p:DefaultParameter x:TypeArguments="x:String"/>
</b:EventToMethodBehavior>
```

Method 'JustAMethodNoMore' will be called with null string parameter

#### GenericParameter

Returns hardcoded in xaml value

Usage (Demo 5):

```
<b:EventToMethodBehavior EventName="Clicked" MethodName="JustAMethodNoMore">
    <p:GenericParameter x:TypeArguments="x:String">Some value passed as param</p:GenericParameter>
</b:EventToMethodBehavior>
```

Method 'JustAMethodNoMore' will be called with string parameter "Some value passed as param"

#### BindableParameter

Can be used to bind value

Usage (Demo 6):

```
<b:EventToMethodBehavior EventName="Clicked" MethodName="JustAMethodTooButWithParameterBinding">
    <p:BindableParameter x:TypeArguments="c:ICommand" Value="{Binding Sample1Command}"/>
    <p:GenericParameter x:TypeArguments="x:String">The best green in the world - Basil</p:GenericParameter>
</b:EventToMethodBehavior>
```

Method 'JustAMethodTooButWithParameterBinding' will be called with command binded from view model and string

#### NamedParameter

If method has default parameters, you can pass named parameter here declaring name of parameter. Be careful using this,
because if someone renames named parameter in method, it will not work

Let say we have method 'MethodWithNamedParam' with tha declaration

```
public void MethodWithNamedParam(ICommand commandParam, string stringParam, int defaultInt = 0, float g = 4.4f, string someParamName = "Default value", object d = null)
{
    ...
}
```

we can pass named parameter with

```
<b:EventToMethodBehavior EventName="Clicked" MethodName="MethodWithNamedParam">
    <p:BindableParameter x:TypeArguments="c:ICommand" Value="{Binding Sample1Command}"/>
    <p:GenericParameter x:TypeArguments="x:String">The best green in the world - Basil</p:GenericParameter>
    <p:NamedParameter x:TypeArguments="x:Int32" Name="defaultInt">606</p:NamedParameter>
    <p:NamedParameter x:TypeArguments="x:String" Name="someParamName">Some special value</p:NamedParameter>
</b:EventToMethodBehavior>
```

And method will be called with passed values as we expected
* commandParam == Sample1Command from view model
* stringParam == "The best green in the world - Basil"
* defaultInt == 606
* g == 4.4f
* someParamName == "Some special value"
* d == null

#### ReturnParameter

This kind of parameter will work only with secuential executors (EventMultipleHandlerBehavior or SequenceHandlerExecutor). For
Parallel executor this parameter will be ignored

The main idea is keep return value from previous method calling and pass it as param

Let say we have method 'ReturnStringMethod' that returns string (Demo 8 and Demo 9)

```
public string ReturnStringMethod()
{
    return "Hello string from ReturnStringMethod";
}
```

Lets build sequence of calling methods

```
<b:EventMultipleHandlerBehavior EventName="Clicked">
    <h:EventToMethodHandler x:TypeArguments="x:String" MethodName="ReturnStringMethod"/>
    <h:EventToMethodHandler MethodName="JustAMethodNoMore">
        <p:ReturnParameter x:TypeArguments="x:String"/>
    </h:EventToMethodHandler>
</b:EventMultipleHandlerBehavior>
```

Firstly 'ReturnStringMethod' will be called, and string "Hello string from ReturnStringMethod" will be passed to method
'JustAMethodNoMore' as parameter

#### GenericTaskReturnParameter

Its kinda hack of problem mentioned above. Its created because we cant declare 'Task<String>' in xaml.

This kind of parameter will work with async method only (ONLY) if you want to skip waiting task that method returns and pass
generic task as parameter

Lets say we need to write code in that way

```
public async Task RunResultActionsWithPrevTaskWithResult(Task<string> task)
{
    ...
    var result = await task;
    ... use result string some how ...
}
```

We can declare behaviors (Demo 10)

```
<b:EventMultipleHandlerBehavior EventName="Clicked">
    <h:EventToMethodHandler x:TypeArguments="x:String" MethodName="GetString">
        <p:ReturnParameter x:TypeArguments="x:Int32"/>
    </h:EventToMethodHandler>
    <a:EventToAsyncMethodHandler x:TypeArguments="x:String" WaitResult="False" MethodName="RunResultActionsAndReturn">
        <p:ReturnParameter x:TypeArguments="x:String"/>
    </a:EventToAsyncMethodHandler>
    <a:EventToAsyncMethodHandler x:TypeArguments="x:String" WaitResult="True" MethodName="RunResultActionsWithPrevTaskWithResult">
        <p:GenericTaskReturnParameter x:TypeArguments="x:String"/>
    </a:EventToAsyncMethodHandler>
</b:EventMultipleHandlerBehavior>
```

We must skip waiting of method 'RunResultActionsAndReturn', and 'Task<String>' will be awailable as parameter in method 'RunResultActionsWithPrevTaskWithResult'

Use debugger to see how it works

### EventMultipleHandlerBehavior

That kind of behavior created to delegate calling things to special handlers. By invoking event, behavior will rize collection of handlers
sequentialy (Demo 10)

### Handlers

There is some standart handlers package provide

1. EventToCommandHandler
2. EventToMethodHandler
3. EventToMethodHandler<T>
4. EventToAsyncMethodHandler
5. EventToAsyncMethodHandler<T>
6. DelayEventHandler
7. SequenceHandlerExecutor
8. ParallelHandlerExecutor
  
So, lets see how it works)

#### EventToCommandHandler

Like EventToCommandBehavior (Demo 11)

```
<b:EventMultipleHandlerBehavior EventName="Clicked">
    <h:EventToCommandHandler Command="{Binding Sample1Command}"/>
    <h:EventToCommandHandler Command="{Binding Sample2Command}"/>
</b:EventMultipleHandlerBehavior>
```

On event 'Clicked' all this commands will be called sequentially

#### EventToMethodHandler

Like EventToMethodBehavior (Demo 12)

```
<b:EventMultipleHandlerBehavior EventName="Clicked">
    <h:EventToMethodHandler MethodName="JustAMethodTooButWithParameterBinding">
        <p:BindableParameter x:TypeArguments="c:ICommand" Value="{Binding Sample1Command}"/>
        <p:GenericParameter x:TypeArguments="x:String">The best green in the world - Basil</p:GenericParameter>
    </h:EventToMethodHandler>
    <h:EventToMethodHandler MethodName="JustAMethodNoMore">
        <p:GenericParameter x:TypeArguments="x:String">Some value passed as param</p:GenericParameter>
    </h:EventToMethodHandler>
</b:EventMultipleHandlerBehavior>
```

All this things will be called sequentialy

By default method calls from view model object. You can call method with binding using property 'TargetMethodCallObject'

#### EventToMethodHandler<T>

Created to specify return type of method (mostly for 'ReturnParameter', Demo 13)

```
<b:EventMultipleHandlerBehavior EventName="Clicked">
    <h:EventToMethodHandler x:TypeArguments="x:Int32" MethodName="GetInt"/>
    <h:EventToMethodHandler x:TypeArguments="x:String" MethodName="GetString">
        <p:ReturnParameter x:TypeArguments="x:Int32"/>
    </h:EventToMethodHandler>
</b:EventMultipleHandlerBehavior>
```

So declaring method like

```
<h:EventToMethodHandler x:TypeArguments="x:Int32" MethodName="GetInt"/>
```

We specify, that this method returns Int32. So next method can receive ReturnParameter with type Int32

```
<h:EventToMethodHandler x:TypeArguments="x:String" MethodName="GetString">
    <p:ReturnParameter x:TypeArguments="x:Int32"/>
</h:EventToMethodHandler>
```

#### EventToAsyncMethodHandler

Reresents method, that returns task and can be awaited. If we skip waiting, we can pass return parameter with type Task.

Calling async method with waiting (Demo 14)

```
<b:EventMultipleHandlerBehavior EventName="Clicked">
    <h:EventToMethodHandler x:TypeArguments="x:String" MethodName="GetString">
        <p:DefaultParameter x:TypeArguments="x:Int32"/>
    </h:EventToMethodHandler>
    <a:EventToAsyncMethodHandler WaitResult="True" MethodName="RunResultActions">
        <p:ReturnParameter x:TypeArguments="x:String"/>
    </a:EventToAsyncMethodHandler>
</b:EventMultipleHandlerBehavior>
```

Calling async method without waiting (Demo 15)

```
<b:EventMultipleHandlerBehavior EventName="Clicked">
    <h:EventToMethodHandler x:TypeArguments="x:String" MethodName="GetString">
        <p:DefaultParameter x:TypeArguments="x:Int32"/>
    </h:EventToMethodHandler>
    <a:EventToAsyncMethodHandler WaitResult="False" MethodName="RunResultActions">
        <p:ReturnParameter x:TypeArguments="x:String"/>
    </a:EventToAsyncMethodHandler>
    <h:EventToMethodHandler MethodName="RunResultActionsWithPrevTask">
        <p:ReturnParameter x:TypeArguments="t:Task"/>
    </h:EventToMethodHandler>
</b:EventMultipleHandlerBehavior>
```

Here method 'RunResultActionsWithPrevTask' will receive Task as parameter

#### EventToAsyncMethodHandler<T>

Represents method that returns generic Task<T> with some result (again problem with generic types in xaml)

There is also we can wait for result in sequential order or dont wait it. And if we do await Task<T>, we can receive
Result of type T as resturn parameter in next method that called. If we dont, we can receive Task<T> using 'GenericTaskReturnParameter'

With waiting result method 'JustAMethodNoMore' will receive string, which is result of returned task above
(method 'RunResultActionsAndReturn', Demo 16)

```
<b:EventMultipleHandlerBehavior EventName="Clicked">
    <h:EventToMethodHandler x:TypeArguments="x:Int32" MethodName="GetInt"/>
    <h:EventToMethodHandler x:TypeArguments="x:String" MethodName="GetString">
        <p:ReturnParameter x:TypeArguments="x:Int32"/>
    </h:EventToMethodHandler>
    <a:EventToAsyncMethodHandler WaitResult="True" MethodName="RunResultActionsAndReturn">
        <p:ReturnParameter x:TypeArguments="x:String"/>
    </a:EventToAsyncMethodHandler>
    <h:EventToMethodHandler MethodName="JustAMethodNoMore">
        <p:ReturnParameter x:TypeArguments="x:String"/>
    </h:EventToMethodHandler>
</b:EventMultipleHandlerBehavior>
```

Without waiting (Demo 17) we will receive Task<String> itself as parameter in method 'RunResultActionsWithPrevTaskWithResult'

```
<b:EventMultipleHandlerBehavior EventName="Clicked">
    <h:EventToMethodHandler x:TypeArguments="x:Int32" MethodName="GetInt"/>
    <h:EventToMethodHandler x:TypeArguments="x:String" MethodName="GetString">
        <p:ReturnParameter x:TypeArguments="x:Int32"/>
    </h:EventToMethodHandler>
    <a:EventToAsyncMethodHandler x:TypeArguments="x:String" WaitResult="False" MethodName="RunResultActionsAndReturn">
        <p:ReturnParameter x:TypeArguments="x:String"/>
    </a:EventToAsyncMethodHandler>
    <a:EventToAsyncMethodHandler x:TypeArguments="x:String" WaitResult="True" MethodName="RunResultActionsWithPrevTaskWithResult">
        <p:GenericTaskReturnParameter x:TypeArguments="x:String"/>
    </a:EventToAsyncMethodHandler>
</b:EventMultipleHandlerBehavior>
```

#### DelayEventHandler

For waiting some time in milliseconds (Demo 18)

Usage:

```
<b:EventMultipleHandlerBehavior EventName="Clicked">
    <h:SequenceHandlerExecutor>
        <h:EventToCommandHandler Command="{Binding Sample1Command}"/>
        <a:DelayEventHandler DelayMilliseconds="4000" WaitResult="True"/>
        <h:EventToMethodHandler MethodName="Method1"/>
    </h:SequenceHandlerExecutor>
</b:EventMultipleHandlerBehavior>
```

After 'Sample1Command' executing sequence will stop on 4 sec and continue with calling method 'Method1'

#### SequenceHandlerExecutor and ParallelHandlerExecutor

To complete whole picture you can combine that executings with parallel or sequential orders (Demos 19 20 21)

For instance lets call couple things with sequence order (Demo 19)

```
<b:EventMultipleHandlerBehavior EventName="Clicked">
    <h:SequenceHandlerExecutor>
        <h:EventToCommandHandler Command="{Binding Sample1Command}"/>
        <h:EventToMethodHandler MethodName="JustAMethodNoMore">
            <p:GenericParameter x:TypeArguments="x:String">Some value passed as param</p:GenericParameter>
        </h:EventToMethodHandler>
        <h:EventToMethodHandler MethodName="Method1"/>
        <h:EventToMethodHandler MethodName="MethodAsync2"/>
    </h:SequenceHandlerExecutor>
</b:EventMultipleHandlerBehavior>
```

And parallel order (Demo 20)

```
<b:EventMultipleHandlerBehavior EventName="Clicked">
    <h:ParallelHandlerExecutor>
        <h:EventToCommandHandler Command="{Binding Sample1Command}"/>
        <h:EventToMethodHandler MethodName="JustAMethodNoMore">
            <p:GenericParameter x:TypeArguments="x:String">Some value passed as param</p:GenericParameter>
        </h:EventToMethodHandler>
        <h:EventToMethodHandler MethodName="Method1"/>
        <h:EventToMethodHandler MethodName="MethodAsync2"/>
    </h:ParallelHandlerExecutor>
</b:EventMultipleHandlerBehavior>
```

Now combine them 😊 (Demo 21)

```
<b:EventMultipleHandlerBehavior EventName="Clicked">
    <h:SequenceHandlerExecutor>
        <h:EventToCommandHandler Command="{Binding Sample1Command}"/>
        <h:EventToMethodHandler MethodName="JustAMethodNoMore">
            <p:GenericParameter x:TypeArguments="x:String">Some value passed as param</p:GenericParameter>
        </h:EventToMethodHandler>
        <h:ParallelHandlerExecutor>
            <h:EventToMethodHandler MethodName="Method1"/>
            <h:EventToMethodHandler MethodName="MethodAsync2"/>
        </h:ParallelHandlerExecutor>
    </h:SequenceHandlerExecutor>
</b:EventMultipleHandlerBehavior>
```

## Feature usage

In near feature there will be first package versions available by [this](https://github.com/ilievmark/Basil.Behaviors.Animations) link
Here will be animations, that can be declared directly in xaml
