using static System.Console;

namespace CSharp_8;

//============================================================
//How to stop NullReferenceExceptions in .NET
//Christian Findlay
//https://www.christianfindlay.com/blog/stop-nullreferenceexceptions

// SEE MORE end of this FILE and CSharp-9

//======================================================================
/*
In the beginning: Reference Types (RT) = COULD BE NULL!
                  We did a lot of null checks but we were clear
 but in C#8 
 we got ---> Nullable Reference Types
             -------------------------
             and everything became confusing - has good parts but
             also brings challengs! Which is why we get this error:
*/
public class ChessPlayer {
    public string FirstName;
    //Error:        ^-- Non-nullable property 'FirstName' must contain a
    //                  non-null value when exiting constructor.
    //                  Consider declaring the property as nullable.
    public Rating Rating { get; set; }
}
public class Rating { public int BlitzRating = 0; }
/*
var player = new ChessPlayer();
var blitzRating = player.Rating.BlitzRating
                                  ^--- System.NullReferenceTypes
        Error: Object reference not set to an instance of an object.
Fix:
public Rating? Rating {get; set;}
var blitzRating = player.Rating?.BlitzRating  
      ^-- will have null and print out null in WriteLine
*/

public static class Nullability8
{
    public static void Run()
    {
        History_Nullable_Disable();
        History_Nullable_Enable();

        WriteLine("");
    }

 /*
    <Project Sdk="Microsoft.NET.Sdk">
        <PropertyGroup>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>   <-- new template on by default
    </PropertyGroup>
    
    C# 8 - start off like this:
        < Nullable>disable</Nullable>
 */

    public static void History_Nullable_Disable()
    {
        //1 - C# 8
        //-----------
        //< Nullable>disable</Nullable>
        int x = 8;      
        WriteLine($"value type not null: {x}");

        //ERROR: Cannot convert null to 'int' because it is a non - nullable value type
        //int y = null;  <-- cause "value types" (VT) couldn't assign null in C# 8

        //It's either null or an int.
        int? y = null;          //Get around it they add nullable ? (question mark)
        //Nullable<int> z;      //Same syntax!

        WriteLine(y is null ? "y is Null" : "y Is not Null");

        //ERROR: Use of unassigned local variable 'z'
        //WriteLine(z is null ? "z is Null" : "z Is not Null");
        //          ^-- underline - cause that's a problem

        int? zz = default;     //default = Assing whatever that value is
                               //For numbers it's a null!!! Thought it was 0
        WriteLine(zz is null ? "zz is Null" : "zz Is not Null");

        //ERROR/WARNING: The annotation for nullable reference types should only
        //be used in code within a '#nullable' annotations
        string? s = default;
        //    ^-- you can't do nullable on objects before nullable "reference types" (RT)
        //        they're intrinsically adding with knowledgeable RT. 
        WriteLine(s is null ? "s is Null" : "s Is not Null");

        //Correct usuage: 
        //string? s = "";    or
        //string s1 = null;
        //       ^--- problem it's not clear from that string it's nullable.
        //            became a problem in maintaining code something was:
        //            1. nullable, 2. need to be null,
        //            3. or write test code "s1 is nul"

        //Make it less ambiguous they introduced this idea of nullable
        WriteLine("");
    }


