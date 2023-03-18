// See https://aka.ms/new-console-template for more information
public interface Strategy
{
    void DoSomething();
}


public class StrategyA : Strategy
{
    public void DoSomething()
    {
        Console.WriteLine("Strategy A: DoSomething()");
    }
}

public class StrategyB : Strategy
{
    public void DoSomething()
    {
        Console.WriteLine("Strategy B: DoSomething()");
    }
}

public class StrategyContext
{
    private Strategy strategy;

    public StrategyContext(Strategy strategy)
    {
        this.strategy = strategy;
    }

    public void SetStrategy(Strategy strategy)
    {
        this.strategy = strategy;
    }

    public void DoSomething()
    {
        strategy.DoSomething();
    }
}
