

public static class Deconstruction
{
    public static void Run()
    {
        First();

        Console.WriteLine("");
    }

    private static void First()
    {
        var person = new PersonClass("John", "Doe");

        //TODO
        //var (firstName, lastName) = person;
        //var record = new PersonRecord("John", "Doe");
        //var (firstName, lastName) = record;
        //Console.WriteLine($"Deconstruction => Deconstructed Person: {firstName} {lastName}");
        Console.WriteLine("");
    }

}