    //Make it less ambiguous they introduced this idea of nullable
    //        <Nullable>enable</Nullable>
    public static void History_Nullable_Enable()
    {
        //Nullability will work only between these two pieces
        //      <Nullable>disable</Nullable>
        //Useful if you want to migrate code across
        #nullable enable
        #nullable disable

        //#nullable enable

        //string a = null;
        ////ERROR:    ^--- Converting null literal or possible null value
        ////               to non - nullable type.
        //WriteLine(a is null ? "a is null" : "a is not null");        

        //#nullable disable

        //===================================================================
        //<Nullable>enable</Nullable>
        string a = null;
        //ERROR:    ^--- Converting null literal or possible null value
        //               to non - nullable type.
        WriteLine(a is null ? "a is null" : "a is not null");

        //Not able to convert a null into a string because implicitly that
        //reference types unless you do something about them
        //CAN NO LONGER HOLD NULL
        //additional information whether you intend for it to be null or not null

        string? aa = null;
        //    ^--- comes down to this idea of you specifying
        //         what you expect to happen 
        WriteLine(aa is null ? "aa is null" : "aa is not null");

        string aaa = "";
        //if (aaa is null)  //NOT POSSIBLE - instrinscally aaa cannot be assigned null
        //                    but aa can be assigned null 
        WriteLine(aaa is null ? "aaa is null" : "aaa is not null");
        //if (aa is null)     //<-- hover aa - says "'aa' may be null"

        //class User
        User user = new User();
        WriteLine(user.Name2);            //Okay
        //WriteLine(user.Name2.Length);   //Warning: "Dereference of a possibly null ref"
        ////user.Name2="George" then message will go away

        //? - Question mark
        //---------------------
        WriteLine(user.Name2?.Length);
        //                  ^-- says if null, return the null short ciruit
        //Fix
        WriteLine(user.Name2 is null ? "null" : user.Name2.Length);
        //          ^-- this syntax is ugly and people hate it.

        //! -Exclamation mark
        //---------------------
        //                  V-- tells compiler u might think it's null but it's not.        //                  says it will exist 
        //WriteLine(user.Name2!.Length);
        //                ^--- and if it is null, will throw an exception

        //User1<string?> user1 = new("Hello");

        //Don't want nullable in the type you want to control in the
        //class structure using generics
        User2<string> user2 = new("Hello");
        //so if I do this:
        //User2<string?> user3 = new("Hello");
        //      ^--- Error: The type 'string?' cannot be used as
        //      type parameter 'T' in the generic type or method
        //      'User2<T>'.Nullability of type argument 'string?'
        //      doesn't match 'notnull' constraint.	

        //Just deal with NULL or NOT NULL - by turn it on/off and opt into it.



        WriteLine("");
    }
}
class User2<T> where T : notnull //<-- this limits 
{
    public int Id { get; set; }
    public T? Name { get; set; }
    public User2(T name) => Name = name;
}

class User1<T> {  public int Id { get; set; }  public T Name { get; set; }
    public User1(T name) => Name = name;
}

class User {
    public int Id { get; set; }
    public string Name { get; set; }    //<-- instrically you're saying it's not null
    //Error:        ^-- Non-nullable property 'Name' must contain a non-null
    // value when exiting constructor.Consider declaring the property as nullable.

    //Fix is either
    public string Name1 { get; set; } = "";
    public string? Name2 { get; set; }
    // provide a constructor --> because name can NOT be null.
    //public User(string name) => Name = name;
}

//============================================================
//How to stop NullReferenceExceptions in .NET
//Christian Findlay
//https://www.christianfindlay.com/blog/stop-nullreferenceexceptions
/*

C# 9 - Make Your Code as Immutable as Possible
-----------------------------------------
C# 9 brings record types to C#. A feature of these types is that they have 
some immutability out of the box. You must specify the value reference in 
the constructor, and the property is read-only – the reference does not change 
for the object’s lifespan. That means that the reference will never become null 
if you pass a reference into the constructor. If you use the NRT feature with 
code rules turned on, you can’t pass null in. Here is a basic example, but this 
has a big flaw, as I will explain later.
*/
#nullable enable
//var testRecord = new TestRecord("Hi", null);
//Console.WriteLine(testRecord.NotNullableProperty);
    public record TestRecord(string NotNullableProperty, string? NullableProperty);
/* You can make regular classes more immutable by passing variables via the constructor 
and making those properties read-only. You can then apply a null guard.
If variables in your class need to change, you need to be more careful throughout 
your code because something could set the reference to null. However, 
code analysis will catch some of these issues
---------------------------------- 
 */

//Use Null Guards – ArgumentNullException
//========================================
public class NullGuardExample
{
    public string NotNullableProperty { get; }
    public string? NullableProperty { get; }
    public NullGuardExample(
        string notNullableProperty,
        string? nullableProperty)
    {
        NotNullableProperty = notNullableProperty ?? throw new ArgumentNullException(nameof(notNullableProperty));
        //   ^-- Tell consumer straight away the issue
        NullableProperty = nullableProperty;
    }
}

/*
//Dependency Injection with Default Implementations
//===================================================
You can safely allow consumers to pass null into your classes if you use the 
Null Object Pattern.
The consumer does not need to implement the interface to create the instance 
of the class. This example follows the null object pattern for the default 
implementation of the dependency. This code respects NRT because you set the 
member variable to a fall-back instance. Add the ? suffix to the type on the 
optional parameter. 
 */

#nullable enable

class Program8
{
    static void Main()
    {
        var someService = new SomeService();
    }
}

public class SomeService
{
    private readonly IPerformsAction performsAction;
    public SomeService(IPerformsAction? performsAction = null)
    {
        this.performsAction = performsAction ?? NullActionPerformer.Instance;
                                            //^--- magic here
    }
}

public interface IPerformsAction
{
    void PerformAction();
}

public class NullActionPerformer : IPerformsAction
{
    public static NullActionPerformer Instance { get; } = new NullActionPerformer();
    public void PerformAction() { }
}



