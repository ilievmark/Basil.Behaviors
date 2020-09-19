# Basil.Behaviors Animations

Animations - wrap on instance of Animation class of Xamarin.Forms, based on [event handling](https://github.com/ilievmark/Basil.Behaviors/tree/master/sample/BehaviorsSample/Pages/EventHandling) infrastructure

Doc structure:

- [Standart](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#standart-animations)
    - [FadeAnimation](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#fadeanimation)
    - [LayoutAnimation](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#layoutanimation)
    - [TranslateAnimation](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#translateanimation)
    - [ScaleAnimation](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#scaleanimation)
    - [RelativeScaleAnimation](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#relativescaleanimation)
    - [RotateAnimation](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#rotateanimation)
    - [RelativeRotateAnimation](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#relativerotateanimation)
    - [RotateXAnimation](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#rotatexanimation)
    - [RelativeRotateXAnimation](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#relativerotatexanimation)
    - [RotateYAnimation](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#rotateyanimation)
    - [RelativeRotateYAnimation](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#relativerotateyanimation)
- [Custom](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#custom-animations)
    - [BackgroundColorAnimations](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#backgroundcoloranimations)
- [Advanced](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#advanced)
    - [Animation combination](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#animation-combination)
    - [Using event handlers with animations](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#animation-combination)
    - [Animate multiple views](https://github.com/ilievmark/Basil.Behaviors/tree/doc_animations_and_impvmts/sample/BehaviorsSample/Pages/Animations#animation-combination)

## Standart animations

It is animations, that already exist in Xamarin forms, and it can be used with extension method of VisualElement class.
We alredy know about methods like ScaleTo, FadeTo or TranslateTo. There is couple of animations that replesents calling
of this methods for target VisualElement instance

All animations derived from class AnimationBase. AnimationBase declares three importand bindable properties

- public uint Length {get; set;}
- public Easing Easing {get; set;}
- public VisualElement Target {get; set;}

- Length - represent time length in milliseconds of animation
- Easing - easing of animation
- Target - custom target (view) that will be animated

### FadeAnimation

Uses FadeTo ext method undergrounds.
Usage examle:

```
<Button Text="Fade animation" BackgroundColor="Cyan" Margin="20">
    <Button.Behaviors>
        <e:EventToMultipleHandlersBehavior EventName="Clicked">
            <s:FadeAnimation Length="500" Opacity="0" />
            <s:FadeAnimation Length="500" Opacity="1" />
        </e:EventToMultipleHandlersBehavior>
    </Button.Behaviors>
</Button>
```

Opacity - opacity, that animation will reach

In action:

![Image of usage FadeAnimation](https://github.com/ilievmark/Basil.Behaviors/blob/master/inf/animations/fade_anim.gif)

!!! IMPORTANT !!!
There is a little problem using two or more animations inside one event handling behaviour. If you rise event twice shortly,
its posible, that animation will change state of your VisualElement instance permanently. To prevent this cases, use SingleRun="True"
bindable property of EventToMultipleHandlersBehavior just like this:

```
<Button Text="Fade animation" BackgroundColor="Cyan" Margin="20">
    <Button.Behaviors>
        <e:EventToMultipleHandlersBehavior EventName="Clicked" SingleRun="True">
            <s:FadeAnimation Length="500" Opacity="0" />
            <s:FadeAnimation Length="500" Opacity="1" />
        </e:EventToMultipleHandlersBehavior>
    </Button.Behaviors>
</Button>
```

### LayoutAnimation

Uses LayoutTo ext method undergrounds.
Usage examle:

```
<Button Text="Layout animation" BackgroundColor="Cyan" Margin="20">
    <Button.Behaviors>
        <e:EventToMultipleHandlersBehavior EventName="Clicked" SingleRun="True">
            <s:LayoutAnimation Length="200" OffsetRectangle="-10,15,20,-30" />
            <s:LayoutAnimation Length="200" OffsetRectangle="10,-15,-20,30" />
        </e:EventToMultipleHandlersBehavior>
    </Button.Behaviors>
</Button>
```

OffsetRectangle - represents rectangle that will be used like offset from current Bounds

In action:

![Image of usage LayoutAnimation](https://github.com/ilievmark/Basil.Behaviors/blob/master/inf/animations/layout_amin.gif)

### TranslateAnimation

Uses TranslateTo ext method undergrounds.
Usage examle:

```
<Button Text="Translation animation" BackgroundColor="Cyan" Margin="20">
    <Button.Behaviors>
        <e:EventToMultipleHandlersBehavior EventName="Clicked">
            <s:TranslateAnimation Length="600" XOffset="35" YOffset="20" />
            <s:TranslateAnimation Length="700" XOffset="43" YOffset="-15" Easing="{x:Static Easing.SpringIn}" />
            <s:TranslateAnimation Length="400" XOffset="-18" YOffset="-25" />
            <s:TranslateAnimation Length="800" XOffset="-60" YOffset="20" Easing="{x:Static Easing.SinOut}" />
        </e:EventToMultipleHandlersBehavior>
    </Button.Behaviors>
</Button>
```

XOffset - offset by X coordinate from current (will be added to current value of coordinate X)
YOffset - offset by Y coordinate from current (will be added to current value of coordinate Y)

In action:

![Image of usage TranslateAnimation](https://github.com/ilievmark/Basil.Behaviors/blob/master/inf/animations/translation_anim.gif)

### ScaleAnimation

Uses ScaleTo ext method undergrounds.
Usage examle:

```
<Button Text="Scale animation" BackgroundColor="Cyan" Margin="20">
    <Button.Behaviors>
        <e:EventToMultipleHandlersBehavior EventName="Clicked">
            <s:ScaleAnimation Length="400" Scale="0.4" />
            <s:ScaleAnimation Length="500" Scale="1" Easing="{x:Static Easing.BounceOut}" />
        </e:EventToMultipleHandlersBehavior>
    </Button.Behaviors>
</Button>
```

Scale - scale, that animation will reach

In action:

![Image of usage ScaleAnimation](https://github.com/ilievmark/Basil.Behaviors/blob/master/inf/animations/scale_anim.gif)

### RelativeScaleAnimation

Uses RelScaleTo ext method undergrounds.
Usage examle:

```
<Button Text="Relative Scale animation" BackgroundColor="Cyan" Margin="20">
    <Button.Behaviors>
        <e:EventToMultipleHandlersBehavior EventName="Clicked" SingleRun="True">
            <s:RelativeScaleAnimation Length="400" Scale="0.1" Easing="{x:Static Easing.CubicIn}" />
        </e:EventToMultipleHandlersBehavior>
    </Button.Behaviors>
</Button>
```

Scale - scale, that animation will increment to current

In action:

![Image of usage RelativeScaleAnimation](https://github.com/ilievmark/Basil.Behaviors/blob/master/inf/animations/rel_scale_anim.gif)

### RotateAnimation

Uses RotateTo ext method undergrounds.
Usage examle:

```
<Button Text="Rotate animation" BackgroundColor="Cyan" Margin="20">
    <Button.Behaviors>
        <e:EventToMultipleHandlersBehavior EventName="Clicked">
            <s:RotateAnimation Length="500" Rotation="45" Easing="{x:Static Easing.SpringIn}" />
            <s:RotateAnimation Length="500" Rotation="0" Easing="{x:Static Easing.SpringOut}" />
        </e:EventToMultipleHandlersBehavior>
    </Button.Behaviors>
</Button>
```

Rotation - rotation, that animation will reach

In action:

![Image of usage RotateAnimation](https://github.com/ilievmark/Basil.Behaviors/blob/master/inf/animations/rotate_anim.gif)

### RelativeRotateAnimation

Uses RelRotateTo ext method undergrounds.
Usage examle:

```
<Button Text="Relative Rotate animation" BackgroundColor="Cyan" Margin="20">
    <Button.Behaviors>
        <e:EventToMultipleHandlersBehavior EventName="Clicked">
            <s:RelativeRotateAnimation Length="600" Rotation="20" Easing="{x:Static Easing.SinInOut}" />
        </e:EventToMultipleHandlersBehavior>
    </Button.Behaviors>
</Button>
```

Rotation - rotation that animation will increment to current

In action:

![Image of usage RelativeRotateAnimation](https://github.com/ilievmark/Basil.Behaviors/blob/master/inf/animations/rel_rotate_anim.gif)

### RotateXAnimation

Uses RotateXTo ext method undergrounds.
Usage examle:

```
<Button Text="RotationX animation" BackgroundColor="Cyan" Margin="20">
    <Button.Behaviors>
        <e:EventToMultipleHandlersBehavior EventName="Clicked" SingleRun="True">
            <s:RotateXAnimation Length="650" RotationX="80" Easing="{x:Static Easing.BounceIn}" />
            <s:RotateXAnimation Length="650" RotationX="0" Easing="{x:Static Easing.BounceOut}" />
        </e:EventToMultipleHandlersBehavior>
    </Button.Behaviors>
</Button>
```

RotationX - rotation by coordinate X, that animation will reach

In action:

![Image of usage RotateXAnimation](https://github.com/ilievmark/Basil.Behaviors/blob/master/inf/animations/rotation_x_anim.gif)

### RelativeRotateXAnimation

Uses RotateXTo ext method undergrounds, but with increment to current RotationX
Usage examle:

```
<Button Text="Relative RotationX animation" BackgroundColor="Cyan" Margin="20">
    <Button.Behaviors>
        <e:EventToMultipleHandlersBehavior EventName="Clicked" SingleRun="True">
            <s:RelativeRotateXAnimation Length="200" RotationX="20"/>
        </e:EventToMultipleHandlersBehavior>
    </Button.Behaviors>
</Button>
```

RotationX - rotation by coordinate X, that animation will increment to current RotationX

In action:

![Image of usage RelativeRotateXAnimation](https://github.com/ilievmark/Basil.Behaviors/blob/master/inf/animations/rel_rotate_x_anim.gif)

### RotateYAnimation

Uses RotateYTo ext method undergrounds.
Usage examle:

```
<Button Text="RotationY animation" BackgroundColor="Cyan" Margin="20">
    <Button.Behaviors>
        <e:EventToMultipleHandlersBehavior EventName="Clicked" SingleRun="True">
            <s:RotateYAnimation Length="650" RotationY="80" Easing="{x:Static Easing.CubicOut}"/>
            <s:RotateYAnimation Length="900" RotationY="-80" Easing="{x:Static Easing.CubicIn}"/>
            <s:RotateYAnimation Length="650" RotationY="0" Easing="{x:Static Easing.BounceOut}"/>
        </e:EventToMultipleHandlersBehavior>
    </Button.Behaviors>
</Button>
```

RotationY - rotation by coordinate Y, that animation will reach

In action:

![Image of usage RotateYAnimation](https://github.com/ilievmark/Basil.Behaviors/blob/master/inf/animations/rotation_y_anim.gif)

### RelativeRotateYAnimation

Uses RotateYTo ext method undergrounds, but with increment to current RotationY
Usage examle:

```
<Button Text="Relative RotationY animation" BackgroundColor="Cyan" Margin="20">
    <Button.Behaviors>
        <e:EventToMultipleHandlersBehavior EventName="Clicked" SingleRun="True">
            <s:RelativeRotateYAnimation Length="200" RotationY="20"/>
        </e:EventToMultipleHandlersBehavior>
    </Button.Behaviors>
</Button>
```

RotationY - rotation by coordinate Y, that animation will increment to current RotationY

In action:

![Image of usage RelativeRotateYAnimation](https://github.com/ilievmark/Basil.Behaviors/blob/master/inf/animations/rel_rotate_y_anim.gif)

## Custom animations

You can create own animations using AnimationBase or CustomAnimationBase. Both classes generic with type arg inherited from VisualElement

AnimationBase class was created to use standart extension methods like ScaleTo or RotateTo. There are options to use different ext methods there,
or use Xamarin tutorial, and your code may be like that

```
var tcs = new TaskCompletionSource<bool>();
var ve = GetAnimationTargetVisualElement();
new Animation(d => (... do animation updates here...) , StartValue, EndValue, Easing)
    .Commit(ve, "AnimationName", Rate, Length, Easing, (d, b) => tcs.SetResult(b));
return tcs.Task;
```

that seems complicated, but this is how all Xamarin animations works.
So if you dont want to see undergrounds of your animation, and you want to code without details, you can create animation with CustomAnimationBase.

Inherited class from CustomAnimationBase mus implement just one method:

```
protected abstract void Tick(TVisual visualElement, double currentValue);
```

here:
TVisual visualElement - visual element that yuo want to animate (TVisual - custom visual element if you want to animate custom properties)
double currentValue - current animation value that animation receive at current frame

Also this class already have bindable properties, like:


### BackgroundColorAnimations

Here is example of using CustomAnimationBase class. There are 4 animations to animate bg color of visual element:

- BackgroundColorAAnimation
- BackgroundColorRAnimation
- BackgroundColorGAnimation
- BackgroundColorBAnimation

Each of animation animate only theirs color channel. For example, implementation of BackgroundColorRAnimation:

```
public class BackgroundColorRAnimation : CustomAnimationBase
{
    protected override void Tick(VisualElement visualElement, double currentValue)
    {
        visualElement.BackgroundColor = new Color(
            currentValue,
            visualElement.BackgroundColor.G,
            visualElement.BackgroundColor.B);
    }
}
```

You can still use all this animations in parallel

```
<Button Text="Background animation" BackgroundColor="Cyan" Margin="20">
    <Button.Behaviors>
        <e:EventToMultipleHandlersBehavior EventName="Clicked">
            <h:ParallelHandlerExecutor WaitResult="True">
                <c:BackgroundColorRAnimation Length="300" StartValue="0" EndValue="0.2"/>
                <c:BackgroundColorGAnimation Length="300" StartValue="1" EndValue="0.3"/>
                <c:BackgroundColorBAnimation Length="300" StartValue="1" EndValue="0.4"/>
            </h:ParallelHandlerExecutor>
            <h:ParallelHandlerExecutor WaitResult="True">
                <c:BackgroundColorRAnimation Length="300" StartValue="0.2" EndValue="1"/>
                <c:BackgroundColorGAnimation Length="300" StartValue="0.3" EndValue="0.5"/>
                <c:BackgroundColorBAnimation Length="300" StartValue="0.4" EndValue="0.1"/>
            </h:ParallelHandlerExecutor>
            <h:ParallelHandlerExecutor WaitResult="True">
                <c:BackgroundColorRAnimation Length="300" StartValue="1" EndValue="0"/>
                <c:BackgroundColorGAnimation Length="300" StartValue="0.5" EndValue="1"/>
                <c:BackgroundColorBAnimation Length="300" StartValue="0.1" EndValue="1"/>
            </h:ParallelHandlerExecutor>
        </e:EventToMultipleHandlersBehavior>
    </Button.Behaviors>
</Button>
```

In action:

![Image of usage background color animations](https://github.com/ilievmark/Basil.Behaviors/blob/update_tutorials_minor_updates/inf/animations/bg_color_anim.gif)

## Advanced

### Animation combination

Since animations based on [event handling](https://github.com/ilievmark/Basil.Behaviors/tree/master/sample/BehaviorsSample/Pages/EventHandling) infrastructure, its posible to use event handlers with it.
You can combine animations with each other to reach complex animation.
For example:

```
<Button Text="Combined animations" BackgroundColor="Cyan" Margin="20">
    <Button.Behaviors>
        <e:EventToMultipleHandlersBehavior EventName="Clicked">
            <h:ParallelHandlerExecutor WaitResult="True">
                <s:RotateXAnimation Length="500" RotationX="75"/>
                <s:RotateYAnimation Length="500" RotationY="45"/>
            </h:ParallelHandlerExecutor>
            <h:ParallelHandlerExecutor WaitResult="True">
                <s:RotateXAnimation Length="500" RotationX="-130"/>
                <s:RotateYAnimation Length="500" RotationY="-90"/>
            </h:ParallelHandlerExecutor>
            <h:ParallelHandlerExecutor WaitResult="True">
                <s:RotateXAnimation Length="500" RotationX="-27"/>
                <s:RotateYAnimation Length="500" RotationY="30"/>
            </h:ParallelHandlerExecutor>
            <h:ParallelHandlerExecutor WaitResult="True">
                <s:RotateXAnimation Length="500" RotationX="0"/>
                <s:RotateYAnimation Length="500" RotationY="0"/>
            </h:ParallelHandlerExecutor>
        </e:EventToMultipleHandlersBehavior>
    </Button.Behaviors>
</Button>
```

There is sequence of rotation animations that will be played as you suppose)

![Image of usage combined animation](https://github.com/ilievmark/Basil.Behaviors/blob/master/inf/animations/combined_diff_anim.gif)

### Using event handlers with animations

Tutorial coming soon!

### Animate multiple views

Tutorial coming soon!

