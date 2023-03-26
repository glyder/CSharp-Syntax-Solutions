using static System.Console;

namespace CSharp_9;

/* On Windows, Debug -> Windows -> Exceptions Settings
  ON - Common Language Runtime Exceptions
      this means that Break on All Exceptions is turned on.
      The debugger always breaks whenever the code
      throws an exception.

    if too many then Tools/options/Debugging
        ON "Enable Just My Code"
           ON "Warn if no user code on launch (Managed only)"
*/


//Turn on NRT (Nullable Aware Context)
//-------------------------------------
//<LangVersion>Latest</LangVersion >
//<Nullable>enable</Nullable>

#nullable enable
#nullable disable

/* How to stop NullReferenceExceptions in .NET
 * =============================================
    Christian Findlay
    https://www.christianfindlay.com/blog/stop-nullreferenceexceptions

    
*/
#nullable enable

public static class NullabilityFixes9
{
    public static void Run()
    {
        //1. Records
        var testRecord = new TestRecord("Hi", null);
        Console.WriteLine(testRecord.NotNullableProperty);

        //2. Null guard
        //var throwMe = new NullGuardExample(null, null);
        var working = new NullGuardExample("George", null);

        //?? null-coalescing operator
        //NotNullableProperty = notNullableProperty ?? throw new ArgumentNullException(nameof(notNullableProperty));

        WriteLine("");
    }
}

//1. Records
public record TestRecord(string NotNullableProperty, 
                         string? NullableProperty);

/* You can make regular classes more immutable by passing variables via the constructor 
and making those properties read-only. You can then apply a null guard.
If variables in your class need to change, you need to be more careful throughout 
your code because something could set the reference to null. However, 
code analysis will catch some of these issues
---------------------------------- 
 */

//2. Use Null Guards – ArgumentNullException
public class NullGuardExample {
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


//From C#8 example - limit the input to NOT NULL.
class User2<T> where T : notnull //<-- this limits the input
{
    public int Id { get; set; }
    public T? Name { get; set; }
    public User2(T name) => Name = name;
}

