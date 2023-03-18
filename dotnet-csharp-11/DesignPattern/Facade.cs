
public class Facade
{
    private readonly Subsystem1 subsystem1 = new Subsystem1();
    private readonly Subsystem2 subsystem2 = new Subsystem2();

    public void Operation()
    {
        subsystem1.Operation1();
        subsystem2.Operation2();
    }
}

public class Subsystem1
{
    public void Operation1()
    {
        Console.WriteLine("Subsystem1: Operation1()");
    }
}

public class Subsystem2
{
    public void Operation2()
    {
        Console.WriteLine("Subsystem2: Operation2()");
    }
}
