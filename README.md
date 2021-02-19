# Frostware.Result

A simple implementation of a functional result type in C#

## Table of contents
* [What is a result type, and why use it](#what-is-a-result-type-and-why-use-it)
* [How to use](#how-to-use)
     * [Method Decleration](#method-decleration)
     * [Result Handling](#result-handling)

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
This distinction is important for pattern matching.

Fail works the same as Pass but has an added optional error message. 



## Method Decleration
Simply make your method return the type "Result" and use the static helper methods on the Result class.
```cs
public Result ExampleMethod()
{
     return Result.Pass(20); //returns Pass<int> { Value = 20 }
     return Result.Pass(); //returns Pass {}
     return Result.Fail(30, "Oporation Failed"); //returns Fail { Value = 30, ErrorMessage = "Oporation Failed" }
     return Result.Fail(new Foo()); //returns Fail { Value = Foo {}, ErrorMessage = "" }
     return Result.Fail(); //returns Fail {ErrorMessage = ""}
}
```

## Result Handling
It is recomended to pattern match over the result, either with a switch statement,

```cs
switch(ExampleMethod())
{
     case Pass<int> x:
         //triggered when result is a pass and contains a value of int
         break;
 
     case Pass<Foo> x:
         //triggered when result is a pass and contains a value of Foo
         break;
 
     case Pass _: //you can ommit the "_" as of .NET 5
         //triggered when result is a pass of any type
         break;
 
 
     case Fail<int> x:
         //triggered when result is a fail and contains a value of int
         break;
 
     case Fail<Foo> x:
         //triggered when result is a fail and contains a value of Foo
         break;
 
     case Fail _: //you can ommit the "_" as of .NET 5
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
Note that if you check for Pass before Pass\<int> the compiler will throw an error stating that the pattern or case has already been handled. This is do to Pass\<int> being a Pass. This is intentional, it means that if you just want to check if an oporation passed and don't care about the value, you can just check for Pass. Same applies to Fail.
