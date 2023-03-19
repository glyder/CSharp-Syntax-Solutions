public interface IProduct
{
    void DoSomething();
}

public class Product : IProduct
{
    public void DoSomething()
    {
        Console.WriteLine("Product1: DoSomething()");
    }
}

public class Product2 : IProduct
{
    public void DoSomething()
    {
        Console.WriteLine("Product2: DoSomething()");
    }
}