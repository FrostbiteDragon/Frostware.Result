# Frostware.Result

A simple implementation of a functional result type in C#

## Table of contents
* [What is a result type, and why use it](#what-is-a-result-type-and-why-use-it)
* [How to use](#how-to-use)


# What is a result type, and why use it?

The result type is an alternative to a try/catch that aims to remove the need for nulls and force handling of failed cases.

By having your methods return Result, it allows you to pattern match (switch) over that result at execution. You can then supply an implementation for every possible state of your method.

# How to use

This is the main result class:
```cs
public class Result
{
     // Pass inherits Result
     public static Result Pass() => new Pass(); 
     // Pass<T> inherits Pass
     public static Result Pass<T>(T value) => new Pass<T>(value); 
     
     //Fail inherits Result
     public static Result Fail(string errorMessage = "") => new Fail(errorMessage); 
     //Fail<T> inherits Fail
     public static Result Fail<T>(T value, string errorMessage = default) => new Fail<T>(value, errorMessage); 
}
```
Pass can either be empty or contain a value.

Pass\<T> is a Pass, Pass is a Result. 
This distinction is important for when you pattern match.

Fail works the same as Pass but has an added optional error message. 


To use, simply make your method return the type "Result" and use one of the above static methods.
```cs
public Result ExampleMethod()
{
     return Result.Pass(20); //returns Pass<int> { Value = 20 }
     return Result.Pass(); //returns Pass {}
     return Result.Fail(new Foo()); //returns Fail { Value = Foo {}, ErrorMessage = "" }
     return Result.Fail(); //returns Fail {ErrorMessage = ""}
}
```
It is recomended to switch over the result, either with a switch statement,

```cs
switch(ExampleMethod())
{
     case Pass<int> x:
         //triggered when result is a pass and contains a value of int
         break;
 
     case Pass<Foo> x:
         //triggered when result is a pass and contains a value of Foo
         break;
 
     case Pass _:
         //triggered when result is a pass of any type
         break;
 
 
     case Fail<int> x:
         //triggered when result is a fail and contains a value of int
         break;
 
     case Fail<Foo> x:
         //triggered when result is a fail and contains a value of Foo
         break;
 
     case Fail _:
         //triggered when result is a fail of any type
         break;
}
```

or a switch expression.
```cs
string result = ExampleMethod() switch
{
     Pass<int> x => $"This is a pass that contains the int {x.Value}",
     Pass<Foo> x => $"This is a pass that contains an instance of the class Foo {x.Value}",
     Pass => "This is an empty pass",
 
     Fail<int> x => $"This failed with message {x.ErrorMessage} and contains the int {x.Value}",
     Fail<Foo> x => $"This failed with message {x.ErrorMessage} and contains an instance of the class foo {x.Value}",
     Fail x => $"This failed with message {x.ErrorMessage}"
}
```
